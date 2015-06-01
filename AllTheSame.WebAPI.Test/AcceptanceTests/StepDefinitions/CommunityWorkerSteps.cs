using System;
using System.Collections.Generic;
using System.Net.Http;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class CommunityWorkerSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/CommunityWorker";

        #region Get - get an item by Id

        //
        [Given(@"the following CommunityWorker GetById input")]
        public void GivenTheFollowingCommunityWorkerGetByIdInput(Table table)
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

        private const string GetListKey = "communityWorker_get_list";
        private const string GetItemKey = "communityWorker_get_item";
        private const string AddItemKey = "communityWorker_add_item";
        private const string EditItemKey = "communityWorker_edit_item";
        private const string DeleteItemKey = "communityWorker_delete_item";
        private const string ExistsItemKey = "communityWorker_exists_item";

        private CommunityWorker _getItem;
        private CommunityWorker _addItem;
        private CommunityWorker _editItem;
        private CommunityWorker _deleteItem;

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
	    [CommunityId] [int] NOT NULL,//17
	    [PersonId] [int] NOT NULL,//1
	    [CreatedOn] [datetime] NULL DEFAULT (getutcdate()),
	    [UpdatedOn] [datetime] NULL,
        */
        private int _communityid = 17;
        private readonly int _personId = 1;
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add CommunityWorker Post api endpoint to add a CommunityWorker it checks if exists pulls item edits it and deletes it"
            )]
        public void
            WhenICallTheAddCommunityWorkerPostApiEndpointToAddACommunityWorkerItChecksIfExistsPullsItemEditsItAndDeletesIt
            ()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(
            @"the add result should be a CommunityWorker Id check exists get by id edit and delete with http response returns"
            )]
        public void ThenTheAddResultShouldBeACommunityWorkerIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<CommunityWorker>(_getIdValue);
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
        [Given(@"the following CommunityWorker Add input")]
        public void GivenTheFollowingCommunityWorkerAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _communityid = Convert.ToInt32(row["CommunityId"]);


                break;
            }

            _addItem = new CommunityWorker
            {
                CommunityId = _communityid,
                PersonId = _personId,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add CommunityWorker Post api endpoint to add a CommunityWorker")]
        public void WhenICallTheAddCommunityWorkerPostApiEndpointToAddACommunityWorker()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a CommunityWorker Id")]
        public void ThenTheAddResultShouldBeACommunityWorkerId()
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
        [When(@"I call the CommunityWorker Get api endpoint")]
        public void WhenICallTheCommunityWorkerGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<CommunityWorker>>();
        }

        [Then(@"the get result should be a list of CommunityWorkers")]
        public void ThenTheGetResultShouldBeAListOfCommunityWorkers()
        {
            //
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<CommunityWorker>);
        }

        //

        #endregion Get - get a list of items

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following CommunityWorker Edit input")]
        public void GivenTheFollowingCommunityWorkerEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit CommunityWorker Put api endpoint to edit a CommunityWorker")]
        public void WhenICallTheEditCommunityWorkerPutApiEndpointToEditACommunityWorker()
        {
            //
        }

        [Then(@"the edit result should be an updated CommunityWorker")]
        public void ThenTheEditResultShouldBeAnUpdatedCommunityWorker()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following CommunityWorker Delete input")]
        public void GivenTheFollowingCommunityWorkerDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete CommunityWorker Post api endpoint to delete a communityWorker")]
        public void WhenICallTheDeleteCommunityWorkerPostApiEndpointToDeleteACommunityWorker()
        {
            //
        }

        [Then(@"the delete result should be an deleted CommunityWorker")]
        public void ThenTheDeleteResultShouldBeAnDeletedCommunityWorker()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following CommunityWorker Id input")]
        public void GivenTheFollowingCommunityWorkerIdInput(Table table)
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

        [When(@"I call the CommunityWorker Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheCommunityWorkerExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the CommunityWorker exists result should be bool true or false")]
        public void ThenTheCommunityWorkerExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<CommunityWorker>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}