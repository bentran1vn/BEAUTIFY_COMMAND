namespace BEAUTIFY_COMMAND.DOMAIN.Exceptions;
public static class UserException
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid userId)
            : base($"The user with the id {userId} was not found.")
        {
        }
    }
}