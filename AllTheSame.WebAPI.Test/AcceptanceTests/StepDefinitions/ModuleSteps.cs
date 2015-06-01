using System;
using System.Collections.Generic;
using System.Net.Http;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class ModuleSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/Module";

        #region Get - get an item by Id

        //
        [Given(@"the following Module GetById input")]
        public void GivenTheFollowingModuleGetByIdInput(Table table)
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

        private const string GetListKey = "module_get_list";
        private const string GetItemKey = "module_get_item";
        private const string AddItemKey = "module_add_item";
        private const string EditItemKey = "module_edit_item";
        private const string DeleteItemKey = "module_delete_item";
        private const string ExistsItemKey = "module_exists_item";

        private Module _getItem;
        private Module _addItem;
        private Module _editItem;
        private Module _deleteItem;

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

        private string _name = "";
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add Module Post api endpoint to add a Module it checks if exists pulls item edits it and deletes it"
            )]
        public void WhenICallTheAddModulePostApiEndpointToAddAModuleItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Module Id check exists get by id edit and delete with http response returns")
        ]
        public void ThenTheAddResultShouldBeAModuleIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<Module>(_getIdValue);
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
        [Given(@"the following Module Add input")]
        public void GivenTheFollowingModuleAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _name = row["Name"];

                break;
            }
            Assert.IsNotNull(_name);

            _addItem = new Module
            {
                Name = _name,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add Module Post api endpoint to add a Module")]
        public void WhenICallTheAddModulePostApiEndpointToAddAModule()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Module Id")]
        public void ThenTheAddResultShouldBeAModuleId()
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
        [When(@"I call the Module Get api endpoint")]
        public void WhenICallTheModuleGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Module>>();
        }

        [Then(@"the get result should be a list of Modules")]
        public void ThenTheGetResultShouldBeAListOfModules()
        {
            //
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Module>);
        }

        //

        #endregion Get - get a list of items

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following Module Edit input")]
        public void GivenTheFollowingModuleEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit Module Put api endpoint to edit a Module")]
        public void WhenICallTheEditModulePutApiEndpointToEditAModule()
        {
            //
        }

        [Then(@"the edit result should be an updated Module")]
        public void ThenTheEditResultShouldBeAnUpdatedModule()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [When(@"I call the delete Module Post api endpoint to delete a Module")]
        public void WhenICallTheDeleteModulePostApiEndpointToDeleteAModule()
        {
            //
        }

        [Then(@"the delete result should be an deleted Module")]
        public void ThenTheDeleteResultShouldBeAnDeletedModule()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following Module Id input")]
        public void GivenTheFollowingModuleIdInput(Table table)
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

        [When(@"I call the Module Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheModuleExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Module exists result should be bool true or false")]
        public void ThenTheModuleExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Module>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}