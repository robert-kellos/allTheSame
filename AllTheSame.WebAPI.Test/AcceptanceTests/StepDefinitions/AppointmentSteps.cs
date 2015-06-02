using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class AppointmentSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/Appointment";

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
        private int _appointmentTypeId;
        private readonly int _residentId = 1;
        private readonly int _vendorWorkerId = 2;
        private bool _remindVendor;
        private bool _alertOnVendorSignIn;
        private bool _alertOnVendorSignOut;
        /*
        [Id] [int] IDENTITY(1,1) NOT NULL,
	    [ResidentId] [int] NOT NULL, //1
	    [VendorWorkerId] [int] NOT NULL, //1
	    [AppointmentTypeId] [int] NOT NULL, //1
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

        #region CRUD Tests

        //

        [When(
            @"I call the add Appointment Post api endpoint to add a Appointment it checks if exists pulls item edits it and deletes it"
            )]
        public void
            WhenICallTheAddAppointmentPostApiEndpointToAddAAppointmentItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(
            @"the add result should be a Appointment Id check exists get by id edit and delete with http response returns"
            )]
        public void ThenTheAddResultShouldBeAAppointmentIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
        {
            //did we get a good result
            Assert.IsTrue(_addItem != null && _addItem.Id > 0);

            //set the returned AddID to current Get
            _addedIdValue = _addItem.Id;
            _getIdValue = _addedIdValue;
            _existsIdValue = _getIdValue;

            //check that the item exists
            var itemReturned = Exists(_existsIdValue);
            Assert.IsTrue(itemReturned);

            //use the value used in exists check
            _getIdValue = _addItem.Id;
            Assert.IsTrue(_getIdValue == _addedIdValue);

            //pull the item by Id
            var resultGet = GetById<Appointment>(_getIdValue);
            Assert.IsNotNull(resultGet);
            _getIdValue = resultGet.Id;
            Assert.IsTrue(_getIdValue == _addedIdValue);

            //Now, let's Edit the newly added item
            _editIdValue = _getIdValue;
            _editItem = resultGet;
            Assert.IsTrue(_editIdValue == _addedIdValue);

            //do an update
            var updateResponse = Update(_editIdValue, _editItem);
            Assert.IsNotNull(updateResponse);

            //pass the item just updated
            _deletedIdValue = _editIdValue;
            Assert.IsTrue(_deletedIdValue == _addedIdValue);

            //delete this same item
            var deleteResponse = Delete(_deletedIdValue);
            Assert.IsNotNull(deleteResponse);
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
                _appointmentTypeId = Convert.ToInt32(row["AppointmentTypeId"]);
                //row["RemindVendor"];
                //row["AlertOnVendorSignIn"];
                //row["AlertOnVendorSignOut"];

                break;
            }
            _startTime = DateTime.Now;
            _endTime = DateTime.Now.AddHours(4);
            _remindVendor = true;
            _alertOnVendorSignIn = true;
            _alertOnVendorSignOut = true;

            Assert.IsNotNull(_description);

            _addItem = new Appointment
            {
                ResidentId = _residentId,
                VendorWorkerId = _vendorWorkerId,
                StartTime = _startTime,
                EndTime = _endTime,
                AppointmentTypeId = _appointmentTypeId,
                Description = _description,
                RemindVendor = _remindVendor,
                AlertOnVendorSignIn = _alertOnVendorSignIn,
                AlertOnVendorSignOut = _alertOnVendorSignOut,

                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add Appointment Post api endpoint to add a Appointment")]
        public void WhenICallTheAddAppointmentPostApiEndpointToAddAAppointment()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Appointment Id")]
        public void ThenTheAddResultShouldBeAAppointmentId()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
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

            _editItem = new Appointment
            {
                Id = _editIdValue,
                Description = _description
            };
        }

        [When(@"I call the edit Appointment Put api endpoint to edit a appointment")]
        public void WhenICallTheEditAppointmentPutApiEndpointToEditAAppointment()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PutAsync(_editItem.Id, _editItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
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
                _deletedId = _deletedIdValue > 0 ? _deletedIdValue.ToString() : row["Id"];
                    //this is just a place holder, using Id from added item

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
            _addItem = new Appointment
            {
                Description = "test",
                CreatedOn = DateTime.UtcNow
            };
            WhenICallTheAddAppointmentPostApiEndpointToAddAAppointment();
            var result = PostResponse<Appointment, Appointment>(_addItem);
            _deletedIdValue = result.Id;

            var response = default(HttpResponseMessage);
            AggregateException error;

            DeleteAsync(_deletedIdValue).ContinueWith(
                t => { response = ActionResponse(t, out error); }
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
    }
}