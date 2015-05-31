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
    public class CommunitySteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "community_get_list";
        private const string GetItemKey = "community_get_item";
        private const string AddItemKey = "community_add_item";
        private const string EditItemKey = "community_edit_item";
        private const string DeleteItemKey = "community_delete_item";
        private const string ExistsItemKey = "community_exists_item";

        private Community _getItem;
        private Community _addItem;
        private Community _editItem;
        private Community _deleteItem;

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

        public override string Uri => "/api/Community";


        #region CRUD Tests
        //

        [When(@"I call the add Community Post api endpoint to add a Community it checks if exists pulls item edits it and deletes it")]
        public void WhenICallTheAddCommunityPostApiEndpointToAddACommunityItChecksIfExistsPullsItemEditsItAndDeletesIt()
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

        [Then(@"the add result should be a Community Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeACommunityIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
        {
            //is the item setup
            Assert.IsTrue(_addItem != null);

            //add the item
            var resultAdd = Add(_addItem);

            //did we get a good result
            Assert.IsTrue(resultAdd != null && resultAdd.Id > 0);

            //set te returned AddID to current Get
            _addedIdValue = resultAdd.Id;
            _getIdValue = _addedIdValue;
            _existsIdValue = _getIdValue;

            //check that the item exists
            var itemReturned = Exists(_existsIdValue);
            Assert.IsNotNull(itemReturned);

            //use the value used in exists check
            _getIdValue = itemReturned.Id;
            Assert.IsTrue(_getIdValue == _addedIdValue);

            //pull the item by Id
            var resultGet = GetById(_getIdValue);
            Assert.IsNotNull(resultGet);
            _getIdValue = resultGet.Id;
            Assert.IsTrue(_getIdValue == _addedIdValue);

            //Now, let's Edit the newly added item
            _editIdValue = _getIdValue;
            _editItem = resultGet;
            Assert.IsTrue(_editIdValue == _addedIdValue);

            //do an update
            Update(_editIdValue, _editItem);

            //pass the item just updated
            _deletedIdValue = _editIdValue;
            Assert.IsTrue(_deletedIdValue == _addedIdValue);

            //delete this same item
            Delete(_deletedIdValue);
        }

        private Community Add(Community item)
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PostAsync(item).ContinueWith(
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

            //grab the resulting added item
            var resultAdd = PostResponse<Community, Community>(item);
            if (resultAdd != null)
            {
                _addedIdValue = resultAdd.Id;
                Assert.IsTrue(_addedIdValue > 0);

                //Let's store the newly added Id in delete/edit, so we can later
                //edit and delete this same record
                _editIdValue = _addedIdValue;
                _deletedIdValue = _addedIdValue;

                ////validate values changed
                Assert.AreEqual(item.Name, resultAdd.Name);
            }

            response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);

            return resultAdd;
        }

        private Community Exists(int id)
        {
            //Check it exists
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(id);

            var resultExists = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var itemReturned = GetResponseById<Community>(id);

            var truth = (itemReturned != null && itemReturned.Id == id);
            Assert.AreEqual(truth, resultExists);

            return itemReturned;
        }

        private Community GetById(int id)
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<Community>(id);

            var resultGet = ScenarioContext.Current[GetItemKey];
            var itemGet = (resultGet as Community);

            Assert.IsNotNull(itemGet);
            Assert.IsTrue(itemGet.Id == id);

            return itemGet;
        }

        private void Update(int id, Community item)
        {
            var error = default(AggregateException);
            var response = default(HttpResponseMessage);

            PutAsync(item.Id, item).ContinueWith(
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
                        Audit.Log.Error("PUT Task Exception ::", error);
                    }
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[EditItemKey] = response;

            //grab the resulting added item
            response = (ScenarioContext.Current[EditItemKey] as HttpResponseMessage);
            var resultEdit = PutResponse<Community, Community>(item.Id, item);
            if (resultEdit != null)
            {
                Assert.IsTrue(id > 0);
                Assert.AreEqual(id, resultEdit.Id);

                //validate values changed
                Assert.AreEqual(item.Name, resultEdit.Name);
            }

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        private void Delete(int id)
        {
            var error = default(AggregateException);
            var response = default(HttpResponseMessage);

            //Now, let's Delete the newly added item
            DeleteAsync(id).ContinueWith(
                t =>
                {
                    if (t.IsCompleted)
                    {
                        if (t.Result != null)
                            response = (t.Result as HttpResponseMessage);
                    }

                    if (!t.IsFaulted) return;
                    error = t.Exception;
                    Audit.Log.Error("POST Task Exception ::", error);
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[DeleteItemKey] = response;

            //grab the resulting added item
            var deleted = GetResponseById<Community>(id);
            Assert.IsNull(deleted);

            response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
        //
        #endregion CRUD Tests

        #region Post - add a new item by a populated item
        //
        [Given(@"the following Community Add input")]
        public void GivenTheFollowingCommunityAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _name = row["Name"];
                
                break;
            }
            Assert.IsNotNull(_name);
            
            _addItem = new Community()
            {
                Name = _name,

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add Community Post api endpoint to add a community")]
        public void WhenICallTheAddCommunityPostApiEndpointToAddACommunity()
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

        [Then(@"the add result should be a Community Id")]
        public void ThenTheAddResultShouldBeACommunityId()
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
            var result = PostResponse<Community, Community>(_addItem);
            if (result != null)
            {

                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                ////validate values changed
                //Assert.AreEqual(_addItem.FirstName, result.FirstName);
                //Assert.AreEqual(_addItem.LastName, result.LastName);
                //Assert.AreEqual(_addItem.Email, result.Email);
                //Assert.AreEqual(_addItem.MobilePhone, result.MobilePhone);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //
        #endregion Post - add a new item by a populated item

        #region Get - get a list of items
        //
        [When(@"I call the Community Get api endpoint")]
        public void WhenICallTheCommunityGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Community>>();
        }

        [Then(@"the get result should be a list of communities")]
        public void ThenTheGetResultShouldBeAListOfCommunities()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Community>);
        }

        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following Community GetById input")]
        public void GivenTheFollowingCommunityGetByIdInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _getId = row["Id"];

                break;
            }
            Assert.IsNotNull(_getId);
            _getIdValue = ConvertToIntValue(_getId);
            Assert.IsTrue(_getIdValue > 0);  
        }

        [When(@"I call the Community Get api endpoint by Id")]
        public void WhenICallTheCommunityGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<Community>(_getIdValue);
        }

        [Then(@"the get by id result should be a Community")]
        public void ThenTheGetByIdResultShouldBeACommunity()
        {
            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as Community);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }
        //
        #endregion Post - add a new item by a populated item

        #region Put - edit an existing item by a populated item, and its Id
        //
        [Given(@"the following Community Edit input")]
        public void GivenTheFollowingCommunityEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit Community Put api endpoint to edit a community")]
        public void WhenICallTheEditCommunityPutApiEndpointToEditACommunity()
        {
            //
        }

        [Then(@"the edit result should be an updated Community")]
        public void ThenTheEditResultShouldBeAnUpdatedCommunity()
        {
            //
        }

        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following Community Delete input")]
        public void GivenTheFollowingCommunityDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete Community Post api endpoint to delete a community")]
        public void WhenICallTheDeleteCommunityPostApiEndpointToDeleteACommunity()
        {
            //
        }

        [Then(@"the delete result should be an deleted Community")]
        public void ThenTheDeleteResultShouldBeAnDeletedCommunity()
        {
            //
        }

        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //
        [Given(@"the following Community Id input")]
        public void GivenTheFollowingCommunityIdInput(Table table)
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

        [When(@"I call the Community Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheCommunityExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Community exists result should be bool true or false")]
        public void ThenTheCommunityExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Community>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //
        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //

        #region helpers
        //
        public int ConvertToIntValue(string value)
        {
            var result = -1;

            int.TryParse(value, out result);

            return result;
        }
        //
        #endregion helpers
    }
}