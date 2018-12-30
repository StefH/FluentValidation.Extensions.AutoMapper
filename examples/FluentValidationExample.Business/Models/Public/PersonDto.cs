namespace FluentValidationExample.Business.Models.Public
{
    public class PersonDto
    {
        public string First { get; set; }

        public string Last { get; set; }

        public AddressDto Address { get; set; }
    }
}