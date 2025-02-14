using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DocGen.Abstract.Domain.RichText;
using DocGen.Abstract.Interface.Content;

namespace DocGen.Abstract.Application.Formatter
{
    /// <summary>
    /// A sample parser converting simple markdown-like formatting 
    /// (**bold**, _italic_, __underline__) to a list of ParsedElement objects.
    /// </summary>
    public class RichTextParser : IContentFormatter<ParsedElement>
    {
        public List<ParsedElement> ParseRichText(string richText)
        {
            var result = new List<ParsedElement>();
            if (string.IsNullOrEmpty(richText))
                return result;

            // 1) BOLD
            var boldPattern = new Regex(@"\*\*(.+?)\*\*");
            var boldTokens = SplitByRegex(richText, boldPattern, isBold: true);

            // 2) ITALIC
            var italicPattern = new Regex(@"_(.+?)_");
            var italicTokens = new List<(string text, bool bold, bool italic, bool underline)>();
            foreach (var t in boldTokens)
            {
                italicTokens.AddRange(SplitByRegex(t.text, italicPattern, t.bold, isItalic: true, t.underline));
            }

            // 3) UNDERLINE
            var underlinePattern = new Regex(@"__(.+?)__");
            var finalTokens = new List<(string text, bool bold, bool italic, bool underline)>();
            foreach (var it in italicTokens)
            {
                finalTokens.AddRange(
                    SplitByRegex(it.text, underlinePattern, it.bold, it.italic, isUnderline: true)
                );
            }

            // 4) Build final result
            foreach (var ft in finalTokens)
            {
                var element = new ParsedElement(ft.text)
                {
                    IsBold = ft.bold,
                    IsItalic = ft.italic,
                    IsUnderline = ft.underline
                };
                result.Add(element);
            }

            return result;
        }

        private List<(string text, bool bold, bool italic, bool underline)> SplitByRegex(
            string input,
            Regex pattern,
            bool isBold = false,
            bool isItalic = false,
            bool isUnderline = false)
        {
            var output = new List<(string text, bool bold, bool italic, bool underline)>();
            int lastIndex = 0;
            var matches = pattern.Matches(input);

            foreach (Match m in matches)
            {
                if (m.Index > lastIndex)
                {
                    var normalText = input.Substring(lastIndex, m.Index - lastIndex);
                    if (!string.IsNullOrEmpty(normalText))
                    {
                        output.Add((normalText, false, false, false));
                    }
                }
                var capturedText = m.Groups[1].Value;
                output.Add((capturedText, isBold, isItalic, isUnderline));

                lastIndex = m.Index + m.Length;
            }

            if (lastIndex < input.Length)
            {
                var tailText = input.Substring(lastIndex);
                if (!string.IsNullOrEmpty(tailText))
                {
                    output.Add((tailText, false, false, false));
                }
            }

            return output;
        }
    }
}
