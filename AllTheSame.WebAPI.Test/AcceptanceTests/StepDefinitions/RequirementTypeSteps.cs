﻿using System;
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
    public class RequirementTypeSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/RequirementType";

        #region Get - get an item by Id

        //
        [Given(@"the following RequirementType GetById input")]
        public void GivenTheFollowingRequirementTypeGetByIdInput(Table table)
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

        private const string GetListKey = "requirementType_get_list";
        private const string GetItemKey = "requirementType_get_item";
        private const string AddItemKey = "requirementType_add_item";
        private const string EditItemKey = "requirementType_edit_item";
        private const string DeleteItemKey = "requirementType_delete_item";
        private const string ExistsItemKey = "requirementType_exists_item";

        private RequirementType _getItem;
        private RequirementType _addItem;
        private RequirementType _editItem;
        private RequirementType _deleteItem;

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
            @"I call the add RequirementType Post api endpoint to add a RequirementType it checks if exists pulls item edits it and deletes it"
            )]
        public void
            WhenICallTheAddRequirementTypePostApiEndpointToAddARequirementTypeItChecksIfExistsPullsItemEditsItAndDeletesIt
            ()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(
            @"the add result should be a RequirementType Id check exists get by id edit and delete with http response returns"
            )]
        public void ThenTheAddResultShouldBeARequirementTypeIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<RequirementType>(_getIdValue);
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
        [Given(@"the following RequirementType Add input")]
        public void GivenTheFollowingRequirementTypeAddInput(Table table)
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

            _addItem = new RequirementType
            {
                Code = _code,
                Label = _label,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add RequirementType Post api endpoint to add a RequirementType")]
        public void WhenICallTheAddRequirementTypePostApiEndpointToAddARequirementType()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a RequirementType Id")]
        public void ThenTheAddResultShouldBeARequirementTypeId()
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
            var result = PostResponse<RequirementType, RequirementType>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                ////validate values changed
                Assert.AreEqual(_addItem.Code, result.Code);
                Assert.AreEqual(_addItem.Label, result.Label);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //

        #endregion Post - add a new item by a populated item

        #region Get - get a list of items

        //
        [When(@"I call the RequirementType Get api endpoint")]
        public void WhenICallTheRequirementTypeGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<RequirementType>>();
        }

        [Then(@"the get result should be a list of RequirementTypes")]
        public void ThenTheGetResultShouldBeAListOfRequirementTypes()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<RequirementType>);
        }

        //

        #endregion Get - get a list of items

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following RequirementType Edit input")]
        public void GivenTheFollowingRequirementTypeEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit RequirementType Put api endpoint to edit a requirement")]
        public void WhenICallTheEditRequirementTypePutApiEndpointToEditARequirementType()
        {
            //
        }

        [Then(@"the edit result should be an updated RequirementType")]
        public void ThenTheEditResultShouldBeAnUpdatedRequirementType()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [When(@"I call the delete RequirementType Post api endpoint to delete a requirement")]
        public void WhenICallTheDeleteRequirementTypePostApiEndpointToDeleteARequirementType()
        {
            //
        }

        [Then(@"the delete result should be an deleted RequirementType")]
        public void ThenTheDeleteResultShouldBeAnDeletedRequirementType()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following RequirementType Id input")]
        public void GivenTheFollowingRequirementTypeIdInput(Table table)
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

        [When(@"I call the RequirementType Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheRequirementTypeExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the RequirementType exists result should be bool true or false")]
        public void ThenTheRequirementTypeExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<RequirementType>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}