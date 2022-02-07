using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(p => p.CategoryName).NotEmpty();
            RuleFor(p => p.CategoryName).MinimumLength(3);
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Description).MinimumLength(25);

        }

    }
}
