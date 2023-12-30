namespace HR.Business.Interfaces;

public interface IEmployeeService
{
    void Create(string? name, string? surname, string? email, int salary); 
    void Active(int id);
    void UpgradeEmployee(int employeeId, int newSalaryAmount);
    void DowngradeEmployee(int employeeId, int newSalaryAmount);
    void ChangeDepartment(int employeeId, int newDepartmentId);
    void ShowAll();
    void ShowInactiveEmployees();
    bool CheckExistence();
}
