using Bogus;
using WebApp.Model;

namespace Tests.Helpers
{
    internal class AddressBuilder : TestDataBuilder<Address, AddressBuilder>
    {
        private Faker<Address> faker;

        public AddressBuilder()
        {
            faker = new Faker<Address>("de")
            .StrictMode(true)
            .RuleFor(p => p.Id, f => f.Random.Long())
            .RuleFor(p => p.Street, f => f.Address.StreetName())
            .RuleFor(p => p.HouseNumber, f => f.Address.BuildingNumber())
            .RuleFor(p => p.PostCode, f => uint.Parse(f.Address.ZipCode()))
            .RuleFor(p => p.City, f => f.Address.City())
            .RuleFor(p => p.Country, f => f.Random.Enum<Country>());
        }

        protected override Address CreateInstanceWithDefaults()
        {
            return faker.Generate();
        }
    }
}