using System.Text.RegularExpressions;

namespace MyPokedex.ApplicationServices.Helper
{
    public static class StringExtensions
    {
        public static string RemoveEscapeSequenceIfAny(this string inputText)
        {
            return inputText.Replace("\a",string.Empty)
                            .Replace("\b", string.Empty)
                            .Replace("\f", string.Empty)
                            .Replace("\n", string.Empty)
                            .Replace("\r", string.Empty)
                            .Replace("\t", string.Empty)
                            .Replace("\v", string.Empty)
                            .Replace("\'", string.Empty)
                            .Replace("\"", string.Empty)
                            .Replace("\\", string.Empty);
        }
    }
}
