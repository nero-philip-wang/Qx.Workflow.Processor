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
                    Title = "�������",
                    Nodes = new List<WorkflowNode>() {
                        new WorkflowNode()
                        {
                             WorkflowId = 1,
                             Title = "������",
                             Flag = NodeFlag.Start,
                             People = null,
                             SignMethod = SignMethod.Na,
                             NextStep = new List<WorkflowNextStep>()
                             {
                                 new WorkflowNextStep()
                                 {
                                     WorkflowNodeId = 1,
                                     NextNodeTitle = "����ȷ��",
                                     Condition = new StepCondition()
                                     {
                                         Function = "x.amount >= 50000",
                                     }
                                 },
                                 new WorkflowNextStep()
                                 {
                                     WorkflowNodeId = 1,
                                     NextNodeTitle = "����",
                                     Condition = null
                                 },
                             }
                        },
                        new WorkflowNode()
                        {
                             WorkflowId = 1,
                             Title = "����ȷ��",
                             Flag = NodeFlag.Process,
                             People = new NodePeople() { UserId = new []{ new Id_Title() {  Id = 11, Title = "����" } } },
                             SignMethod = SignMethod.And,
                             NextStep = new List<WorkflowNextStep>()
                             {
                                 new WorkflowNextStep()
                                 {
                                     WorkflowNodeId = 1,
                                     NextNodeTitle = "����",
                                     Condition = null
                                 },
                             }
                        },
                        new WorkflowNode()
                        {
                             WorkflowId = 1,
                             Title = "����",
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
                var promoter = new Id_Title() { Id = 1212, Title = "�찮" };
                var dept = new Id_Title() { Id = 1212, Title = "�찮" };

                var wf = processor.Create(new CreationRequest(wfset.Select(c => c.Id).First(), promoter, dept, data));

                processor.Approve(new ApprovalRequest(wf.Id, new Id_Title() { Id = 11, Title = "����" }, ApprovalAction.Approval, "ͳһ", data));
            });
        }
    }
}