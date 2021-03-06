﻿using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using AllTheSame.Common.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace AllTheSame.WebAPI.Test
{
    /// <summary>
    ///     BaseServiceTest
    /// </summary>
    public abstract class BaseServiceTest : IDisposable
    {
        /// <summary>
        ///     The _client URL
        /// </summary>
        private string _clientUrl;

        /// <summary>
        ///     The client
        /// </summary>
        protected HttpClient Client = new HttpClient();

        /// <summary>
        ///     Gets or sets the client URL.
        /// </summary>
        /// <value>
        ///     The client URL.
        /// </value>
        public string ClientUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_clientUrl))
                {
                    _clientUrl = ConfigurationManager.AppSettings["WebAPI.Test.ClientBaseURL"];
                }
                return _clientUrl;
            }
            set { _clientUrl = value; }
        }

        /// <summary>
        ///     Gets the URI.
        /// </summary>
        /// <value>
        ///     The URI.
        /// </value>
        public abstract string Uri { get; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Setups this instance.
        /// </summary>
        [TestInitialize]
        public virtual void Setup()
        {
            PostSetup();
        }

        /// <summary>
        ///     Tear-downs this instance.
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            Dispose();
        }

        /// <summary>
        ///     Gets the asynchronous.
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<HttpResponseMessage> GetAsync()
        {
            return await Client.GetAsync(string.Format("{0}{1}", ClientUrl, Uri));
        }

        /// <summary>
        ///     Gets the asynchronous by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        protected virtual async Task<HttpResponseMessage> GetAsyncById(long? id)
        {
            return await Client.GetAsync(string.Format("{0}{1}/{2}", ClientUrl, Uri, id));
        }

        /// <summary>
        ///     Gets the asynchronous exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        protected virtual async Task<HttpResponseMessage> GetAsyncExists(long? id)
        {
            return await Client.GetAsync(string.Format("{0}{1}/Exists/{2}", ClientUrl, Uri, id));
        }

        /// <summary>
        ///     Gets the response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual T GetResponse<T>()
        {
            var response = GetAsync();
            var content = response.Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        ///     Gets the response by identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        protected virtual T GetResponseById<T>(long? id)
        {
            var response = GetAsyncById(id);
            var content = response.Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        ///     Gets the response exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        protected virtual bool GetResponseExists<T>(long? id)
        {
            var response = GetAsyncExists(id);
            var result = response.Result.Content.ReadAsStringAsync().Result;
            return bool.Parse(result);
        }

        /// <summary>
        ///     Posts the response.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="orgId">The org identifier.</param>
        /// <returns></returns>
        protected virtual TResponse PostResponse<TModel, TResponse>(TModel model, int orgId)
        {
            var response = PostAsync(model);
            return JsonConvert.DeserializeObject<TResponse>(response.Result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        ///     Puts the response.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected virtual TResponse PutResponse<TModel, TResponse>(long? id, TModel model)
        {
            var response = PutAsync(id, model);
            return JsonConvert.DeserializeObject<TResponse>(response.Result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        ///     Posts the response.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected virtual TResponse PostResponse<TModel, TResponse>(TModel model)
        {
            var response = PostAsync(model);
            return JsonConvert.DeserializeObject<TResponse>(response.Result.Content.ReadAsStringAsync().Result);
        }

        // -- ///////////////////////

        /// <summary>
        ///     Posts the asynchronous.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected virtual async Task<HttpResponseMessage> PostAsync<TModel>(TModel model)
        {
            var formatter = new JsonMediaTypeFormatter {UseDataContractJsonSerializer = true, Indent = false};

            var canWrite = formatter.CanWriteType(typeof (TModel));

            return await Client.PostAsync(string.Format("{0}{1}", ClientUrl, Uri),
                new ObjectContent(typeof (TModel), model, formatter));
        }

        /// <summary>
        ///     Puts the asynchronous.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        protected virtual async Task<HttpResponseMessage> PutAsync<TModel>(long? id, TModel model)
        {
            var endpoint = string.Format("{0}{1}/{2}", ClientUrl, Uri, id);
            var formatter = new JsonMediaTypeFormatter {UseDataContractJsonSerializer = true, Indent = false};


            var canWrite = formatter.CanWriteType(typeof (TModel));

            return await Client.PutAsync(endpoint,
                new ObjectContent(typeof (TModel), model, formatter));
        }

        /// <summary>
        ///     Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        protected virtual async Task<HttpResponseMessage> DeleteAsync(long? id)
        {
            return await Client.DeleteAsync(string.Format("{0}{1}/{2}", ClientUrl, Uri, id));
        }

        /// <summary>
        ///     Posts the setup.
        /// </summary>
        protected virtual void PostSetup()
        {
        }

        /// <summary>
        ///     Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Client?.Dispose();
            }
        }

        #region Helpers

        /// <summary>
        ///     Actions the response.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        protected static HttpResponseMessage ActionResponse(Task<HttpResponseMessage> task, out AggregateException error)
        {
            HttpResponseMessage response = null;
            if (task.IsCompleted)
            {
                if (task.Result != null)
                    response = task.Result;
            }

            if (!task.IsFaulted)
            {
                error = null;
                return response;
            }
            error = task.Exception;
            Audit.Log.Error("ACTION Task Exception ::", error);

            return response;
        }

        protected virtual TModel Add<TModel>(TModel model, out HttpResponseMessage response)
        {
            var result = default(TModel);
            AggregateException error;
            var actionResponse = default(HttpResponseMessage);

            //Now, let's Delete the newly added item
            PostAsync(model).ContinueWith(
                t => { actionResponse = ActionResponse(t, out error); }
                ).Wait();

            response = actionResponse;

            if (response.IsSuccessStatusCode)
                result = PostResponse<TModel, TModel>(model);

            return result;
        }

        protected virtual bool Exists(int id)
        {
            var response = GetResponseExists<bool>(id);

            return response;
        }

        protected virtual TModel GetById<TModel>(int id)
        {
            var response = GetResponseById<TModel>(id);

            return response;
        }

        protected virtual HttpResponseMessage Update<TModel>(int id, TModel model)
        {
            AggregateException error;
            var response = default(HttpResponseMessage);

            //Now, let's Delete the newly added item
            PutAsync(id, model).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            return response;
        }

        protected virtual HttpResponseMessage Delete(int id)
        {
            AggregateException error;
            var response = default(HttpResponseMessage);

            //Now, let's Delete the newly added item
            DeleteAsync(id).ContinueWith(
                t => { response = ActionResponse(t, out error); }
                ).Wait();

            return response;
        }

        public virtual int ConvertToIntValue(string value)
        {
            var result = -1;

            int.TryParse(value, out result);

            return result;
        }

        //

        #endregion Helpers
    }
}