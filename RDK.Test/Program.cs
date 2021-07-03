using System;
using System.Timers;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection;
using Autofac;
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
using RDK.Initialization;

namespace RDK.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings.LoadBasic();


            Log.Debug("Repository starter with: ");
            Log.Debug("Mámalo, tengo todo lo que necesito a continuación.".RemoveAccents());

            IContainer testContainer = AutofacConfig.Configure();
            using ILifetimeScope scope = testContainer.BeginLifetimeScope();
        }
    }
}
