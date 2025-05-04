using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Country:BaseEntity
    {
        // One To Many Relation With City
        [JsonIgnore]
        public List<City>? Cities { get; set; }
    }
}
