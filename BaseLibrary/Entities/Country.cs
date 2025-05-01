namespace BaseLibrary.Entities
{
    public class Country:BaseEntity
    {
        // One To Many Relation With City
        public List<City>? Cities { get; set; }
    }
}
