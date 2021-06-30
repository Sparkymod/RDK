using RDK.Core.IO;
using RDK.Initialization.Serilog;

namespace RDK
{
    public static class Settings
    {
        private static ConsoleBase ConsoleInterface { get; set; } = new ConsoleBase();

        private static readonly string ConsoleTitle = "RDK Library";
        private static readonly string Version = "0.0.1";
        private static readonly string[] AsciiLogo =
        {
            " ____    ____    _  __",
            "|  _ \\  |  _ \\  | |/ /",
            "| |_) | | | | | | ' / ",
            "|  _ <  | |_| | | . \\ ",
            "|_| \\_\\ |____/  |_|\\_\\",
            "Library use as helper in C# .NET 5.0 projects",
        };

        // Load basic settings for initialization
        public static void LoadBasic()
        {
            Settings.LoadConsole();
        }

        private static void LoadConsole()
        {
            SerilogConfiguration.Load();
            ConsoleInterface.Logo = AsciiLogo;
            ConsoleInterface.DrawAsciiLogo();
            ConsoleInterface.SetTitle($"{ConsoleTitle} - {Version}");
            ConsoleInterface.Start();
        }
    }
}
