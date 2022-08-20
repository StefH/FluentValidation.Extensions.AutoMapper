using AutoMapper;
using FluentValidationExample.Business6.Interfaces.Public;
using FluentValidationExample.Business6.Models.Public;
using FluentValidationExample.WebApi6.Models;
using Microsoft.AspNetCore.Mvc;
using Stef.Validation;

namespace FluentValidationExample.WebApi6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonsController"/> class.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="mapper">The mapper.</param>
    public PersonsController(IPersonService service, IMapper mapper)
    {
        _service = Guard.NotNull(service);
        _mapper = Guard.NotNull(mapper);
    }

    // GET api/persons
    [HttpGet]
    public ActionResult<IEnumerable<Person>> Get()
    {
        return new[] { new Person { FirstName = "f-1" }, new Person { FirstName = "f-1" } };
    }

    // POST api/persons
    [HttpPost]
    public IActionResult Post([FromBody] Person person)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var dto = _mapper.Map<PersonDto>(person);
        _service.Add(dto);

        return Ok();
    }
}