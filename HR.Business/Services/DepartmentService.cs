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
    public void Create(string? name, string? description, int employeeLimit, int companyId)
    {
        if (String.IsNullOrEmpty(name)) throw new ArgumentNullException();
        if (employeeLimit < 3) throw new MinLimitEmployeeException($"Department should at least contain 3 employees at its maximum.");
        Company? company = companyService.FindCompanyById(companyId);
        if (company is null) throw new NotFoundException($"Company with {companyId} ID does not exist.");
        if (company is not null)
        {
            foreach (var departments in HrDbContext.Departments)
            {
                if (departments.CompanyId.Id == companyId && name.ToLower() == departments.Name.ToLower())
                    throw new AlreadyExistException($"A department with {name} name exist within the company with {companyId} ID.");
            }
        }
        Department department = new(name, description, employeeLimit, company);
        HrDbContext.Departments.Add(department);
    }
    public void AddEmployee(int employeeId, int departmentId)  // bura ele etmek olar ki eger ishci harasa aiddise onda add edilmesin
    {
        Employee? employee =
            HrDbContext.Employees.Find(e => e.Id == employeeId);
        if (employee is null)
            throw new NotFoundException($"Employee with {employeeId} ID is not found.");
        Department? dbDepartment =
            HrDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department with {departmentId} ID is not found.");
        if (dbDepartment is not null && dbDepartment.CurrentEmployeeCount >= dbDepartment.EmployeeLimit)
            throw new CapacityLimitException($"Employee can not be add to this department as department is already full.\n" +
                                                 $"Currently, the number of employees is: {dbDepartment.CurrentEmployeeCount}.");
        foreach(var employees in HrDbContext.Employees)
        {
            if (employees.Id == employeeId && employee.DepartmentId is not null)
                throw new AlreadyEmployeedException($"Employee with {employeeId} ID is already employed in {employee.DepartmentId.Name} department.");
            if (employees.Id == employeeId && employee.DepartmentId is null)
            {
                employee.DepartmentId = dbDepartment;
                dbDepartment.CurrentEmployeeCount++;
            }
        } 
    }

    public Department? GetDepartmentById(int departmentId)
    {
        Department? dbDepartment =
            HrDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department with {departmentId} ID does not exist.");
        return dbDepartment;
    }

    public void GetDepartmentEmployees(string? departmentName)
    {
        Department? dbDepartment =
            HrDbContext.Departments.Find(d => d.Name.ToLower() == departmentName.ToLower() && d.IsActive is true);
        if (dbDepartment is null)
            throw new NotFoundException($"Department with {departmentName} name is not found.");
        foreach (var employee in HrDbContext.Employees)
        {
            if (employee.IsActive == true)
            {
                if (employee.DepartmentId is null) continue;
                if (dbDepartment.Id == employee.DepartmentId.Id) 
                {
                    Console.WriteLine($"Employee ID: {employee.Id};\n" +
                                      $"Employee name: {employee.Name};\n" +
                                      $"Employee surname: {employee.Surname}\n" +
                                      $"Employee salary: {employee.Salary}\n" +
                                      $"Department name: {employee.DepartmentId.Name}\n" +
                                      $"Company name: {employee.DepartmentId.CompanyId.Name}\n" +
                                      $"----------------------------------------");
                }
            }
        }
    }

    public void UpdateDepartment(int departmentId, string? newDepartmentName, int newEmployeeLimit)
    {
        if (String.IsNullOrEmpty(newDepartmentName)) throw new ArgumentNullException();
        Department? dbDepartment = GetDepartmentById(departmentId);
        if (dbDepartment is null) throw new NotFoundException($"Department with {departmentId} ID is not found.");
        if (dbDepartment is not null)
        {
            foreach (var departments in HrDbContext.Departments)
            {
                if (departments.CompanyId.Id == departmentId && newDepartmentName.ToLower() == departments.Name.ToLower())
                    throw new AlreadyExistException($"A department with {newDepartmentName} name exist within the company.");
            }
        }
        if (newEmployeeLimit <= dbDepartment.CurrentEmployeeCount)
            throw new MinLimitEmployeeException($"New employee limit should be higher than current number of employees in order to update department.");
        dbDepartment.Name = newDepartmentName;
        dbDepartment.EmployeeLimit = newEmployeeLimit;

    }

    public void ShowAllDepartments()
    {
        foreach (var department in HrDbContext.Departments)
        {
            if (department.IsActive == true)
            {
                Console.WriteLine($"ID: {department.Id};\n" +
                                  $"Name: {department.Name};\n" +
                                  $"Company ID: {department.CompanyId.Id};\n" +
                                  $"Company Name: {department.CompanyId.Name}\n" +
                                  $"------------------------------------------- ");
            }
        }
    }

    public void DeleteDepartment(int departmentId)
    {
        Department? dbDepartment =
            HrDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department with {departmentId} ID is not found.");
        if (dbDepartment is not null)
        {
            foreach (var employees in HrDbContext.Employees)
            {
                if (employees.IsActive == true && employees.DepartmentId.Id == departmentId)
                    throw new NotAllowedDeleteException($"Department with {departmentId} ID can not be deleted as it contains employees.");
            }
        }
        dbDepartment.IsActive = false;
    }

    public bool CheckExistence()
    {
        foreach (var department in HrDbContext.Departments)
        {
            if (department.IsActive == true)
            {
                return true;
            }
        }
        return false;
    }

    public void FireEmployee(int employeeId, int departmentId)
    {
        Employee? dbEmployee =
            HrDbContext.Employees.Find(e => e.Id == employeeId);
        if (dbEmployee is null)
            throw new NotFoundException($"Employee with {employeeId} ID is not found.");
        Department? dbDepartment =
            HrDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null)
            throw new NotFoundException($"Department with {departmentId} ID is not found.");
        if (dbEmployee is not null && dbDepartment is not null)
        {
            if (dbEmployee.IsActive == true && dbDepartment.IsActive == true)
            {
                if (dbEmployee.DepartmentId.Id == departmentId)
                {
                    dbEmployee.IsActive = false;
                    dbDepartment.CurrentEmployeeCount--;
                }
                else
                {
                    Console.WriteLine($"Employee with {employeeId} is not found within department.");
                }
            }
        }
    }
}
