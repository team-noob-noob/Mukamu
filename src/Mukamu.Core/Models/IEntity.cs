using System;

namespace Mukamu.Core.Models
{
    /// <summary>Common properties for all Entities</summary>
    public interface IEntity
    {
        /// <summary>Unique identitier of all instances of an entity</summary>
        Guid Id { get; set; }

        /// <summary>Date when the instance is created</summary>
        DateTime CreatedAt { get; set; }

        /// <summary>Date when the instance is updated</summary>
        DateTime UpdatedAt { get; set; }

        /// <summary>Date when the instance is marked as deleted</summary>
        /// <remarks>Serves as a flag if the object is deleted</remarks>
        DateTime? DeletedAt { get; set; }
    }
}
