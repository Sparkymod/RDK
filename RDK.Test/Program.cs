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
using Pastel;

namespace RDK.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.Settings.LoadBasic();

            IContainer testContainer = Config.Autofac.Configure();
            using ILifetimeScope scope = testContainer.BeginLifetimeScope();

            string line;
            while ((line = Console.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line) || line.Length > 0 && line.Substring(0, 1).Equals("/"))
                {
                    string[] argss = line[1..].ToLower().Split(" ");
                }
            }
        }
    }
}