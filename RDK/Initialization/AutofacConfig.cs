using Autofac;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace RDK.Initialization
{
    public static class AutofacConfig
    {
        public static IContainer Configure()
        {
            ContainerBuilder builder = new();

            builder.RegisterLogger();
            builder.RegisterAssemblyTypes(Assembly.LoadFrom("RDK")).SingleInstance();
            return builder.Build();
        }

        public static void RegisterLogger(this ContainerBuilder builder)
        {
            builder.Register((log) =>
            {
                LoggerFactory factory = new();
                factory.AddSerilog();
                return factory;
            })
                .As<ILoggerFactory>()
                .SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>))
                .SingleInstance();
        }
    }
}
