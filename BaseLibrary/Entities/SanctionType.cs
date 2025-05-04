using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class SanctionType:BaseEntity
    {
        // Many to one Relation with Sanaction
        [JsonIgnore]
        public List<Sanction>? Sanactions { get; set; }
    }
}
