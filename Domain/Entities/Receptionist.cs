﻿namespace Domain.Entities
{
    public class Receptionist : EntityBase
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string AccountId { get; set; }
        public Guid OfficeId { get; set; }
    }
}
