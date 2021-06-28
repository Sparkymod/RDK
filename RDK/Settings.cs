using RDK.Core;
using RDK.Initialization.Serilog;

namespace RDK
{
    public static class Settings
    {
        private static ConsoleBase ConsoleInterface { get; set; } = new ConsoleBase();

        private static readonly string ConsoleTitle = "RDK Server";
        private static readonly string Version = "0.0.1";

        // Load basic settings for initialization
        public static void LoadBasic()
        {
            Settings.LoadConsole();
        }

        private static void LoadConsole()
        {
            SerilogConfiguration.Load();
            ConsoleInterface.DrawAsciiLogo();
            ConsoleInterface.SetTitle($"{ConsoleTitle} - {Version}");
            ConsoleInterface.Start();
        }
    }
}
