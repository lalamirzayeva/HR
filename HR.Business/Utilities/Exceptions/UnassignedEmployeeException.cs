namespace HR.Business.Utilities.Exceptions;

public class UnassignedEmployeeException:Exception
{
    public UnassignedEmployeeException(string message) : base(message) { }
}
