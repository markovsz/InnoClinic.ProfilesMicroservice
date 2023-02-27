namespace Application.DTOs.Outgoing
{
    public class DoctorsPaginationOutgoingDto
    {
        public IEnumerable<DoctorOutgoingDto> Doctors { get; set; }
        public int PagesCount { get; set; }
    }
}
