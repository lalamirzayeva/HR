using HR.Business.Interfaces;
using HR.Business.Utilities.Exceptions;
using HR.Core.Entities;
using HR.DataAccess.Contexts;

namespace HR.Business.Services;

public class AdminService : IAdminService
{
    public void Create(string? username, string? password)
    {
        if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            throw new ArgumentNullException();
        if (password.Length < 5) throw new PasswordLengthException($"Password should contain at least 5 characters.");
        Admin admin = new(username, password);
        HrDbContext.Admins.Add(admin);
    }

    public void EnterProfile(string? username, string? password)
    {
        if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            throw new ArgumentNullException();
        foreach (var admin in HrDbContext.Admins)
        {
            if (admin.Username != username || admin.Password != password)
                throw new InvalidCredentialsException($"Username or password is incorrect.");
            else Console.WriteLine("Successfully entered.");
        }
    }
}
