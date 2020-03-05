using System;
using Bogus;
using WebApp.Model;

namespace Tests.Helpers
{
    internal class ParticipantBuilder : TestDataBuilder<Participant, ParticipantBuilder>
    {
        private Address address;
        Faker<Participant> faker;
        
        public ParticipantBuilder()
        {
            
        faker = new Faker<Participant>("de")
            .StrictMode(true)
            .RuleFor(p => p.Id, f => f.Random.Long())
            .RuleFor(p => p.Name, f => f.Name.FullName())
            .RuleFor(p => p.Phone, f => f.Phone.PhoneNumber("(# ## ##) ### ###-##"))
            .RuleFor(p => p.Email, f => f.Person.Email)
            .RuleFor(p => p.Birthday, f => f.Date.Past(50, DateTime.Now))
            .RuleFor(p => p.Address, address ?? AddressBuilder.New())
            ;
        }

        public ParticipantBuilder WithAddress(Address address) {
            this.address = address;
            return this;
        }
        
        protected override Participant CreateInstanceWithDefaults()
        {
            return faker.Generate();
        }
    }

}