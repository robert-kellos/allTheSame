using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     PermissionMetadata
    /// </summary>
    [MetadataType(typeof (PermissionMetadata))]
    public partial class Permission: Entity<int>
    {
    }
}