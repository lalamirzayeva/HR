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

    public void FireEmployee(int employeeId)
    {
        Employee? employee = HrDbContext.Employees.Find(e => e.Id == employeeId);
        if (employee is null) throw new NotFoundException($"Employee with {employeeId} ID is not found.");
        employee.IsActive = false;
    }

    public void Active(int id)
    {
        Employee? employee = HrDbContext.Employees.Find(e => e.Id == id);
        if (employee is null) throw new NotFoundException($"Employee with {id} ID is not found.");
        employee.IsActive = true;
    }
    public void ChangeDepartment(int employeeId, int newDepartmentId)
    {
        throw new NotImplementedException();
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
                                  $"************************************");
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
}
