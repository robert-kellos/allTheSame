using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     VendorWorker_AlertMetadata
    /// </summary>
    [MetadataType(typeof (VendorWorker_AlertMetadata))]
    public partial class VendorWorker_Alert: Entity<int>
    {
    }
}