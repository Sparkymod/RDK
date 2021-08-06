﻿using RDK.Core.IO;
using System.Globalization;
using Autofac;
using Serilog;
using Pastel;
using RDK.Core.Styling;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using RDK.Database;
using RDK.Database.Manager;

namespace RDK
{
    public static class Settings
    {
        // Current settings for this library.
        public static class Config
        {
            private static ConsoleBase ConsoleInterface { get; set; } = new ConsoleBase();
            public static readonly string ConsoleTitle = "RDK Library";
            public static readonly string Version = "0.0.7";
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

            // Load basic settings for initialization.
            public static void LoadBasic()
            {
                LoadConsole();
                SetCultureInfo();
            }

            // Load Console settings.
            private static void LoadConsole()
            {
                Serilog.Load();

                ConsoleInterface.Logo = AsciiLogo;
                ConsoleInterface.DrawAsciiLogo();
                ConsoleInterface.SetTitle($"{ConsoleTitle} - {Version}");
                ConsoleInterface.Start();
            }

            // Set Global Language for this program
            private static void SetCultureInfo()
            {
                // Force Globalization to en-US because we use periods instead of commas for decimals
                CultureInfo.CurrentCulture = new CultureInfo(Language);
            }

            // Load Database settings
            internal static string GetConnectionString()
            {
                string server = Environment.GetEnvironmentVariable("DB_IP");
                string port = Environment.GetEnvironmentVariable("DB_PORT");
                string name = Environment.GetEnvironmentVariable("DB_NAME");
                string user = Environment.GetEnvironmentVariable("DB_USER");
                string password = Environment.GetEnvironmentVariable("DB_PASSWORD");

                return $"server={server};port={port};database={name};user={user};password={password}";
            }
        }

        // Autofac Settings.
        public static class Autofac
        {
            public static IContainer Configure()
            {
                ContainerBuilder builder = new();
                RegisterLogger(builder);

                builder.Register(db =>
                {
                    DbContextOptionsBuilder optionsBuilder = new ();
                    optionsBuilder.UseMySQL(Config.GetConnectionString());
                    return new DatabaseManager(new DatabaseContext(optionsBuilder.Options));
                });
                return builder.Build();
            }

            private static void RegisterLogger(ContainerBuilder builder)
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

        // Serilog Settings.
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

        // Dot Environment Settings.
        public static class DotEnv
        {
            public static void Load(string filePath)
            {
                foreach (string line in File.ReadAllLines(filePath))
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

        // Set constant paths.
        public static class Paths
        {
            public static readonly string SOLUTION_DIR = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../.."));
        }
    }
}
