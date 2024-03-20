namespace ASP.NET_CRUD_Operation_Project.Models.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public long Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}
