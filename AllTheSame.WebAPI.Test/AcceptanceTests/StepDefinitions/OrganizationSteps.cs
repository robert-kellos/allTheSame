using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AllTheSame.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class OrganizationSteps : BaseServiceTest
        //AuthenticatedTest //- Allows automatic fetching of token for each get call
    {
        public override string Uri => "/api/Organization";

        #region Local Properties/Fields

        //
        private const string HttpResponseKey = "http_response";

        private const string GetListKey = "organization_get_list";
        private const string GetItemKey = "organization_get_item";
        private const string AddItemKey = "organization_add_item";
        private const string EditItemKey = "organization_edit_item";
        private const string DeleteItemKey = "organization_delete_item";
        private const string ExistsItemKey = "organization_exists_item";

        private Organization _getItem;
        private Organization _addItem;
        private Organization _editItem;
        private Organization _deleteItem;

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

        #region Post - add a new item by a populated item

        //
        [Given(@"the following Organization Add input")]
        public void GivenTheFollowingOrganizationAddInput(Table table)
        {
            Assert.IsNotNull(table);
            foreach (var row in table.Rows)
            {
                _name = row["Name"];

                break;
            }
            Assert.IsNotNull(_name);

            _addItem = new Organization
            {
                Name = _name,
                CreatedOn = DateTime.UtcNow
            };
        }

        [When(@"I call the add Organization Post api endpoint to add a organization")]
        public void WhenICallTheAddOrganizationPostApiEndpointToAddAOrganization()
        {
            var response = default(HttpResponseMessage);
            AggregateException error;

            PostAsync(_addItem).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            Assert.IsNotNull(response);
            ScenarioContext.Current[AddItemKey] = response;
        }

        [Then(@"the add result should be a Organization Id")]
        public void ThenTheAddResultShouldBeAOrganizationId()
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
            var result = PostResponse<Organization, Organization>(_addItem);
            if (result != null)
            {
                _addedIdValue = result.Id;
                Assert.IsTrue(_addedIdValue > 0);

                ////validate values changed
                Assert.AreEqual(_addItem.Name, result.Name);
            }

            var response = (ScenarioContext.Current[AddItemKey] as HttpResponseMessage);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        //

        #endregion Post - add a new item by a populated item

        #region Get - get a list of items

        //
        [When(@"I call the Organization Get api endpoint")]
        public void WhenICallTheOrganizationGetApiEndpoint()
        {
            ScenarioContext.Current[GetListKey] = GetResponse<IList<Organization>>();
        }

        [Then(@"the get result should be a list of organizations")]
        public void ThenTheGetResultShouldBeAListOfOrganizations()
        {
            //
            var list = ScenarioContext.Current[GetListKey];
            Assert.IsNotNull(list);
            Assert.IsNotNull(list as IList<Organization>);
        }

        //

        #endregion Get - get a list of items

        #region Get - get an item by Id

        //
        [Given(@"the following Organization GetById input")]
        public void GivenTheFollowingOrganizationGetByIdInput(Table table)
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

        [When(@"I call the Organization Get api endpoint by Id")]
        public void WhenICallTheOrganizationGetApiEndpointById()
        {
            ScenarioContext.Current[GetItemKey] = GetResponseById<Organization>(_getIdValue);
        }

        [Then(@"the get by id result should be a Organization")]
        public void ThenTheGetByIdResultShouldBeAOrganization()
        {
            var result = ScenarioContext.Current[GetItemKey];
            var item = (result as Organization);

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Id == _getIdValue);
        }

        //

        #endregion Get - get an item by Id

        #region Put - edit an existing item by a populated item, and its Id

        //
        [Given(@"the following Organization Edit input")]
        public void GivenTheFollowingOrganizationEditInput(Table table)
        {
            //
        }

        [When(@"I call the edit Organization Put api endpoint to edit a organization")]
        public void WhenICallTheEditOrganizationPutApiEndpointToEditAOrganization()
        {
            //
        }

        [Then(@"the edit result should be an updated Organization")]
        public void ThenTheEditResultShouldBeAnUpdatedOrganization()
        {
            //
        }

        //

        #endregion Put - edit an existing item by a populated item, and its Id

        #region Post - delete an existing item by a populated item

        //
        [Given(@"the following Organization Delete input")]
        public void GivenTheFollowingOrganizationDeleteInput(Table table)
        {
            //
        }

        [When(@"I call the delete Organization Post api endpoint to delete a organization")]
        public void WhenICallTheDeleteOrganizationPostApiEndpointToDeleteAOrganization()
        {
            //
        }

        [Then(@"the delete result should be an deleted Organization")]
        public void ThenTheDeleteResultShouldBeAnDeletedOrganization()
        {
            //
        }

        //

        #endregion Post - delete an existing item by a populated item

        #region Get - Exists, verify Exists function checks and return a valid bool for exists or not

        //
        [Given(@"the following Organization Id input")]
        public void GivenTheFollowingOrganizationIdInput(Table table)
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

        [When(@"I call the Organization Exists Get api endpoint by Id to verify if it exists")]
        public void WhenICallTheOrganizationExistsGetApiEndpointByIdToVerifyIfItExists()
        {
            ScenarioContext.Current[ExistsItemKey] = GetResponseExists<bool>(_existsIdValue);
        }

        [Then(@"the Organization exists result should be bool true or false")]
        public void ThenTheOrganizationExistsResultShouldBeBoolTrueOrFalse()
        {
            var result = ScenarioContext.Current[ExistsItemKey];

            //call manually to verify Exists returned correctly
            var item = GetResponseById<Organization>(_existsIdValue);

            var truth = (item != null && item.Id == _existsIdValue);
            Assert.AreEqual(truth, result);
        }

        //

        #endregion Get - Exists, verify Exists function checks and return a valid bool for exists or not
    }
}