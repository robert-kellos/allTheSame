using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AllTheSame.Common.Extensions;
using AllTheSame.Common.Logging;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class PersonSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/Person";
        //
        
        #region Local Properties/Fields

        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "person_get_list";
        private const string GetItemKey = "person_get_item";
        private const string AddItemKey = "person_add_item";
        private const string EditItemKey = "person_edit_item";
        private const string DeleteItemKey = "person_delete_item";
        private const string ExistsItemKey = "person_exists_item";

        private Person _getItem;
        private Person _addItem;
        private Person _editItem;
        private Person _deleteItem;

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

        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";
        private string _mobileNumber = "";
        //

        #endregion Local Properties/Fields

        #region CRUD Tests

        //

        [When(
            @"I call the add Person Post api endpoint to add a Person it checks if exists pulls item edits it and deletes it"
            )]
        public void WhenICallTheAddPersonPostApiEndpointToAddAPersonItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Person Id check exists get by id edit and delete with http response returns")
        ]
        public void ThenTheAddResultShouldBeAPersonIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<Person>(_getIdValue);
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
        [Given(@"the following Person Add input")]
        public void GivenTheFollowingPersonAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _firstName = row["FirstName"];
                _lastName = row["LastName"];
                _email = row["Email"];
                _mobileNumber = row["MobileNumber"];

                break;
            }
            Assert.IsNotNull(_firstName);
            Assert.IsNotNull(_email);
            Assert.IsNotNull(_email.IsValidEmailAddress());

            _addItem = new Person
            {
                FirstName = _firstName,
                LastName = _lastName,
                Email = _email,
                MobilePhone = _mobileNumber,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add Person Post api endpoint to add a Person")]
        public void WhenICallTheAddPersonPostApiEndpointToAddAPerson()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Person Id")]
        public void ThenTheAddResultShouldBeAPersonId()
        {
            _addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<Person, Person>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                //validate values changed
                Assert.AreEqual(_addItem.FirstName, result.FirstName);
                Assert.AreEqual(_addItem.LastName, result.LastName);
                Assert.AreEqual(_addItem.Email, result.Email);
                Assert.AreEqual(_addItem.MobilePhone, result.MobilePhone);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //

        #endregion Post - add a new item by a populated item

        #region Get - get a list of items

        //
        [When(@"I call the Person Get api endpoint")]
        public void WhenICallThePersonGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Person>>();
        }

        [Then(@"the get result should be a list of People")]
        public void ThenTheGetResultShouldBeAListOfPeople()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Person>);
        }

        //

        #endregion Get - get a list of items

        #region Get - get an item by Id

        //
        [Given(@"the following Person GetById input")]
        public void GivenTheFollowingPersonGetByIdInput(Table table)
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

        [When(@"I call the Person Get api endpoint by Id")]
        public void WhenICallThePersonGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<Person>(_getIdValue);
        }

        [Then(@"the get by id result should be a Person")]
        public void ThenTheGetByIdResultShouldBeAPerson()
        {
            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as Person);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }

        //

        #endregion Get - get an item by Id

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following Person Edit input")]
        public void GivenTheFollowingPersonEditInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _editId = row["Id"];
                _firstName = row["FirstName"];
                _lastName = row["LastName"];
                _email = row["Email"];
                _mobileNumber = row["MobileNumber"];

                break;
            }
            Assert.IsNotNull(_editId);
            _editIdValue = ConvertToIntValue(_editId);

            //var temp = GetResponseById<Person>(_editIdValue);

            Assert.IsTrue(_editIdValue > 0);
            Assert.IsNotNull(_firstName);
            Assert.IsNotNull(_email);
            Assert.IsNotNull(_email.IsValidEmailAddress());

            _editItem = new Person
            {
                Id = _editIdValue,
                FirstName = _firstName,
                LastName = _lastName,
                Email = _email,
                MobilePhone = _mobileNumber
            };
        }

        [When(@"I call the edit Person Put api endpoint to edit a Person")]
        public void WhenICallTheEditPersonPutApiEndpointToEditAPerson()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PutAsync(_editItem.Id, _editItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[EditItemKey] = response;
        }

        [Then(@"the edit result should be an updated Person")]
        public void ThenTheEditResultShouldBeAnUpdatedPerson()
        {
            //grab the resulting added item
            var response = (ScenarioContext.Current[EditItemKey] as HttpResponseMessage);
            var result = PutResponse<Person, Person>(_editItem.Id, _editItem);
            if (result != null)
            {
                Assert.IsTrue(_editIdValue > 0);
                Assert.AreEqual(_editIdValue, result.Id);

                //validate values changed
                Assert.AreEqual(_editItem.FirstName, result.FirstName);
                Assert.AreEqual(_editItem.LastName, result.LastName);
                Assert.AreEqual(_editItem.Email, result.Email);
                Assert.AreEqual(_editItem.MobilePhone, result.MobilePhone);
            }

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following Person Delete input")]
        public void GivenTheFollowingPersonDeleteInput(Table table)
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

            //var last = GetResponse<List<Person>>();
            //var l = last[last.Count - 1];
            //_deletedIdValue = l.Id;
        }

        [When(@"I call the delete Person Post api endpoint to delete a Person")]
        public void WhenICallTheDeletePersonPostApiEndpointToDeleteAPerson()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            DeleteAsync(_deletedIdValue).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[DeleteItemKey] = response;
        }

        [Then(@"the delete result should be an deleted Person")]
        public void ThenTheDeleteResultShouldBeAnDeletedPerson()
        {
            //grab the resulting added item
            var deleted = GetResponseById<Person>(_deletedIdValue);
            Assert.IsNull(deleted);

            var response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following Person Id input")]
        public void GivenTheFollowingPersonIdInput(Table table)
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

        [When(@"I call the Person Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallThePersonExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Person exists result should be bool true or false")]
        public void ThenThePersonExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Person>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}