using FluentValidation;
using DocGen.Abstract.Interface.Content.Concrete;
using DocGen.Abstract.Constants;

namespace DocGen.Abstract.Validation
{
    /// <summary>
    /// FluentValidation rules for BodySection model.
    /// </summary>
    public class BodySectionValidator : AbstractValidator<BodySection>
    {
        public BodySectionValidator()
        {
            RuleFor(x => x.HierarchyLevel)
                .InclusiveBetween(1, 10)
                .WithMessage(Messages.TitleLevelIntervalErrorMessages);

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty.");
        }
    }
}
