using System.Collections.Generic;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using System.Net.Http;
using System;
using AllTheSame.Common.Logging;
using System.Net;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class VendorCredentialSteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "VendorCredential_get_list";
        private const string GetItemKey = "VendorCredential_get_item";
        private const string AddItemKey = "VendorCredential_add_item";
        private const string EditItemKey = "VendorCredential_edit_item";
        private const string DeleteItemKey = "VendorCredential_delete_item";
        private const string ExistsItemKey = "VendorCredential_exists_item";

        private VendorCredential _getItem;
        private VendorCredential _addItem;
        private VendorCredential _editItem;
        private VendorCredential _deleteItem;

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

        private int _vendorWorkerId = 13;
        private int _requirementId = 1;
        private bool _isAttested = true;
        private bool _isConfirmed = true;
        private DateTime _confirmedOn = DateTime.UtcNow;
        //private int _userId = 1;
        //
        #endregion Local Properties/Fields

        public override string Uri => "/api/VendorCredential";

        #region CRUD Tests
        //

        [When(@"I call the add VendorCredential Post api endpoint to add a VendorCredential it checks if exists pulls item edits it and deletes it")]
        public void WhenICallTheAddVendorCredentialPostApiEndpointToAddAVendorCredentialItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a VendorCredential Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeAVendorCredentialIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<VendorCredential>(_getIdValue);
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
        [Given(@"the following VendorCredential Add input")]
        public void GivenTheFollowingVendorCredentialAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _isAttested = Convert.ToBoolean(row["IsAttested"]);
                _isConfirmed = Convert.ToBoolean(row["IsConfirmed"]);

                break;
            }

            _addItem = new VendorCredential()
            {
                RequirementId = _requirementId,
                VendorWorkerId = _vendorWorkerId,
                IsAttested = _isAttested,
                IsConfirmed = _isConfirmed,
                ConfirmedOn = _confirmedOn,

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add VendorCredential Post api endpoint to add a VendorCredential")]
        public void WhenICallTheAddVendorCredentialPostApiEndpointToAddAVendorCredential()
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PostAsync(_addItem).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("POST Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a VendorCredential Id")]
        public void ThenTheAddResultShouldBeAVendorCredentialId()
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PostAsync(_addItem).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("POST Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Item Id")]
        public void ThenTheAddResultShouldBeAItemId()
        {
            _addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<VendorCredential, VendorCredential>(_addItem);
            if (result != null)
            {

                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                ////validate values changed
                //Assert.AreEqual(_addItem.RequirementId, result.RequirementId);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //
        #endregion Post - add a new item by a populated item

        #region Get - get a list of items
        //
        [When(@"I call the VendorCredential Get api endpoint")]
        public void WhenICallTheVendorCredentialGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<VendorCredential>>();
        }

        [Then(@"the get result should be a list of VendorCredentials")]
        public void ThenTheGetResultShouldBeAListOfVendorCredentials()
        {
            //
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<VendorCredential>);
        }

        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following VendorCredential GetById input")]
        public void GivenTheFollowingVendorCredentialGetByIdInput(Table table)
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PostAsync(_addItem).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (t.IsFaulted)
                    {
                        error = t.Exception;
                        Audit.Log.Error("POST Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        //
        #endregion Post - add a new item by a populated item

        #region Put - edit an existing item by a populated item, and its Id
        //
        [Given(@"the following VendorCredential Edit input")]
        public void GivenTheFollowingVendorCredentialEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit VendorCredential Put api endpoint to edit a VendorCredential")]
        public void WhenICallTheEditVendorCredentialPutApiEndpointToEditAVendorCredential()
        {
            //
        }

        [Then(@"the edit result should be an updated VendorCredential")]
        public void ThenTheEditResultShouldBeAnUpdatedVendorCredential()
        {
            //
        }

        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following VendorCredential Delete input")]
        public void GivenTheFollowingVendorCredentialDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete VendorCredential Post api endpoint to delete a VendorCredential")]
        public void WhenICallTheDeleteVendorCredentialPostApiEndpointToDeleteAVendorCredential()
        {
            //
        }

        [Then(@"the delete result should be an deleted VendorCredential")]
        public void ThenTheDeleteResultShouldBeAnDeletedVendorCredential()
        {
            //
        }

        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //
        [Given(@"the following VendorCredential Id input")]
        public void GivenTheFollowingVendorCredentialIdInput(Table table)
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

        [When(@"I call the VendorCredential Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheVendorCredentialExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the VendorCredential exists result should be bool true or false")]
        public void ThenTheVendorCredentialExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<VendorCredential>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //
        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //

    }
}