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
    public class AlertTypeSteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "alertType_get_list";
        private const string GetItemKey = "alertType_get_item";
        private const string AddItemKey = "alertType_add_item";
        private const string EditItemKey = "alertType_edit_item";
        private const string DeleteItemKey = "alertType_delete_item";
        private const string ExistsItemKey = "alertType_exists_item";

        private AlertType _getItem;
        private AlertType _addItem;
        private AlertType _editItem;
        private AlertType _deleteItem;

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
        
        /*
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [Code] [varchar](50) NOT NULL,
        [FormatText] [nvarchar](500) NOT NULL,
        [CreatedOn] [datetime] NULL,
        [UpdatedOn] [datetime] NULL,
        */
        private string _code = "";
        private string _formatText = "";
        //
        #endregion Local Properties/Fields

        public override string Uri => "/api/AlertType";

        #region CRUD Tests
        //

        [When(@"I call the add AlertType Post api endpoint to add a AlertType it checks if exists pulls item edits it and deletes it")]
        public void WhenICallTheAddAlertTypePostApiEndpointToAddAAlertTypeItChecksIfExistsPullsItemEditsItAndDeletesIt()
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

        [Then(@"the add result should be a AlertType Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeAAlertTypeIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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

        private AlertType Add(AlertType item)
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
            var resultAdd = PostResponse<AlertType, AlertType>(item);
            if (resultAdd != null)
            {
                _addedIdValue = resultAdd.Id;
                Assert.IsTrue(_addedIdValue > 0);

                //Let's store the newly added Id in delete/edit, so we can later
                //edit and delete this same record
                _editIdValue = _addedIdValue;
                _deletedIdValue = _addedIdValue;

                ////validate values changed
                Assert.AreEqual(item.Code, resultAdd.Code);
            }

            response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            return resultAdd;
        }

        private AlertType Exists(int id)
        {
            //Check it exists
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(id);

            var resultExists = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var itemReturned = GetResponseById<AlertType>(id);

            var truth = (itemReturned != null && itemReturned.Id == id);
            Assert.AreEqual(truth, resultExists);

            return itemReturned;
        }

        private AlertType GetById(int id)
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<AlertType>(id);

            var resultGet = ScenarioContext.Current[GetItemKey];
            var itemGet = (resultGet as AlertType);

            Assert.IsNotNull(itemGet);
            Assert.IsTrue(itemGet.Id == id);

            return itemGet;
        }

        private void Update(int id, AlertType item)
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
            var resultEdit = PutResponse<AlertType, AlertType>(item.Id, item);
            if (resultEdit != null)
            {
                Assert.IsTrue(id > 0);
                Assert.AreEqual(id, resultEdit.Id);

                //validate values changed
                Assert.AreEqual(item.Code, resultEdit.Code);
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
            var deleted = GetResponseById<AlertType>(id);
            Assert.IsNull(deleted);

            response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
        //
        #endregion CRUD Tests

        
        #region Post - add a new item by a populated item
        //
        [Given(@"the following AlertType Add input")]
        public void GivenTheFollowingAlertTypeAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _code = row["Code"];
                _formatText = row["FormatText"];

                break;
            }
            Assert.IsNotNull(_code);

            _addItem = new AlertType()
            {
                Code = _code,
                FormatText = _formatText,

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add AlertType Post api endpoint to add a alertType")]
        public void WhenICallTheAddAlertTypePostApiEndpointToAddAAlertType()
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

        [Then(@"the add result should be a AlertType Id")]
        public void ThenTheAddResultShouldBeAAlertTypeId()
        {
            //_addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<AlertType, AlertType>(_addItem);
            if (result != null)
            {

                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                //Let's store the newly added Id in delete/edit, so we can later
                //edit and delete this same record
                _editIdValue = _addedIdValue;
                _deletedIdValue = _addedIdValue;

                ////validate values changed
                Assert.AreEqual(_addItem.Code, result.Code);
                Assert.AreEqual(_addItem.FormatText, result.FormatText);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //
        #endregion Post - add a new item by a populated item

        #region Get - get a list of items
        //
        [When(@"I call the AlertType Get api endpoint")]
        public void WhenICallTheAlertTypeGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<AlertType>>();
        }

        [Then(@"the get result should be a list of AlertTypes")]
        public void ThenTheGetResultShouldBeAListOfAlertTypes()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<AlertType>);
        }

        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following AlertType GetById input")]
        public void GivenTheFollowingAlertTypeGetByIdInput(Table table)
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

        [When(@"I call the AlertType Get api endpoint by Id")]
        public void WhenICallTheAlertGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<AlertType>(_getIdValue);
        }

        [Then(@"the get by id result should be a AlertType")]
        public void ThenTheGetByIdResultShouldBeAAlertType()
        {
            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as AlertType);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }
        
        //
        #endregion Post - add a new item by a populated item

        #region Put - edit an existing item by a populated item, and its Id
        //
        [Given(@"the following AlertType Edit input")]
        public void GivenTheFollowingAlertTypeEditInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _editId = _addedIdValue > 0 ? _addedIdValue.ToString() : row["Id"];
                _code = row["Code"];
                _formatText = row["FormatText"];

                break;
            }
            Assert.IsNotNull(_editId);
            _editIdValue = ConvertToIntValue(_editId);

            //var temp = GetResponseById<AlertType>(_editIdValue);

            Assert.IsTrue(_editIdValue > 0);
            Assert.IsNotNull(_code);

            _editItem = new AlertType()
            {
                Id = _editIdValue,
                Code = _code,
                FormatText = _formatText,
            };
        }

        [When(@"I call the edit AlertType Put api endpoint to edit a alertType")]
        public void WhenICallTheEditAlertTypePutApiEndpointToEditAAlertType()
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

        [Then(@"the edit result should be an updated AlertType")]
        public void ThenTheEditResultShouldBeAnUpdatedAlertType()
        {
            //grab the resulting added item
            var response = (ScenarioContext.Current[EditItemKey] as HttpResponseMessage);
            var result = PutResponse<AlertType, AlertType>(_editItem.Id, _editItem);
            if (result != null)
            {
                Assert.IsTrue(_editIdValue > 0);
                Assert.AreEqual(_editIdValue, result.Id);

                //validate values changed
                Assert.AreEqual(_editItem.Code, result.Code);
                Assert.AreEqual(_editItem.FormatText, result.FormatText);
            }

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following AlertType Delete input")]
        public void GivenTheFollowingAlertTypeDeleteInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _deletedId = _addedIdValue > 0 ? _addedIdValue.ToString() : row["Id"]; //this is just a place holder, using Id from added item

                break;
            }

            //because we are not using input data unless > 0 is entered - it comes from add
            if (_deletedIdValue > 0)
            {
                Assert.IsNotNull(_deletedId);
                _deletedIdValue = ConvertToIntValue(_deletedId);

                Assert.IsTrue(_deletedIdValue > -1);
            }
        }

        [When(@"I call the delete AlertType Post api endpoint to delete a alertType")]
        public void WhenICallTheDeleteAlertTypePostApiEndpointToDeleteAAlertType()
        {
            _addItem = new AlertType()
            {
                Code = "test",
                FormatText = "test",

                CreatedOn = DateTime.UtcNow,
            };
            WhenICallTheAddAlertTypePostApiEndpointToAddAAlertType();
            var result = PostResponse<AlertType, AlertType>(_addItem);
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

        [Then(@"the delete result should be an deleted AlertType")]
        public void ThenTheDeleteResultShouldBeAnDeletedAlertType()
        {
            //grab the resulting added item
            var deleted = GetResponseById<AlertType>(_deletedIdValue);
            Assert.IsNull(deleted);

            var response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //

        [Given(@"the following AlertType Id input")]
        public void GivenTheFollowingAlertTypeIdInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _existsId = row["Id"];

                break;
            }
            Assert.IsNotNull(_existsId);
            _existsIdValue = ConvertToIntValue(_existsId);

            Assert.IsTrue(_existsIdValue > 0);
        }

        [When(@"I call the AlertType Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheAlertTypeExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the AlertType exists result should be bool true or false")]
        public void ThenTheAlertTypeExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<AlertType>(_existsIdValue);

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