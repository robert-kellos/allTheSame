using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AllTheSame.Entity.Model;
using AllTheSame.Service;
using AllTheSame.WebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AllTheSame.WebAPI
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    /// <summary>
    /// </summary>
    public class ApplicationUserManager : UserManager<User, int>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApplicationUserManager" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {
        }

        /// <summary>
        ///     Creates the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new AllTheSameUserStore(new Entity.Model.AllTheSameDbContext()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        /// <summary>
        ///     Generates the user identity asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="authenticationType">Type of the authentication.</param>
        /// <returns></returns>
        public Task<ClaimsIdentity> GenerateUserIdentityAsync(User user, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = CreateIdentityAsync(user, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        ///     Returns List of Role Claims for Identity via PrimarySid as UserSession.SessionId
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public void PopulateAppRoles(ClaimsIdentity identity, int orgId)
        {
            var sessionId = identity.FindFirstValue(ClaimTypes.PrimarySid);
            if (sessionId == null) return;

            var roles = ServiceProxy.AuthServiceProxy.GetUserRoles(Guid.Parse(sessionId), orgId);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }
    }
}