using Entities.Concrete.CustomerClasses;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    internal class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
