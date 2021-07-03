using RDK.Core.IO;
using RDK.Initialization;
using System.Globalization;

namespace RDK
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
            Settings.LoadConsole();
            Settings.SetCultureInfo();
        }

        private static void LoadConsole()
        {
            SerilogConfig.Load();

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
}
