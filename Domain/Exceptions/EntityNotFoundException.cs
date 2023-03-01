namespace Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() 
            : base("entity wasn't found")
        { 
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}
