using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     User_Org_PermissionMetadata
    /// </summary>
    [MetadataType(typeof (User_Org_PermissionMetadata))]
    public partial class User_Org_Permission: Entity<int>
    {
    }
}