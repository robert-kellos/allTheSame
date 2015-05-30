using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     VendorCredDocumentMetadata
    /// </summary>
    [MetadataType(typeof (VendorCredDocumentMetadata))]
    public partial class VendorCredDocument: Entity<int>
    {
    }
}