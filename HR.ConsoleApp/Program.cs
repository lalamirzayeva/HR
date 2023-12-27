﻿using HR.Business.Services;
using HR.Business.Utilities.Helpers;
using HR.Core.Entities;
using HR.DataAccess.Contexts;

Console.WriteLine("HR System Start:");

CompanyService companyService = new();
DepartmentService departmentService = new();
EmployeeService employeeService = new();
bool runApp = true;
while (runApp)
{
    Console.WriteLine("Choose the option:");
    Console.WriteLine("1 - Create company\n" +
                      "2 - Show all departments of the company\n" +
                      "3 - Show all companies\n" +
                      "-----------------------------------------\n" +
                      "4 - Create department\n" +
                      "5 - Add employee to the department\n" +
                      "6 - Show department's employees\n" +
                      "7 - Configure department's name and employee limit\n" +
                      "8 - Show all departments\n" +
                      //"7 - Show all departments of the company\n" +
                      "-----------------------------------------\n" +
                      "9 - Create employee\n" +
                      "10 - Fire employee\n" +
                      "11 - Show all employees in the system\n" +
                      "-----------------------------------------\n" +
                      "0 - Exit");
    string? option = Console.ReadLine();
    int optionNumber;
    bool isInt = int.TryParse(option, out optionNumber);
    if (isInt)
    {
        if (optionNumber >= 0 && optionNumber <= 11)
        {
            switch (optionNumber) 
            {
                case (int)MenuEnum.CreateCompany:
                    try
                    {
                        Console.WriteLine("Enter company name:");
                        string? companyName = Console.ReadLine();
                        Console.WriteLine("Enter info for the company:");
                        string? companyInfo = Console.ReadLine();
                        companyService.Create(companyName, companyInfo);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.CreateCompany;
                    }
                    break;
                case (int)MenuEnum.ShowAllDepartmentsInCo:
                    try
                    {
                        Console.WriteLine("Enter company's name:");
                        string? companyName = Console.ReadLine();
                        companyService.GetAllDepartments(companyName);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.ShowAllDepartmentsInCo;
                    }
                    break;
                case (int)MenuEnum.ShowAllCompanies:
                        Console.WriteLine("All companies listed below:");
                        companyService.ShowAll();
                    break;
                case (int)MenuEnum.CreateDepartment:
                    try
                    {
                        var check = companyService.CheckExistence();
                        if (check is true) 
                        {
                            Console.WriteLine("Enter department name:");
                            string? departmentName = Console.ReadLine();
                            Console.WriteLine("Enter description for the department:");
                            string? departmentDesc = Console.ReadLine();
                            Console.WriteLine("Enter max number of employees for the department:");
                            int employeeLimit = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter company ID to which this department will belong to:");
                            int companyId = Convert.ToInt32(Console.ReadLine());
                            departmentService.Create(departmentName, departmentDesc, employeeLimit, companyId);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Firstly, create a company.");
                            Console.ResetColor();
                            goto case (int)MenuEnum.CreateCompany;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.CreateDepartment;
                    }
                    break;
                case (int)MenuEnum.AddEmployee:
                    try
                    {
                        var check = employeeService.CheckExistence();
                        if (check is true)
                        {
                            Console.WriteLine("Enter employee's ID:");
                            int employeeID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter department's ID:");
                            int departmentID = Convert.ToInt32(Console.ReadLine());
                            departmentService.AddEmployee(employeeID, departmentID);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No employee is found to add any department. Add employee to the system.");
                            Console.ResetColor();
                            goto case(int)MenuEnum.CreateEmployee;
                        }            
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.AddEmployee;
                    }
                    break;
                case (int)MenuEnum.ShowEmployeesOfDepartment:
                    try
                    {
                        Console.WriteLine("Enter department's name:");
                        string? departmentName = Console.ReadLine();
                        departmentService.GetDepartmentEmployees(departmentName);                     
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.ShowEmployeesOfDepartment;
                    }
                    break;
                case (int)MenuEnum.UpdateDepartment:
                    try
                    {
                        Console.WriteLine("Enter department ID you want to change:");
                        int departmentId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Set new name for the department:");
                        string? newName = Console.ReadLine();
                        Console.WriteLine("Set new employee limit for the department:");
                        int newEmployeeLimit = Convert.ToInt32(Console.ReadLine());
                        departmentService.UpdateDepartment(departmentId,newName,newEmployeeLimit);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.UpdateDepartment;
                    }
                    break;
                case (int)MenuEnum.ShowAllDeps:
                    Console.WriteLine("All departments listed below:");
                    departmentService.ShowAllDepartments();
                    break;
                case (int)MenuEnum.CreateEmployee:
                    try
                    {
                        Console.WriteLine("Enter employee's name:");
                        string? employeeName = Console.ReadLine();
                        Console.WriteLine("Enter employee's surname:");
                        string? employeeSurname = Console.ReadLine();
                        Console.WriteLine("Enter employee's email:");
                        string? employeeMail = Console.ReadLine();
                        Console.WriteLine("Enter employee's salary:");
                        int employeeSalary = Convert.ToInt32(Console.ReadLine());
                        employeeService.Create(employeeName,employeeSurname,employeeMail,employeeSalary);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.CreateEmployee;
                    }
                    break;
                case (int)MenuEnum.FireEmployee:
                    try
                    {
                        Console.WriteLine("Enter ID of the employee you want to fire:");
                        int employeeID = Convert.ToInt32(Console.ReadLine());
                        employeeService.FireEmployee(employeeID);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.FireEmployee;
                    }
                    break;
                case (int)MenuEnum.ShowAllEmployees:
                    Console.WriteLine("All employees are listed below:");
                    employeeService.ShowAll();
                    break;
                default:
                    runApp = false;
                    break;
            }   
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Please, enter correct option number.");
            Console.ResetColor();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Please, enter correct format to choose an option.");
        Console.ResetColor();
    }

}

