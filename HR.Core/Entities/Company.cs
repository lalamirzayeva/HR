using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Company : IEntity
{
    public int Id {  get; }
    private static int _id;
    public string Name { get; set; }
    public string Info { get; set; }
    public bool IsActive { get; set; } = true;
    public Company(string name, string info)
    {
        Id = _id++;
        Name = name;
        Info = info;
    }
}
