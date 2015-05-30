using System;

namespace AllTheSame.Common.Interfaces.Generic
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    public interface IEntity<TType> where TType : struct
    {
        /// <summary>
        ///     Gets or sets the entity identifier.
        /// </summary>
        /// <value>
        ///     The entity identifier.
        /// </value>
        TType Id { get; set; }

        /// <summary>
        ///     Gets or sets the state of the entity.
        /// </summary>
        /// <value>
        ///     The state of the entity.
        /// </value>
        //EntityState EntityState { get; set; }
        byte[] Version { get; set; }

        /// <summary>
        ///     Gets or sets the last synchronize identifier.
        /// </summary>
        /// <value>
        ///     The last synchronize identifier.
        /// </value>
        int LastSyncId { get; set; }

        /// <summary>
        ///     CreatedOn
        /// </summary>
        DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     UpdatedOn
        /// </summary>
        DateTime? UpdatedOn { get; set; }
    }
}