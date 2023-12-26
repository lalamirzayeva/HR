namespace HR.Business.Interfaces;

public interface IEmployeeService
{
    void Create(string? name, string? surname, string? email, int salary); 
    void Active(int id);
    void FireEmployee(int employeeId);
    void ChangeDepartment(int employeeId, int newDepartmentId);
    void ShowAll();
}
