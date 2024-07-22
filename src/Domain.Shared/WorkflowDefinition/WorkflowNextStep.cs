using System.ComponentModel;

public class WorkflowNextStep
{
    public long WorkflowNodeId { get; set; }

    public StepCondition Condition { get; set; }

    public string NextNodeTitle { get; set; }
}