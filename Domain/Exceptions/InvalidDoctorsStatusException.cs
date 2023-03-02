namespace Domain.Exceptions
{
    public class InvalidDoctorsStatusException : Exception
    {
        public InvalidDoctorsStatusException()
            : base($"doctor's status value is invalid")
        { 
        }
    }
}
