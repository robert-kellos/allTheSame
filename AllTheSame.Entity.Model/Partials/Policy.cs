using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     PolicyMetadata
    /// </summary>
    [MetadataType(typeof (PolicyMetadata))]
    public partial class Policy: Entity<int>
    {
    }
}