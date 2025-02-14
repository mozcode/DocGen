namespace DocGen.Abstract.Domain.RichText
{
    /// <summary>
    /// Represents a parsed piece of text with style flags (bold, italic, underline).
    /// </summary>
    public class ParsedElement
    {
        public string Text { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderline { get; set; }

        public ParsedElement(string text)
        {
            Text = text;
        }
    }
}
