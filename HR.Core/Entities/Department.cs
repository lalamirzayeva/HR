using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Department : IEntity
{
    public int Id { get; }
    private static int _id;
    public string Name { get; set; }
    public string Description { get; set; }
    public int EmployeeLimit { get; set; }
    public int CurrentEmployeeCount { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public Company CompanyId { get; set; }
    public Company Company { get; set; }  // bu mene lazimdir? axirda bax


    public Department(string name, string description, int employeeLimit, Company companyId)
    {
        Id = _id++;
        Name = name;
        Description = description;
        EmployeeLimit = employeeLimit;
        CompanyId = companyId;
        IsActive = true;
        IsDeleted = false;
    }
}
