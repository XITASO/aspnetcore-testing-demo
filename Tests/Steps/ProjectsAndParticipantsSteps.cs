using System;
using TechTalk.SpecFlow;
using System.Threading.Tasks;
using WebApp.DTO;
using Tests.Setup;
using WebApp.Model;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace Tests.Steps
{
    [Binding]
    public class ProjectsAndParticipantsSteps
    {
        private readonly TestContext testContext;

        public ProjectsAndParticipantsSteps(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Given(@"there is one project ""(.*)""")]
        public void ThereIsOneProject(string projectTitle)
        {
            var create = new ProjectCreateDto
            {

                Start = new DateTime(2020, 3, 10),
                End = new DateTime(2020, 3, 17),
                Title = projectTitle,
                Description = "Demonstrating some tools and patterns to test ASP.NET Core applications.",
                MaxParticipants = 16
            };
            Task.WaitAll(testContext.PostAsync(testContext.ProjectsEndpoint, create));
        }

        [Given(@"there is a participant ""(.*)""")]
        public void ThereIsAParticipant(string participantName)
        {
            var createParticipant = new ParticipantCreateDto
            {
                Name = participantName,
                Phone = "0 123 456797",
                Email = "someuser@example.com",
                Address = new AddressDto
                {
                    Street = "Augsburger Straße",
                    HouseNumber = "56",
                    PostCode = 86381,
                    City = "Krumbach",
                    Country = Country.Germany
                },
                Birthday = new DateTime(1983, 5, 15)
            };
            Task.WaitAll(testContext.PostAsync(testContext.ParticipantsEndPoint, createParticipant));

        }

        [When(@"I add ""(.*)"" to ""(.*)""")]
        public void WhenIAddAParticipantToAProject(string participantName, string projectName)
        {
            const long projectId = 1;
            const long participantId = 1;
            Task.WaitAll(testContext.PutAsync($"/api/Projects/{projectId}", participantId));
        }


        [Then(@"the project ""(.*)"" contains a participant named ""(.*)""")]
        public void ThenTheProjectContainsAParticipantNamed(string projectName, string participantName)
        {
             testContext.GetAsync<IEnumerable<ParticipantDto>>("/api/Projects/1/participants")
                .ContinueWith(getParticipants => {
                    getParticipants.Result.Select(p => p.Name).Should().Contain(participantName);
                });
        }
    }
}