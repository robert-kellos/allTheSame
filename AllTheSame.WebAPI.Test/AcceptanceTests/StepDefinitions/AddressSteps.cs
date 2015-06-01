using System.Collections.Generic;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using System;
using System.Net.Http;
using AllTheSame.Common.Logging;
using System.Net;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class AddressSteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "address_get_list";
        private const string GetItemKey = "address_get_item";
        private const string AddItemKey = "address_add_item";
        private const string EditItemKey = "address_edit_item";
        private const string DeleteItemKey = "address_delete_item";
        private const string ExistsItemKey = "address_exists_item";

        private Address _getItem;
        private Address _addItem;
        private Address _editItem;
        private Address _deleteItem;

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

        private string _line1 = "";
        private string _line2 = "";
        private string _city = "";
        private string _state = "";
        private string _country = "";
        private string _postalCode = "";
        //
        #endregion Local Properties/Fields

        public override string Uri => "/api/Address";

        #region CRUD Tests
        //

        [When(@"I call the add Address Post api endpoint to add a Address it checks if exists pulls item edits it and deletes it")]
        public void WhenICallTheAddAddressPostApiEndpointToAddAAddressItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Address Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeAAddressIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<Address>(_getIdValue);
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
        [Given(@"the following Address Add input")]
        public void GivenTheFollowingAddressAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _line1 = row["Line1"];
                _line2 = row["Line2"];
                _city = row["City"];
                _state = row["State"];
                _country = row["Country"];
                _postalCode = row["PostalCode"];

                break;
            }
            Assert.IsNotNull(_line1);
            Assert.IsNotNull(_city);
            Assert.IsNotNull(_state);
            Assert.IsNotNull(_postalCode);
            //Assert.IsNotNull(_city.IsValidEmailAddress());

            _addItem = new Address()
            {
                Line1 = _line1,
                Line2 = _line2,
                City = _city,
                State = _state,
                Country = _country,
                PostalCode = _postalCode,

                CreatedOn = DateTime.UtcNow,
            };

        }

        [When(@"I call the add Address Post api endpoint to add a Address")]
        public void WhenICallTheAddAddressPostApiEndpointToAddAAddress()
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

        [Then(@"the add result should be a Address Id")]
        public void ThenTheAddResultShouldBeAAddressId()
        {
            //_addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<Address, Address>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                //Let's store the newly added Id in delete/edit, so we can later
                //edit and delete this same record
                _editIdValue = _addedIdValue;
                _deletedIdValue = _addedIdValue;

                //validate values changed
                Assert.AreEqual(_addItem.Line1, result.Line1);
                Assert.AreEqual(_addItem.Line2, result.Line2);
                Assert.AreEqual(_addItem.City, result.City);
                Assert.AreEqual(_addItem.State, result.State);
                Assert.AreEqual(_addItem.Country, result.Country);
                Assert.AreEqual(_addItem.PostalCode, result.PostalCode);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //
        #endregion Post - add a new item by a populated item

        #region Get - get a list of items
        //
        [When(@"I call the Address Get api endpoint")]
        public void WhenICallTheAddressGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Address>>();
        }

        [Then(@"the get result should be a list of Addresses")]
        public void ThenTheGetResultShouldBeAListOfAddresses()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Address>);
        }

        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following Address GetById input")]
        public void GivenTheFollowingAddressGetByIdInput(Table table)
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

        [When(@"I call the Address Get api endpoint by Id")]
        public void WhenICallTheAddressGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<Address>(_getIdValue);
        }

        [Then(@"the get by id result should be a Address")]
        public void ThenTheGetByIdResultShouldBeAAddress()
        {
            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as Address);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }

        //
        #endregion Get - get an item by Id

        #region Put - edit an existing item by a populated item, and its Id
        //
        [Given(@"the following Address Edit input")]
        public void GivenTheFollowingAddressEditInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _editId = _addedIdValue > 0 ? _addedIdValue.ToString() : row["Id"];

                _line1 = row["Line1"];
                _line2 = row["Line2"];
                _city = row["City"];
                _state = row["State"];
                _country = row["Country"];
                _postalCode = row["PostalCode"];

                break;
            }
            Assert.IsNotNull(_editId);
            _editIdValue = ConvertToIntValue(_editId);

            //var temp = GetResponseById<Item>(_editIdValue);

            Assert.IsTrue(_editIdValue > 0);
            Assert.IsNotNull(_line1);
            Assert.IsNotNull(_city);
            Assert.IsNotNull(_state);
            Assert.IsNotNull(_postalCode);
            //Assert.IsNotNull(_email.IsValidEmailAddress());

            _editItem = new Address()
            {
                Id = _editIdValue,

                Line1 = _line1,
                Line2 = _line2,
                City = _city,
                State = _state,
                Country = _country,
                PostalCode = _postalCode,

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the edit Address Put api endpoint to edit a address")]
        public void WhenICallTheEditAddressPutApiEndpointToEditAAddress()
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

        [Then(@"the edit result should be an updated Address")]
        public void ThenTheEditResultShouldBeAnUpdatedAddress()
        {
            //grab the resulting added item
            var response = (ScenarioContext.Current[EditItemKey] as HttpResponseMessage);
            var result = PutResponse<Address, Address>(_editItem.Id, _editItem);
            if (result != null)
            {
                Assert.IsTrue(_editIdValue > 0);
                Assert.AreEqual(_editIdValue, result.Id);

                ////validate values changed
                Assert.IsNotNull(_line1);
                Assert.IsNotNull(_city);
                Assert.IsNotNull(_state);
                Assert.IsNotNull(_postalCode);
            }

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following Address Delete input")]
        public void GivenTheFollowingAddressDeleteInput(Table table)
        {
            Assert.IsNotNull(table);

            foreach (var row in table.Rows)
            {
                _deletedId = _addedIdValue > 0 ? _addedIdValue.ToString() : row["Id"]; //this is just a place holder, using Id from added item

                break;
            }
            Assert.IsNotNull(_deletedId);
            _deletedIdValue = ConvertToIntValue(_deletedId);

            Assert.IsTrue(_deletedIdValue > -1);

            //var last = GetResponse<List<Address>>();
            //var l = last[last.Count - 1];
            //_deletedIdValue = l.Id;
        }

        [When(@"I call the delete Address Post api endpoint to delete a address")]
        public void WhenICallTheDeleteAddressPostApiEndpointToDeleteAAddress()
        {
            _addItem = new Address()
            {
                Line1 = "test",
                Line2 = "test",
                City = "test",
                State = "test",
                Country = "test",
                PostalCode = "11111",

                CreatedOn = DateTime.UtcNow,
            };
            WhenICallTheAddAddressPostApiEndpointToAddAAddress();
            var result = PostResponse<Address, Address>(_addItem);
            _deletedIdValue = result.Id;

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


        [Then(@"the delete result should be an deleted Address")]
        public void ThenTheDeleteResultShouldBeAnDeletedAddress()
        {
            //grab the resulting added item
            var deleted = GetResponseById<Address>(_deletedIdValue);
            Assert.IsNull(deleted);

            var response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);         
        }

        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //
        [Given(@"the following Address Id input")]
        public void GivenTheFollowingAddressIdInput(Table table)
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

        [When(@"I call the Address Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheAddressExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Address exists result should be bool true or false")]
        public void ThenTheAddressExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Address>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //
        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
    }
}