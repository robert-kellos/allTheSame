using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;

namespace AllTheSame.WebAPI.Test.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class AuthenticateSteps : BaseServiceTest
    {
        public override string Uri => "/Token";

        [Given(@"I have entered valid credentials")]
        public void GivenIHaveEnteredValidCredentials(Table table)
        {
            Setup();
            string un = null, pw = null;
            foreach (var row in table.Rows)
            {
                un = row["Username"];
                pw = row["Password"];
                break;
            }
            Assert.IsNotNull(un, "Username not provided");
            Assert.IsNotNull(pw, "Password not provided");
            ScenarioContext.Current.Add("Username", un);
            ScenarioContext.Current.Add("Password", pw);
        }

        [When(@"I post data to the Token endpoint")]
        public void WhenIPostDataToTheTokenEndpoint()
        {
            var un = ScenarioContext.Current["Username"] as string;
            var pw = ScenarioContext.Current["Password"] as string;
            Assert.IsNotNull(un, "Username not provided");
            Assert.IsNotNull(pw, "Password not provided");

            var tokenDetails = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", un),
                new KeyValuePair<string, string>("password", pw)
            };

            var tokenPostData = new FormUrlEncodedContent(tokenDetails);
            var tokenResult = (new HttpClient()).PostAsync(ClientUrl + "/Token", tokenPostData).Result;
            Assert.AreEqual(HttpStatusCode.OK, tokenResult.StatusCode);

            var body = JObject.Parse(tokenResult.Content.ReadAsStringAsync().Result);

            var token = (string) body["access_token"];
            ScenarioContext.Current["access_token"] = token;
        }

        [Then(@"the result is an access_token")]
        public void ThenTheResultIsAnAccess_Token()
        {
            Assert.IsNotNull(ScenarioContext.Current["access_token"]);
        }
    }
}