using System;
using System.Timers;
using System.Threading.Tasks;
using Serilog;
using RDK;
using RDK.Core;
using RDK.Core.Attributes;
using RDK.Core.Cache;
using RDK.Core.Mathematics;
using RDK.Core.Threading;
using RDK.Core.Styling;
using RDK.Core.IO;
using Pastel;

namespace RDK
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Settings.LoadBasic();

            /* =============================================
             * =============================================
             * ===============   TEST AREA   ===============
             * =============================================
             * =============================================
             */
            var gen = new Generator();
            Log.Debug(gen.ToString());
            Log.Debug(Convert.ToString(GuidGenerator.Int()));
            Log.Debug(Convert.ToString(GuidGenerator.Long()));
            Log.Debug(Convert.ToString(GuidGenerator.String()));
        }
    }

    public class Generator : Singleton<Generator>
    {
        public Generator()
        {

        }
    }
}
