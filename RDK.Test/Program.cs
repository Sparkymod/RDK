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
using System.Collections.Generic;
using System.Linq;

namespace RDK.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings.LoadBasic();

            Log.Debug("Repository starter with: ".RemoveSpecialChars());
            Log.Debug("continuación.".RemoveAccents());
            Log.Debug(new AsyncRandom().RandomString(10).FirstLetterUpper().ConcatCopy(3));
            Log.Debug("Wrap \"a few words\" with quotes.".EscapeString());

            LoadAssembly load = new();
            load.RegisterAssembly(Assembly.LoadFrom("RDK"));
            load.Init();

            IContainer testContainer = AutofacConfig.Configure();
            using ILifetimeScope scope = testContainer.BeginLifetimeScope();

            Log.Debug(Cryptography.GeneratePassword());

        }
    }

    public class LoadAssembly
    {
        private List<Assembly> m_assemblies = new();

        public static ILogger logger = Log.Logger;

        public void RegisterAssembly(Assembly assembly)
        {
            m_assemblies.Add(assembly);
        }

        public void Init()
        {
            foreach (Assembly assembly in m_assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsClass)
                    {
                        logger.Debug(type.FullName());
                    }
                }
            }
        }
    }
}
