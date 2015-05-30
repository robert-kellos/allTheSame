using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     VendorCredentialMetadata
    /// </summary>
    [MetadataType(typeof (VendorCredentialMetadata))]
    public partial class VendorCredential: Entity<int>
    {
    }
}