using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IDepartmentService
{
    void Create(string? name, string? description, int employeeLimit, int companyId);
    void AddEmployee(Employee employee, int departmentId);
    void UpdateDepartment(int departmentId, string newDepartmentName, int newEmployeeLimit);
    void GetDepartmentEmployees(int departmentId);
    Department? GetDepartmentById(int departmentId);
    void ShowDepartmentsInCompany (int companyName);
    void ShowAllDepartments();
}
