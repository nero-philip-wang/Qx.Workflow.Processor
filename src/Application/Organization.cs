using Qx.ApiFx.Core;
using System.Collections.Generic;
using System.Linq;

namespace Qx.Workflow.Processor
{
    public class Organization : IOrganization, IScoped
    {
        private readonly IEfRepository<long, User> userSet;
        private readonly IEfRepository<long, Role> roleSet;
        private readonly IEfRepository<long, Dept> deptSet;

        public Organization(IEfRepository<long, User> userSet, IEfRepository<long, Role> roleSet, IEfRepository<long, Dept> deptSet)
        {
            this.userSet = userSet;
            this.roleSet = roleSet;
            this.deptSet = deptSet;
        }

        public List<Id_Title> GetUser(NodePeople people)
        {
            var list = new List<Id_Title>();
            if (people.UserId != null && people.UserId.Any())
            {
                list.AddRange(people.UserId);
            }
            if (people.RoleId != null && people.RoleId.Any())
            {
                var idSet = people.RoleId.Select(x => x.Id);
                var roleUsers = roleSet
                    .Where(c => idSet.Contains(c.Id))
                    .SelectMany(c => c.Members.Select(d => new Id_Title()
                    {
                        Id = d.Id,
                        Title = d.Title
                    }))
                    .ToArray();
                list.AddRange(roleUsers);
            }
            if (people.LeaderLevel.HasValue)
            {
                var dept = deptSet.Get(people.DeptId.Value);
                for (int i = 0; i < people.LeaderLevel; i++)
                {
                    dept = dept.Parent ?? dept;
                }
                if (dept.Manager.IsNotNull())
                {
                    list.Add(new Id_Title()
                    {
                        Id = dept.Manager.Id,
                        Title = dept.Manager.Title,
                    });
                }
            }
            if (people.DataFieldValue != null)
            {
                var idset = people.DataFieldValue.Split(",", System.StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.As<long>());
                var users = userSet
                    .Where(c => idset.Contains(c.Id))
                    .Select(c => new Id_Title()
                    {
                        Id = c.Id,
                        Title = c.Title
                    })
                    .ToArray();
                list.AddRange(users);
            }

            return list;
        }
    }
}