namespace HR.Business.Utilities.Exceptions;

public class AlreadyEmployeedException:Exception
{
    public AlreadyEmployeedException(string message) : base(message) { }
}
