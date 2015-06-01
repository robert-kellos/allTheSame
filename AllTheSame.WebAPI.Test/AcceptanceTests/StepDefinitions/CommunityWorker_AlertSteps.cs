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
    public class CommunityWorkerAlertSteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "communityWorker_Alert_get_list";
        private const string GetItemKey = "communityWorker_Alert_get_item";
        private const string AddItemKey = "communityWorker_Alert_add_item";
        private const string EditItemKey = "communityWorker_Alert_edit_item";
        private const string DeleteItemKey = "communityWorker_Alert_delete_item";
        private const string ExistsItemKey = "communityWorker_Alert_exists_item";

        private CommunityWorker_Alert _getItem;
        private CommunityWorker_Alert _addItem;
        private CommunityWorker_Alert _editItem;
        private CommunityWorker_Alert _deleteItem;

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
	    [CommunityWorkerId] [int] NOT NULL,//23
	    [AlertId] [int] NOT NULL,//17
	    [IsRead] [bit] NOT NULL,//1
	    [CreatedOn] [datetime] NULL,
	    [UpdatedOn] [datetime] NULL,
            
        */
        private int _communityWorkId = 24;
        private int _alertId = 17;
        private bool _isRead = true;
        //
        #endregion Local Properties/Fields

        public override string Uri => "/api/CommunityWorker_Alert";

        #region CRUD Tests
        //

        [When(@"I call the add CommunityWorker_Alert Post api endpoint to add a CommunityWorker_Alert it checks if exists pulls item edits it and deletes it")]
        public void WhenICallTheAddCommunityWorker_AlertPostApiEndpointToAddACommunityWorker_AlertItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a CommunityWorker_Alert Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeACommunityWorker_AlertIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<CommunityWorker_Alert>(_getIdValue);
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
        [Given(@"the following CommunityWorker_Alert Add input")]
        public void GivenTheFollowingCommunityWorker_AlertAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _isRead = Convert.ToBoolean(row["IsRead"]);

                break;
            }

            _addItem = new CommunityWorker_Alert()
            {
                CommunityWorkerId = _communityWorkId,
                AlertId = _alertId,
                IsRead = _isRead,

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add CommunityWorker_Alert Post api endpoint to add a CommunityWorker_Alert")]
        public void WhenICallTheAddCommunityWorker_AlertPostApiEndpointToAddACommunityWorker_Alert()
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

        [Then(@"the add result should be a CommunityWorker_Alert Id")]
        public void ThenTheAddResultShouldBeACommunityWorker_AlertId()
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
        [When(@"I call the CommunityWorker_Alert Get api endpoint")]
        public void WhenICallTheCommunityWorker_AlertGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<CommunityWorker_Alert>>();
        }

        [Then(@"the get result should be a list of CommunityWorker_Alerts")]
        public void ThenTheGetResultShouldBeAListOfCommunityWorker_Alerts()
        {
            //
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<CommunityWorker_Alert>);
        }

        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following CommunityWorker_Alert GetById input")]
        public void GivenTheFollowingCommunityWorker_AlertGetByIdInput(Table table)
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
        [Given(@"the following CommunityWorker_Alert Edit input")]
        public void GivenTheFollowingCommunityWorker_AlertEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit CommunityWorker_Alert Put api endpoint to edit a CommunityWorker_Alert")]
        public void WhenICallTheEditCommunityWorker_AlertPutApiEndpointToEditACommunityWorker_Alert()
        {
            //
        }

        [Then(@"the edit result should be an updated CommunityWorker_Alert")]
        public void ThenTheEditResultShouldBeAnUpdatedCommunityWorker_Alert()
        {
            //
        }

        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following CommunityWorker_Alert Delete input")]
        public void GivenTheFollowingCommunityWorker_AlertDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete CommunityWorker_Alert Post api endpoint to delete a CommunityWorker_Alert")]
        public void WhenICallTheDeleteCommunityWorker_AlertPostApiEndpointToDeleteACommunityWorker_Alert()
        {
            //
        }

        [Then(@"the delete result should be an deleted CommunityWorker_Alert")]
        public void ThenTheDeleteResultShouldBeAnDeletedCommunityWorker_Alert()
        {
            //
        }

        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //
        [Given(@"the following CommunityWorker_Alert Id input")]
        public void GivenTheFollowingCommunityWorker_AlertIdInput(Table table)
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

        [When(@"I call the CommunityWorker_Alert Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheCommunityWorker_AlertExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the CommunityWorker_Alert exists result should be bool true or false")]
        public void ThenTheCommunityWorker_AlertExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<CommunityWorker_Alert>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //
        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
    }
}