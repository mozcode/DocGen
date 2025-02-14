using DocumentFormat.OpenXml.Wordprocessing;

namespace DocGen.Word.Utility
{
    /// <summary>
    /// Helper methods for OpenXML Word conversions (justification, etc.).
    /// </summary>
    public static class WordHelper
    {
        /// <summary>
        /// #RRGGBB -> "RRGGBB" olarak kırpma.
        /// Word'ün Color.Value property’sinde '#' olmaz.
        /// </summary>
        public static string NormalizeHexColor(string hexColor)
        {
            if (string.IsNullOrEmpty(hexColor) || !hexColor.StartsWith("#"))
                return "000000";
            return hexColor.Substring(1);
        }

        public static Justification ConvertJustification(string justificationName)
        {
            return justificationName.ToLower() switch
            {
                "left" => new Justification { Val = JustificationValues.Left },
                "center" => new Justification { Val = JustificationValues.Center },
                "right" => new Justification { Val = JustificationValues.Right },
                "justified" => new Justification { Val = JustificationValues.Both },
                _ => new Justification { Val = JustificationValues.Left }
            };
        }
    }
}
