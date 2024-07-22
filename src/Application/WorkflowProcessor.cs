using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Localization;
using Qx.ApiFx.Core;
using Senparc.Weixin.MP.AdvancedAPIs.Card;
using System.Text.Json;
using System.Threading.Tasks;

namespace Qx.Workflow.Processor
{
    [ServiceType(typeof(IWorkflowProcessor))]
    public class WorkflowProcessor : IWorkflowProcessor, IScoped
    {
        private readonly IEfRepository<long, WorkflowInstance> instanceSet;
        private readonly IEfRepository<long, Workflow> workflowSet;
        private readonly IStringLocalizer<WorkflowProcessor> _;
        private readonly IOrganization organization;

        public WorkflowProcessor(IEfRepository<long, WorkflowInstance> instanceSet, IEfRepository<long, Workflow> workflowSet, IStringLocalizer<WorkflowProcessor> _,
            IOrganization organization)
        {
            this.instanceSet = instanceSet;
            this.workflowSet = workflowSet;
            this._ = _;
            this.organization = organization;
        }

        public async Task<long> Create(CreationRequest request)
        {
            Workflow wf;
            try
            {
                wf = workflowSet.Get(request.WorkflowId);
            }
            catch (BusinessException)
            {
                throw new BusinessException(_["工作流不存在"]);
            }

            var instance = wf.CreateInstance(request.Promoter, request.Dept, request.Data, organization);
            instanceSet.Add(instance);
            instanceSet.SaveChanges();
            return instance.Id;
        }

        public async Task Approve(ApprovalRequest request)
        {
            WorkflowInstance instance;
            try
            {
                instance = instanceSet.Get(request.InstanceId);
            }
            catch (BusinessException)
            {
                throw new BusinessException(_["工作流不存在"]);
            }

            instance.Approval(request.Reviewer, request.Action, request.Remark, request.Data, organization);
            instanceSet.Update(instance);
            instanceSet.SaveChanges();
        }
    }
}