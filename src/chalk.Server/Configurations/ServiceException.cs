namespace chalk.Server.Configurations;

public class ServiceException : BadHttpRequestException
{
    public string Description { get; private set; }

    public ServiceException(string description, int statusCode) : base(description, statusCode)
    {
        Description = description;
    }
}