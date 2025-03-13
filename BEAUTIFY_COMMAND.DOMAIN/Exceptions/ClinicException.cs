namespace BEAUTIFY_COMMAND.DOMAIN.Exceptions;
public static class ClinicException
{
    public class ClinicNotFoundException : NotFoundException
    {
        public ClinicNotFoundException(Guid clinicId)
            : base($"The clinic with the id {clinicId} was not found.")
        {
        }

        public ClinicNotFoundException()
            : base("The clinic was not found.")
        {
        }
    }
}