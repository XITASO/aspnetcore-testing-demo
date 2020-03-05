using System;

namespace WebApp.Model
{
    public class Participant : Entity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public DateTime Birthday { get; set; }
    }
}