namespace HR.Business.Utilities.Exceptions;

public class CapacityLimitException:Exception
{
    public CapacityLimitException(string message) : base(message) { }
}
