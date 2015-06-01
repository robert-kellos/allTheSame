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
    public class CommunityTypeSteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "communityType_get_list";
        private const string GetItemKey = "communityType_get_item";
        private const string AddItemKey = "communityType_add_item";
        private const string EditItemKey = "communityType_edit_item";
        private const string DeleteItemKey = "communityType_delete_item";
        private const string ExistsItemKey = "communityType_exists_item";

        private CommunityType _getItem;
        private CommunityType _addItem;
        private CommunityType _editItem;
        private CommunityType _deleteItem;

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
	    [Code] [varchar](50) NOT NULL,
	    [Label] [nvarchar](100) NULL,
	    [CreatedOn] [datetime] NULL DEFAULT (getutcdate()),
	    [UpdatedOn] [datetime] NULL,
        */
        private string _code = "";
        private string _label = "";
        //
        #endregion Local Properties/Fields

        public override string Uri => "/api/CommunityType";


        #region CRUD Tests
        //

        [When(@"I call the add CommunityType Post api endpoint to add a CommunityType it checks if exists pulls item edits it and deletes it")]
        public void WhenICallTheAddCommunityTypePostApiEndpointToAddACommunityTypeItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a CommunityType Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeACommunityTypeIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<CommunityType>(_getIdValue);
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
        [Given(@"the following CommunityType Add input")]
        public void GivenTheFollowingCommunityTypeAddInput(Table table)
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

            _addItem = new CommunityType()
            {
                Code = _code,
                Label = _label,

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add CommunityType Post api endpoint to add a CommunityType")]
        public void WhenICallTheAddCommunityTypePostApiEndpointToAddACommunityType()
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

        [Then(@"the add result should be a CommunityType Id")]
        public void ThenTheAddResultShouldBeACommunityTypeId()
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
        [When(@"I call the CommunityType Get api endpoint")]
        public void WhenICallTheCommunityTypeGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<CommunityType>>();
        }

        [Then(@"the get result should be a list of CommunityTypes")]
        public void ThenTheGetResultShouldBeAListOfCommunityTypes()
        {
         
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<CommunityType>);
        }

        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following CommunityType GetById input")]
        public void GivenTheFollowingCommunityTypeGetByIdInput(Table table)
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

        [Given(@"the following CommunityType Edit input")]
        public void GivenTheFollowingCommunityTypeEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit CommunityType Put api endpoint to edit a CommunityType")]
        public void WhenICallTheEditCommunityTypePutApiEndpointToEditACommunityType()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the edit result should be an updated CommunityType")]
        public void ThenTheEditResultShouldBeAnUpdatedCommunityType()
        {
            //
        }

        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following CommunityType Delete input")]
        public void GivenTheFollowingCommunityTypeDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete CommunityType Post api endpoint to delete a communityTypes")]
        public void WhenICallTheDeleteCommunityTypePostApiEndpointToDeleteACommunityType()
        {
            //
        }

        [Then(@"the delete result should be an deleted CommunityType")]
        public void ThenTheDeleteResultShouldBeAnDeletedCommunityType()
        {
            //
        }

        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //

        [Given(@"the following CommunityType Id input")]
        public void GivenTheFollowingCommunityTypeIdInput(Table table)
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

        [When(@"I call the CommunityType Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheCommunityTypeExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the CommunityType exists result should be bool true or false")]
        public void ThenTheCommunityTypeExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<CommunityType>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //
        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
    }
}