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
    public class VendorCredDocumentSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/VendorCredDocument";

        #region Get - get an item by Id

        //
        [Given(@"the following VendorCredDocument GetById input")]
        public void GivenTheFollowingVendorCredDocumentGetByIdInput(Table table)
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

        private const string GetListKey = "VendorCredDocument_get_list";
        private const string GetItemKey = "VendorCredDocument_get_item";
        private const string AddItemKey = "VendorCredDocument_add_item";
        private const string EditItemKey = "VendorCredDocument_edit_item";
        private const string DeleteItemKey = "VendorCredDocument_delete_item";
        private const string ExistsItemKey = "VendorCredDocument_exists_item";

        private VendorCredDocument _getItem;
        private VendorCredDocument _addItem;
        private VendorCredDocument _editItem;
        private VendorCredDocument _deleteItem;

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

        private readonly int _vendorCredId = 41;
        private string _title = "";
        private string _url = "";
        private string _text = "";
        private readonly string _docType = "txt";

        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add VendorCredDocument Post api endpoint to add a VendorCredDocument it checks if exists pulls item edits it and deletes it"
            )]
        public void
            WhenICallTheAddVendorCredDocumentPostApiEndpointToAddAVendorCredDocumentItChecksIfExistsPullsItemEditsItAndDeletesIt
            ()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(
            @"the add result should be a VendorCredDocument Id check exists get by id edit and delete with http response returns"
            )]
        public void ThenTheAddResultShouldBeAVendorCredDocumentIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<VendorCredDocument>(_getIdValue);
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
        [Given(@"the following VendorCredDocument Add input")]
        public void GivenTheFollowingVendorCredDocumentAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _title = row["Title"];
                _url = row["URL"];
                _text = row["Text"];

                break;
            }
            Assert.IsNotNull(_title);

            _addItem = new VendorCredDocument
            {
                VendorCredId = _vendorCredId,
                DocType = _docType,
                Title = _title,
                URL = _url,
                Text = _text,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add VendorCredDocument Post api endpoint to add a VendorCredDocument")]
        public void WhenICallTheAddVendorCredDocumentPostApiEndpointToAddAVendorCredDocument()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a VendorCredDocument Id")]
        public void ThenTheAddResultShouldBeAVendorCredDocumentId()
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
            var result = PostResponse<VendorCredDocument, VendorCredDocument>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                ////validate values changed
                Assert.AreEqual(_addItem.Title, result.Title);
                Assert.AreEqual(_addItem.Text, result.Text);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //

        #endregion Post - add a new item by a populated item

        #region Get - get a list of items

        //
        [When(@"I call the VendorCredDocument Get api endpoint")]
        public void WhenICallTheVendorCredDocumentGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<VendorCredDocument>>();
        }

        [Then(@"the get result should be a list of VendorCredDocuments")]
        public void ThenTheGetResultShouldBeAListOfVendorCredDocuments()
        {
            //
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<VendorCredDocument>);
        }

        //

        #endregion Get - get a list of items

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following VendorCredDocument Edit input")]
        public void GivenTheFollowingVendorCredDocumentEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit VendorCredDocument Put api endpoint to edit a VendorCredDocuments")]
        public void WhenICallTheEditVendorCredDocumentPutApiEndpointToEditAVendorCredDocument()
        {
            //
        }

        [Then(@"the edit result should be an updated VendorCredDocument")]
        public void ThenTheEditResultShouldBeAnUpdatedVendorCredDocument()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following VendorCredDocument Delete input")]
        public void GivenTheFollowingVendorCredDocumentDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete VendorCredDocument Post api endpoint to delete a VendorCredDocuments")]
        public void WhenICallTheDeleteVendorCredDocumentPostApiEndpointToDeleteAVendorCredDocument()
        {
            //
        }

        [Then(@"the delete result should be an deleted VendorCredDocument")]
        public void ThenTheDeleteResultShouldBeAnDeletedVendorCredDocument()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following VendorCredDocument Id input")]
        public void GivenTheFollowingVendorCredDocumentIdInput(Table table)
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

        [When(@"I call the VendorCredDocument Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheVendorCredDocumentExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the VendorCredDocument exists result should be bool true or false")]
        public void ThenTheVendorCredDocumentExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<VendorCredDocument>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}