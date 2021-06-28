using Dagher.Core;
using Dagher.Initialization.Serilog;

namespace Dagher
{
    public static class Settings
    {
        private static ConsoleBase ConsoleInterface
        {
            get;
            set;
        }

        private static readonly string ConsoleTitle = "Dagher Server";
        private static readonly string Version = "0.0.1";

        // Load basic settings for initialization
        public static void LoadBasicSettings()
        {
            SerilogConfiguration.Load();
            Settings.LoadConsoleSettings();
        }

        private static void LoadConsoleSettings()
        {
            ConsoleInterface = new ConsoleBase();
            ConsoleInterface.DrawAsciiLogo();
            ConsoleInterface.SetTitle($"{ConsoleTitle} - {Version}");
            ConsoleInterface.Start();
        }
    }
}
