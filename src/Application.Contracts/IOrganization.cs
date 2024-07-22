using System.Collections.Generic;
using System.Text.Json;

namespace Qx.Workflow.Processor
{
    public interface IOrganization
    {
        List<Id_Title> GetUser(NodePeople people);
    }
}