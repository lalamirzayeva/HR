﻿using HR.Business.Interfaces;
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
    public void AddEmployee(int employeeId, int departmentId) 
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
       employee.DepartmentId = dbDepartment;
       dbDepartment.CurrentEmployeeCount++;        
    }

    public void Create(string? name, string? description, int employeeLimit, int companyId)
    {
        if (String.IsNullOrEmpty(name)) throw new ArgumentNullException();
        if (employeeLimit < 3) throw new MinNumEmployeeException($"Department should at least contain 3 employees.");
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

    public Department? GetDepartmentById(int departmentId)
    {
        if (departmentId < 0) 
            throw new ArgumentOutOfRangeException(); 
        Department? dbDepartment = 
            HrDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null) 
            throw new NotFoundException($"Department with {departmentId} ID does not exist.");
        return dbDepartment; 
    }

    public void GetDepartmentEmployees(int departmentId)
    {
        Department? dbDepartment = 
            HrDbContext.Departments.Find(d => d.Id == departmentId);
        if (dbDepartment is null) 
            throw new NotFoundException($"Department with {departmentId} ID is not found.");
        if (dbDepartment is not null)
        {
            foreach (var employee in HrDbContext.Employees)
            {
                if (employee.IsActive == true && dbDepartment.Id == departmentId)
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
        if (String.IsNullOrEmpty(newDepartmentName)) throw new ArgumentNullException();
        Department? dbDepartment = GetDepartmentById(departmentId);
        if (dbDepartment is null) throw new NotFoundException($"Department with {departmentId} ID is not found.");
        if (dbDepartment is not null) 
        {
            foreach (var departments in HrDbContext.Departments)
            {
                if (departments.CompanyId.Id == dbDepartment.CompanyId.Id && newDepartmentName.ToLower() == departments.Name.ToLower())
                    throw new AlreadyExistException($"A department with {newDepartmentName} name exist within the company.");
            }
        }
        if (newEmployeeLimit <= dbDepartment.CurrentEmployeeCount) 
            throw new MinNumEmployeeException($"New employee limit should be higher than current number of employees in order to update department.");
        dbDepartment.Name = newDepartmentName;
        dbDepartment.EmployeeLimit = newEmployeeLimit;
    
    }

    public void ShowAllDepartments()
    {
        foreach (var department in HrDbContext.Departments)
        {
            if (department.IsActive == true)
            {
                Console.WriteLine($"ID: {department.Id}; " +
                                  $"Name: {department.Name}; " +
                                  $"Company ID: {department.CompanyId.Id}; " +
                                  $"Company Name: {department.CompanyId.Name} ");
            }
        }
    }
}
