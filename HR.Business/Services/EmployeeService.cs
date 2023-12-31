using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Contexts;

namespace HR.Business.Services;

public class EmployeeService : IEmployeeService
{
    private IDepartmentService departmentService {  get;  }
    public EmployeeService()
    {
        departmentService = new DepartmentService();
    }
    public void Create(string? name, string? surname, string? email, int salary)
    {
        if (String.IsNullOrEmpty(name)) throw new ArgumentNullException();
        if (salary < 345) 
            throw new MinWageException($"Minimum amount of wage should be 345 manats according to the legislation.");
        Employee employee = new(name, surname, email, salary);
        HrDbContext.Employees.Add(employee);
    }

    public void Active(int id)
    {
        Employee? employee = 
            HrDbContext.Employees.Find(e => e.Id == id);
        if (employee is null) 
            throw new NotFoundException($"Employee with {id} ID is not found.");
        employee.IsActive = true;
    }
    public void ChangeDepartment(int employeeId, int newDepartmentId)
    {
        Employee? dbEmployee = 
            HrDbContext.Employees.Find(e => e.Id == employeeId && e.IsActive is true);
        if (dbEmployee is null) 
            throw new NotFoundException($"Employee with {employeeId} ID is not found.");
        Department? dbDepartment = 
            HrDbContext.Departments.Find(d => d.Id == newDepartmentId && d.IsActive == true);
        if (dbDepartment is null) 
            throw new NotFoundException($"Department with {newDepartmentId} ID is not found.");
        if (dbDepartment.CurrentEmployeeCount >= dbDepartment.EmployeeLimit)
            throw new CapacityLimitException($"Can not transfer as department with {dbDepartment.Id} ID is already full.");
        dbEmployee.DepartmentId.CurrentEmployeeCount--;
        dbEmployee.DepartmentId = dbDepartment;
        dbDepartment.CurrentEmployeeCount++;
    }

    public void ShowAll()
    {
        foreach (var employee in HrDbContext.Employees)
        {
            if (employee.IsActive == true)
            {
                Console.WriteLine($"Employee ID: {employee.Id};\n" +
                                  $"Name: {employee.Name};\n" +
                                  $"Surname: {employee.Surname};\n" +
                                  $"Employee salary: {employee.Salary};\n" +
                                  $"----------------------------------");
            }
        }
    }
    public bool CheckExistence()
    {
        foreach (var employee in HrDbContext.Employees)
        {
            if (employee.IsActive == true)
            {
                return true;
            }
        }
        return false;
    }

    public void UpgradeEmployee(int employeeId, int newSalaryAmount)
    {
        Employee? employee = 
            HrDbContext.Employees.Find(e =>e.Id == employeeId && e.IsActive is true);
        if (employee is null) 
            throw new NotFoundException($"Employee with {employeeId} ID is not found.");
        if (newSalaryAmount <= employee.Salary) 
            throw new UpgradeNotAllowed("New salary amount can not be less than previous salary amount in order to upgrade employee's salary.");
        employee.Salary = newSalaryAmount;
    }

    public void DowngradeEmployee(int employeeId, int newSalaryAmount)
    {
        Employee? employee =
            HrDbContext.Employees.Find(e => e.Id == employeeId && e.IsActive is true);
        if (employee is null)
            throw new NotFoundException($"Employee with {employeeId} ID is not found.");
        if (newSalaryAmount >= employee.Salary)
            throw new UpgradeNotAllowed("New salary amount can not be higher than previous salary amount in order to downgrade employee's salary.");
        if (newSalaryAmount < 345)
            throw new MinWageException($"Salary amount can not be less than 345 manat according to the legislation.");
        employee.Salary = newSalaryAmount;
    }

    public void ShowInactiveEmployees()
    {
        foreach(var employee in HrDbContext.Employees)
        {
            if(employee.IsActive is false)
                Console.WriteLine($"Employee ID: {employee.Id}\n" +
                                  $"Employee name: {employee.Name}\n" +
                                  $"Employee surname: {employee.Surname}\n" +
                                  $"Employee salary: {employee.Salary}\n" +
                                  $"----------------------------------");
        }
    }

    public bool CheckInactiveEmployees()
    {
        foreach (var employee in HrDbContext.Employees)
        {
            if (employee.IsActive is false)
            {
                return true;
            }
        }
        return false;
    }

    public void ActivateInactive(int employeeID)
    {
        Employee? dbEmployee = HrDbContext.Employees.Find(e => e.Id == employeeID && e.IsActive is false);
        if (dbEmployee is null) throw new NotFoundException($"Employee with {employeeID} ID is not found.");
        foreach(var employee in HrDbContext.Employees)
        {
            if(employeeID == employee.Id)
                employee.IsActive = true;
        }
    }
}
