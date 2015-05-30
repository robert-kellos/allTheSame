using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     RoleMetadata
    /// </summary>
    [MetadataType(typeof (RoleMetadata))]
    public partial class Role: Entity<int>
    {
    }
}