using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class OverTime:OtherBaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        public int NumberOfDays => (EndTime - StartDate).Days;

        // Many To One Relation With Vacation Type
        public OverTimeType? OverTimeType { get; set; }
        [Required] 
        public int OverTimeTypeId { get; set; }
    }
}
