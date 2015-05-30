using System.ComponentModel.DataAnnotations;
using AllTheSame.Entity.Model.Metadata;
using AllTheSame.Common.Core;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    /// </summary>
    [MetadataType(typeof (CommunityAdminMetadata))]
    public partial class CommunityAdmin: Entity<int>
    {
    }
}