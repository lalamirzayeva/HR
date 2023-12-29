namespace HR.Business.Interfaces;

public interface IAdminService
{
    void Create(string? username, string? password);
    void EnterProfile(string? username, string? password);
}
