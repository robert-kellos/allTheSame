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

        private const string GetListKey = "visit_get_list";
        private const string GetItemKey = "visit_get_item";
        private const string AddItemKey = "visit_add_item";
        private const string EditItemKey = "visit_edit_item";
        private const string DeleteItemKey = "visit_delete_item";
        private const string ExistsItemKey = "visit_exists_item";

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

        private string _line1 = "";
        private string _line2 = "";
        private string _city = "";
        private string _state = "";
        private string _country = "";
        private string _postalCode = "";
        //
        #endregion Local Properties/Fields

        public override string Uri => "/api/Visit";

        #region Post - add a new item by a populated item
        //
        [Given(@"the following Visit Add input")]
        public void GivenTheFollowingVisitAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                //_line1 = row["Line1"];
                //_line2 = row["Line2"];
                //_city = row["City"];
                //_state = row["State"];
                //_country = row["Country"];
                //_postalCode = row["PostalCode"];

                break;
            }
            //Assert.IsNotNull(_line1);
            //Assert.IsNotNull(_city);
            //Assert.IsNotNull(_city.IsValidEmailAddress());

            _addItem = new Visit()
            {
                //Line1 = _line1,
                //Line2 = _line2,
                //City = _city,
                //State = _state,
                //Country = _country,
                //PostalCode = _postalCode,

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add Visit Post api endpoint to add a visit")]
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
        [When(@"I call the Visit Get api endpoint")]
        public void WhenICallTheVisitGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Visit>>();
        }

        [Then(@"the get result should be a list of visits")]
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

        //[Then(@"the add result should be a Item Id")]
        //public void ThenTheAddResultShouldBeAItemId()
        //{
        //    _addedIdValue = -1;

        //    //grab the resulting added item
        //    var result = PostResponse<Item, Item>(_addItem);
        //    if (result != null)
        //    {

        //        _addedIdValue = result.Id;
        //        Assert.IsTrue(_addedIdValue > 0);

        //        ////validate values changed
        //        //Assert.AreEqual(_addItem.FirstName, result.FirstName);
        //        //Assert.AreEqual(_addItem.LastName, result.LastName);
        //        //Assert.AreEqual(_addItem.Email, result.Email);
        //        //Assert.AreEqual(_addItem.MobilePhone, result.MobilePhone);
        //    }

        //    var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

        //    Assert.IsNotNull(response);
        //    Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        //}
        //
        #endregion Post - add a new item by a populated item

        #region Put - edit an existing item by a populated item, and its Id
        //
        [Given(@"the following Visit Edit input")]
        public void GivenTheFollowingVisitEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit Visit Put api endpoint to edit a visit")]
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

        [When(@"I call the delete Visit Post api endpoint to delete a visit")]
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