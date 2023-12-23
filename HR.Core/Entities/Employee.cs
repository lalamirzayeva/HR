﻿using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Employee : IEntity
{
    public int Id {  get; }
    private static int _id;
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public int Salary { get; set; }
    public Department DepartmentId { get; set; }

    public Employee(string name, string surname, string email, int salary, Department departmentId)
    {
        Id = _id++;
        Name = name;
        Surname = surname;
        Email = email;
        Salary = salary;
        DepartmentId = departmentId;
    }
}
