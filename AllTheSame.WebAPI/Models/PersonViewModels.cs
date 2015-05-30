using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AllTheSame.WebAPI.Models
{
    // Models returned by PersonController actions.
    /// <summary>
    /// </summary>
    public class PersonDetails
    {
        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the mobile phone.
        /// </summary>
        /// <value>
        ///     The mobile phone.
        /// </value>
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}