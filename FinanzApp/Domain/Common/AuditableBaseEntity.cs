using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int LasModifiedBy { get; set; }
        public DateTime? LasModified { get; set; }
    }
}
