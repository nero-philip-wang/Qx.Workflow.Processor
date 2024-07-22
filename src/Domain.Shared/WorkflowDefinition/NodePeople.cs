using System.Collections.Generic;
using System.Text.Json.Serialization;

public class NodePeople
{
    public IEnumerable<Id_Title> UserId { get; set; }
    public IEnumerable<Id_Title> RoleId { get; set; }
    public int? LeaderLevel { get; set; }
    public string FromDataField { get; set; }

    [JsonIgnore]
    public string DataFieldValue { get; set; }

    [JsonIgnore]
    public long? DeptId { get; set; }
}