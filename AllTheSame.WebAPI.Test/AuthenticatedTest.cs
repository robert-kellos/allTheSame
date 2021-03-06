﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace AllTheSame.WebAPI.Test
{
    public abstract class AuthenticatedTest : BaseServiceTest
    {
        protected virtual void FetchAuthtoken()
        {
            var tokenDetails = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", Username),
                new KeyValuePair<string, string>("password", Password)
            };

            var tokenPostData = new FormUrlEncodedContent(tokenDetails);
            var tokenResult = (new HttpClient()).PostAsync(ClientUrl + "/Token", tokenPostData).Result;
            Assert.AreEqual(HttpStatusCode.OK, tokenResult.StatusCode);

            var body = JObject.Parse(tokenResult.Content.ReadAsStringAsync().Result);

            Token = (string) body["access_token"];
        }

        protected override async Task<HttpResponseMessage> GetAsync()
        {
            AddAuthHeaders();
            return await base.GetAsync();
        }

        protected override async Task<HttpResponseMessage> GetAsyncById(long? id)
        {
            AddAuthHeaders();
            return await base.GetAsyncById(id);
        }

        protected override T GetResponseById<T>(long? id)
        {
            AddAuthHeaders();
            return base.GetResponseById<T>(id);
        }

        protected override Task<HttpResponseMessage> PostAsync<TModel>(TModel model)
        {
            AddAuthHeaders();
            return base.PostAsync(model);
        }

        protected override T GetResponse<T>()
        {
            AddAuthHeaders();
            return base.GetResponse<T>();
        }

        protected override TResponse PostResponse<TModel, TResponse>(TModel model, int orgId)
        {
            AddAuthHeaders();
            return base.PostResponse<TModel, TResponse>(model, orgId);
        }

        protected virtual void AddAuthHeaders()
        {
            if (Client.DefaultRequestHeaders.Contains("Authorization") == false)
                Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);

            if (Client.DefaultRequestHeaders.Contains("orgId") == false)
                Client.DefaultRequestHeaders.Add("orgId", OrgId.ToString());
        }

        #region Locals

        //

        public int OrgId { get; set; } = 16;

        protected virtual string Username { get; set; } = "super@admin.net";

        protected virtual string Password { get; set; } = "testpasswordA1_";

        public string Token { get; set; }
        //

        #endregion Locals
    }
}