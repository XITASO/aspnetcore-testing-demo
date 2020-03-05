using System;
using FluentAssertions;
using Tests.Helpers;
using WebApp.Exceptions;
using WebApp.Model;
using Xunit;

namespace Tests.UniTests
{
    public class ProjectTests
    {
        [Fact]
        public void Participants_must_not_be_older_than_50_years() 
        {
            var fiftyYearsBefore = DateTime.Now.AddYears(-50);
            Project project = ProjectBuilder.New();
            Participant toOld = ParticipantBuilder.New()
                                    .With(p => p.Birthday = fiftyYearsBefore);
            
            Action addTooOldParticipant = () => project.Add(toOld);

            addTooOldParticipant.Should().Throw<ParticipantTooOldException>();
        }


        // Bad unit test example
        [Fact]
        public void I_cannot_add_more_participants_than_max_participants() {
            Project project = new Project {
                Title = "My Testproject",
                Description = "Some description",
                MaxParticipants = 1,
                Start = new DateTime(2020, 1, 3),
                End = new DateTime(2020, 1, 1)
            };
            var participant1 = new Participant {
                Name = "Hans Mayer",
                Birthday = new DateTime(1978, 5, 23),
                Email = "mayer.hans@example.com",
                Phone = "0 7464 564 42"
            };
            var participant2 = new Participant {
                Name = "Peter Hansen",
                Birthday = new DateTime(1983, 1, 2),
            };
            project.Add(participant1);

            Action addSecondParticipant = () => project.Add(participant2);

            addSecondParticipant.Should().Throw<CapacityReachedException>();
        }

        // better with test data builders
        [Fact]
        public void I_cannot_add_more_participants_than_max_participants_withBuilders() {
            Project project = ProjectBuilder.New().With(p => p.MaxParticipants = 1);
            project.Add(ParticipantBuilder.New());

            Action addSecondParticipant = () => project.Add(ParticipantBuilder.New());

            addSecondParticipant.Should().Throw<CapacityReachedException>();
        }
    }
    
}