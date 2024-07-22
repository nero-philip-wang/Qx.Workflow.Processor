using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Qx.ApiFx.Core;
using Senparc.NeuChar.App.AppStore.Api;
using System.Collections.Generic;
using System.Linq;

namespace Qx.Workflow.Processor.Processor.Controllers
{
    [Area("base")]
    [ApiVersion("1.0")]
    public class WorkflowsController : Qx.ApiFx.Mvc.ApiBaseController
    {
        private readonly IEfRepository<long, Workflow> workflowSet;

        public WorkflowsController(IEfRepository<long, Workflow> workflowSet)
        {
            this.workflowSet = workflowSet;
        }

        [HttpGet("{id}")]
        public Workflow Get([FromRoute] long id)
        {
            return workflowSet.Get(id);
        }

        [HttpGet]
        public IEnumerable<Workflow> List()
        {
            return workflowSet.ToList();
        }

        [HttpPost]
        public void Add(Workflow workflow)
        {
            workflowSet.Add(workflow, true);
        }

        [HttpPut]
        public void Update(Workflow workflow)
        {
            workflowSet.Update(workflow, true);
        }

        [HttpDelete("{id}")]
        public void Remove(long id)
        {
            workflowSet.Remove(workflowSet.Get(id));
        }
    }
}