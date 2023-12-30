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
            else 
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully entered.");
                Console.ResetColor();
            }
        }
    }
    public bool CheckExistence()
    {
        foreach (var admins in HrDbContext.Admins)
        {
            if (admins.IsActive == true)
            {
                return true;
            }
        }
        return false;
    }

    public void Order(string? username, string? password, string orderingItem)
    {
        if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            throw new ArgumentNullException();
        foreach (var admin in HrDbContext.Admins)
        {
            if (admin.Username != username || admin.Password != password)
                throw new InvalidCredentialsException($"Username or password is incorrect.");
            else
            {
                if(orderingItem == "Hamburger" && admin.CurrentBalance >= 10)
                {
                    admin.CurrentBalance = admin.CurrentBalance - 10;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Your hamburger will be delivered in 15 minutes.\n" +
                                      $"Your current balance is {admin.CurrentBalance} manats.");
                    Console.ResetColor();
                    break;
                }
                if (orderingItem == "Pizza" && admin.CurrentBalance >= 15)
                {
                    admin.CurrentBalance = admin.CurrentBalance - 15;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Your pizza will be delivered in 15 minutes.\n" +
                                      $"Your current balance is {admin.CurrentBalance} manats.");
                    Console.ResetColor();
                    break;
                }
                if (orderingItem == "Doner" && admin.CurrentBalance >= 5)
                {
                    admin.CurrentBalance = admin.CurrentBalance - 5;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Your doner & ayran will be delivered in 15 minutes.\n" +
                                      $"Your current balance is {admin.CurrentBalance} manats.");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Don't have enough money to order.\n" +
                                      $"Your current balance is {admin.CurrentBalance} manats.");
                    Console.ResetColor();
                }
            }
        }
    }

    public void CheckBalance(string? username, string? password)
    {
        if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            throw new ArgumentNullException();
        foreach (var admin in HrDbContext.Admins)
        {
            if (admin.Username != username || admin.Password != password)
                throw new InvalidCredentialsException($"Username or password is incorrect.");
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Your current balance is {admin.CurrentBalance} manats.");
                Console.ResetColor();
            }
        }
    }
}
