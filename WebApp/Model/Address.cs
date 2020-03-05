namespace WebApp.Model
{
    public class Address : Entity
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public uint PostCode { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }
    }
}