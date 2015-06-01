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
    public class AlertSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/Alert";

        #region Local Properties/Fields

        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "alert_get_list";
        private const string GetItemKey = "alert_get_item";
        private const string AddItemKey = "alert_add_item";
        private const string EditItemKey = "alert_edit_item";
        private const string DeleteItemKey = "alert_delete_item";
        private const string ExistsItemKey = "alert_exists_item";

        private Alert _getItem;
        private Alert _addItem;
        private Alert _editItem;
        private Alert _deleteItem;

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

        private string _description = "";
        private int _alertTypeId = 1;

        /*
        [Id] [int] IDENTITY(1,1) NOT NULL,
	    [AlertTypeId] [int] NOT NULL,
	    [AppointmentId] [int] NULL,
	    [KioskId] [int] NULL,
	    [Description] [nvarchar](max) NULL,
	    [Version] [timestamp] NOT NULL,
	    [CreatedOn] [datetime] NULL,
	    [UpdatedOn] [datetime] NULL,
        */
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add Alert Post api endpoint to add a alert it checks if exists pulls item edits it and deletes it"
            )]
        public void WhenICallTheAddAlertPostApiEndpointToAddAAlertItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Alert Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeAAlertIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<Alert>(_getIdValue);
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
        [Given(@"the following Alert Add input")]
        public void GivenTheFollowingAlertAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _description = row["Description"];
                _alertTypeId = Convert.ToInt32(row["AlertTypeId"]);

                break;
            }
            Assert.IsNotNull(_description);

            _addItem = new Alert
            {
                Description = _description,
                AlertTypeId = _alertTypeId,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add Alert Post api endpoint to add a Alert")]
        public void WhenICallTheAddAlertPostApiEndpointToAddAAlert()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Alert Id")]
        public void ThenTheAddResultShouldBeAAlertId()
        {
            //_addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<Alert, Alert>(_addItem);
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
        [When(@"I call the Alert Get api endpoint")]
        public void WhenICallTheAlertGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Alert>>();
        }

        [Then(@"the get result should be a list of Alerts")]
        public void ThenTheGetResultShouldBeAListOfAlerts()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Alert>);
        }

        //

        #endregion Get - get a list of items

        #region Get - get an item by Id

        //
        [Given(@"the following Alert GetById input")]
        public void GivenTheFollowingAlertGetByIdInput(Table table)
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

        [When(@"I call the Alert Get api endpoint by Id")]
        public void WhenICallTheAlertGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<Alert>(_getIdValue);
        }

        [Then(@"the get by id result should be a Alert")]
        public void ThenTheGetByIdResultShouldBeAAlert()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<Alert>(_getIdValue);

            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as Alert);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }


        //

        #endregion Post - add a new item by a populated item

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following Alert Edit input")]
        public void GivenTheFollowingAlertEditInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _editId = _addedIdValue > 0 ? _addedIdValue.ToString() : row["Id"];
                _description = row["Description"];

                break;
            }
            Assert.IsNotNull(_editId);
            _editIdValue = ConvertToIntValue(_editId);

            //var temp = GetResponseById<Alert>(_editIdValue);

            Assert.IsTrue(_editIdValue > 0);
            Assert.IsNotNull(_description);

            _editItem = new Alert
            {
                Id = _editIdValue,
                Description = _description
            };
        }

        [When(@"I call the edit Alert Put api endpoint to edit a alert")]
        public void WhenICallTheEditAlertPutApiEndpointToEditAAlert()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PutAsync(_editItem.Id, _editItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[EditItemKey] = response;
        }

        [Then(@"the edit result should be an updated Alert")]
        public void ThenTheEditResultShouldBeAnUpdatedAlert()
        {
            //grab the resulting added item
            var response = (ScenarioContext.Current[EditItemKey] as HttpResponseMessage);
            var result = PutResponse<Alert, Alert>(_editItem.Id, _editItem);
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
        [Given(@"the following Alert Delete input")]
        public void GivenTheFollowingAlertDeleteInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _deletedId = _addedIdValue > 0 ? _addedIdValue.ToString() : row["Id"];
                    //this is just a place holder, using Id from added item

                break;
            }
            Assert.IsNotNull(_deletedId);
            _deletedIdValue = ConvertToIntValue(_deletedId);

            Assert.IsTrue(_deletedIdValue > -1);

            //if (_deletedIdValue != 0) return;
            //var last = GetResponse<List<Alert>>();
            //var l = last[last.Count - 1];
            //_deletedIdValue = l.Id;
        }

        [When(@"I call the delete Alert Post api endpoint to delete a alert")]
        public void WhenICallTheDeleteAlertPostApiEndpointToDeleteAAlert()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            DeleteAsync(_deletedIdValue).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[DeleteItemKey] = response;
        }

        [Then(@"the delete result should be an deleted Alert")]
        public void ThenTheDeleteResultShouldBeAnDeletedAlert()
        {
            //grab the resulting added item
            var deleted = GetResponseById<Alert>(_deletedIdValue);
            Assert.IsNull(deleted);

            var response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following Alert Id input")]
        public void GivenTheFollowingAlertIdInput(Table table)
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

        [When(@"I call the Alert Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheAlertExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Alert exists result should be bool true or false")]
        public void ThenTheAlertExistsResultShouldBeBoolTrueOrFalse()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);

            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Alert>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}