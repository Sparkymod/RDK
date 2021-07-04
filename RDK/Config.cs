using RDK.Core.IO;
using System.Globalization;
using Autofac;
using Serilog;
using Pastel;
using RDK.Core.Styling;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace RDK
{
    public static class Config
    {
        public static class Settings
        {
            private static ConsoleBase ConsoleInterface { get; set; } = new ConsoleBase();
            public static readonly string ConsoleTitle = "RDK Library";
            public static readonly string Version = "0.0.3";
            public static readonly string[] AsciiLogo =
            {
            $"{ConsoleTitle} - {Version}",
            " ____    ____    _  __",
            "|  _ \\  |  _ \\  | |/ /",
            "| |_) | | | | | | ' / ",
            "|  _ <  | |_| | | . \\ ",
            "|_| \\_\\ |____/  |_|\\_\\",
            "Library use as helper in C# .NET 5.0 projects.",
        };
            public static readonly string Language = "en-US";

            // Load basic settings for initialization
            public static void LoadBasic()
            {
                LoadConsole();
                SetCultureInfo();
            }

            private static void LoadConsole()
            {
                Serilog.Load();

                ConsoleInterface.Logo = AsciiLogo;
                ConsoleInterface.DrawAsciiLogo();
                ConsoleInterface.SetTitle($"{ConsoleTitle} - {Version}");
                ConsoleInterface.Start();
            }

            private static void SetCultureInfo()
            {
                // Force Globalization to en-US because we use periods instead of commas for decimals
                CultureInfo.CurrentCulture = new CultureInfo(Language);
            }
        }

        public static class Autofac
        {
            public static IContainer Configure()
            {
                ContainerBuilder builder = new();

                RegisterLogger(builder);
                builder.RegisterAssemblyTypes(Assembly.LoadFrom("RDK")).SingleInstance();
                return builder.Build();
            }

            public static void RegisterLogger(ContainerBuilder builder)
            {
                builder.Register((log) =>
                {
                    LoggerFactory factory = new();
                    factory.AddSerilog();
                    return factory;
                })
                    .As<ILoggerFactory>()
                    .SingleInstance();
                builder.RegisterGeneric(typeof(Logger<>))
                    .As(typeof(ILogger<>))
                    .SingleInstance();
            }
        }

        public static class Serilog
        {
            private static string Template { get; set; } = "{Timestamp:HH:mm:ss} [{Level:u3}]: {Message:lj} {NewLine}" + "{Exception}".Pastel("#E05561");

            /// <summary>
            /// Custom configuration for serilog to show on console and save to file.
            /// </summary>
            public static void Load()
            {
                Log.Logger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.Console(theme: ThemeCollection.RDKSerilogTheme,
                    outputTemplate: Template)
                    .CreateLogger();
            }
        }
    }
}
