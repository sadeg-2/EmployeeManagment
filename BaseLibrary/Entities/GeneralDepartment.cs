using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class GeneralDepartment : BaseEntity
    {
        // One To Many Relation With Departments
        [JsonIgnore]
        public List<Department>? Departments { get; set; }
    }
}
