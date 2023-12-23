using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Department : IEntity
{
    public int Id { get; }
    private static int _id;
    public string Name { get; set; }
    public string Description { get; set; }
    public int EmployeeLimit { get; set; }
    public Company CompanyId { get; set; }


    public Department(string name, string description, int employeeLimit, Company companyId)
    {
        Id = _id++;
        Name = name;
        Description = description;
        EmployeeLimit = employeeLimit;
        CompanyId = companyId;
    }
}
