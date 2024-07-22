using System.Threading.Tasks;

namespace Qx.Workflow.Processor
{
    public interface IWorkflowProcessor
    {
        Task<long> Create(CreationRequest request);

        Task Approve(ApprovalRequest request);
    }
}