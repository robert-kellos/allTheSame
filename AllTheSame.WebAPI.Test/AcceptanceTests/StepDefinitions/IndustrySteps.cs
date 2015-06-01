using System;
using System.Collections.Generic;
using System.Net.Http;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class IndustrySteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/Industry";

        #region Get - get an item by Id

        //
        [Given(@"the following Industry GetById input")]
        public void GivenTheFollowingIndustryGetByIdInput(Table table)
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

        private const string GetListKey = "industry_get_list";
        private const string GetItemKey = "industry_get_item";
        private const string AddItemKey = "industry_add_item";
        private const string EditItemKey = "industry_edit_item";
        private const string DeleteItemKey = "industry_delete_item";
        private const string ExistsItemKey = "industry_exists_item";

        private Industry _getItem;
        private Industry _addItem;
        private Industry _editItem;
        private Industry _deleteItem;

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

        #region CRUD Tests

        //

        [When(
            @"I call the add Industry Post api endpoint to add a Industry it checks if exists pulls item edits it and deletes it"
            )]
        public void WhenICallTheAddIndustryPostApiEndpointToAddAIndustryItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(
            @"the add result should be a Industry Id check exists get by id edit and delete with http response returns")
        ]
        public void ThenTheAddResultShouldBeAIndustryIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<Industry>(_getIdValue);
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
        [Given(@"the following Industry Add input")]
        public void GivenTheFollowingIndustryAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _code = row["Code"];
                _label = row["Label"];

                break;
            }
            Assert.IsNotNull(_code);
            Assert.IsNotNull(_label);

            _addItem = new Industry
            {
                Code = _code,
                Label = _label,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add Industry Post api endpoint to add a Industry")]
        public void WhenICallTheAddIndustryPostApiEndpointToAddAIndustry()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Industry Id")]
        public void ThenTheAddResultShouldBeAIndustryId()
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
        [When(@"I call the Industry Get api endpoint")]
        public void WhenICallTheIndustryGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Industry>>();
        }

        [Then(@"the get result should be a list of Industries")]
        public void ThenTheGetResultShouldBeAListOfIndustries()
        {
            //
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Industry>);
        }

        //

        #endregion Get - get a list of items

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following Industry Edit input")]
        public void GivenTheFollowingIndustryEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit Industry Put api endpoint to edit a Industry")]
        public void WhenICallTheEditIndustryPutApiEndpointToEditAIndustry()
        {
            //
        }

        [Then(@"the edit result should be an updated Industry")]
        public void ThenTheEditResultShouldBeAnUpdatedIndustry()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following Industry Delete input")]
        public void GivenTheFollowingIndustryDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete Industry Post api endpoint to delete a Industry")]
        public void WhenICallTheDeleteIndustryPostApiEndpointToDeleteAIndustry()
        {
            //
        }

        [Then(@"the delete result should be an deleted Industry")]
        public void ThenTheDeleteResultShouldBeAnDeletedIndustry()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following Industry Id input")]
        public void GivenTheFollowingIndustryIdInput(Table table)
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

        [When(@"I call the Industry Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheIndustryExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Industry exists result should be bool true or false")]
        public void ThenTheIndustryExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Industry>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}