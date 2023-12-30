using HR.Business.Services;
using HR.Business.Utilities.Helpers;
using HR.Core.Entities;
using HR.DataAccess.Contexts;
using System.ComponentModel.Design;

Console.ForegroundColor = ConsoleColor.DarkMagenta;
Console.WriteLine("HR System Start:");
Console.ResetColor();

CompanyService companyService = new();
DepartmentService departmentService = new();
EmployeeService employeeService = new();
AdminService adminService = new();
bool runApp = true;
bool runCanteen = false;
Menu:
while (runApp)
{
Start:
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("\nChoose the option:\n");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
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
                      "12 - Upgrade employee\n" +
                      "13 - Downgrade employee\n" +
                      "14 - Change employee's department\n" +
                      "15 - Fire employee\n" +
                      "16 - Show all employees in the system\n" +
                      "-----------------------------------------\n" +
                      "17 - Create HR admin profile\n" +
                      "18 - Enter canteen\n" +
                      "-----------------------------------------\n" +
                      "0 - Exit");
    Console.ResetColor();
    string? option = Console.ReadLine();
    int optionNumber;
    bool isInt = int.TryParse(option, out optionNumber);
    if (isInt)
    {
        if (optionNumber >= 0 && optionNumber <= 18)
        {
            switch (optionNumber)
            {
                case (int)MenuEnum.CreateCompany:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Enter company name:");
                        Console.ResetColor();
                        string? companyName = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Enter info for the company:");
                        Console.ResetColor();
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
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Enter company's name:");
                                Console.ResetColor();
                                string? companyName = Console.ReadLine();
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                companyService.GetAllDepartments(companyName);
                                Console.ResetColor();
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
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter company's name:");
                            Console.ResetColor();
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
                        goto case (int)MenuEnum.DeleteCompany;
                    }
                    break;
                case (int)MenuEnum.ShowAllCompanies:
                    var checkShowAllCo = companyService.CheckExistence();
                    if (checkShowAllCo is true)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("All companies are listed below:");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        companyService.ShowAll();
                        Console.ResetColor();
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
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter department name:");
                            Console.ResetColor();
                            string? departmentName = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter description for the department:");
                            Console.ResetColor();
                            string? departmentDesc = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter max number of employees for the department:");
                            Console.ResetColor();
                            int employeeLimit = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter company ID to which this department will belong to:");
                            Console.ResetColor();
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
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Enter employee's ID:");
                                Console.ResetColor();
                                int employeeID = Convert.ToInt32(Console.ReadLine());
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Enter department's ID:");
                                Console.ResetColor();
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
                            goto case (int)MenuEnum.CreateEmployee;
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
                        var checkDep = departmentService.CheckExistence();
                        if (checkDep is true)
                        {
                            var checkEmpEx = employeeService.CheckExistence();
                            if (checkEmpEx is true)
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Enter department's name:");
                                Console.ResetColor();
                                string? departmentName = Console.ReadLine();
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                departmentService.GetDepartmentEmployees(departmentName);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Currently, no employee is found in the system. Create an employee then add to the department. ");
                                Console.ResetColor();
                                goto case (int)MenuEnum.CreateEmployee;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No department is found in the system. Create department then add employees.");
                            Console.ResetColor();
                            goto case (int)MenuEnum.CreateDepartment;
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
                case (int)MenuEnum.UpdateDepartment:
                    try
                    {
                        var checkDep = departmentService.CheckExistence();
                        if (checkDep is true)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter department ID you want to change:");
                            Console.ResetColor();
                            int departmentId = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Set new name for the department:");
                            Console.ResetColor();
                            string? newName = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Set new employee limit for the department:");
                            Console.ResetColor();
                            int newEmployeeLimit = Convert.ToInt32(Console.ReadLine());
                            departmentService.UpdateDepartment(departmentId, newName, newEmployeeLimit);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No department is found in the system to update.");
                            Console.ResetColor();
                            goto Start;
                        }
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
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter department ID you want to delete:");
                            Console.ResetColor();
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
                        goto Start;
                    }
                    break;
                case (int)MenuEnum.ShowAllDeps:
                    var checkShowDep = departmentService.CheckExistence();
                    if (checkShowDep is true)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("All departments listed below:");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        departmentService.ShowAllDepartments();
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("No department is found in the system.");
                        Console.ResetColor();
                        goto Start;
                    }
                    break;
                case (int)MenuEnum.CreateEmployee:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Enter employee's name:");
                        Console.ResetColor();
                        string? employeeName = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Enter employee's surname:");
                        Console.ResetColor();
                        string? employeeSurname = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Enter employee's email:");
                        Console.ResetColor();
                        string? employeeMail = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Enter employee's salary:");
                        Console.ResetColor();
                        int employeeSalary = Convert.ToInt32(Console.ReadLine());
                        employeeService.Create(employeeName, employeeSurname, employeeMail, employeeSalary);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.CreateEmployee;
                    }
                    break;
                case (int)MenuEnum.UpgradeEmployee:
                    try
                    {
                        var checkEmpUp = employeeService.CheckExistence();
                        if (checkEmpUp is true)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter ID of the employee you want to upgrade:");
                            Console.ResetColor();
                            int employeeID = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter new salary amount:");
                            Console.ResetColor();
                            int newSalaryAmount = Convert.ToInt32(Console.ReadLine());
                            employeeService.UpgradeEmployee(employeeID, newSalaryAmount);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No employee is found in the system to upgrade. Firstly, create an employee.");
                            Console.ResetColor();
                            goto case (int)MenuEnum.CreateEmployee;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.UpgradeEmployee;
                    }
                    break;
                case (int)MenuEnum.DowngradeEmployee:
                    try
                    {
                        var checkEmpDown = employeeService.CheckExistence();
                        if (checkEmpDown is true)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter ID of the employee you want to downgrade:");
                            Console.ResetColor();
                            int employeeID = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter new salary amount:");
                            Console.ResetColor();
                            int newSalaryAmount = Convert.ToInt32(Console.ReadLine());
                            employeeService.DowngradeEmployee(employeeID, newSalaryAmount);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No employee is found in the system to downgrade. Firstly, create an employee.");
                            Console.ResetColor();
                            goto case (int)MenuEnum.CreateEmployee;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.DowngradeEmployee;
                    }
                    break;
                case (int)MenuEnum.ChangeEmployeeDepartment:
                    try
                    {
                        var checkChangeDep = departmentService.CheckExistence();
                        if (checkChangeDep is true)
                        {
                            var checkChange = employeeService.CheckExistence();
                            if (checkChange is true)
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Enter employee ID:");
                                Console.ResetColor();
                                int employeeId = Convert.ToInt32(Console.ReadLine());
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("Enter new department's ID to transfer employee:");
                                Console.ResetColor();
                                int newDepartmentId = Convert.ToInt32(Console.ReadLine());
                                employeeService.ChangeDepartment(employeeId, newDepartmentId);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("No employee is found in the system to transfer. Firstly, create an employee.");
                                Console.ResetColor();
                                goto case (int)MenuEnum.CreateEmployee;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No department is found in the system for transfering. Create department.");
                            Console.ResetColor();
                            goto case (int)MenuEnum.CreateDepartment;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto case (int)MenuEnum.ChangeEmployeeDepartment;
                    }
                    break;
                case (int)MenuEnum.FireEmployee:
                    try
                    {
                        var checkEmp = employeeService.CheckExistence();
                        if (checkEmp is true)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter ID of the employee you want to fire:");
                            Console.ResetColor();
                            int employeeID = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter ID of the department this employee works:");
                            Console.ResetColor();
                            int departmentId = Convert.ToInt32(Console.ReadLine());
                            departmentService.FireEmployee(employeeID, departmentId);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No employee is found in the system to fire.");
                            Console.ResetColor();
                            goto Start;
                        }
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
                    var checkShowEmp = employeeService.CheckExistence();
                    if (checkShowEmp is true)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("All employees are listed below:");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        employeeService.ShowAll();
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("No employee is found in the system to show.");
                        Console.ResetColor();
                        goto Start;
                    }
                    break;
                case (int)MenuEnum.CreateAdminProfile:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Set username:");
                        Console.ResetColor();
                        string? username = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Set new password:");
                        Console.ResetColor();
                        string? password = Console.ReadLine();
                        adminService.Create(username, password);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto Start;
                    }
                    break;
                case (int)MenuEnum.EnterCanteen:
                    try
                    {
                        var checkAdmin = adminService.CheckExistence();
                        if (checkAdmin is true)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter your username:");
                            Console.ResetColor();
                            string? username = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Enter your password:");
                            Console.ResetColor();
                            string? password = Console.ReadLine();
                            adminService.EnterProfile(username, password);
                            runCanteen = true;
                            goto Canteen;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("No admin profile is exist. Firstly, create a profile.");
                            Console.ResetColor();
                            goto case (int)MenuEnum.CreateAdminProfile;
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

Canteen:
while (runCanteen)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\nChoose the meal to order:\n");
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("1 - Hamburger = 10 manats\n" +
                      "2 - Pepperoni pizza = 15 manats\n" +
                      "3 - Doner & ayran = 5 manats\n" +
                      "-----------------------------------------\n" +
                      "4 - Check your balance\n" +
                      "0 - Back to main menu");
    Console.ResetColor();
    string? option = Console.ReadLine();
    int optionNumber;
    bool isInt = int.TryParse(option, out optionNumber);
    if (isInt)
    {
        if (optionNumber >= 0 && optionNumber <= 4)
        {
            switch (optionNumber)
            {
                case (int)MenuCanteen.Hamburger:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter your username:");
                        Console.ResetColor();
                        string? username = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter your password:");
                        Console.ResetColor();
                        string? password = Console.ReadLine();
                        adminService.Order(username,password,"Hamburger");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto Canteen;
                    }
                    break;
                case (int)MenuCanteen.Pizza:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter your username:");
                        Console.ResetColor();
                        string? username = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter your password:");
                        Console.ResetColor();
                        string? password = Console.ReadLine();
                        adminService.Order(username, password, "Pizza");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto Canteen;
                    }
                    break;
                case (int)MenuCanteen.Doner:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter your username:");
                        Console.ResetColor();
                        string? username = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter your password:");
                        Console.ResetColor();
                        string? password = Console.ReadLine();
                        adminService.Order(username, password, "Doner");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto Canteen;
                    }
                    break;
                case (int)MenuCanteen.Balance:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter your username:");
                        Console.ResetColor();
                        string? username = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter your password:");
                        Console.ResetColor();
                        string? password = Console.ReadLine();
                        adminService.CheckBalance(username,password);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                        goto Canteen;
                    }
                    break;
                default:
                    runCanteen = false;
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

