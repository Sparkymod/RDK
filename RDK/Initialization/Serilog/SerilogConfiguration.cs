using Serilog;
using Pastel;
using RDK.Core.Styling;

namespace RDK.Initialization.Serilog
{
    public class SerilogConfiguration
    {
        private static string Template { get; set; } = "{Timestamp:HH:mm:ss} [{Level:u3}]: {Message:lj} {NewLine}"+"{Exception}".Pastel("#E05561");

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
