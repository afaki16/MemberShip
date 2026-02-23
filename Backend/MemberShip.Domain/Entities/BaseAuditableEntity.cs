using System;

namespace MemberShip.Domain.Entities
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
} 
