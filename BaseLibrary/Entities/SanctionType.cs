namespace BaseLibrary.Entities
{
    public class SanctionType:BaseEntity
    {
        // Many to one Relation with Sanaction
        public List<Sanction>? Sanactions { get; set; }
    }
}
