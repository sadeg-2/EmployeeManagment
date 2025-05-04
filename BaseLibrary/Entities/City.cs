
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class City:BaseEntity
    {
        // Many To One Relation with General Country
        public Country? Country { get; set; }
        public int CountryId { get; set; }

        // One To Many Relation With Town
        [JsonIgnore]
        public List<Town>? Towns { get; set; }
    }
}
