using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AllTheSame.Entity.Model;
using AllTheSame.Service.Interfaces;
using AllTheSame.WebAPI.Controllers.Base;
using AllTheSame.WebAPI.Models;
using AllTheSame.WebAPI.Providers;
using AllTheSame.WebAPI.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace AllTheSame.WebAPI.Controllers
{
    /// <summary>
    /// AccountController
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController<User>
    {
        /// <summary>
        ///     The local login provider
        /// </summary>
        private const string LocalLoginProvider = "Local";

        /// <summary>
        ///     The _user manager
        /// </summary>
        private ApplicationUserManager _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
        }

        public AccountController(Entity.Model.AllTheSameDbContext context):base(context)
        {
        }
        

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="accessTokenFormat">The access token format.</param>
        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        /// <summary>
        ///     Gets the user manager.
        /// </summary>
        /// <value>
        ///     The user manager.
        /// </value>
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        /// <summary>
        ///     Gets the access token format.
        /// </summary>
        /// <value>
        ///     The access token format.
        /// </value>
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        /// <summary>
        ///     Gets the user information.
        /// </summary>
        /// <returns></returns>
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // GET api/Account/UserLast
        /// <summary>
        ///     Gets the user last.
        /// </summary>
        /// <returns></returns>
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserLast")]
        public UserLastViewModel GetUserLast()
        {
            var result = default(UserLastViewModel);
            //var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            var testObj = Proxy.Users;
            var singleOrDefault = testObj.SingleOrDefault(u => u.Id == 1);
            if (singleOrDefault != null)
            {
                var thisPerson = singleOrDefault.Person;

                if (thisPerson != null)
                {
                    result = new UserLastViewModel
                    {
                        Id = thisPerson.Id,
                        Firstname = thisPerson.FirstName,
                        Lastname = thisPerson.LastName
                    };
                }
            }
            else
            {
                result = new UserLastViewModel
                {
                    Id = -1,
                    Firstname = "Not",
                    Lastname = "Here"
                };
            }

            return result;
        }

        // POST api/Account/Logout
        /// <summary>
        ///     Logouts this instance.
        /// </summary>
        /// <returns></returns>
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        /// <summary>
        ///     Gets the manage information.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="generateState">if set to <c>true</c> [generate state].</param>
        /// <returns></returns>
        [Route("ManageInfo")]
        public async Task<HttpResponseMessage> GetManageInfo(string returnUrl, bool generateState = false)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());

            if (user == null)
            {
                return null;
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // POST api/Account/ChangePassword
        /// <summary>
        ///     Changes the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        /// <summary>
        ///     Sets the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId<int>(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/AddExternalLogin
        /// <summary>
        ///     Adds the external login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            var ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                                                              && ticket.Properties.ExpiresUtc.HasValue
                                                              &&
                                                              ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            var externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId<int>(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        /// <summary>
        ///     Removes the login.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId<int>());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId<int>(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        /// <summary>
        ///     Gets the external login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            var user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            var hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                var oAuthIdentity = await UserManager.GenerateUserIdentityAsync(user, OAuthDefaults.AuthenticationType);
                var cookieIdentity =
                    await UserManager.GenerateUserIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);

                var properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                var identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        /// <summary>
        ///     Gets the external logins.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <param name="generateState">if set to <c>true</c> [generate state].</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            var descriptions = Authentication.GetExternalAuthenticationTypes();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            return descriptions.Select(description => new ExternalLoginViewModel
            {
                Name = description.Caption, Url = Url.Route("ExternalLogin", new
                {
                    provider = description.AuthenticationType, response_type = "token", client_id = Startup.PublicClientId, redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri, state
                }),
                State = state
            }).ToList();
        }

        // POST api/Account/Register
        /// <summary>
        ///     Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = model.Email,
                Person = new Person {Email = model.Email}
            };

            var result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RegisterExternal
        /// <summary>
        ///     Registers the external.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new User
            {
                UserName = model.Email,
                Person = new Person {Email = model.Email}
            };

            var result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        /// <summary>
        ///     Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Test Service Constructors

        private IAuthService _service;
        private IUserService _userService;
        private IUserSessionService _userSessionService;
        //
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public AccountController(IAuthService authService)
        {
            _service = authService;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userSessionService">The user session service.</param>
        public AccountController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        //

        #endregion Test Service Constructors

        #region Helpers

        /// <summary>
        ///     Gets the authentication.
        /// </summary>
        /// <value>
        ///     The authentication.
        /// </value>
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        /// <summary>
        ///     Gets the error result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        /// <summary>
        /// </summary>
        private class ExternalLoginData
        {
            /// <summary>
            ///     Gets or sets the login provider.
            /// </summary>
            /// <value>
            ///     The login provider.
            /// </value>
            public string LoginProvider { get; private set; }

            /// <summary>
            ///     Gets or sets the provider key.
            /// </summary>
            /// <value>
            ///     The provider key.
            /// </value>
            public string ProviderKey { get; private set; }

            /// <summary>
            ///     Gets or sets the name of the user.
            /// </summary>
            /// <value>
            ///     The name of the user.
            /// </value>
            private string UserName { get; set; }

            /// <summary>
            ///     Gets the claims.
            /// </summary>
            /// <returns></returns>
            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            /// <summary>
            ///     Froms the identity.
            /// </summary>
            /// <param name="identity">The identity.</param>
            /// <returns></returns>
            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                var providerKeyClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || string.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || string.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        /// <summary>
        /// RandomOAuthStateGenerator
        /// </summary>
        private static class RandomOAuthStateGenerator
        {
            /// <summary>
            ///     The _random
            /// </summary>
            private static readonly RandomNumberGenerator Random = new RNGCryptoServiceProvider();

            /// <summary>
            ///     Generates the specified strength in bits.
            /// </summary>
            /// <param name="strengthInBits">The strength in bits.</param>
            /// <returns></returns>
            /// <exception cref="System.ArgumentException">strengthInBits must be evenly divisible by 8.;strengthInBits</exception>
            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits%bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                var strengthInBytes = strengthInBits/bitsPerByte;

                var data = new byte[strengthInBytes];
                Random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}