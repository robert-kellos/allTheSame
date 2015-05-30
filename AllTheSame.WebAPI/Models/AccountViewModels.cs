using System.Collections.Generic;

namespace AllTheSame.WebAPI.Models
{
    // Models returned by AccountController actions.

    /// <summary>
    /// </summary>
    public class ExternalLoginViewModel
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the URL.
        /// </summary>
        /// <value>
        ///     The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        /// <value>
        ///     The state.
        /// </value>
        public string State { get; set; }
    }

    /// <summary>
    /// </summary>
    public class ManageInfoViewModel
    {
        /// <summary>
        ///     Gets or sets the local login provider.
        /// </summary>
        /// <value>
        ///     The local login provider.
        /// </value>
        public string LocalLoginProvider { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the logins.
        /// </summary>
        /// <value>
        ///     The logins.
        /// </value>
        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        /// <summary>
        ///     Gets or sets the external login providers.
        /// </summary>
        /// <value>
        ///     The external login providers.
        /// </value>
        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    /// <summary>
    /// </summary>
    public class UserInfoViewModel
    {
        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has registered.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has registered; otherwise, <c>false</c>.
        /// </value>
        public bool HasRegistered { get; set; }

        /// <summary>
        ///     Gets or sets the login provider.
        /// </summary>
        /// <value>
        ///     The login provider.
        /// </value>
        public string LoginProvider { get; set; }
    }

    /// <summary>
    /// </summary>
    public class UserLoginInfoViewModel
    {
        /// <summary>
        ///     Gets or sets the login provider.
        /// </summary>
        /// <value>
        ///     The login provider.
        /// </value>
        public string LoginProvider { get; set; }

        /// <summary>
        ///     Gets or sets the provider key.
        /// </summary>
        /// <value>
        ///     The provider key.
        /// </value>
        public string ProviderKey { get; set; }
    }

    /// <summary>
    /// </summary>
    public class UserLastViewModel
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the firstname.
        /// </summary>
        /// <value>
        ///     The firstname.
        /// </value>
        public string Firstname { get; set; }

        /// <summary>
        ///     Gets or sets the lastname.
        /// </summary>
        /// <value>
        ///     The lastname.
        /// </value>
        public string Lastname { get; set; }
    }
}