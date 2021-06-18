using API.DTO;
using FluentValidation;

namespace RestAPI_Detailed.Services
{
    public class Validator : AbstractValidator<Activity>
    {
        public Validator()
        {
            RuleFor(x=> x.Title).NotEmpty();
            RuleFor(x=> x.Category).NotEmpty();
            RuleFor(x=> x.Date).NotEmpty();
        }
    }
}