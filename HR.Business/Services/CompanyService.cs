using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Contexts;
using System.Xml.Linq;

namespace HR.Business.Services;

public class CompanyService : ICompanyService
{ 
    public void Create(string? name, string? info)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException();
        Company? dbCompany = 
            HrDbContext.Companies.Find(c => c.Name.ToLower() == name.ToLower() && c.IsActive is true);
        if (dbCompany is not null)
            throw new AlreadyExistException($"A company with the {name} name is already exist.");
        if (name.Length < 2) 
            throw new CoNameException($"The company name should contain at least 3 letters.");
        Company company = new Company(name, info);
        HrDbContext.Companies.Add(company);
    }

    public void GetAllDepartments(string? companyName)
    {
        if (string.IsNullOrEmpty(companyName)) throw new ArgumentNullException();
        var dbCompany =
            HrDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower() && c.IsActive is true);
        if (dbCompany is null)
            throw new AlreadyExistException($"A company with {companyName} name does not exist.");
        if (dbCompany is not null)
        {
            foreach (var department in HrDbContext.Departments)
            {
                if (department.CompanyId.Name.ToLower() == companyName.ToLower() && department.IsActive is true)
                    Console.WriteLine($"ID: {department.Id};  Department name: {department.Name}");
            }
        }
    }

    public void GetAllDepartmentsById(int companyId)
    {
        foreach (var department in HrDbContext.Departments)
        {
            if (department.CompanyId.Id == companyId)
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
        foreach (var companies in HrDbContext.Companies)
        {
            if (companies.IsActive == true)
            { 
                return HrDbContext.Companies.Find(c => c.Id == companyId && c.IsActive is true); 
            }
        }
        return null;
    }
    public void ShowAll()
    {
       foreach (var company in HrDbContext.Companies) 
       {
            if (company.IsActive == true)
            {
                Console.WriteLine($"Company ID: {company.Id}\n" +
                                  $"Company name: {company.Name}\n" +
                                  $"Company info: {company.Info}\n" +
                                  $"****************************");
            }
        }
    }

    public bool CheckExistence()
    {
        foreach (var company in HrDbContext.Companies)
        {
            if (company.IsActive == true)
            {
                return true;
            }           
        }
        return false;
    }

    public void Delete(string? companyName)
    {
        if (string.IsNullOrEmpty(companyName)) throw new ArgumentNullException();
        Company? dbCompany =
            HrDbContext.Companies.Find(c => c.Name.ToLower() == companyName.ToLower() && c.IsActive is true);
        if (dbCompany is null)
            throw new NotFoundException($"A company with {companyName} name is not found.");
        if (dbCompany is not null)
        {
            foreach (var department in HrDbContext.Departments)
            {
                if (department.IsActive == true && department.CompanyId.Name.ToLower() == companyName.ToLower())
                    throw new NotAllowedDeleteException($"This company cannot be deleted as it contains departments.");
            }
        }
        dbCompany.IsActive = false;
    }
}
