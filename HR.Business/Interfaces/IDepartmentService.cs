using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IDepartmentService
{
    void Create(string? name, string? description, int employeeLimit, int companyId);
    void AddEmployee(int employeeId, int departmentId);
    void FireEmployee(int employeeId, int departmentId);
    void UpdateDepartment(int departmentId, string newDepartmentName, int newEmployeeLimit);
    void GetDepartmentEmployees(string? departmentName);
    Department? GetDepartmentById(int departmentId);
    void DeleteDepartment(int departmentId);
    bool CheckExistence();
    void ShowAllDepartments();
}
