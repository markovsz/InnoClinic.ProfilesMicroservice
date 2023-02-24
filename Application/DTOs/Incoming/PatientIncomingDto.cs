namespace Application.DTOs.Incoming
{
    public class PatientIncomingDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public bool IsLinkedToAccount { get; set; }
        public DateTime BirthDate { get; set; }
        public string AccountId { get; set; }
    }
}
