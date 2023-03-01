using Domain.Enums;

namespace Domain.RequestParameters
{
    public class DoctorParameters : PaginationParameters
    {
        public string? FirstNameSearch { get; set; }
        public string? LastNameSearch { get; set; }
        public string? MiddleNameSearch { get; set; }
        public Guid? SpectializationId { get; set; }
        public Guid? OfficeId { get; set; }
    }
}
