using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllTheSame.Entity.Model.Custom
{
    /// <summary>
    /// This model consists of a composite of models required for AccountManagement
    /// </summary>
    public class AccountData
    {
        /// <summary>
        /// Gets or sets the user data.
        /// </summary>
        /// <value>
        /// The user data.
        /// </value>
        public User UserData { get; set; }
        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        public Person PersonData { get; set; }
        /// <summary>
        /// Gets or sets the address data.
        /// </summary>
        /// <value>
        /// The address data.
        /// </value>
        public Address AddressData { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        DateTime? LastUpdated { get; set; }
    }
}
