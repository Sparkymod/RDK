using System;
using System.Timers;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection;
using Autofac;
using Serilog;
using RDK;
using RDK.Core;
using RDK.Core.Helpers;
using RDK.Core.Threading;
using RDK.Core.Styling;
using RDK.Core.Security;
using RDK.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace RDK.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.Settings.LoadBasic();

            Log.Debug("Repository starter with: ".RemoveSpecialChars());
            Log.Debug("continuación.".RemoveAccents());
            Log.Debug(new AsyncRandom().RandomString(10).FirstLetterUpper().ConcatCopy(3));
            Log.Debug("Wrap \"a few words\" with quotes.".EscapeString());

            IContainer testContainer = Config.Autofac.Configure();
            using ILifetimeScope scope = testContainer.BeginLifetimeScope();

            Log.Debug(Cryptography.GeneratePassword());

        }
    }
}
