using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Contexts;
using System.Xml.Linq;

namespace HR.Business.Services;

public class CompanyService : ICompanyService
{

    public void Create(string name, string info)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException();
        Company? dbCompany = 
            HrDbContext.Companies.Find(c => c.Name.ToLower() == name.ToLower());
        if (dbCompany is not null)
            throw new AlreadyExistException($"A company with the {name} name is already exist.");
        if (name.Length < 2) 
            throw new CoNameException($"The company name should contain at least 3 letters.");
        Company company = new Company(name, info);
        HrDbContext.Companies.Add(company);
    }

    public void GetAllDepartments(string companyName)
    {
        if (string.IsNullOrEmpty(companyName)) throw new ArgumentNullException();
        Company? dbCompany =
            HrDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null)
            throw new AlreadyExistException($"A company with {companyName} name does not exist.");
        else foreach (var department in HrDbContext.Departments)
            {
                if (department.Company.Name.ToLower() == companyName.ToLower())
                    Console.WriteLine($"ID: {department.Id};  Department name:{department.Name}");
            }
    }

    public void GetAllDepartmentsById(int companyId)
    {
        foreach (var department in HrDbContext.Departments)
        {
            if (department.Company.Id == companyId)
            {
                Console.WriteLine($"ID:{department.Id}; Department name:{department.Name}");
            }
        }
    }
    public void Active(string companyName)
    {
        if (string.IsNullOrEmpty(companyName)) throw new ArgumentNullException();
        Company? dbCompany =
            HrDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower());
        if (dbCompany is null)
            throw new NotFoundException($"A company with {companyName} name is not found.");
        dbCompany.IsActive = true;
    }
    public Company? FindCompanyById(int companyId)
    {
        if (companyId < 0) throw new ArgumentOutOfRangeException();
        return HrDbContext.Companies.Find(c => c.Id == companyId);
    }
    public void ShowAll()
    {
       foreach (var company in HrDbContext.Companies) 
       {
            if (company.IsActive == true)
            {
                Console.WriteLine($"Company ID: {company.Id};  Company Name: {company.Name}");
            }
        }
    }
}
