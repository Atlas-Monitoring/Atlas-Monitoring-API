namespace Atlas_Monitoring.CustomException
{
    public class CustomNoContentException : Exception
    {
        public CustomNoContentException(string? message) : base(message)
        {
        }
    }
}
