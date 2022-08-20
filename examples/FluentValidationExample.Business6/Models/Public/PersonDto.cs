namespace FluentValidationExample.Business6.Models.Public;

public class PersonDto
{
    public string First { get; set; }

    public string Last { get; set; }

    public AddressDto Address { get; set; }
}