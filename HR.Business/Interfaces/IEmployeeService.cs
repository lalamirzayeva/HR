namespace HR.Business.Interfaces;

public interface IEmployeeService
{
    void Create(string name, string surname, string email, int salary); // int department id sildim onsuz da yaratmaq olsun
    void Active(int id);
    void FireEmployee(int employeeId);
    void ChangeDepartment(int employeeId, int newDepartmentId);
    void ShowAll();
}
