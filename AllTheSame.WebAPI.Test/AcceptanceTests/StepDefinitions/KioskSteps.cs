using System;
using System.Collections.Generic;
using System.Net.Http;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class KioskSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/Kiosk";

        #region Get - get an item by Id

        //
        [Given(@"the following Kiosk GetById input")]
        public void GivenTheFollowingKioskGetByIdInput(Table table)
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        //

        #endregion Post - add a new item by a populated item

        #region Local Properties/Fields

        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "kisok_get_list";
        private const string GetItemKey = "kisok_get_item";
        private const string AddItemKey = "kisok_add_item";
        private const string EditItemKey = "kisok_edit_item";
        private const string DeleteItemKey = "kisok_delete_item";
        private const string ExistsItemKey = "kisok_exists_item";

        private Kiosk _getItem;
        private Kiosk _addItem;
        private Kiosk _editItem;
        private Kiosk _deleteItem;

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

        private int _communityId = 1;
        private int _kioskStatusId = 1;
        private string _name = "test";
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add Kiosk Post api endpoint to add a Kiosk it checks if exists pulls item edits it and deletes it"
            )]
        public void WhenICallTheAddKioskPostApiEndpointToAddAKioskItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Kiosk Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeAKioskIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<Kiosk>(_getIdValue);
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
        [Given(@"the following Kiosk Add input")]
        public void GivenTheFollowingKioskAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _communityId = Convert.ToInt32(row["CommunityId"]);
                _kioskStatusId = Convert.ToInt32(row["KioskStatusId"]);
                _name = row["Name"];

                break;
            }

            _addItem = new Kiosk
            {
                CommunityId = _communityId,
                KioskStatusId = _kioskStatusId,
                Name = _name,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add Kiosk Post api endpoint to add a Kiosk")]
        public void WhenICallTheAddKioskPostApiEndpointToAddAKiosk()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Kiosk Id")]
        public void ThenTheAddResultShouldBeAKioskId()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }


        //

        #endregion Post - add a new item by a populated item

        #region Get - get a list of items

        //
        [When(@"I call the Kiosk Get api endpoint")]
        public void WhenICallTheKioskGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Kiosk>>();
        }

        [Then(@"the get result should be a list of Kiosks")]
        public void ThenTheGetResultShouldBeAListOfKiosks()
        {
            //
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Kiosk>);
        }

        //

        #endregion Get - get a list of items

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following Kiosk Edit input")]
        public void GivenTheFollowingKioskEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit Kiosk Put api endpoint to edit a Kiosk")]
        public void WhenICallTheEditKioskPutApiEndpointToEditAKiosk()
        {
            //
        }

        [Then(@"the edit result should be an updated Kiosk")]
        public void ThenTheEditResultShouldBeAnUpdatedKiosk()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following Kiosk Delete input")]
        public void GivenTheFollowingKioskDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete Kiosk Post api endpoint to delete a Kiosk")]
        public void WhenICallTheDeleteKioskPostApiEndpointToDeleteAKiosk()
        {
            //
        }

        [Then(@"the delete result should be an deleted Kiosk")]
        public void ThenTheDeleteResultShouldBeAnDeletedKiosk()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following Kiosk Id input")]
        public void GivenTheFollowingKioskIdInput(Table table)
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

        [When(@"I call the Kiosk Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheKioskExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Kiosk exists result should be bool true or false")]
        public void ThenTheKioskExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Kiosk>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}