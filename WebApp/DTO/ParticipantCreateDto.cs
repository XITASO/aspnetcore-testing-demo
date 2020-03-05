using System;

namespace WebApp.DTO
{
    public class ParticipantCreateDto
    {

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
        public DateTime Birthday { get; set; }

    }
}