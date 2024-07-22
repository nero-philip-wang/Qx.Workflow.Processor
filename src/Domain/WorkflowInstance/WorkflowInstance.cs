using Microsoft.EntityFrameworkCore;
using Qx.ApiFx.Domain;
using System;
using System.Collections.Generic;

namespace Qx.Workflow.Processor
{
    public class WorkflowInstance : AuditedAggregateRoot<long>
    {
        public string TransactionNo { get; set; }
        public long WorkflowId { get; set; }
        public string WorkflowTitle { get; set; }

        public Workflow Workflow { get; set; }

        public WorkflowNode? CurrentNode { get; set; }

        public ApprovalAction Status { get; set; }
        public virtual List<ApprovalLog> ApprovalLogs { get; set; }
        public DateTime? ProcessEndTime { get; set; }
        public Id_Title Promoter { get; set; }
        public Id_Title PromoterDept { get; set; }
    }
}