
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Vacation:OtherBaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int NumberOfDays { get; set; }

        public DateTime EndTime => StartDate.AddDays(NumberOfDays);


        public VacationType? VacationType { get; set; }
        public int VacationTypeId { get; set; }
    }
}
