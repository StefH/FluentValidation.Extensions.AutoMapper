﻿using FluentValidation;
using FluentValidationExample.Business.Models.Public;
using JetBrains.Annotations;
using Stef.Validation;

namespace FluentValidationExample.Business.Validation
{
    internal class PersonValidator : AbstractValidator<PersonDto>
    {
        public PersonValidator([NotNull] IValidator<AddressDto> addressValidator)
        {
            Guard.NotNull(addressValidator, nameof(addressValidator));

            RuleFor(dto => dto.First)
                .NotEmpty()
                .Must(CheckFirst).WithMessage("no 'a' allowed");

            RuleFor(dto => dto.Last)
                .NotEmpty();

            RuleFor(dto => dto.Address)
                .SetValidator(addressValidator);
        }

        private bool CheckFirst(string value)
        {
            return !value.Contains("a");
        }
    }
}