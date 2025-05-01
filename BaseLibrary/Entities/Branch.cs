
namespace BaseLibrary.Entities
{
    public class Branch : BaseEntity
    {
        // Many To One RelationShip with Department
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }

        // One To Many Relation With Employee
        public List<Employee>? Employees { get; set; }
    }
}
