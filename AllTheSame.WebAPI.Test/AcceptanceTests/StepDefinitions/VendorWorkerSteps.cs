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
    public class VendorWorkerSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/VendorWorker";

        #region Get - get an item by Id

        //
        [Given(@"the following VendorWorker GetById input")]
        public void GivenTheFollowingVendorWorkerGetByIdInput(Table table)
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

        private const string GetListKey = "vendorWorker_get_list";
        private const string GetItemKey = "vendorWorker_get_item";
        private const string AddItemKey = "vendorWorker_add_item";
        private const string EditItemKey = "vendorWorker_edit_item";
        private const string DeleteItemKey = "vendorWorker_delete_item";
        private const string ExistsItemKey = "vendorWorker_exists_item";

        private VendorWorker _getItem;
        private VendorWorker _addItem;
        private VendorWorker _editItem;
        private VendorWorker _deleteItem;

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
        private int _vendorId = 1;
        private int _vendorTypeId = 1;
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add VendorWorker Post api endpoint to add a VendorWorker it checks if exists pulls item edits it and deletes it"
            )]
        public void
            WhenICallTheAddVendorWorkerPostApiEndpointToAddAVendorWorkerItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(
            @"the add result should be a VendorWorker Id check exists get by id edit and delete with http response returns"
            )]
        public void ThenTheAddResultShouldBeAVendorWorkerIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<VendorWorker>(_getIdValue);
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
        [Given(@"the following VendorWorker Add input")]
        public void GivenTheFollowingVendorWorkerAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _personId = Convert.ToInt32(row["PersonId"]);
                _vendorId = Convert.ToInt32(row["VendorId"]);
                _vendorTypeId = Convert.ToInt32(row["VendorTypeId"]);

                break;
            }

            _addItem = new VendorWorker
            {
                PersonId = _personId,
                VendorId = _vendorId,
                VendorTypeId = _vendorTypeId,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add VendorWorker Post api endpoint to add a VendorWorker")]
        public void WhenICallTheAddVendorWorkerPostApiEndpointToAddAVendorWorker()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a VendorWorker Id")]
        public void ThenTheAddResultShouldBeAVendorWorkerId()
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
            var result = PostResponse<VendorWorker, VendorWorker>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                ////validate values changed
                Assert.AreEqual(_addItem.PersonId, result.PersonId);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //

        #endregion Post - add a new item by a populated item

        #region Get - get a list of items

        //
        [When(@"I call the VendorWorker Get api endpoint")]
        public void WhenICallTheVendorWorkerGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<VendorWorker>>();
        }

        [Then(@"the get result should be a list of VendorWorkers")]
        public void ThenTheGetResultShouldBeAListOfVendorWorkers()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<VendorWorker>);
        }

        //

        #endregion Get - get a list of items

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following VendorWorker Edit input")]
        public void GivenTheFollowingVendorWorkerEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit VendorWorker Put api endpoint to edit a vendorWorker")]
        public void WhenICallTheEditVendorWorkerPutApiEndpointToEditAVendorWorker()
        {
            //
        }

        [Then(@"the edit result should be an updated VendorWorker")]
        public void ThenTheEditResultShouldBeAnUpdatedVendorWorker()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following VendorWorker Delete input")]
        public void GivenTheFollowingVendorWorkerDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete VendorWorker Post api endpoint to delete a vendorWorker")]
        public void WhenICallTheDeleteVendorWorkerPostApiEndpointToDeleteAVendorWorker()
        {
            //
        }

        [Then(@"the delete result should be an deleted VendorWorker")]
        public void ThenTheDeleteResultShouldBeAnDeletedVendorWorker()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following VendorWorker Id input")]
        public void GivenTheFollowingVendorWorkerIdInput(Table table)
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

        [When(@"I call the VendorWorker Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheVendorWorkerExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the VendorWorker exists result should be bool true or false")]
        public void ThenTheVendorWorkerExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<VendorWorker>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}