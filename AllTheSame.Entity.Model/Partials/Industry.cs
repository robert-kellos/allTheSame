using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     IndustryMetadata
    /// </summary>
    [MetadataType(typeof (IndustryMetadata))]
    public partial class Industry: Entity<int>
    {
    }
}