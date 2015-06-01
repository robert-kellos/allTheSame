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
    public class VendorAdminSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/VendorAdmin";

        #region Get - get an item by Id

        //
        [Given(@"the following VendorAdmin GetById input")]
        public void GivenTheFollowingVendorAdminGetByIdInput(Table table)
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

        private const string GetListKey = "VendorAdmin_get_list";
        private const string GetItemKey = "VendorAdmin_get_item";
        private const string AddItemKey = "VendorAdmin_add_item";
        private const string EditItemKey = "VendorAdmin_edit_item";
        private const string DeleteItemKey = "VendorAdmin_delete_item";
        private const string ExistsItemKey = "VendorAdmin_exists_item";

        private VendorAdmin _getItem;
        private VendorAdmin _addItem;
        private VendorAdmin _editItem;
        private VendorAdmin _deleteItem;

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

        private int _personId = 1;
        private int _vendorid = 1;
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add VendorAdmin Post api endpoint to add a VendorAdmin it checks if exists pulls item edits it and deletes it"
            )]
        public void
            WhenICallTheAddVendorAdminPostApiEndpointToAddAVendorAdminItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(
            @"the add result should be a VendorAdmin Id check exists get by id edit and delete with http response returns"
            )]
        public void ThenTheAddResultShouldBeAVendorAdminIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<VendorAdmin>(_getIdValue);
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
        [Given(@"the following VendorAdmin Add input")]
        public void GivenTheFollowingVendorAdminAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _personId = Convert.ToInt32(row["PersonId"]);
                _vendorid = Convert.ToInt32(row["VendorId"]);

                break;
            }

            _addItem = new VendorAdmin
            {
                PersonId = _personId,
                VendorId = _vendorid,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add VendorAdmin Post api endpoint to add a VendorAdmin")]
        public void WhenICallTheAddVendorAdminPostApiEndpointToAddAVendorAdmin()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a VendorAdmin Id")]
        public void ThenTheAddResultShouldBeAVendorAdminId()
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
            var result = PostResponse<VendorAdmin, VendorAdmin>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                ////validate values changed
                Assert.AreEqual(_addItem.PersonId, result.PersonId);
                Assert.AreEqual(_addItem.VendorId, result.VendorId);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //

        #endregion Post - add a new item by a populated item

        #region Get - get a list of items

        //
        [When(@"I call the VendorAdmin Get api endpoint")]
        public void WhenICallTheVendorAdminGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<VendorAdmin>>();
        }

        [Then(@"the get result should be a list of VendorAdmins")]
        public void ThenTheGetResultShouldBeAListOfVendorAdmins()
        {
            //
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<VendorAdmin>);
        }

        //

        #endregion Get - get a list of items

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following VendorAdmin Edit input")]
        public void GivenTheFollowingVendorAdminEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit VendorAdmin Put api endpoint to edit a VendorAdmin")]
        public void WhenICallTheEditVendorAdminPutApiEndpointToEditAVendorAdmin()
        {
            //
        }

        [Then(@"the edit result should be an updated VendorAdmin")]
        public void ThenTheEditResultShouldBeAnUpdatedVendorAdmin()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following VendorAdmin Delete input")]
        public void GivenTheFollowingVendorAdminDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete VendorAdmin Post api endpoint to delete a VendorAdmin")]
        public void WhenICallTheDeleteVendorAdminPostApiEndpointToDeleteAVendorAdmin()
        {
            //
        }

        [Then(@"the delete result should be an deleted VendorAdmin")]
        public void ThenTheDeleteResultShouldBeAnDeletedVendorAdmin()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following VendorAdmin Id input")]
        public void GivenTheFollowingVendorAdminIdInput(Table table)
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

        [When(@"I call the VendorAdmin Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheVendorAdminExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the VendorAdmin exists result should be bool true or false")]
        public void ThenTheVendorAdminExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<VendorAdmin>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}