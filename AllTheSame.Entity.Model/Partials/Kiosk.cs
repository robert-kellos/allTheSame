using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     KioskMetadata
    /// </summary>
    [MetadataType(typeof (KioskMetadata))]
    public partial class Kiosk: Entity<int>
    {
    }
}