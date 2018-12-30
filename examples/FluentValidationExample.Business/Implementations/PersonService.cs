using FluentValidation;
using FluentValidationExample.Business.Interfaces.Public;
using FluentValidationExample.Business.Models.Public;
using FluentValidationExample.Common.Validation;

namespace FluentValidationExample.Business.Implementations
{
    internal class PersonService : IPersonService
    {
        private readonly IValidator<PersonDto> _validator;

        public PersonService(IValidator<PersonDto> validator)
        {
            Guard.NotNull(validator, nameof(validator));

            _validator = validator;
        }

        public void Add(PersonDto dto)
        {
            Guard.NotNull(dto, nameof(dto));

            var result = _validator.Validate(dto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
