using Serilog;

namespace Dagher.Initialization.Serilog
{
    public class SerilogConfiguration
    {
        // Custom configuration for serilog to show on Console and file
        public static void Load()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }
    }
}
