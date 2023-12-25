using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Contexts;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentService
{
    private ICompanyService companyService { get; }
    public DepartmentService()
    {
        companyService = new CompanyService();
    }
    public void AddEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public void Create(string name, string description, int employeeLimit, int companyId)
    {

        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(); // aid oldugu company gore unique olmalidir
        foreach (var departments in HrDbContext.Departments)  // yuz faiz problem cixacaq console-da yoxla
        {
            if (departments.Company.Id == companyId)
            {
                if (Equals(name, departments))
                {
                    throw new AlreadyExistException($"A department with {name} name exist within the company with {companyId} ID.");
                }
            }
        }
        if (employeeLimit < 3) throw new MinNumEmployeeException($"Department should at least contain 3 employees.");
        Company? company = companyService.FindCompanyById(companyId);
        if (company is null) throw new NotFoundException($"Company with {companyId} does not exist.");
        Department department = new(name, description, employeeLimit, company);
        HrDbContext.Departments.Add(department);
    }

    public Department? GetDepartmentById(int departmentId)
    {
        if (departmentId < 0) 
            throw new ArgumentOutOfRangeException();  // show alldan sonra sechim verer menuda
        Department? dbDepartment = 
            HrDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null) 
            throw new NotFoundException($"Department with {departmentId} ID does not exist.");
        return dbDepartment;
    }

    public void GetDepartmentEmployees(string departmentName)
    {
        throw new NotImplementedException();
    }

    public void ShowDepartmentsInCompany(Company company)
    {
        throw new NotImplementedException();
    }

    public void UpdateDepartment(string newDepartmentName, int newEmployeeLimit)
    {
        throw new NotImplementedException();
    }

}
