using HR.Core.Entities;

namespace HR.Business.Interfaces;

public interface ICompanyService
{
    void Create(string? name, string? info);
    void GetAllDepartments(string? companyName);
    void GetAllDepartmentsById(int companyId);
    void Active(string companyName);
    void Delete(string? companyName);
    bool CheckExistence();
    Company? FindCompanyById (int companyId);
    void ShowAll();
}
