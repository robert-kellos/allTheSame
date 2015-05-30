using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AllTheSame.Common.Core;
using AllTheSame.WebAPI.Providers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace AllTheSame.WebAPI
{
    /// <summary>
    ///     Startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        ///     Gets the o authentication options.
        /// </summary>
        /// <value>
        ///     The o authentication options.
        /// </value>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        /// <summary>
        ///     Gets the public client identifier.
        /// </summary>
        /// <value>
        ///     The public client identifier.
        /// </value>
        public static string PublicClientId { get; private set; }

        /// <summary>
        ///     Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            //- Add User roles for this request
            app.Use(RequestHandler);
        }

        /// <summary>
        ///     Requests the handler.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="next">The next.</param>
        /// <returns></returns>
        private static Task RequestHandler(IOwinContext context, Func<Task> next)
        {
            var usr = context.Authentication.User;

            if (usr == null) return next.Invoke();

            var identity = usr.Identity as ClaimsIdentity;
            if (identity == null) return next.Invoke();

            var manager = context.GetUserManager<ApplicationUserManager>();
            string[] headerValues;
            int orgId;

            if (context.Request.Headers.TryGetValue(AppConstants.CustomHeaderCode.OrgId, out headerValues) &&
                int.TryParse(headerValues.FirstOrDefault(), out orgId))
            {
                //- Fetch Role Claims for User and organization
                manager.PopulateAppRoles(identity, orgId);
            }
            return next.Invoke();
        }
    }
}