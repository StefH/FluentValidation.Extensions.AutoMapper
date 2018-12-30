# FluentValidation.Extensions.AutoMapper
FluentValidation Extensions for AutoMapper

# Problem

When the normal MVC validation is defined on a ViewModel and additional validation is done in the business-layer on the dto's using FluentValidation, the error messages are using the property names from the dto instead of the view-model.
The following code shows this problem:

### ViewModel Example
The flattened **Person** view model class.

``` c#
public class Person
{
    [Required]
    public string FirstName { get; set; }

    // ...

    [Required]
    public string Street { get; set; }

    [Required]
    public string City { get; set; }
}
```


# DTO models example

The **PersonDto** model class:
``` c#
public class PersonDto
{
    public string First { get; set; }

    public AddressDto Address { get; set; }
}
```

The **AddressDto** model class:
``` c#
public class AddressDto
{
    public string Street { get; set; }

    public string City { get; set; }
}
```

When posting an invalif Person object to the WebAPI, you get an error back like (notice that the property names from the DTO are used!):
``` json
{
    "First": [
        "no 'a' allowed"
    ],
    "Address.City": [
        "The length of 'City' must be 2 characters or fewer. You entered 3 characters."
    ],
    "Address.Street": [
        "The length of 'Street' must be 3 characters or fewer. You entered 4 characters."
    ]
}
```

# Solution
This library solves this issue by implementing a custom **FluentValidation - PropertyNameResolver** which uses the AutoMapper mapping definitions to translate the errors with DTO property names into view model errors.
Example response will be like:
``` json
{
    "City": [
        "The length of 'City' must be 2 characters or fewer. You entered 3 characters."
    ],
    "Street": [
        "The length of 'Street' must be 3 characters or fewer. You entered 4 characters."
    ],
    "FirstName": [
        "no 'a' allowed"
    ]
}
```