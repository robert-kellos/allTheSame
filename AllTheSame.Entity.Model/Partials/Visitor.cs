using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     VisitorMetadata
    /// </summary>
    [MetadataType(typeof (VisitorMetadata))]
    public partial class Visitor: Entity<int>
    {
    }
}