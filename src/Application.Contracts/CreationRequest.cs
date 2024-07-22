using System.Text.Json;

namespace Qx.Workflow.Processor
{
    public record CreationRequest(long WorkflowId, Id_Title Promoter, Id_Title Dept, JsonElement Data);
}