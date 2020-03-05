using Bogus;
using WebApp.Model;

namespace Tests.Helpers {
    class ProjectBuilder : TestDataBuilder<Project, ProjectBuilder>
    {
        private readonly Faker<Project> faker;
        private readonly string[] fakeTitles = new string[] {
            "Some testing project",
            "Evaluation of testint methods",
            "Demonstration of testing patterns"
        };

        public ProjectBuilder()
        {
            faker = new Faker<Project>("de")
                .StrictMode(true)
                .RuleFor(p => p.Id, f => f.Random.Long())
                .RuleFor(p => p.Start, f => f.Date.Future())
                .RuleFor(p => p.End, (f, p) => f.Date.Soon(30, p.Start))
                .RuleFor(p => p.Title, f => f.Random.ListItem(fakeTitles))
                .RuleFor(p => p.MaxParticipants, f => f.Random.UInt(3, 20))
                .RuleFor(p => p.Description, f => f.Lorem.Paragraph());
        }
        protected override Project CreateInstanceWithDefaults()
        {
            return faker.Generate();
        }
    }

}