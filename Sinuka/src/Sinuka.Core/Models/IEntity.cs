using System;

namespace Sinuka.Core.Models
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        DateTime DeletedAt { get; set; }
    }
}

//sample