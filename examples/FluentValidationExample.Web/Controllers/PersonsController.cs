using AutoMapper;
using FluentValidationExample.Business.Interfaces.Public;
using FluentValidationExample.Business.Models.Public;
using FluentValidationExample.Common.Validation;
using FluentValidationExample.Web.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FluentValidationExample.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _service;
        [NotNull] private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonsController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="mapper">The mapper.</param>
        public PersonsController([NotNull] IPersonService service, [NotNull] IMapper mapper)
        {
            Guard.NotNull(service, nameof(service));
            Guard.NotNull(mapper, nameof(mapper));

            _service = service;
            _mapper = mapper;
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
}