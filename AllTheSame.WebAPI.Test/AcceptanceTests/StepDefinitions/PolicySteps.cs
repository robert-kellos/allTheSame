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
    public class PolicySteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/Policy";

        #region Get - get an item by Id

        //
        [Given(@"the following Policy GetById input")]
        public void GivenTheFollowingPolicyGetByIdInput(Table table)
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

        private const string GetListKey = "policy_get_list";
        private const string GetItemKey = "policy_get_item";
        private const string AddItemKey = "policy_add_item";
        private const string EditItemKey = "policy_edit_item";
        private const string DeleteItemKey = "policy_delete_item";
        private const string ExistsItemKey = "policy_exists_item";

        private Policy _getItem;
        private Policy _addItem;
        private Policy _editItem;
        private Policy _deleteItem;

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

        private readonly int _communityId = 1;
        private string _description = "";
        private readonly string _documentUrl = "www.google.com";
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add Policy Post api endpoint to add a Policy it checks if exists pulls item edits it and deletes it"
            )]
        public void WhenICallTheAddPolicyPostApiEndpointToAddAPolicyItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Policy Id check exists get by id edit and delete with http response returns")
        ]
        public void ThenTheAddResultShouldBeAPolicyIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<Policy>(_getIdValue);
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
        [Given(@"the following Policy Add input")]
        public void GivenTheFollowingPolicyAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _description = row["Description"];

                break;
            }

            _addItem = new Policy
            {
                CommunityId = _communityId,
                Description = _description,
                DocumentURL = _documentUrl,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add Policy Post api endpoint to add a Policy")]
        public void WhenICallTheAddPolicyPostApiEndpointToAddAPolicy()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Policy Id")]
        public void ThenTheAddResultShouldBeAPolicyId()
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
            var result = PostResponse<Policy, Policy>(_addItem);
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
        [When(@"I call the Policy Get api endpoint")]
        public void WhenICallThePolicyGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Policy>>();
        }

        [Then(@"the get result should be a list of Policies")]
        public void ThenTheGetResultShouldBeAListOfPolicys()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Policy>);
        }

        //

        #endregion Get - get a list of items

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following Policy Edit input")]
        public void GivenTheFollowingPolicyEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit Policy Put api endpoint to edit a policy")]
        public void WhenICallTheEditPolicyPutApiEndpointToEditAPolicy()
        {
            //
        }

        [Then(@"the edit result should be an updated Policy")]
        public void ThenTheEditResultShouldBeAnUpdatedPolicy()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following Policy Delete input")]
        public void GivenTheFollowingPolicyDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete Policy Post api endpoint to delete a policy")]
        public void WhenICallTheDeletePolicyPostApiEndpointToDeleteAPolicy()
        {
            //
        }

        [Then(@"the delete result should be an deleted Policy")]
        public void ThenTheDeleteResultShouldBeAnDeletedPolicy()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following Policy Id input")]
        public void GivenTheFollowingPolicyIdInput(Table table)
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

        [When(@"I call the Policy Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallThePolicyExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Policy exists result should be bool true or false")]
        public void ThenThePolicyExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Policy>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}