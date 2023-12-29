using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Employee : IEntity
{
    public int Id {  get; }
    private static int _id;
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public int Salary { get; set; }
    public bool IsActive { get; set; }
    public Department DepartmentId { get; set; }  
    public override string ToString()
    {
        return "Employee: "+ Id + " " + Name + " " + Surname; 
    }
    
    public Employee(string? name, string? surname, string? email, int salary) 
    {
        Id = _id++;
        Name = name;
        Surname = surname;
        Email = email;
        Salary = salary;
        IsActive = true;
    }
}
