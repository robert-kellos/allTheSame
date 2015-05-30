using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     VendorTypeMetadata
    /// </summary>
    [MetadataType(typeof (VendorTypeMetadata))]
    public partial class VendorType: Entity<int>
    {
    }
}