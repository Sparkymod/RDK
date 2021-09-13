using Pastel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace RDK.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Normalize char with accents.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The same <see langword="string"/> without any accents.</returns>
        public static string RemoveAccents(this string input)
            => string.Concat(input.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark));

        /// <summary>
        /// Normalize char with accents.
        /// </summary>
        /// <param name="input"></param>
        /// <returns><see langword="true"/> if finds a word with accent; otherwise <see langword="false"/>.</returns>
        public static bool HasAccents(this string input) => input.Normalize(NormalizationForm.FormD)
                .Any(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark);

        /// <summary>
        /// Lower all other strings, except the first one.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Same <see langword="string"/> all lower except the first one.</returns>
        public static string FirstLetterUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            char[] letters = input.ToLower().ToCharArray();
            letters[0] = char.ToUpper(letters[0]);

            return new string(letters);
        }

        /// <summary>
        /// Copy the same string and concatenate it x times.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="times"></param>
        /// <returns>Same <see langword="string"/> x times added.</returns>
        public static string ConcatCopy(this string input, int times)
        {
            StringBuilder sBuilder = new(input.Length * times);

            for (int i = 0; i < times; i++)
            {
                sBuilder.Append(input);
            }

            return sBuilder.ToString();
        }

        /// <summary>
        /// Randomize string within the size specified.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="size"></param>
        /// <returns>Random <see langword="string"/>.</returns>
        public static string RandomString(this Random random, int size)
        {
            StringBuilder sBuilder = new();

            for (int i = 0; i < size; i++)
            {
                //26 letters in the alphabet, ascii + 65 for the capital letters
                sBuilder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))));
            }
            return sBuilder.ToString();
        }

        public static string[] SplitAdvanced(this string expression, string[] delimiters, string qualifier, bool ignoreCase)
        {
            bool qualifierState = false;
            int startIndex = 0;
            List<string> values = new();

            for (int charIndex = 0; charIndex < expression.Length - 1; charIndex++)
            {
                if (qualifier != null)
                {
                    if (string.Compare(expression.Substring(charIndex, qualifier.Length), qualifier, ignoreCase) == 0)
                    {
                        qualifierState = !(qualifierState);
                    }
                    else if (!qualifierState && delimiters.Any(x => string.Compare(expression.Substring(charIndex, x.Length), x, ignoreCase) == 0))
                    {
                        values.Add(expression[startIndex..charIndex]);
                        startIndex = charIndex + 1;
                    }
                }
            }

            if (startIndex < expression.Length)
            {
                values.Add(expression[startIndex..]);
            }

            string[] returnValues = new string[values.Count];
            values.CopyTo(returnValues);
            return returnValues;
        }

        public static string[] SplitAdvanced(this string expression, string delimiter)
            => SplitAdvanced(expression, delimiter, "", false);

        public static string[] SplitAdvanced(this string expression, string delimiter, string qualifier)
            => SplitAdvanced(expression, delimiter, qualifier, false);


        public static string[] SplitAdvanced(this string expression, string delimiter, string qualifier, bool ignoreCase)
            => SplitAdvanced(expression, new[] { delimiter }, qualifier, ignoreCase);

        /// <summary>
        /// Character that invokes an alternative interpretation on the following characters in a character sequence.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Same <see langword="string"/> adding the literal escaped characters.</returns>
        public static string EscapeString(this string input) => input == null ? null : Regex.Replace(input, @"[\r\n\x00\x1a\\'""]", @"\$0");

        /// <summary>
        /// Find and remove any special char, only letting numbers and letters in.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Same <see langword="string"/> without any special char.</returns>
        public static string RemoveSpecialChars(this string input) => input == null ? null : Regex.Replace(input, @"[^0-9a-zA-Z]", " ");

        /// <summary>
        /// Converts a string that has been HTML-encoded for HTTP transmission into a decoded or encoded string.
        /// </summary>
        /// <param name="str">The string to decode.</param>
        /// <returns>A decoded string by default; otherwise mode=1 for an encoded string.</returns>
        public static string HtmlEntities(this string str, byte mode = 0)
        {
            return mode switch
            {
                1 => HttpUtility.HtmlEncode(str),
                _ => HttpUtility.HtmlDecode(str),
            };
        }

        public static string RemoveWhitespace(this string input) => new(input.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());

        public static string GetMD5(this string input) => Security.Cryptography.GetMD5Hash(input);

        public static string GetSHA256(this string input) => Security.Cryptography.GetSHA256(input);

        public static string GetRSA(this string input, RSAParameters param) => Security.Cryptography.EncryptRSA(input, param);
    }

    /// <summary>
    /// Make the strings to a specific color using the NuGet Package <b>Pastel.</b>
    /// </summary>
    public static class ColorStringExtensions
    {
        public static string Green(this string input) => input.Pastel("#aced66");

        public static string Red(this string input) => input.Pastel("#E05561");

        public static string Yellow(this string input) => input.Pastel("#FFE212");
    }
}
