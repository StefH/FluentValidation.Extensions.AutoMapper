using FluentValidation;
using FluentValidationExample.Business6.Interfaces.Public;
using FluentValidationExample.Business6.Models.Public;
using Stef.Validation;

namespace FluentValidationExample.Business6.Implementations;

internal class PersonService : IPersonService
{
    private readonly IValidator<PersonDto> _validator;

    public PersonService(IValidator<PersonDto> validator)
    {
        _validator = Guard.NotNull(validator);
    }

    public void Add(PersonDto dto)
    {
        Guard.NotNull(dto);

        var result = _validator.Validate(dto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}