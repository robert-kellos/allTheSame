using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     KioskStatusMetadata
    /// </summary>
    [MetadataType(typeof (KioskStatusMetadata))]
    public partial class KioskStatus: Entity<int>
    {
    }
}