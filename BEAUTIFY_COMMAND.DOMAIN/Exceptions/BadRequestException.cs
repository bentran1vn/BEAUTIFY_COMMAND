namespace BEAUTIFY_COMMAND.DOMAIN.Exceptions;
public abstract class BadRequestException : DomainException
{
    protected BadRequestException(string message)
        : base("Bad Request", message)
    {
    }
}