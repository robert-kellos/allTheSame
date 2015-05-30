using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     VendorWorkerMetadata
    /// </summary>
    [MetadataType(typeof (VendorWorkerMetadata))]
    public partial class VendorWorker: Entity<int>
    {
    }
}