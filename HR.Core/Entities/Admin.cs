using HR.Core.Interfaces;

namespace HR.Core.Entities;

public class Admin : IEntity
{
    public int Id {  get; }
    private static int _id;
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int CurrentBalance { get; set; }
    public bool IsActive { get; set; }
    public Admin(string username, string password)
    {
        Username = username;
        Password = password;
        CurrentBalance = 100;
        IsActive = true;
    }
}
