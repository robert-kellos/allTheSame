using System.ComponentModel.DataAnnotations;
using AllTheSame.Entity.Model.Metadata;
using AllTheSame.Common.Core;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     DataSyncMetadata
    /// </summary>
    [MetadataType(typeof (DataSyncMetadata))]
    public partial class DataSync : Entity<int>
    {
        
    }
}