using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     RequirementTypeMetadata
    /// </summary>
    [MetadataType(typeof (RequirementTypeMetadata))]
    public partial class RequirementType: Entity<int>
    {
    }
}