using Qx.ApiFx.Core;
using Qx.ApiFx.Domain;
using System.Collections.Generic;

public class WorkflowNode : Entity<long>
{
    public long WorkflowId { get; set; }
    public NodeFlag Flag { get; set; }
    public string Title { get; set; }

    public NodePeople? People { get; set; }

    public SignMethod SignMethod { get; set; }
    public float PassRate { get; set; }
    public List<WorkflowNextStep>? NextStep { get; set; }
}