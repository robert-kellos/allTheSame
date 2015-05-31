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
    public class DataSyncSteps : BaseServiceTest//AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        #region Local Properties/Fields
        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "dataSync_get_list";
        private const string GetItemKey = "dataSync_get_item";
        private const string AddItemKey = "dataSync_add_item";
        private const string EditItemKey = "dataSync_edit_item";
        private const string DeleteItemKey = "dataSync_delete_item";
        private const string ExistsItemKey = "dataSync_exists_item";

        private DataSync _getItem;
        private DataSync _addItem;
        private DataSync _editItem;
        private DataSync _deleteItem;

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

        public override string Uri => "/api/DataSync";

        #region Post - add a new item by a populated item
        //
        [Given(@"the following DataSync Add input")]
        public void GivenTheFollowingDataSyncAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                //_firstName = row["FirstName"];
                //_lastName = row["LastName"];
                //_email = row["Email"];
                //_mobileNumber = row["MobileNumber"];

                break;
            }
            //Assert.IsNotNull(_firstName);
            //Assert.IsNotNull(_email);
            //Assert.IsNotNull(_email.IsValidEmailAddress());

            _addItem = new DataSync()
            {

                CreatedOn = DateTime.UtcNow,
            };
        }

        [When(@"I call the add DataSync Post api endpoint to add a dataSync")]
        public void WhenICallTheAddDataSyncPostApiEndpointToAddADataSync()
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

        [Then(@"the add result should be a DataSync Id")]
        public void ThenTheAddResultShouldBeADataSyncId()
        {
            _addedIdValue = -1;

            //grab the resulting added item
            var result = PostResponse<DataSync, DataSync>(_addItem);
            if (result != null)
            {

                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                //validate values changed
                //Assert.AreEqual(_addDataSync.FirstName, result.FirstName);
                //Assert.AreEqual(_addDataSync.LastName, result.LastName);
                //Assert.AreEqual(_addDataSync.Email, result.Email);
                //Assert.AreEqual(_addDataSync.MobilePhone, result.MobilePhone);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }
        //
        #endregion Post - add a new item by a populated item

        #region Get - get a list of items
        //
        [When(@"I call the DataSync Get api endpoint")]
        public void WhenICallTheDataSyncGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<DataSync>>();
        }

        [Then(@"the get result should be a list of DataSyncs")]
        public void ThenTheGetResultShouldBeAListOfDataSyncs()
        {
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<DataSync>);
        }
        //
        #endregion Get - get a list of items

        #region Get - get an item by Id
        //
        [Given(@"the following DataSync GetById input")]
        public void GivenTheFollowingDataSyncGetByIdInput(Table table)
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

        [When(@"I call the DataSync Get api endpoint by Id")]
        public void WhenICallTheDataSyncGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<DataSync>(_getIdValue);
        }

        [Then(@"the get by id result should be a DataSync")]
        public void ThenTheGetByIdResultShouldBeADataSync()
        {
            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as DataSync);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }
        //
        #endregion Get - get an item by Id

        #region Put - edit an existing item by a populated item, and its Id
        //
        [Given(@"the following DataSync Edit input")]
        public void GivenTheFollowingDataSyncEditInput(Table table)
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

            //var temp = GetResponseById<DataSync>(_editIdValue);

            //Assert.IsTrue(_editIdValue > 0);
            //Assert.IsNotNull(_firstName);
            //Assert.IsNotNull(_email);
            //Assert.IsNotNull(_email.IsValidEmailAddress());

            _editItem = new DataSync()
            {
                Id = _editIdValue,
                
            };
            
        }

        [When(@"I call the edit DataSync Put api endpoint to edit a dataSync")]
        public void WhenICallTheEditDataSyncPutApiEndpointToEditADataSync()
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            PutAsync(_editItem.Id, _editItem).ContinueWith(
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
        }

        [Then(@"the edit result should be an updated DataSync")]
        public void ThenTheEditResultShouldBeAnUpdatedDataSync()
        {
            //grab the resulting added item
            var response = (ScenarioContext.Current[EditItemKey] as HttpResponseMessage);
            var result = PutResponse<DataSync, DataSync>(_editItem.Id, _editItem);
            if (result != null)
            {
                Assert.IsTrue(_editIdValue > 0);
                Assert.AreEqual(_editIdValue, result.Id);

                //validate values changed
                //Assert.AreEqual(_editDataSync.FirstName, result.FirstName);
                //Assert.AreEqual(_editDataSync.LastName, result.LastName);
                //Assert.AreEqual(_editDataSync.Email, result.Email);
                //Assert.AreEqual(_editDataSync.MobilePhone, result.MobilePhone);
            }
            
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
        //
        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item
        //
        [Given(@"the following DataSync Delete input")]
        public void GivenTheFollowingDataSyncDeleteInput(Table table)
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

            var last = GetResponse<List<DataSync>>();
            var l = last[last.Count - 1];
            _deletedIdValue = l.Id;
        }

        [When(@"I call the delete DataSync Post api endpoint to delete a dataSync")]
        public void WhenICallTheDeleteDataSyncPostApiEndpointToDeleteADataSync()
        {
            var response = default(HttpResponseMessage);
            var error = default(AggregateException);

            DeleteAsync(_deletedIdValue).ContinueWith(
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
            ScenarioContext.Current[DeleteItemKey] = response;
        }

        [Then(@"the delete result should be an deleted DataSync")]
        public void ThenTheDeleteResultShouldBeAnDeletedDataSync()
        {
            //grab the resulting added item
            var deleted = GetResponseById<DataSync>(_deletedIdValue);
            Assert.IsNull(deleted);

            var response = (ScenarioContext.Current[DeleteItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);         
        }
        //
        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not
        //
        [Given(@"the following DataSync Id input")]
        public void GivenTheFollowingDataSyncIdInput(Table table)
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

        [When(@"I call the DataSync Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheDataSyncExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the DataSync exists result should be bool true or false")]
        public void ThenTheDataSyncExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<DataSync>(_existsIdValue);

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