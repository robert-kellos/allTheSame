using System.ComponentModel.DataAnnotations;
using AllTheSame.Common.Core;
using AllTheSame.Entity.Model.Metadata;

// ReSharper disable CheckNamespace

namespace AllTheSame.Entity.Model
{
    /// <summary>
    ///     UserSessionMetadata
    /// </summary>
    [MetadataType(typeof (UserSessionMetadata))]
    public partial class UserSession: Entity<int>
    {
    }
}