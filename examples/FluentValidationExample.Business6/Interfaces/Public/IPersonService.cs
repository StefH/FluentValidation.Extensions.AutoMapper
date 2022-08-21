using FluentValidationExample.Business6.Models.Public;

namespace FluentValidationExample.Business6.Interfaces.Public;

public interface IPersonService
{
    void Add(PersonDto dto);
}