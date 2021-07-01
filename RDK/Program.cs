using System;
using System.Timers;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Serilog;
using RDK;
using RDK.Core;
using RDK.Core.Attributes;
using RDK.Core.Cache;
using RDK.Core.Mathematics;
using RDK.Core.Threading;
using RDK.Core.Styling;
using RDK.Core.IO;
using RDK.Core.Cryptography;
using RDK.Core.Extensions;
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

            string input = "prueba";
            string md5hash = Cryptography.GetMD5Hash(input);

            Log.Debug(Convert.ToString(GuidGenerator.Int()));
            Log.Debug(Convert.ToString(GuidGenerator.Long()));
            Log.Debug(Convert.ToString(GuidGenerator.String()));

            Log.Debug(md5hash+" Hash");
            Log.Debug(Cryptography.VerifyMD5Hash(input, md5hash).ToString());
        }
    }
}
