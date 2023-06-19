using Entities.Concrete.ApplicationClasses;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    internal class ApplicationValidator : AbstractValidator<Application>
    {
        public ApplicationValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
