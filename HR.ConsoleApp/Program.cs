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
    Console.WriteLine("Choose the option:");
    Console.WriteLine("1 - Create company\n" +
                      "2 - Show all departments of the company\n" +
                      "3 - Show all companies\n" +
                      "-----------------------------------------\n" +
                      "4 - Create department\n" +
                      "5 - Add employee to the department\n" +
                      "6 - Show department's employees\n" +
                      //"7 - Show all departments of the company\n" +
                      "-----------------------------------------\n" +
                      "7 - Create employee\n" +
                      "8 - Fire employee\n" +
                      "9 - Show all employees in the system\n" +
                      "0 - Exit");
    string? option = Console.ReadLine();
    int optionNumber;
    bool isInt = int.TryParse(option, out optionNumber);
    if (isInt)
    {
        if (optionNumber >= 0 && optionNumber <= 10)
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
                        Console.WriteLine(ex.Message);
                        goto case (int)MenuEnum.CreateCompany;
                    }
                    break;
                case (int)MenuEnum.ShowAllDepartmentsInCo:
                    try
                    {
                        Console.WriteLine("Enter company:");
                        string? companyName = Console.ReadLine();
                        companyService.GetAllDepartments(companyName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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
                        Console.WriteLine("Enter department name:");
                        string? departmentName = Console.ReadLine();
                        Console.WriteLine("Enter description for the department:");
                        string? departmentDesc = Console.ReadLine();
                        Console.WriteLine("Enter max number of employees for the department:");
                        int employeeLimit = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter company ID to which this department will belong to:");
                        int companyId = Convert.ToInt32(Console.ReadLine());
                        departmentService.Create(departmentName,departmentDesc,employeeLimit,companyId);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto case (int)MenuEnum.CreateDepartment;
                    }
                    break;
                case (int)MenuEnum.AddEmployee:
                    try
                    {
                        Console.WriteLine("Enter department name:");
                        string? departmentName = Console.ReadLine();
                        Console.WriteLine("Enter description for the department:");
                        string? departmentDesc = Console.ReadLine();
                        Console.WriteLine("Enter max number of employees for the department:");
                        int employeeLimit = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter company ID to which this department will belong to:");
                        int companyId = Convert.ToInt32(Console.ReadLine());
                        //departmentService.AddEmployee(,)

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto case (int)MenuEnum.AddEmployee;
                    }
                    break;
                case (int)MenuEnum.ShowEmployeesOfDepartment:
                    try
                    {
                        Console.WriteLine("Enter department ID:");
                        int departmentId = Convert.ToInt32(Console.ReadLine());
                        departmentService.GetDepartmentEmployees(departmentId);                     
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        goto case (int)MenuEnum.ShowEmployeesOfDepartment;
                    }
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
                        Console.WriteLine(ex.Message);
                        goto case (int)MenuEnum.CreateEmployee;
                    }
                    break;

            }   
        }
    }

}
