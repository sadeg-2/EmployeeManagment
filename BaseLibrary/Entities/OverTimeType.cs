using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class OverTimeType:BaseEntity
    {
        [JsonIgnore]
        public List<OverTime> OverTimes { get; set; }
    }
}
