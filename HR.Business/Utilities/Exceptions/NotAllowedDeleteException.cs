namespace HR.Business.Utilities.Exceptions
{
    public class NotAllowedDeleteException:Exception
    {
        public NotAllowedDeleteException(string message) : base(message) { }
    }
}
