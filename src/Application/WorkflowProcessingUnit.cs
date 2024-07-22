using Qx.ApiFx.Core;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace Qx.Workflow.Processor
{
    public static class WorkflowProcessingUnit
    {
        public static WorkflowInstance CreateInstance(this Workflow workflow, Id_Title promoter, Id_Title dept, JsonElement data, IOrganization org)
        {
            var node = workflow.Nodes.First(c => c.Flag == NodeFlag.Start);
            var instance = new WorkflowInstance()
            {
                //Id = 1,
                TransactionNo = DateTime.Now.ToFileTime().ToString(),
                Workflow = workflow,
                WorkflowTitle = workflow.Title,
                WorkflowId = workflow.Id,
                CurrentNode = node,
                Status = ApprovalAction.Submit,
                PromoterDept = dept,
                Promoter = promoter,
                ApprovalLogs = new List<ApprovalLog>()
                {
                    new ApprovalLog()
                    {
                         //Id = 1,
                         //WorkflowInstanceId = 1,
                         Action = ApprovalAction.Submit,
                         NodeId = node.Id,
                         NodeTitle = node.Title,
                         OperatorId = promoter.Id,
                         OperatorTitle = promoter.Title,
                         OperatorTime = DateTime.Now,
                         ExtraData = data,
                         IsTodo = false,
                    }
                }
            };

            instance.AppendNextNode(data, org);
            return instance;
        }

        public static WorkflowInstance Approval(this WorkflowInstance instance, Id_Title reviewer, ApprovalAction action, string remark, JsonElement data, IOrganization org)
        {
            if (instance.Status != ApprovalAction.Submit || instance.CurrentNode == null)
            {
                return instance;
            }

            var allowedUser = instance.ApprovalLogs.Where(c => c.IsTodo);
            if (!allowedUser.Any(c => c.OperatorId == reviewer.Id))
            {
                return instance;
            }

            var currentNode = instance.CurrentNode;
            var currentLogs = instance.ApprovalLogs.Where(c => c.NodeId == instance.CurrentNode.Id);
            if (action == ApprovalAction.Reject)
            {
                var log = currentLogs.First(c => c.OperatorId == reviewer.Id);
                log.ExtraData = data;
                log.OperatorTime = DateTime.Now;
                log.Action = action;

                instance.End(ApprovalAction.Reject);
            }
            else if (action == ApprovalAction.Approval)
            {
                var log = currentLogs.First(c => c.OperatorId == reviewer.Id);
                log.ExtraData = data;
                log.OperatorTime = DateTime.Now;
                log.Action = action;
                log.IsTodo = false;
                log.Remark = remark;

                var passed = false;
                switch (currentNode.SignMethod)
                {
                    case SignMethod.Na:
                        passed = true;
                        break;

                    case SignMethod.And:
                        passed = currentLogs.All(c => c.Action == ApprovalAction.Approval);
                        break;

                    case SignMethod.Or:
                        passed = currentLogs.Any(c => c.Action == ApprovalAction.Approval);
                        break;

                    case SignMethod.Vote:
                        passed = (100f * currentLogs.Count(c => c.Action == ApprovalAction.Approval) / currentLogs.Count()) >= currentNode.PassRate;
                        break;
                }
                if (passed)
                {
                    foreach (var l in currentLogs)
                    {
                        l.IsTodo = false;
                    }
                    instance.AppendNextNode(data, org);
                }
            }

            return instance;
        }

        private static WorkflowNode GetNextNode(this WorkflowInstance instance, JsonElement data)
        {
            var array = new object[] { data is JsonElement ? new JsonDynamicObject((JsonElement)data) : data };
            var current = instance.CurrentNode;
            var next = current.NextStep.FirstOrDefault(c => c.Condition?.Function != null && array.AnyDynamic(x => c.Condition.Function));
            if (next == null) next = current.NextStep.First(c => c.Condition?.Function == null);
            var nextNode = instance.Workflow.Nodes.First(c => c.Title == next.NextNodeTitle);
            return nextNode;
        }

        private static NodePeople Fill(this NodePeople people, JsonElement data, Id_Title dept)
        {
            if (people.FromDataField.IsNotNull())
            {
                people.DataFieldValue = data.GetProperty(people.FromDataField).GetString();
            }
            people.DeptId = dept.Id;
            return people;
        }

        private static WorkflowInstance AppendNextNode(this WorkflowInstance instance, JsonElement data, IOrganization org)
        {
            var next = instance.GetNextNode(data);
            if (next.Flag == NodeFlag.End)
            {
                instance.End(ApprovalAction.Approval);
                return instance;
            }
            var nextPeople = org.GetUser(next.People.Fill(data, instance.PromoterDept));
            instance.ApprovalLogs.AddRange(nextPeople.Select(c => new ApprovalLog()
            {
                //Id = 10,
                WorkflowInstanceId = 1,
                NodeId = next.Id,
                NodeTitle = next.Title,
                OperatorId = c.Id,
                OperatorTitle = c.Title,
                IsTodo = true,
            }));
            instance.CurrentNode = next;
            return instance;
        }

        private static WorkflowInstance End(this WorkflowInstance instance, ApprovalAction action)
        {
            var currentNode = instance.CurrentNode;
            var currentLogs = instance.ApprovalLogs.Where(c => c.NodeId == instance.CurrentNode.Id);
            foreach (var log in currentLogs)
            {
                log.IsTodo = false;
            }

            instance.Status = action;
            instance.CurrentNode = null;
            instance.ProcessEndTime = DateTime.Now;
            return instance;
        }
    }
}