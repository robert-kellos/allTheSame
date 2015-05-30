using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Common.Interfaces;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     AddressMetadata
    /// </summary>
    [MetadataType(typeof (AddressMetadata))]
    public partial class Address : Entity<int>, ICustomizeSchema
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
                return _excludeProps;
            }
        }
    }
}