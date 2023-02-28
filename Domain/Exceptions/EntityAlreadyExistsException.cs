namespace Domain.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException() 
            : base("entity already exist")
        {
        }

        public EntityAlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
