namespace BaseLibrary.Entities
{
    public class GeneralDepartment : BaseEntity
    {
        // One To Many Relation With Departments
        public List<Department>? Departments { get; set; }
    }
}
