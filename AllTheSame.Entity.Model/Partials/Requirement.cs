using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     RequirementMetadata
    /// </summary>
    [MetadataType(typeof (RequirementMetadata))]
    public partial class Requirement: Entity<int>
    {
    }
}