﻿using Domain.Enums;

namespace Application.DTOs.Outgoing
{
    public class DoctorOutgoingDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string AccountId { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid OfficeId { get; set; }
        public int CareerStartYear { get; set; }
        public DoctorStatuses Status { get; set; }
    }
}
