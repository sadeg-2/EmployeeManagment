using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Department:BaseEntity
    {
        // Many To One Relation with General Department
        public GeneralDepartment? GeneralDepartment { get; set; }
        public int GeneralDepartmentId { get; set; }

        // One To Many Relation With Branch
        [JsonIgnore]
        public List<Branch>? Branches { get; set; }

    }
}
