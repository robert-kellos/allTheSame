using System.ComponentModel.DataAnnotations;

namespace AllTheSame.WebAPI.Models
{
    // Models used as parameters to AccountController actions.

    /// <summary>
    ///     AddExternalLoginBindingModel
    /// </summary>
    public class AddExternalLoginBindingModel
    {
        /// <summary>
        ///     Gets or sets the external access token.
        /// </summary>
        /// <value>
        ///     The external access token.
        /// </value>
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    /// <summary>
    /// </summary>
    public class ChangePasswordBindingModel
    {
        /// <summary>
        ///     Gets or sets the old password.
        /// </summary>
        /// <value>
        ///     The old password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        /// <summary>
        ///     Gets or sets the new password.
        /// </summary>
        /// <value>
        ///     The new password.
        /// </value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        ///     Gets or sets the confirm password.
        /// </summary>
        /// <value>
        ///     The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// </summary>
    public class RegisterBindingModel
    {
        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    /// <summary>
    /// </summary>
    public class RegisterExternalBindingModel
    {
        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// </summary>
    public class RemoveLoginBindingModel
    {
        /// <summary>
        ///     Gets or sets the login provider.
        /// </summary>
        /// <value>
        ///     The login provider.
        /// </value>
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        /// <summary>
        ///     Gets or sets the provider key.
        /// </summary>
        /// <value>
        ///     The provider key.
        /// </value>
        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    /// <summary>
    /// </summary>
    public class SetPasswordBindingModel
    {
        /// <summary>
        ///     Gets or sets the new password.
        /// </summary>
        /// <value>
        ///     The new password.
        /// </value>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        ///     Gets or sets the confirm password.
        /// </summary>
        /// <value>
        ///     The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}