using Microsoft.Extensions.DependencyInjection;
using Qx.ApiFx.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace Qx.Workflow.Processor
{
    public class WorkflowProcessorTest : IntegrationTest
    {
        [Fact]
        public void GetOrCreateWf()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            base.TestPack(s =>
            {
            },
            app =>
            {
                var db = app.ApplicationServices.GetService<IEfRepository<long, Workflow>>();

                var workflow = new Workflow()
                {
                    Catalog = "EA",
                    Title = "报价审核",
                    Nodes = new List<WorkflowNode>() {
                        new WorkflowNode()
                        {
                             WorkflowId = 1,
                             Title = "发起人",
                             Flag = NodeFlag.Start,
                             People = null,
                             SignMethod = SignMethod.Na,
                             NextStep = new List<WorkflowNextStep>()
                             {
                                 new WorkflowNextStep()
                                 {
                                     WorkflowNodeId = 1,
                                     NextNodeTitle = "经理确认",
                                     Condition = new StepCondition()
                                     {
                                         Function = "x.amount >= 50000",
                                     }
                                 },
                                 new WorkflowNextStep()
                                 {
                                     WorkflowNodeId = 1,
                                     NextNodeTitle = "结束",
                                     Condition = null
                                 },
                             }
                        },
                        new WorkflowNode()
                        {
                             WorkflowId = 1,
                             Title = "经理确认",
                             Flag = NodeFlag.Process,
                             People = new NodePeople() { UserId = new []{ new Id_Title() {  Id = 11, Title = "经理" } } },
                             SignMethod = SignMethod.And,
                             NextStep = new List<WorkflowNextStep>()
                             {
                                 new WorkflowNextStep()
                                 {
                                     WorkflowNodeId = 1,
                                     NextNodeTitle = "结束",
                                     Condition = null
                                 },
                             }
                        },
                        new WorkflowNode()
                        {
                             WorkflowId = 1,
                             Title = "结束",
                             Flag = NodeFlag.End,
                             People = null,
                        },
                    }
                };
                db.Add(workflow, true);

                Assert.NotEqual(workflow.Id, 0);
            });
        }

        [Fact]
        public void TestCreate()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            base.TestPack(s =>
            {
            },
            app =>
            {
                var p = app.ApplicationServices;
                var processor = p.GetService<IWorkflowProcessor>();
                var wfset = p.GetService<IEfRepository<long, Workflow>>();

                var data = JsonSerializer.SerializeToElement(new { amount = 150000 });
                var promoter = new Id_Title() { Id = 1212, Title = "徐爱" };
                var dept = new Id_Title() { Id = 1212, Title = "徐爱" };

                var wf = processor.Create(new CreationRequest(wfset.Select(c => c.Id).First(), promoter, dept, data));

                processor.Approve(new ApprovalRequest(wf.Id, new Id_Title() { Id = 11, Title = "经理" }, ApprovalAction.Approval, "统一", data));
            });
        }
    }
}