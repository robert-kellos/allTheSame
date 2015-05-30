using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     CommunityWorker_AlertMetadata
    /// </summary>
    [MetadataType(typeof (CommunityWorker_AlertMetadata))]
    public partial class CommunityWorker_Alert: Entity<int>
    {
    }
}