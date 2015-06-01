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
    public class FamilyMemberSteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "familyMember_get_list";
        private const string GetItemKey = "familyMember_get_item";
        private const string AddItemKey = "familyMember_add_item";
        private const string EditItemKey = "familyMember_edit_item";
        private const string DeleteItemKey = "familyMember_delete_item";
        private const string ExistsItemKey = "familyMember_exists_item";

        private FamilyMember _getItem;
        private FamilyMember _addItem;
        private FamilyMember _editItem;
        private FamilyMember _deleteItem;

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
        private int _residentId = 20;
        //
        #endregion Local Properties/Fields

        public override string Uri => "/api/FamilyMember";

        #region CRUD Tests
        //

        [When(@"I call the add FamilyMember Post api endpoint to add a FamilyMember it checks if exists pulls item edits it and deletes it")]
        public void WhenICallTheAddFamilyMemberPostApiEndpointToAddAFamilyMemberItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a FamilyMember Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeAFamilyMemberIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<FamilyMember>(_getIdValue);
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
        [Given(@"the following FamilyMember Add input")]
        public void GivenTheFollowingFamilyMemberAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _personId = Convert.ToInt32(row["PersonId"]);
                _residentId = Convert.ToInt32(row["ResidentId"]);

                break;
            }

            _addItem = new FamilyMember()
            {
                PersonId = _personId,
                ResidentId = _residentId,

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add FamilyMember Post api endpoint to add a FamilyMember")]
        public void WhenICallTheAddFamilyMemberPostApiEndpointToAddAFamilyMember()
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

        [Then(@"the add result should be a FamilyMember Id")]
        public void ThenTheAddResultShouldBeAFamilyMemberId()
        {
            _addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<FamilyMember, FamilyMember>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                //validate values changed
                //Assert.AreEqual(_addFamilyMember.FirstName, result.FirstName);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }
        //
        #endregion Post - add a new item by a populated item

        #region Get - get a list of items
        //
        [When(@"I call the FamilyMember Get api endpoint")]
        public void WhenICallTheFamilyMemberGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<FamilyMember>>();
        }

        [Then(@"the get result should be a list of FamilyMembers")]
        public void ThenTheGetResultShouldBeAListOfFamilyMembers()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<FamilyMember>);
        }
        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following FamilyMember GetById input")]
        public void GivenTheFollowingFamilyMemberGetByIdInput(Table table)
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

        [When(@"I call the FamilyMember Get api endpoint by Id")]
        public void WhenICallTheFamilyMemberGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<FamilyMember>(_getIdValue);
        }

        [Then(@"the get by id result should be a FamilyMember")]
        public void ThenTheGetByIdResultShouldBeAFamilyMember()
        {
            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as FamilyMember);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }
        //
        #endregion Get - get an item by Id

        #region Put - edit an existing item by a populated item, and its Id
        //
        [Given(@"the following FamilyMember Edit input")]
        public void GivenTheFollowingFamilyMemberEditInput(Table table)
        {
            Assert.IsNotNull(table);
            
            foreach (var row in table.Rows)
            {
                //_editId = row["Id"];
                //_firstName = row["FirstName"];
                //_lastName = row["LastName"];
                //_email = row["Email"];
                //_mobileNumber = row["MobileNumber"];

                break;
            }
            Assert.IsNotNull(_editId);
            _editIdValue = ConvertToIntValue(_editId);

            //var temp = GetResponseById<FamilyMember>(_editIdValue);

            //Assert.IsTrue(_editIdValue > 0);
            //Assert.IsNotNull(_firstName);
            //Assert.IsNotNull(_email);
            //Assert.IsNotNull(_email.IsValidEmailAddress());

            _editItem = new FamilyMember()
            {
                Id = _editIdValue,
                
            };
            
        }

        [When(@"I call the edit FamilyMember Put api endpoint to edit a FamilyMember")]
        public void WhenICallTheEditFamilyMemberPutApiEndpointToEditAFamilyMember()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PutAsync(_editItem.Id, _editItem).ContinueWith(
                t =>
                {
                    response = ActionResponse(t, out error);
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[EditItemKey] = response;
        }

        [Then(@"the edit result should be an updated FamilyMember")]
        public void ThenTheEditResultShouldBeAnUpdatedFamilyMember()
        {
            //grab the resulting added item
            var response = (ScenarioContext.Current[EditItemKey] as HttpResponseMessage);
            var result = PutResponse<FamilyMember, FamilyMember>(_editItem.Id, _editItem);
            if (result != null)
            {
                Assert.IsTrue(_editIdValue > 0);
                Assert.AreEqual(_editIdValue, result.Id);

                //validate values changed
                //Assert.AreEqual(_editItem.FirstName, result.FirstName);
                //Assert.AreEqual(_editItem.LastName, result.LastName);
                //Assert.AreEqual(_editItem.Email, result.Email);
                //Assert.AreEqual(_editItem.MobilePhone, result.MobilePhone);
            }
            
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following FamilyMember Delete input")]
        public void GivenTheFollowingFamilyMemberDeleteInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _deletedId = row["Id"]; //this is just a place holder, using Id from added item

                break;
            }
            Assert.IsNotNull(_deletedId);
            _deletedIdValue = ConvertToIntValue(_deletedId);

            Assert.IsTrue(_deletedIdValue > -1);

            var last = GetResponse<List<FamilyMember>>();
            var l = last[last.Count - 1];
            _deletedIdValue = l.Id;
        }

        [When(@"I call the delete FamilyMember Post api endpoint to delete a familyMember")]
        public void WhenICallTheDeleteFamilyMemberPostApiEndpointToDeleteAFamilyMember()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            DeleteAsync(_deletedIdValue).ContinueWith(
                t =>
                {
                    response = ActionResponse(t, out error);
                }
            ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[DeleteItemKey] = response;
        }

        [Then(@"the delete result should be an deleted FamilyMember")]
        public void ThenTheDeleteResultShouldBeAnDeletedFamilyMember()
        {
            //grab the resulting added item
            var deleted = GetResponseById<FamilyMember>(_deletedIdValue);
            Assert.IsNull(deleted);

            var response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);         
        }
        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //
        [Given(@"the following FamilyMember Id input")]
        public void GivenTheFollowingFamilyMemberIdInput(Table table)
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

        [When(@"I call the FamilyMember Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheFamilyMemberExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the FamilyMember exists result should be bool true or false")]
        public void ThenTheFamilyMemberExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<FamilyMember>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //
        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
    }
}