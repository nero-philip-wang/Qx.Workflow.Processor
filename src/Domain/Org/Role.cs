using Qx.ApiFx.Domain;
using System.Collections.Generic;

namespace Qx.Workflow.Processor
{
    public class Role : FullAuditedEntity<long>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public virtual List<User> Members { get; set; }
    }
}