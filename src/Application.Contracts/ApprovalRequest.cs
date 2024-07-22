using System.Text.Json;

namespace Qx.Workflow.Processor
{
    public record ApprovalRequest(long InstanceId, Id_Title Reviewer, ApprovalAction Action, string Remark, JsonElement Data);
}