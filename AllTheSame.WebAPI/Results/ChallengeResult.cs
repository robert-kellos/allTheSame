﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace AllTheSame.WebAPI.Results
{
    /// <summary>
    ///     ChallengeResult
    /// </summary>
    public class ChallengeResult : IHttpActionResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChallengeResult" /> class.
        /// </summary>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="controller">The controller.</param>
        public ChallengeResult(string loginProvider, ApiController controller)
        {
            LoginProvider = loginProvider;
            Request = controller.Request;
        }

        /// <summary>
        ///     Gets or sets the login provider.
        /// </summary>
        /// <value>
        ///     The login provider.
        /// </value>
        public string LoginProvider { get; set; }

        /// <summary>
        ///     Gets or sets the request.
        /// </summary>
        /// <value>
        ///     The request.
        /// </value>
        public HttpRequestMessage Request { get; set; }

        /// <summary>
        ///     Creates an <see cref="T:System.Net.Http.HttpResponseMessage" /> asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        ///     A task that, when completed, contains the <see cref="T:System.Net.Http.HttpResponseMessage" />.
        /// </returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            Request.GetOwinContext().Authentication.Challenge(LoginProvider);

            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized) {RequestMessage = Request};
            return Task.FromResult(response);
        }
    }
}