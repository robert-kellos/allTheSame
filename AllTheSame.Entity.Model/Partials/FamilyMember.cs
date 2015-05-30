using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     FamilyMemberMetadata
    /// </summary>
    [MetadataType(typeof (FamilyMemberMetadata))]
    public partial class FamilyMember: Entity<int>
    {
    }
}