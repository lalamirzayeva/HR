using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Contexts;

namespace HR.Business.Services;

public class DepartmentService : IDepartmentService
{
    public void AddEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public void Create(string name, string description, int employeeLimit, int companyId)
    {

        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(); // aid oldugu company gore unique olmalidir
                                                                           //    Department? dbdDepartment = 
                                                                           //        HrDbContext.Departments.Find(d => d.Name.ToLower() == name.ToLower());
                                                                           //    if (dbCompany is not null)
                                                                           //        throw new AlreadyExistException($"A company with the {name} name is already exist.");
                                                                           //    if (name.Length < 2)
                                                                           //        throw new CoNameException($"The company name should contain at least 3 letters.");
                                                                           //    Company company = new Company(name, info);
                                                                           //    HrDbContext.Companies.Add(company);                                                                   
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
