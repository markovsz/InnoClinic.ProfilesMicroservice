using Domain.Enums;

namespace Domain.Entities
{
    public class Doctor : EntityBase
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid AccountId { get; set; }
        public Guid SpectializationId { get; set; }
        public Guid OfficeId { get; set; }
        public int CareerStartYear { get; set; }
        public DoctorStatuses Status { get; set; }
    }
}
