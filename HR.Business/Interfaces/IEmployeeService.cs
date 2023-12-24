namespace HR.Business.Interfaces;

public interface IEmployeeService
{
    void Create(string name, string surname, string email, int salary, int departmentId);
    void FireEmployee(int employeeId);
    void ChangeDepartment(int departmentId, string newDepartmentName);
}
