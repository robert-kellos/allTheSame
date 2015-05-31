using System.Collections.Generic;
using AllTheSame.Common.Extensions;
using AllTheSame.Common.Helpers;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using AllTheSame.Common.Logging;
using System.Net.Http;
using System.Web.Http.Results;
using System.Net;
using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using AllTheSame.WebAPI.Models;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class AppointmentSteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "appointment_get_list";
        private const string GetItemKey = "appointment_get_item";
        private const string AddItemKey = "appointment_add_item";
        private const string EditItemKey = "appointment_edit_item";
        private const string DeleteItemKey = "appointment_delete_item";
        private const string ExistsItemKey = "appointment_exists_item";

        private Appointment _getItem;
        private Appointment _addItem;
        private Appointment _editItem;
        private Appointment _deleteItem;

        private string _getId = "-1";
        private int _getIdValue = -1;

        private string _editId = "-1";
        private int _editIdValue = -1;

        private string _addedId = "-1";
        private int _addedIdValue = -1;

        private string _deletedId = "-1";
        private int _deletedIdValue = -1;

        private string _existsId = "-1";
        private int _existsIdValue = -1;

        private DateTime _startTime;
        private DateTime _endTime;
        private string _description = "";
        private bool _remindVendor;
        private bool _alertOnVendorSignIn;
        private bool _alertOnVendorSignOut;
        /*
        [Id] [int] IDENTITY(1,1) NOT NULL,
	    [ResidentId] [int] NOT NULL,
	    [VendorWorkerId] [int] NOT NULL,
	    [AppointmentTypeId] [int] NOT NULL,
	    [StartTime] [datetime] NOT NULL,
	    [EndTime] [datetime] NOT NULL,
	    [Description] [nvarchar](max) NULL,
	    [RemindVendor] [bit] NOT NULL,
	    [AlertOnVendorSignIn] [bit] NOT NULL,
	    [AlertOnVendorSignOut] [bit] NOT NULL,
	    [Version] [timestamp] NOT NULL,
	    [CreatedOn] [datetime] NULL,
	    [UpdatedOn] [datetime] NULL,
        */
        //
        #endregion Local Properties/Fields

        public override string Uri => "/api/Appointment";

        #region CRUD Tests
        //

        [When(@"I call the add Appointment Post api endpoint to add a Appointment it checks if exists pulls item edits it and deletes it")]
        public void WhenICallTheAddAppointmentPostApiEndpointToAddAAppointmentItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PostAsync(_addItem).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("POST Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Appointment Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeAAppointmentIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
        {
            //is the item setup
            Assert.IsTrue(_addItem != null);

            //add the item
            var resultAdd = Add(_addItem);

            //did we get a good result
            Assert.IsTrue(resultAdd != null && resultAdd.Id > 0);

            //set te returned AddID to current Get
            _addedIdValue = resultAdd.Id;
            _getIdValue = _addedIdValue;
            _existsIdValue = _getIdValue;

            //check that the item exists
            var itemReturned = Exists(_existsIdValue);
            Assert.IsNotNull(itemReturned);

            //use the value used in exists check
            _getIdValue = itemReturned.Id;
            Assert.IsTrue(_getIdValue == _addedIdValue);

            //pull the item by Id
            var resultGet = GetById(_getIdValue);
            Assert.IsNotNull(resultGet);
            _getIdValue = resultGet.Id;
            Assert.IsTrue(_getIdValue == _addedIdValue);

            //Now, let's Edit the newly added item
            _editIdValue = _getIdValue;
            _editItem = resultGet;
            Assert.IsTrue(_editIdValue == _addedIdValue);

            //do an update
            Update(_editIdValue, _editItem);

            //pass the item just updated
            _deletedIdValue = _editIdValue;
            Assert.IsTrue(_deletedIdValue == _addedIdValue);

            //delete this same item
            Delete(_deletedIdValue);
        }

        private Appointment Add(Appointment item)
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PostAsync(item).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("POST Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;

            //grab the resulting added item
            var resultAdd = PostResponse<Appointment, Appointment>(item);
            if (resultAdd != null)
            {
                _addedIdValue = resultAdd.Id;
                Assert.IsTrue(_addedIdValue > 0);

                //Let's store the newly added Id in delete/edit, so we can later
                //edit and delete this same record
                _editIdValue = _addedIdValue;
                _deletedIdValue = _addedIdValue;

                ////validate values changed
                Assert.AreEqual(item.Description, resultAdd.Description);
            }

            response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            return resultAdd;
        }

        private Appointment Exists(int id)
        {
            //Check it exists
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(id);

            var resultExists = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var itemReturned = GetResponseById<Appointment>(id);

            var truth = (itemReturned != null && itemReturned.Id == id);
            Assert.AreEqual(truth, resultExists);

            return itemReturned;
        }

        private Appointment GetById(int id)
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<Appointment>(id);

            var resultGet = ScenarioContext.Current[GetItemKey];
            var itemGet = (resultGet as Appointment);

            Assert.IsNotNull(itemGet);
            Assert.IsTrue(itemGet.Id == id);

            return itemGet;
        }

        private void Update(int id, Appointment item)
        {
            var error = default(AggregateException);
            var response = default(HttpResponseMessage);

            PutAsync(item.Id, item).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("PUT Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[EditItemKey] = response;

            //grab the resulting added item
            response = (ScenarioContext.Current[EditItemKey] as HttpResponseMessage);
            var resultEdit = PutResponse<Appointment, Appointment>(item.Id, item);
            if (resultEdit != null)
            {
                Assert.IsTrue(id > 0);
                Assert.AreEqual(id, resultEdit.Id);

                //validate values changed
                Assert.AreEqual(item.Description, resultEdit.Description);
            }

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        private void Delete(int id)
        {
            var error = default(AggregateException);
            var response = default(HttpResponseMessage);

            //Now, let's Delete the newly added item
            DeleteAsync(id).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("POST Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[DeleteItemKey] = response;

            //grab the resulting added item
            var deleted = GetResponseById<Appointment>(id);
            Assert.IsNull(deleted);

            response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
        //
        #endregion CRUD Tests


        #region Post - add a new item by a populated item
        //
        [Given(@"the following Appointment Add input")]
        public void GivenTheFollowingAppointmentAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                //row["StartTime"],
                //row["EndTime"],
                _description = row["Description"];
                //row["RemindVendor"];
                //row["AlertOnVendorSignIn"];
                //row["AlertOnVendorSignOut"];

                break;
            }
            _startTime = DateTime.UtcNow;
            _endTime = DateTime.UtcNow.AddHours(4);
            _remindVendor = true;
            _alertOnVendorSignIn = true;
            _alertOnVendorSignOut = true;

            Assert.IsNotNull(_description);

            _addItem = new Appointment()
            {
                StartTime = _startTime,
                EndTime = _endTime,
                Description = _description,
                RemindVendor = _remindVendor,
                AlertOnVendorSignIn = _alertOnVendorSignIn,
                AlertOnVendorSignOut = _alertOnVendorSignOut,

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add Appointment Post api endpoint to add a Appointment")]
        public void WhenICallTheAddAppointmentPostApiEndpointToAddAAppointment()
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PostAsync(_addItem).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("POST Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Appointment Id")]
        public void ThenTheAddResultShouldBeAAppointmentId()
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PostAsync(_addItem).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("POST Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Item Id")]
        public void ThenTheAddResultShouldBeAItemId()
        {
            //_addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<Appointment, Appointment>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                //Let's store the newly added Id in delete/edit, so we can later
                //edit and delete this same record
                _editIdValue = _addedIdValue;
                _deletedIdValue = _addedIdValue;

                ////validate values changed
                Assert.AreEqual(_addItem.Description, result.Description);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //
        #endregion Post - add a new item by a populated item
                
        #region Get - get a list of items
        //
        [When(@"I call the Appointment Get api endpoint")]
        public void WhenICallTheAppointmentGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Appointment>>();
        }

        [Then(@"the get result should be a list of Appointments")]
        public void ThenTheGetResultShouldBeAListOfAppointments()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Appointment>);
        }
        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following Appointment GetById input")]
        public void GivenTheFollowingAppointmentGetByIdInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _getId = row["Id"];

                break;
            }
            Assert.IsNotNull(_getId);
            _getIdValue = ConvertToIntValue(_getId);
            Assert.IsTrue(_getIdValue > 0);  
        }

        [When(@"I call the Appointment Get api endpoint by Id")]
        public void WhenICallTheAppointmentGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<Appointment>(_getIdValue);
        }

        [Then(@"the get by id result should be a Appointment")]
        public void ThenTheGetByIdResultShouldBeAAppointment()
        {
            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as Appointment);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }

        //
        #endregion Post - add a new item by a populated item

        #region Put - edit an existing item by a populated item, and its Id
        //
        [Given(@"the following Appointment Edit input")]
        public void GivenTheFollowingAppointmentEditInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _editId = _editIdValue > 0 ? _editIdValue.ToString() : row["Id"];
                _description = row["Description"];

                break;
            }
            Assert.IsNotNull(_editId);
            _editIdValue = ConvertToIntValue(_editId);

            //var temp = GetResponseById<Appointment>(_editIdValue);

            Assert.IsTrue(_editIdValue > 0);
            Assert.IsNotNull(_description);

            _editItem = new Appointment()
            {
                Id = _editIdValue,
                Description = _description,
            };
        }

        [When(@"I call the edit Appointment Put api endpoint to edit a appointment")]
        public void WhenICallTheEditAppointmentPutApiEndpointToEditAAppointment()
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PutAsync(_editItem.Id, _editItem).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("PUT Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[EditItemKey] = response;
        }

        [Then(@"the edit result should be an updated Appointment")]
        public void ThenTheEditResultShouldBeAnUpdatedAppointment()
        {
            //grab the resulting added item
            var response = (ScenarioContext.Current[EditItemKey] as HttpResponseMessage);
            var result = PutResponse<Appointment, Appointment>(_editItem.Id, _editItem);
            if (result != null)
            {
                Assert.IsTrue(_editIdValue > 0);
                Assert.AreEqual(_editIdValue, result.Id);

                //validate values changed
                Assert.AreEqual(_editItem.Description, result.Description);
            }

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following Appointment Delete input")]
        public void GivenTheFollowingAppointmentDeleteInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _deletedId = _deletedIdValue > 0 ? _deletedIdValue.ToString() : row["Id"]; //this is just a place holder, using Id from added item

                break;
            }
            Assert.IsNotNull(_deletedId);
            _deletedIdValue = ConvertToIntValue(_deletedId);

            Assert.IsTrue(_deletedIdValue > -1);

            var last = GetResponse<List<Appointment>>();
            var l = last[last.Count - 1];
            _deletedIdValue = l.Id;
        }

        [When(@"I call the delete Appointment Post api endpoint to delete a appointment")]
        public void WhenICallTheDeleteAppointmentPostApiEndpointToDeleteAAppointment()
        {
            _addItem = new Appointment()
            {
                Description = "test",

                CreatedOn = DateTime.UtcNow,
            };
            WhenICallTheAddAppointmentPostApiEndpointToAddAAppointment();
            var result = PostResponse<Appointment, Appointment>(_addItem);
            _deletedIdValue = result.Id;

            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            DeleteAsync(_deletedIdValue).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("POST Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[DeleteItemKey] = response;
        }

        [Then(@"the delete result should be an deleted Appointment")]
        public void ThenTheDeleteResultShouldBeAnDeletedAppointment()
        {
            //grab the resulting added item
            var deleted = GetResponseById<Appointment>(_deletedIdValue);
            Assert.IsNull(deleted);

            var response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //
        [When(@"I call the Appointment Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheAppointmentExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Appointment exists result should be bool true or false")]
        public void ThenTheAppointmentExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Appointment>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //
        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //

        #region helpers
        //
        public int ConvertToIntValue(string value)
        {
            var result = -1;

            int.TryParse(value, out result);

            return result;
        }
        //
        #endregion helpers
    }
}