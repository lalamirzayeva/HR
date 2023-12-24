using HR.Business.Interfaces;
using HR.Core.Entities;

namespace HR.Business.Services;

public class EmployeeService : IEmployeeService
{
    private IDepartmentService departmentService {  get;  }
    public EmployeeService()
    {
        departmentService = new DepartmentService();
    }
    public void ChangeDepartment(int departmentId, string newDepartmentName)
    {
        throw new NotImplementedException();
    }

    public void Create(string name, string surname, string email, int salary, int departmentId)
    {
        if (String.IsNullOrEmpty(name)) throw new ArgumentNullException();
        //Department? department = departmentService.;

    }

    public void FireEmployee(int employeeId)
    {
        throw new NotImplementedException();
    }
}
