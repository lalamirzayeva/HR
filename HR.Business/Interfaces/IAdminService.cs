namespace HR.Business.Interfaces;

public interface IAdminService
{
    void Create(string? username, string? password);
    void EnterProfile(string? username, string? password);
    bool CheckExistence();
    void Order(string? username, string? password, string orderingItem);
    void CheckBalance(string? username, string? password);
}
