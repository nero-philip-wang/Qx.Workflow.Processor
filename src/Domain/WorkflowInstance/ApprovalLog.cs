using Qx.ApiFx.Domain;
using System;

namespace Qx.Workflow.Processor
{
    public class ApprovalLog : Entity<long>
    {
        public long WorkflowInstanceId { get; set; }
        public long NodeId { get; set; }
        public string NodeTitle { get; set; }
        public long OperatorId { get; set; }
        public string OperatorTitle { get; set; }
        public ApprovalAction? Action { get; set; }
        public DateTime? OperatorTime { get; set; }
        public string? Remark { get; set; }

        public object? ExtraData { get; set; }

        public bool IsTodo { get; set; }
    }
}