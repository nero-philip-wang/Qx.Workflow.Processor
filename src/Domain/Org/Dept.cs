using Qx.ApiFx.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Workflow.Processor
{
    public class Dept : FullAuditedEntity<long>
    {
        /// <summary>
        /// long 最大为 9,223,372,036,854,775,807，共18位半
        /// 前2位半为商户号我，范围 100-900
        /// 后每两位为一个级别编码，范围 10-99 ，最大支持8级分支机构,
        /// *00-00-00-00-00-00-00-00-00
        /// </summary>
        public override long Id { get; set; }

        public long ParentId { get; set; }
        public virtual Dept Parent { get; set; }
        public string Title { get; set; }
        public long? ManagerId { get; set; }
        public virtual User Manager { get; set; }
        public virtual List<User> Members { get; set; }
    }
}