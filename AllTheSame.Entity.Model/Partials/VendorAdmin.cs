using System.ComponentModel.DataAnnotations;
using AllTheSame.Entity.Model.Metadata;
using AllTheSame.Common.Core;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     VendorAdminMetadata
    /// </summary>
    [MetadataType(typeof (VendorAdminMetadata))]
    public partial class VendorAdmin: Entity<int>
    {
    }
}