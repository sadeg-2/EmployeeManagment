using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Town : BaseEntity
    {
        // One To Many Relation With Employee
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }

        // Many To One Relation with General City
        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
