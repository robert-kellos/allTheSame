using System.ComponentModel.DataAnnotations;
using AllTheSame.Entity.Model.Metadata;
using AllTheSame.Common.Core;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     CommunityTypeMetadata
    /// </summary>
    [MetadataType(typeof (CommunityTypeMetadata))]
    public partial class CommunityType : Entity<int>
    {
    }
}