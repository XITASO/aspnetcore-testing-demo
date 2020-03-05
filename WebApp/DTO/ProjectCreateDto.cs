using System;

namespace WebApp.DTO
{
    public class ProjectCreateDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint MaxParticipants { get; set; }
    }
}