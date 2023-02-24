namespace Application.DTOs.Outgoing
{
    public class ReceptionistOutgoingDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string AccountId { get; set; }
        public Guid OfficeId { get; set; }
    }
}
