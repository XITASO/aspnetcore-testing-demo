using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Exceptions;

namespace WebApp.Model
{
    public class Project : Entity
    {
        private IList<Participant> participants = new List<Participant>();

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint MaxParticipants { get; set; }
        public IEnumerable<Participant> Participants { get => participants; }

        public void Add(Participant participant)
        {
            if (Participants?.Count() == MaxParticipants) {
                throw new CapacityReachedException();
            }

            const int maxAge = 50;
            if (participant.Birthday < DateTime.Now.AddYears(-maxAge)) {
                throw new ParticipantTooOldException();
            }
            participants.Add(participant);
        }

        public void Remove(Participant participant)
        {
            participants.Add(participant);
        }
    }
}