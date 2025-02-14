using FluentValidation;
using DocGen.Abstract.Interface.Content;

namespace DocGen.Abstract.Validation
{
    /// <summary>
    /// Validates BaseTableCell, ensuring RowNumber and ColumnNumber >= 0.
    /// </summary>
    public class BaseTableCellValidator : AbstractValidator<BaseTableCell>
    {
        public BaseTableCellValidator()
        {
            RuleFor(x => x.RowNumber)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Satır numarası negatif olamaz.");

            RuleFor(x => x.ColumnNumber)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Sütun numarası negatif olamaz.");
        }
    }
}
