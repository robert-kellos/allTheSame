using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     VisitMetadata
    /// </summary>
    [MetadataType(typeof (VisitMetadata))]
    public partial class Visit: Entity<int>
    {
    }
}