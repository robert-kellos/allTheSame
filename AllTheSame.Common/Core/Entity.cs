using System;
using System.Data.Entity;
using AllTheSame.Common.Interfaces.Generic;

namespace AllTheSame.Common.Core
{
    /// <summary>
    /// </summary>
    public interface IBaseEntity
    {
    }

    /// <summary>
    /// </summary>
    [Serializable]
    public abstract class BaseEntity : IBaseEntity
    {
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    [Serializable]
    public abstract class Entity<TType> : BaseEntity, IEntity<TType> where TType : struct
    {
        /// <summary>
        ///     Gets or sets the state of the entity.
        /// </summary>
        /// <value>
        ///     The state of the entity.
        /// </value>
        public virtual EntityState EntityState { get; set; }

        /// <summary>
        ///     Gets or sets the entity identifier.
        /// </summary>
        /// <value>
        ///     The entity identifier.
        /// </value>
        public virtual TType Id { get; set; }

        /// <summary>
        ///     Gets or sets the last synchronize identifier.
        /// </summary>
        /// <value>
        ///     The last synchronize identifier.
        /// </value>
        public virtual int LastSyncId { get; set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        public virtual byte[] Version { get; set; }

        /// <summary>
        ///     CreatedOn
        /// </summary>
        public virtual DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     UpdatedOn
        /// </summary>
        public virtual DateTime? UpdatedOn { get; set; }
    }
}