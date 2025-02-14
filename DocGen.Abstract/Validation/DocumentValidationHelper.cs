using FluentValidation;
using FluentValidation.Results;
using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.Content.Concrete;
using DocGen.Abstract.Domain.Table;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocGen.Abstract.Validation
{
    /// <summary>
    /// Provides a central method to validate an entire document,
    /// checking BodySections and table cells via their respective validators.
    /// </summary>
    public static class DocumentValidationHelper
    {
        public static void ValidateDocument(IDocumentContent documentContent)
        {
            ValidateBodySections(documentContent.BodyContent.BodySections);
            ValidateSubContent(documentContent.MetaHeaderContent.TablesRows);
            ValidateSubContent(documentContent.MetaFooterContent.TablesRows);
            ValidateSubContent(documentContent.MetaSignatureContent.TablesRows);
        }

        private static void ValidateBodySections(List<IBodySection> sections)
        {
            var bodySectionValidator = new BodySectionValidator();

            foreach (var section in sections)
            {
                if (section is BodySection concreteSection)
                {
                    ValidationResult result = bodySectionValidator.Validate(concreteSection);
                    if (!result.IsValid)
                        throw new ValidationException(result.Errors);
                }

                if (section.SubBodySections?.Count > 0)
                {
                    ValidateBodySections(section.SubBodySections);
                }
            }
        }

        private static void ValidateSubContent(List<TablesRow> rows)
        {
            var tableCellValidator = new BaseTableCellValidator();

            foreach (var row in rows)
            {
                foreach (var cell in row.TableCells)
                {
                    if (cell is BaseTableCell baseCell)
                    {
                        var result = tableCellValidator.Validate(baseCell);
                        if (!result.IsValid)
                            throw new ValidationException(result.Errors);
                    }
                }
            }
        }
    }
}
