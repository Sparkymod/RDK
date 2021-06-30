using System;
using System.IO;
using Serilog.Sinks.SystemConsole.Themes;

namespace RDK.Core.Styling
{
    public class CustomConsoleTheme : ConsoleTheme
    {
        /// <summary>
        /// True if styling applied by the theme is written into the output, and can thus be
        /// buffered and measured.
        /// </summary>
        public override bool CanBuffer => false;

        /// <summary>
        /// Begin a span of text in the specified <paramref name="style"/>.
        /// </summary>
        /// <param name="output">Output destination.</param>
        /// <param name="style">Style to apply.</param>
        /// <returns></returns>
        protected override int ResetCharCount => 0;

        /// <summary>
        /// Reset the output to un-styled colors.
        /// </summary>
        /// <param name="output">The output.</param>
        public override void Reset(TextWriter output)
        {
            Console.ResetColor();
        }

        // Custom RDK Theme
        /// <summary>
        /// The number of characters written by the <see cref="Reset(TextWriter)"/> method.
        /// </summary>
        public override int Set(TextWriter output, ConsoleThemeStyle style)
        {
            Console.BackgroundColor = ConsoleColor.Black;

            switch (style)
            {
                case ConsoleThemeStyle.Text:                                
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ConsoleThemeStyle.SecondaryText:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case ConsoleThemeStyle.TertiaryText:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ConsoleThemeStyle.Null:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case ConsoleThemeStyle.Number:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case ConsoleThemeStyle.LevelInformation:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case ConsoleThemeStyle.LevelWarning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ConsoleThemeStyle.LevelError:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Beep();
                    break;
                case ConsoleThemeStyle.LevelFatal:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Beep();
                    break;
                default:
                    break;
            }
            return 0;
        }
    }
}
