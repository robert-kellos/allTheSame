using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using AllTheSame.Common.Logging;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace AllTheSame.WebAPI
{
    /// <summary>
    ///     GlobalExceptionFilter
    /// </summary>
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        ///     Called when [exception].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        /// <exception cref="HttpResponseMessage"></exception>
        /// <exception cref="StringContent">An error occurred, please try again or contact the administrator.</exception>
        public override void OnException(HttpActionExecutedContext context)
        {
            //Log Critical errors
            Debug.WriteLine(context.Exception);

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An error occurred, please try again or contact the administrator."),
                ReasonPhrase = "Critical Exception"
            });
        }
    }

    /// <summary>
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        ///     The lock object
        /// </summary>
        private static readonly object LockObj = new object();

        /// <summary>
        ///     The _config
        /// </summary>
        private static HttpConfiguration _config;

        /// <summary>
        ///     Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            lock (LockObj)
            {
                if (_config != null)
                    return; //- Web API Has already been configured need to check to support both IIS and OWIN hosting
                _config = config;
            }
            config.Filters.Add(new GlobalExceptionFilter());
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );

            Audit.Log.Debug("WebApiConfig :: configuration has been set.");
        }
    }
}