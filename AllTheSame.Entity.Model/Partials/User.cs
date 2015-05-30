using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Interfaces;
using AllTheSame.Common.Interfaces.Generic;
using AllTheSame.Entity.Model.Metadata;
using Microsoft.AspNet.Identity;

// ReSharper disable once CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    /// UserMetadata
    /// </summary>
    [MetadataType(typeof (UserMetadata))]
    public partial class User : IUser<int>, IUser, IEntity<int>, ICustomizeSchema
    {
        /// <summary>
        /// The _exclude props
        /// </summary>
        private static List<string> _excludeProps;

        /// <summary>
        /// List of property names to exclude from schema generation
        /// </summary>
        /// <returns></returns>
        public List<string> GetExcludeProperties()
        {
            {
                if (_excludeProps != null) return _excludeProps;

                _excludeProps = new List<string>();
                this.ExcludePropertiesOfType(typeof (ICollection<VendorCredential>), _excludeProps);
                this.ExcludePropertiesOfType(typeof (ICollection<User_Org_Permission>), _excludeProps);

                return _excludeProps;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string IUser<string>.Id
        {
            get { return Id.ToString(); }
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName
        {
            get { return Username; }
            set { Username = value; }
        }

        /// <summary>
        /// Gets or sets the last synchronize identifier.
        /// </summary>
        /// <value>
        /// The last synchronize identifier.
        /// </value>
        public int LastSyncId { get; set; }
    }
}