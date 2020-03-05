using System;
using System.Threading.Tasks;
using FluentAssertions;
using Tests.Helpers;
using Tests.Setup;
using WebApp.DTO;
using WebApp.Model;
using Xunit;

namespace Tests
{
    public class ProjectsTest: IDisposable
    {
        private TestContext testContext;

        public ProjectsTest()
        {
            testContext = new TestContext();
        }

        [Fact]
        public async Task GetProject_Returns_Project() {
            Project project = ProjectBuilder.New();
            testContext.DBContext.Projects.Add(project);
            await testContext.DBContext.SaveChangesAsync();

            var result = await testContext.GetAsync<ProjectDto>($"/api/Projects/{project.Id}");
            
            result.Title.Should().Be(project.Title);
            result.Description.Should().Be(project.Description);
            result.Start.Should().Be(project.Start);
            result.End.Should().Be(project.End);
            result.MaxParticipants.Should().Be(project.MaxParticipants);
            result.Id.Should().Be(project.Id);
        }

        public void Dispose()
        {
            testContext?.Dispose();
            testContext = null;
        }
    }
}