using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Contexts;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentService
{
    private ICompanyService companyService { get; }
    public DepartmentService()
    {
        companyService = new CompanyService();
    }
    public void AddEmployee(Employee employee, int departmentId) 
    {
        Department? dbDepartment = 
            HrDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null) 
            throw new NotFoundException($"Department with {departmentId} ID is not found.");
        if (dbDepartment is not null)
        {
            if (dbDepartment.CurrentEmployeeCount >= dbDepartment.EmployeeLimit)
                throw new CapacityLimitException($"Employee can not be add to this department as department is already full.\n" +
                                                 $"Currently, the number of employees is: {dbDepartment.CurrentEmployeeCount}.");
            if (dbDepartment.CurrentEmployeeCount < dbDepartment.EmployeeLimit)
            {
                employee.DepartmentId = dbDepartment;
                dbDepartment.CurrentEmployeeCount++;
            }
        }
    }

    public void Create(string? name, string? description, int employeeLimit, int companyId)
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

    public void GetDepartmentEmployees(int departmentId)
    {
        if (departmentId < 0) throw new ArgumentOutOfRangeException();
        var dbDepartmentId = 
            HrDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartmentId is null) 
            throw new NotFoundException($"Department with {departmentId} ID is not found.");
        if (dbDepartmentId is not null)
        {
            foreach (var employee in HrDbContext.Employees)
            {
                if (employee.IsActive == true && Equals(dbDepartmentId,departmentId))
                {
                    Console.WriteLine($"Employee ID: {employee.Id}; " +
                                      $"Employee name: {employee.Name}; " +
                                      $"Employee surname: {employee.Surname}" +
                                      $"Employee salary: {employee.Salary}");
                }
            }
        }   
    }

    public void ShowDepartmentsInCompany(int companyId)
    {
        foreach(var department in HrDbContext.Departments)
        {
            if (department.Company.Id == companyId && department.IsActive == true)
            {
                Console.WriteLine($"Department ID: {department.Id}; Department Name: {department.Name}");
            }
        }
    }
    
    public void UpdateDepartment(int departmentId, string? newDepartmentName, int newEmployeeLimit)
    {
        var dbDepartment = GetDepartmentById(departmentId);
        if (string.IsNullOrEmpty(newDepartmentName)) throw new ArgumentNullException();
        foreach (var departments in HrDbContext.Departments)  
        {
            if (departments.Company.Id == dbDepartment.Company.Id)
            {
                if (Equals(newDepartmentName, departments))
                {
                    throw new AlreadyExistException($"A department with {newDepartmentName} name exist within the company.");
                }
            }
        }
        if (newEmployeeLimit < dbDepartment.EmployeeLimit) 
            throw new MinNumEmployeeException($"New employee limit should be higher than previous employee limit count in order to update department.");
        else
        {
            dbDepartment.Name = newDepartmentName;
            dbDepartment.EmployeeLimit = newEmployeeLimit;
        }
        //if (departmentId < 0) throw new ArgumentOutOfRangeException();
        //Department? department = 
        //    HrDbContext.Departments.Find(d => d.Id == departmentId);
        //if (department is null) 
        //    throw new NotFoundException($"Department with {departmentId} ID is not found.");
    }

}
