namespace HR.Business.Utilities.Exceptions;

public class PasswordLengthException:Exception
{
    public PasswordLengthException(string message) : base(message) { }
}
