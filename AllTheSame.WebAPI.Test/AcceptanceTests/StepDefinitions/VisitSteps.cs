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
    public class VisitSteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "Visit_get_list";
        private const string GetItemKey = "Visit_get_item";
        private const string AddItemKey = "Visit_add_item";
        private const string EditItemKey = "Visit_edit_item";
        private const string DeleteItemKey = "Visit_delete_item";
        private const string ExistsItemKey = "Visit_exists_item";

        private Visit _getItem;
        private Visit _addItem;
        private Visit _editItem;
        private Visit _deleteItem;

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

  //      | ResidentId | VendorWorkerId | VisitorId | TimeIn                  | TimeOut                 |
		//| 20         | 13             | 1         | 2015-06-01 12:00:00.000 | 2015-06-01 01:00:00.000 |
        private int _residentId = 20;
        private int _vendorWorkerId = 1;
        private DateTime _timeIn = DateTime.UtcNow;
        private DateTime _timeOut = DateTime.UtcNow.AddHours(4);
        //
        #endregion Local Properties/Fields

        public override string Uri => "/api/Visit";

        #region CRUD Tests
        //

        [When(@"I call the add Visit Post api endpoint to add a Visit it checks if exists pulls item edits it and deletes it")]
        public void WhenICallTheAddVisitPostApiEndpointToAddAVisitItChecksIfExistsPullsItemEditsItAndDeletesIt()
        {
            HttpResponseMessage response;

            _addItem = Add(_addItem, out response);

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Visit Id check exists get by id edit and delete with http response returns")]
        public void ThenTheAddResultShouldBeAVisitIdCheckExistsGetByIdEditAndDeleteWithHttpResponseReturns()
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
            var resultGet = GetById<Visit>(_getIdValue);
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
        [Given(@"the following Visit Add input")]
        public void GivenTheFollowingVisitAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _residentId = Convert.ToInt32(row["ResidentId"]);
                _vendorWorkerId = Convert.ToInt32(row["VendorWorkerId"]);
                
                break;
            }

            _addItem = new Visit()
            {
                ResidentId = _residentId,
                VendorWorkerId = _vendorWorkerId,
                TimeIn = _timeIn,
                TimeOut = _timeOut,
                
                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add Visit Post api endpoint to add a Visit")]
        public void WhenICallTheAddVisitPostApiEndpointToAddAVisit()
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

        [Then(@"the add result should be a Visit Id")]
        public void ThenTheAddResultShouldBeAVisitId()
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
            var result = PostResponse<Visit, Visit>(_addItem);
            if (result != null)
            {

                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                ////validate values changed
                Assert.AreEqual(_addItem.ResidentId, result.ResidentId);
                Assert.AreEqual(_addItem.VendorWorkerId, result.VendorWorkerId);
                Assert.AreEqual(_addItem.TimeIn, result.TimeIn);
                Assert.AreEqual(_addItem.TimeOut, result.TimeOut);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //
        #endregion Post - add a new item by a populated item

        #region Get - get a list of items
        //
        [When(@"I call the Visit Get api endpoint")]
        public void WhenICallTheVisitGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Visit>>();
        }

        [Then(@"the get result should be a list of Visits")]
        public void ThenTheGetResultShouldBeAListOfVisits()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Visit>);
        }

        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following Visit GetById input")]
        public void GivenTheFollowingVisitGetByIdInput(Table table)
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
        [Given(@"the following Visit Edit input")]
        public void GivenTheFollowingVisitEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit Visit Put api endpoint to edit a Visit")]
        public void WhenICallTheEditVisitPutApiEndpointToEditAVisit()
        {
            //
        }

        [Then(@"the edit result should be an updated Visit")]
        public void ThenTheEditResultShouldBeAnUpdatedVisit()
        {
            //
        }

        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following Visit Delete input")]
        public void GivenTheFollowingVisitDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete Visit Post api endpoint to delete a Visit")]
        public void WhenICallTheDeleteVisitPostApiEndpointToDeleteAVisit()
        {
            //
        }

        [Then(@"the delete result should be an deleted Visit")]
        public void ThenTheDeleteResultShouldBeAnDeletedVisit()
        {
            //
        }

        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //
        [Given(@"the following Visit Id input")]
        public void GivenTheFollowingVisitIdInput(Table table)
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

        [When(@"I call the Visit Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheVisitExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Visit exists result should be bool true or false")]
        public void ThenTheVisitExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Visit>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //
        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //

    }
}