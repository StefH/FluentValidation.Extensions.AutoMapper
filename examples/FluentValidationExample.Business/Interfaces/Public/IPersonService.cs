using FluentValidationExample.Business.Models.Public;

namespace FluentValidationExample.Business.Interfaces.Public
{
    public interface IPersonService
    {
        void Add(PersonDto dto);
    }
}
