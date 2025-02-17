namespace BEAUTIFY_COMMAND.DOMAIN.Exceptions;
public static class UserClinicException
{
    public class UserClinicNotFoundException : Exception
    {
        public UserClinicNotFoundException(Guid id) : base($"UserClinic with id {id} not found")
        {
        }

        public UserClinicNotFoundException() : base($"User Clinic not found")
        {
        }
    }
}