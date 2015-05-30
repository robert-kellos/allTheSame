using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     ModuleMetadata
    /// </summary>
    [MetadataType(typeof (ModuleMetadata))]
    public partial class Module: Entity<int>
    {
    }
}