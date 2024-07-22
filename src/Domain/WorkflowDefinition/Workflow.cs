using Qx.ApiFx.Domain;
using System.Collections.Generic;

namespace Qx.Workflow.Processor
{
    public class Workflow : FullAuditedAggregateRoot<long>
    {
        public string Title { get; set; }
        public string Catalog { get; set; }
        public virtual List<WorkflowNode> Nodes { get; set; }
    }
}