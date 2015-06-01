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
    public class CommunitySteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/Community";

        #region Local Properties/Fields

        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "community_get_list";
        private const string GetItemKey = "community_get_item";
        private const string AddItemKey = "community_add_item";
        private const string EditItemKey = "community_edit_item";
        private const string DeleteItemKey = "community_delete_item";
        private const string ExistsItemKey = "community_exists_item";

        private Community _getItem;
        private Community _addItem;
        private Community _editItem;
        private Community _deleteItem;

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
    [OrgId] [int] NOT NULL, //5
    [CommunityTypeId] [int] NULL, //1
    [IndustryId] [int] NULL, //1
    [Name] [nvarchar](100) NOT NULL,
    [Description] [nvarchar](500) NULL,
    [Raiting] [int] NULL,
    [NumBeds] [int] NULL,
    [Version] [timestamp] NOT NULL,
    [CreatedOn] [datetime] NULL DEFAULT(getutcdate()),
    [UpdatedOn] [datetime] NULL,
        */
        private readonly int _orgId = 5;
        private readonly int _communityTypeId = 1;
        private readonly int _industryId = 1;
        private string _name = "";
        private string _description = "test";
        private int _numBeds = 100;
        private int _raiting = 6;
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add Community Post api endpoint to add a Community it checks if exists pulls item edits it and deletes it"
            )]
        public void WhenICallTheAddCommunityPostApiEndpointToAddACommunityItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(
            @"the add result should be a Community Id check exists get by id edit and delete with http response returns"
            )]
        public void ThenTheAddResultShouldBeACommunityIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<Community>(_getIdValue);
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
        [Given(@"the following Community Add input")]
        public void GivenTheFollowingCommunityAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _name = row["Name"];
                _description = row["Description"];
                _numBeds = Convert.ToInt32(row["NumBeds"]);
                _raiting = Convert.ToInt32(row["Raiting"]);

                break;
            }
            Assert.IsNotNull(_name);

            _addItem = new Community
            {
                IndustryId = _industryId,
                OrgId = _orgId,
                CommunityTypeId = _communityTypeId,
                Name = _name,
                Raiting = _raiting,
                Description = _description,
                NumBeds = _numBeds,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add Community Post api endpoint to add a Community")]
        public void WhenICallTheAddCommunityPostApiEndpointToAddACommunity()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Community Id")]
        public void ThenTheAddResultShouldBeACommunityId()
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Item Id")]
        public void ThenTheAddResultShouldBeAItemId()
        {
            _addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<Community, Community>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                ////validate values changed
                Assert.AreEqual(_addItem.IndustryId, result.IndustryId);
                Assert.AreEqual(_addItem.OrgId, result.OrgId);
                Assert.AreEqual(_addItem.CommunityTypeId, result.CommunityTypeId);
                Assert.AreEqual(_addItem.Name, result.Name);
                Assert.AreEqual(_addItem.Description, result.Description);
                Assert.AreEqual(_addItem.Raiting, result.Raiting);
                Assert.AreEqual(_addItem.NumBeds, result.NumBeds);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //

        #endregion Post - add a new item by a populated item

        #region Get - get a list of items

        //
        [When(@"I call the Community Get api endpoint")]
        public void WhenICallTheCommunityGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Community>>();
        }

        [Then(@"the get result should be a list of Communities")]
        public void ThenTheGetResultShouldBeAListOfCommunities()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Community>);
        }

        //

        #endregion Get - get a list of items

        #region Get - get an item by Id

        //
        [Given(@"the following Community GetById input")]
        public void GivenTheFollowingCommunityGetByIdInput(Table table)
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

        [When(@"I call the Community Get api endpoint by Id")]
        public void WhenICallTheCommunityGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<Community>(_getIdValue);
        }

        [Then(@"the get by id result should be a Community")]
        public void ThenTheGetByIdResultShouldBeACommunity()
        {
            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as Community);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }

        //

        #endregion Post - add a new item by a populated item

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following Community Edit input")]
        public void GivenTheFollowingCommunityEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit Community Put api endpoint to edit a community")]
        public void WhenICallTheEditCommunityPutApiEndpointToEditACommunity()
        {
            //
        }

        [Then(@"the edit result should be an updated Community")]
        public void ThenTheEditResultShouldBeAnUpdatedCommunity()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following Community Delete input")]
        public void GivenTheFollowingCommunityDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete Community Post api endpoint to delete a community")]
        public void WhenICallTheDeleteCommunityPostApiEndpointToDeleteACommunity()
        {
            //
        }

        [Then(@"the delete result should be an deleted Community")]
        public void ThenTheDeleteResultShouldBeAnDeletedCommunity()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following Community Id input")]
        public void GivenTheFollowingCommunityIdInput(Table table)
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

        [When(@"I call the Community Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheCommunityExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Community exists result should be bool true or false")]
        public void ThenTheCommunityExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Community>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}