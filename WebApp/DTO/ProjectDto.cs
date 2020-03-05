using System;

namespace WebApp.DTO
{
    public class ProjectDto
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint MaxParticipants { get; set; }
    }
}