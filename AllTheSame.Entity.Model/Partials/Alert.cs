using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Interfaces;
using AllTheSame.Entity.Model.Metadata;
using AllTheSame.Common.Core;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     AlertMetadata
    /// </summary>
    [MetadataType(typeof (AlertMetadata))]
    public partial class Alert : Entity<int>, ICustomizeSchema
    {
        /// <summary>
        ///     The _exclude props
        /// </summary>
        private static List<string> _excludeProps;

        /// <summary>
        ///     List of property names to exclude from schema generation
        /// </summary>
        /// <returns></returns>
        public List<string> GetExcludeProperties()
        {
            {
                if (_excludeProps != null) return _excludeProps;

                _excludeProps = new List<string>();
                this.ExcludePropertiesOfType(typeof (ICollection<>), _excludeProps);
                this.ExcludePropertiesOfType(typeof (User), _excludeProps);
                this.ExcludePropertiesOfType(typeof (Kiosk), _excludeProps);
                this.ExcludePropertiesOfType(typeof (Appointment), _excludeProps);
                this.ExcludePropertiesOfType(typeof(CommunityWorker_Alert), _excludeProps);
                this.ExcludePropertiesOfType(typeof(VendorWorker_Alert), _excludeProps);



                return _excludeProps;
            }
        }
    }
}