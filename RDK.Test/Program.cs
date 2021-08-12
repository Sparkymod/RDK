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
using System.Numerics;
using DijkstraAlgorithm.Graphing;
using Dijkstra.NET;
using Dijkstra.NET.Graph;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Windows;
using RDK.Database.Core;

namespace RDK.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings.DotEnv.Load();
            Settings.Config.LoadBasic();
            Settings.Autofac.InitializeDatabase();
            Settings.Autofac.Manager(out AccountManager accountManager);

            try
            {
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return;
            }

        }
    }
}