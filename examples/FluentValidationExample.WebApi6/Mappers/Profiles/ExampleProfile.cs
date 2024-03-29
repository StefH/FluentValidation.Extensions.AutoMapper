﻿using AutoMapper;
using FluentValidationExample.Business6.Models.Public;
using FluentValidationExample.WebApi6.Models;

namespace FluentValidationExample.WebApi6.Mappers.Profiles;

public class ExampleProfile : Profile
{
    public ExampleProfile()
    {
        CreateMap<Person, AddressDto>()
            .ForMember(dto => dto.Street, opt => opt.MapFrom(model => model.Street))
            .ForMember(dto => dto.City, opt => opt.MapFrom(model => model.City));

        CreateMap<Person, PersonDto>()
            .ForMember(dto => dto.First, opt => opt.MapFrom(model => model.FirstName))
            .ForMember(dto => dto.Last, opt => opt.MapFrom(model => model.LastName))
            .ForMember(dto => dto.Address, opt => opt.MapFrom(model => model));
    }
}