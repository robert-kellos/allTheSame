using System.Collections.Generic;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using System.Net.Http;
using System;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class KioskStatusSteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "kisokStatus_get_list";
        private const string GetItemKey = "kisokStatus_get_item";
        private const string AddItemKey = "kisokStatus_add_item";
        private const string EditItemKey = "kisokStatus_edit_item";
        private const string DeleteItemKey = "kisokStatus_delete_item";
        private const string ExistsItemKey = "kisokStatus_exists_item";

        private KioskStatus _getItem;
        private KioskStatus _addItem;
        private KioskStatus _editItem;
        private KioskStatus _deleteItem;

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

        private string _code = "";
        private string _label = "";
        //
        #endregion Local Properties/Fields

        public override string Uri => "/api/KioskStatus";

        #region CRUD Tests
        //

        [When(@"I call the add KioskStatus Post api endpoint to add a KioskStatus it checks if exists pulls item edits it and deletes it")]
        public void WhenICallTheAddKioskStatusPostApiEndpointToAddAKioskStatusItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a KioskStatus Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeAKioskStatusIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<KioskStatus>(_getIdValue);
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
        [Given(@"the following KioskStatus Add input")]
        public void GivenTheFollowingKioskStatusAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _code = row["Code"];
                _label = row["Label"];

                break;
            }

            _addItem = new KioskStatus()
            {
                Code = _code,
                Label = _label,

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add KioskStatus Post api endpoint to add a KioskStatus")]
        public void WhenICallTheAddKioskStatusPostApiEndpointToAddAKioskStatus()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t =>
                {
                    response = ActionResponse(t, out error);
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a KioskStatus Id")]
        public void ThenTheAddResultShouldBeAKioskStatusId()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t =>
                {
                    response = ActionResponse(t, out error);
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

       
        //
        #endregion Post - add a new item by a populated item

        #region Get - get a list of items
        //
        [When(@"I call the KioskStatus Get api endpoint")]
        public void WhenICallTheKioskStatusGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<KioskStatus>>();
        }

        [Then(@"the get result should be a list of KioskStatuses")]
        public void ThenTheGetResultShouldBeAListOfKioskStatuses()
        {
            //
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<KioskStatus>);
        }

        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following KioskStatus GetById input")]
        public void GivenTheFollowingKioskStatusGetByIdInput(Table table)
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t =>
                {
                    response = ActionResponse(t, out error);
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        //
        #endregion Post - add a new item by a populated item

        #region Put - edit an existing item by a populated item, and its Id
        //
        [Given(@"the following KioskStatus Edit input")]
        public void GivenTheFollowingKioskStatusEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit KioskStatus Put api endpoint to edit a KioskStatus")]
        public void WhenICallTheEditKioskStatusPutApiEndpointToEditAKioskStatus()
        {
            //
        }

        [Then(@"the edit result should be an updated KioskStatus")]
        public void ThenTheEditResultShouldBeAnUpdatedKioskStatus()
        {
            //
        }

        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following KioskStatus Delete input")]
        public void GivenTheFollowingKioskStatusDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete KioskStatus Post api endpoint to delete a KioskStatus")]
        public void WhenICallTheDeleteKioskStatusPostApiEndpointToDeleteAKioskStatus()
        {
            //
        }

        [Then(@"the delete result should be an deleted KioskStatus")]
        public void ThenTheDeleteResultShouldBeAnDeletedKioskStatus()
        {
            //
        }

        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //
        [Given(@"the following KioskStatus Id input")]
        public void GivenTheFollowingKioskStatusIdInput(Table table)
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

        [When(@"I call the KioskStatus Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheKioskStatusExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the KioskStatus exists result should be bool true or false")]
        public void ThenTheKioskStatusExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<KioskStatus>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //
        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
    }
}