﻿using System.ComponentModel.DataAnnotations;

namespace FluentValidationExample.WebApi6.Models;

public class Person
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string City { get; set; }
}