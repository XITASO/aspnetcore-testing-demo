using System;

namespace WebApp.DTO
{
    public class ParticipantDto
    {
        public long Id {get; set;}
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }

    }
}