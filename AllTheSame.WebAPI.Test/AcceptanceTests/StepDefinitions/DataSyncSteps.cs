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
    public class DataSyncSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/DataSync";

        #region Local Properties/Fields

        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "dataSync_get_list";
        private const string GetItemKey = "dataSync_get_item";
        private const string AddItemKey = "dataSync_add_item";
        private const string EditItemKey = "dataSync_edit_item";
        private const string DeleteItemKey = "dataSync_delete_item";
        private const string ExistsItemKey = "dataSync_exists_item";

        private DataSync _getItem;
        private DataSync _addItem;
        private DataSync _editItem;
        private DataSync _deleteItem;

        private string _getId = "-1";
        private int _getIdValue = -1;

        private readonly string _editId = "-1";
        private int _editIdValue = -1;

        private string _addedId = "-1";
        private int _addedIdValue = -1;

        private string _deletedId = "-1";
        private int _deletedIdValue = -1;

        private string _existsId = "-1";
        private int _existsIdValue = -1;

        private int _kioskId = 1;
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add DataSync Post api endpoint to add a DataSync it checks if exists pulls item edits it and deletes it"
            )]
        public void WhenICallTheAddDataSyncPostApiEndpointToAddADataSyncItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(
            @"the add result should be a DataSync Id check exists get by id edit and delete with http response returns")
        ]
        public void ThenTheAddResultShouldBeADataSyncIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<DataSync>(_getIdValue);
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
        [Given(@"the following DataSync Add input")]
        public void GivenTheFollowingDataSyncAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _kioskId = Convert.ToInt32(row["KioskId"]);

                break;
            }

            _addItem = new DataSync
            {
                KioskId = _kioskId,
                CreatedOn = DateTime.UtcNow.AddDays(1)
            };
        }

        [When(@"I call the add DataSync Post api endpoint to add a DataSync")]
        public void WhenICallTheAddDataSyncPostApiEndpointToAddADataSync()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a DataSync Id")]
        public void ThenTheAddResultShouldBeADataSyncId()
        {
            _addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<DataSync, DataSync>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                //validate values changed
                //Assert.AreEqual(_addDataSync.FirstName, result.FirstName);
            }

            var response = (ScenarioContext.Current[AddItemKey]);

            Assert.IsNotNull(response);
            //Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //

        #endregion Post - add a new item by a populated item

        #region Get - get a list of items

        //
        [When(@"I call the DataSync Get api endpoint")]
        public void WhenICallTheDataSyncGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<DataSync>>();
        }

        [Then(@"the get result should be a list of DataSyncs")]
        public void ThenTheGetResultShouldBeAListOfDataSyncs()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<DataSync>);
        }

        //

        #endregion Get - get a list of items

        #region Get - get an item by Id

        //
        [Given(@"the following DataSync GetById input")]
        public void GivenTheFollowingDataSyncGetByIdInput(Table table)
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

        [When(@"I call the DataSync Get api endpoint by Id")]
        public void WhenICallTheDataSyncGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<DataSync>(_getIdValue);
        }

        [Then(@"the get by id result should be a DataSync")]
        public void ThenTheGetByIdResultShouldBeADataSync()
        {
            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as DataSync);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }

        //

        #endregion Get - get an item by Id

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following DataSync Edit input")]
        public void GivenTheFollowingDataSyncEditInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                //_editId = row["Id"];
                //_firstName = row["FirstName"];
                //_lastName = row["LastName"];
                //_email = row["Email"];
                //_mobileNumber = row["MobileNumber"];
            }
            Assert.IsNotNull(_editId);
            _editIdValue = ConvertToIntValue(_editId);

            //var temp = GetResponseById<DataSync>(_editIdValue);

            //Assert.IsTrue(_editIdValue > 0);
            //Assert.IsNotNull(_firstName);
            //Assert.IsNotNull(_email);
            //Assert.IsNotNull(_email.IsValidEmailAddress());

            _editItem = new DataSync
            {
                Id = _editIdValue
            };
        }

        [When(@"I call the edit DataSync Put api endpoint to edit a DataSync")]
        public void WhenICallTheEditDataSyncPutApiEndpointToEditADataSync()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PutAsync(_editItem.Id, _editItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[EditItemKey] = response;
        }

        [Then(@"the edit result should be an updated DataSync")]
        public void ThenTheEditResultShouldBeAnUpdatedDataSync()
        {
            //grab the resulting added item
            var response = (ScenarioContext.Current[EditItemKey] as HttpResponseMessage);
            var result = PutResponse<DataSync, DataSync>(_editItem.Id, _editItem);
            if (result != null)
            {
                Assert.IsTrue(_editIdValue > 0);
                Assert.AreEqual(_editIdValue, result.Id);

                //validate values changed
                //Assert.AreEqual(_editDataSync.FirstName, result.FirstName);
                //Assert.AreEqual(_editDataSync.LastName, result.LastName);
                //Assert.AreEqual(_editDataSync.Email, result.Email);
                //Assert.AreEqual(_editDataSync.MobilePhone, result.MobilePhone);
            }

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following DataSync Delete input")]
        public void GivenTheFollowingDataSyncDeleteInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _deletedId = row["Id"]; //this is just a place holder, using Id from added item

                break;
            }
            Assert.IsNotNull(_deletedId);
            _deletedIdValue = ConvertToIntValue(_deletedId);

            Assert.IsTrue(_deletedIdValue > -1);

            //var last = GetResponse<List<DataSync>>();
            //var l = last[last.Count - 1];
            //_deletedIdValue = l.Id;
        }

        [When(@"I call the delete DataSync Post api endpoint to delete a DataSync")]
        public void WhenICallTheDeleteDataSyncPostApiEndpointToDeleteADataSync()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            DeleteAsync(_deletedIdValue).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[DeleteItemKey] = response;
        }

        [Then(@"the delete result should be an deleted DataSync")]
        public void ThenTheDeleteResultShouldBeAnDeletedDataSync()
        {
            //grab the resulting added item
            var deleted = GetResponseById<DataSync>(_deletedIdValue);
            Assert.IsNull(deleted);

            var response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following DataSync Id input")]
        public void GivenTheFollowingDataSyncIdInput(Table table)
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

        [When(@"I call the DataSync Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheDataSyncExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the DataSync exists result should be bool true or false")]
        public void ThenTheDataSyncExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<DataSync>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}