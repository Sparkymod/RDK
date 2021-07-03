using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace RDK.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Normalize char with accents.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>The same string without any accents.</returns>
        public static string RemoveAccents(this string source)
            => string.Concat(source.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark));

        /// <summary>
        /// Normalize char with accents.
        /// </summary>
        /// <param name="source"></param>
        /// <returns><see langword="true"/> if finds a word with accent; otherwise <see langword="false"/>.</returns>
        public static bool HasAccents(this string source) => source.Normalize(NormalizationForm.FormD)
                .Any(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark);
    }
}
