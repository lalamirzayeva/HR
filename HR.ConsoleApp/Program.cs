using HR.Business.Services;
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
    Start:
    Console.WriteLine("Choose the option:");
    Console.WriteLine("1 - Create company\n" +
                      "2 - Show all departments of the company\n" +
                      "3 - Delete company\n" +
                      "4 - Show all companies\n" +
                      "-----------------------------------------\n" +
                      "5 - Create department\n" +
                      "6 - Add employee to the department\n" +
                      "7 - Show department's employees\n" +
                      "8 - Configure department's name and employee limit\n" +
                      "9 - Delete department\n" +
                      "10 - Show all departments\n" +
                      "-----------------------------------------\n" +
                      "11 - Create employee\n" +
                      "12 - Fire employee\n" +
                      "13 - Show all employees in the system\n" +
                      "-----------------------------------------\n" +
                      "0 - Exit");
    string? option = Console.ReadLine();
    int optionNumber;
    bool isInt = int.TryParse(option, out optionNumber);
    if (isInt)
    {
        if (optionNumber >= 0 && optionNumber <= 13)
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
                        var checkShowDepInCo = companyService.CheckExistence();
                        if (checkShowDepInCo is true) 
                        {
                            var checkDepEx = departmentService.CheckExistence();
                            if (checkDepEx is true)
                            {
                                Console.WriteLine("Enter company's name:");
                                string? companyName = Console.ReadLine();
                                companyService.GetAllDepartments(companyName);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("No department is found in the system, create department and link to the company ID.");
                                Console.ResetColor();
                                goto case (int)MenuEnum.CreateDepartment;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No company is found in the system, first create a company.");
                            Console.ResetColor();
                            goto case (int)MenuEnum.CreateCompany;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.ShowAllDepartmentsInCo;
                    }
                    break;
                case (int)MenuEnum.DeleteCompany:
                    try
                    {
                        var checkDelCo = companyService.CheckExistence();
                        if (checkDelCo is true) 
                        {
                            Console.WriteLine("Enter company's name:");
                            string? companyName = Console.ReadLine();
                            companyService.Delete(companyName);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No company is found in the system to delete. Firstly, create company.");
                            Console.ResetColor();
                            goto case (int)MenuEnum.CreateCompany;
                        }       
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto Start;
                    }
                    break;
                case (int)MenuEnum.ShowAllCompanies:
                        var checkShowAllCo = companyService.CheckExistence();
                    if (checkShowAllCo is true)
                    {
                        Console.WriteLine("All companies listed below:");
                        companyService.ShowAll();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("No company is found in the system.");
                        Console.ResetColor();
                        goto Start;
                    }
                    break;
                case (int)MenuEnum.CreateDepartment:
                    try
                    {
                        var checkCreateDep = companyService.CheckExistence();
                        if (checkCreateDep is true) 
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
                        var checkAddEmployee = employeeService.CheckExistence();
                        if (checkAddEmployee is true)
                        {
                            var checkDepExist = departmentService.CheckExistence();
                            if (checkDepExist is true) 
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
                                Console.WriteLine("No department is found in the system. Create department then add employees to the department.");
                                Console.ResetColor();
                                goto case (int)MenuEnum.CreateDepartment;
                            }
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
                case (int)MenuEnum.DeleteDepartment:
                    try
                    {
                        var checkDelDep = departmentService.CheckExistence();
                        if (checkDelDep is true) 
                        {
                            Console.WriteLine("Enter department ID you want to delete:");
                            int departmentId = Convert.ToInt32(Console.ReadLine());
                            departmentService.DeleteDepartment(departmentId);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Currently, no department is found in the system.\n" +
                                              $"Firstly create company, if there is none in the system or create department directly to add this company.");
                            Console.ResetColor();
                            goto Start;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.DeleteDepartment;
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

