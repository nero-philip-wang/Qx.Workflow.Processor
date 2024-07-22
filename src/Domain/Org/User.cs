using Qx.ApiFx.Domain;

namespace Qx.Workflow.Processor
{
    public class User : FullAuditedEntity<long>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
    }
}