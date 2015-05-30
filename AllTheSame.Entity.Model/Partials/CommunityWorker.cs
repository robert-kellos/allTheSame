using System.ComponentModel.DataAnnotations;
using AllTheSame.Entity.Model.Metadata;
using AllTheSame.Common.Core;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     CommunityWorkerMetadata
    /// </summary>
    [MetadataType(typeof (CommunityWorkerMetadata))]
    public partial class CommunityWorker: Entity<int>
    {
    }
}