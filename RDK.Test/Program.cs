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
using RDK.Core.Security;
using RDK.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using Pastel;
using System.IO;
using RDK.Api;
using RDK.Database.Manager;

namespace RDK.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings.DotEnv.Load();
            Settings.Config.LoadBasic();

            IContainer testContainer = Settings.Autofac.Configure();
            using ILifetimeScope scope = testContainer.BeginLifetimeScope();
            scope.Resolve<DatabaseManager>();

            DatabaseManager.InitDatabase();
        }
    }
}