
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Sanction : OtherBaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Punishment { get; set; } = string.Empty;

        [Required]
        public DateTime PunishmentDate { get; set; }

        // Many to one with Sanaction Type
        public SanctionType? SanactionType { get; set; }



    }
}
