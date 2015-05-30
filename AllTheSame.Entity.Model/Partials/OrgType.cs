using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     OrgTypeMetadata
    /// </summary>
    [MetadataType(typeof (OrgTypeMetadata))]
    public partial class OrgType: Entity<int>
    {
    }
}