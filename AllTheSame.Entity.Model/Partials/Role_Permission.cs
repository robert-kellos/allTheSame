using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     Role_PermissionMetadata
    /// </summary>
    [MetadataType(typeof (Role_PermissionMetadata))]
    public partial class Role_Permission: Entity<int>
    {
    }
}