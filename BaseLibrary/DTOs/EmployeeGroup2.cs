
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class EmployeeGroup2
    {
        [Required]
        public string JobName { get; set; } = string.Empty;

        [Required,Range(1,99999,ErrorMessage ="You Must Select Branch")]
        public int BranchId { get; set; }
        [Required, Range(1, 99999, ErrorMessage = "You Must Select Town")]
        public int TownId { get; set; }

        [Required]
        public string? Other { get; set; } 
    }
}
