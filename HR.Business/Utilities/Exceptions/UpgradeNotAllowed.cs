namespace HR.Business.Utilities.Exceptions;


public class UpgradeNotAllowed:Exception
{
    public UpgradeNotAllowed(string message) : base(message) { }
}
