using FluentValidation;

namespace ServiceLog.Models.Domain.Validation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Category name cannot be empty.")
                .MinimumLength(3)
                .WithMessage("Category name must be at least 3 characters long.")
                .MaximumLength(50)
                .WithMessage("Category name must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Category description cannot be empty.")
                .MinimumLength(3)
                .WithMessage("Category description must be at least 3 characters long.");

            RuleForEach(x => x.ServiceOptions).ChildRules(option =>
            {
                option.RuleFor(o => o.Name)
                    .NotEmpty()
                    .WithMessage("Service option name cannot be empty.")
                    .MinimumLength(3)
                    .WithMessage("Service option name must be at least 3 characters long.")
                    .MaximumLength(50)
                    .WithMessage("Service option name must not exceed 50 characters.");

                option.RuleFor(o => o.Description)
                    .NotEmpty()
                    .WithMessage("Service option description cannot be empty.")
                    .MinimumLength(3)
                    .WithMessage("Service option description must be at least 3 characters long.");

                option.RuleFor(o => o.Note)
                    .MaximumLength(500)
                    .WithMessage("Service option note cannot exceed 500 characters.");
            });
        }
    }
}
