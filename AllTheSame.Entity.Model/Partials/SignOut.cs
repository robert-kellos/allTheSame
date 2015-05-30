using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     SignOutMetadata
    /// </summary>
    [MetadataType(typeof (SignOutMetadata))]
    public partial class SignOut: Entity<int>
    {
    }
}