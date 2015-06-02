using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class RequirementSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/Requirement";

        #region Get - get an item by Id

        //
        [Given(@"the following Requirement GetById input")]
        public void GivenTheFollowingRequirementGetByIdInput(Table table)
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

        private const string GetListKey = "requirement_get_list";
        private const string GetItemKey = "requirement_get_item";
        private const string AddItemKey = "requirement_add_item";
        private const string EditItemKey = "requirement_edit_item";
        private const string DeleteItemKey = "requirement_delete_item";
        private const string ExistsItemKey = "requirement_exists_item";

        private Requirement _getItem;
        private Requirement _addItem;
        private Requirement _editItem;
        private Requirement _deleteItem;

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
        private readonly int _requirementTypeId = 1;
        private readonly int _communityId = 1;
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add Requirement Post api endpoint to add a Requirement it checks if exists pulls item edits it and deletes it"
            )]
        public void
            WhenICallTheAddRequirementPostApiEndpointToAddARequirementItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(
            @"the add result should be a Requirement Id check exists get by id edit and delete with http response returns"
            )]
        public void ThenTheAddResultShouldBeARequirementIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<Requirement>(_getIdValue);
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
        [Given(@"the following Requirement Add input")]
        public void GivenTheFollowingRequirementAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _description = row["Description"];

                break;
            }
            Assert.IsNotNull(_description);

            _addItem = new Requirement
            {
                CommunityId = _communityId,
                RequirementTypeId = _requirementTypeId,
                Description = _description,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add Requirement Post api endpoint to add a Requirement")]
        public void WhenICallTheAddRequirementPostApiEndpointToAddARequirement()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Requirement Id")]
        public void ThenTheAddResultShouldBeARequirementId()
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
            _addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<Requirement, Requirement>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

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
        [When(@"I call the Requirement Get api endpoint")]
        public void WhenICallTheRequirementGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Requirement>>();
        }

        [Then(@"the get result should be a list of Requirements")]
        public void ThenTheGetResultShouldBeAListOfRequirements()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Requirement>);
        }

        //

        #endregion Get - get a list of items

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following Requirement Edit input")]
        public void GivenTheFollowingRequirementEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit Requirement Put api endpoint to edit a requirement")]
        public void WhenICallTheEditRequirementPutApiEndpointToEditARequirement()
        {
            //
        }

        [Then(@"the edit result should be an updated Requirement")]
        public void ThenTheEditResultShouldBeAnUpdatedRequirement()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [When(@"I call the delete Requirement Post api endpoint to delete a requirement")]
        public void WhenICallTheDeleteRequirementPostApiEndpointToDeleteARequirement()
        {
            //
        }

        [Then(@"the delete result should be an deleted Requirement")]
        public void ThenTheDeleteResultShouldBeAnDeletedRequirement()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following Requirement Id input")]
        public void GivenTheFollowingRequirementIdInput(Table table)
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

        [When(@"I call the Requirement Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheRequirementExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Requirement exists result should be bool true or false")]
        public void ThenTheRequirementExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Requirement>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}