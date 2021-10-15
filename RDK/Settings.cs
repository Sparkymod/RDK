using RDK.Core.IO;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Globalization;
using System.IO;

namespace RDK
{
    public static class Settings
    {
        /// <summary>
        /// Set constant paths.
        /// </summary>
        public static class Paths
        {
            public static readonly string SOLUTION_DIR = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../.."));
        }

        /// <summary>
        /// Current settings for this library.
        /// </summary>
        public static class Config
        {
            private static ConsoleBase ConsoleInterface { get; set; } = new ConsoleBase();
            public static string ConsoleTitle { get; private set; } = Environment.GetEnvironmentVariable("TITLE");
            public static string Version { get; private set; } = Environment.GetEnvironmentVariable("VERSION");
            public static string Language { get; private set; } = Environment.GetEnvironmentVariable("LANGUAGE");
            public static string[] AsciiLogo { get; private set; } =
            {
            $"{ConsoleTitle} - {Version}",
            " ____    ____    _  __",
            "|  _ \\  |  _ \\  | |/ /",
            "| |_) | | | | | | ' / ",
            "|  _ <  | |_| | | . \\ ",
            "|_| \\_\\ |____/  |_|\\_\\",
            "Library use as helper in C# .NET 6.0 projects.",
            };

            /// <summary>
            /// Load basic settings for initialization.
            /// </summary>
            public static void LoadBasic()
            {
                LoadConsole();
                SetCultureInfo();
            }

            /// <summary>
            /// Load Console settings.
            /// </summary>
            private static void LoadConsole()
            {
                ConsoleInterface.Logo = AsciiLogo;
                ConsoleInterface.DrawAsciiLogo();
                ConsoleInterface.SetTitle($"{ConsoleTitle} - {Version}");
                ConsoleInterface.Start();
            }

            /// <summary>
            /// Set Global Language for this program.
            /// </summary>
            private static void SetCultureInfo()
            {
                CultureInfo.CurrentCulture = new CultureInfo(Language);
            }

            /// <summary>
            /// Load Database settings.
            /// </summary>
            /// <returns>Connection string for MySQL DB.</returns>
            public static string GetMySQLDBConnectionString()
            {
                string server = Environment.GetEnvironmentVariable("DB_IP");
                string port = Environment.GetEnvironmentVariable("DB_PORT");
                string name = Environment.GetEnvironmentVariable("DB_NAME");
                string user = Environment.GetEnvironmentVariable("DB_USER");
                string password = Environment.GetEnvironmentVariable("DB_PASSWORD");

                return $"server={server};port={port};database={name};user={user};password={password}";
            }

            /// <summary>
            /// Initialize Serilog.
            /// </summary>
            /// <returns>Instanciate a serilog for the current project.</returns>
            public static Logger InitializeSerilog()
            {
                Log.Logger = Serilog.Config().CreateLogger();
                return Serilog.Config().CreateLogger();
            }
        }

        /// <summary>
        /// Serilog Settings.
        /// </summary>
        public static class Serilog
        {
            public static string Template { get; set; } = "{Timestamp:HH:mm:ss} [{Level:u4}]: {Message:lj} {NewLine}" + "{Exception}";

            /// <summary>
            /// Custom configuration for serilog to show on console and save to file.
            /// </summary>
            public static LoggerConfiguration Config()
            {
                return new LoggerConfiguration()
                    .MinimumLevel.Override("Default", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .MinimumLevel.Verbose()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(theme: Theme.RDKSerilogTheme, outputTemplate: Template)
                    .WriteTo.File(Path.Combine(Paths.SOLUTION_DIR, "maplecodex2.log"), LogEventLevel.Error);
            }
        }

        /// <summary>
        /// Dot Environment Settings.
        /// </summary>
        public static class DotEnv
        {
            public static string FilePath { get; set; }

            public static void Load(string filepath = "rdk.env")
            {
                FilePath = Path.Combine(Paths.SOLUTION_DIR, filepath);

                if (!File.Exists(FilePath))
                {
                    throw new ArgumentException(".env file not found!");
                }
                foreach (string line in File.ReadAllLines(FilePath))
                {
                    string[] parts = line.Split('=', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length != 2)
                    {
                        continue;
                    }

                    Environment.SetEnvironmentVariable(parts[0], parts[1]);
                }
            }
        }

        /// <summary>
        /// Diferent styles for specific things.
        /// </summary>
        public static class Theme
        {
            public static CustomConsoleTheme RDKSerilogTheme { get; } = new CustomConsoleTheme();

            public sealed class CustomConsoleTheme : ConsoleTheme
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

                /// <summary>
                /// Custom RDK Theme =
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
    }
}
