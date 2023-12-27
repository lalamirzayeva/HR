namespace HR.Business.Utilities.Exceptions;

public class MinLimitEmployeeException:Exception
{
    public MinLimitEmployeeException(string message) : base(message) { }
}
