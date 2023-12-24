using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface IDepartmentService
{
    void Create(string name, string description, int employeeLimit, int companyId);
    void AddEmployee(Employee employee);
    void UpdateDepartment(string newDepartmentName, int newEmployeeLimit);
    void GetDepartmentEmployees(string departmentName);
}
