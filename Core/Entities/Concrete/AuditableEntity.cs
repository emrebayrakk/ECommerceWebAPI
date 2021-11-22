using System;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class AuditableEntity:BaseEntity,ICreatedEntity,IUpdatedEntity
    {
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
