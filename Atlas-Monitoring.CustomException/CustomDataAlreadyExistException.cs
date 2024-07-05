namespace Atlas_Monitoring.CustomException
{
    public class CustomDataAlreadyExistException : Exception
    {
        public CustomDataAlreadyExistException(string? message) : base(message)
        {
        }
    }
}
