using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Qx.ApiFx.Core;
using Senparc.NeuChar.App.AppStore.Api;
using System.Linq;
using System.Threading.Tasks;

namespace Qx.Workflow.Processor
{
    [Area("process")]
    [ApiVersion("1.0")]
    public class WorkflowController : Qx.ApiFx.Mvc.ApiBaseController
    {
        private readonly IWorkflowProcessor processor;

        public WorkflowController(IWorkflowProcessor processor)
        {
            this.processor = processor;
        }

        [HttpPost("approve")]
        public Task Approve([FromBody] ApprovalRequest request)
        {
            return processor.Approve(request);
        }

        [HttpPost("create")]
        public Task<long> Create([FromBody] CreationRequest request)
        {
            return processor.Create(request);
        }
    }
}