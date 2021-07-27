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
using System.ComponentModel;
using IContainer = Autofac.IContainer;

namespace RDK.Test
{
    class Program
    {
        public static readonly CommandManager CommandManager = new();

        static void Main(string[] args)
        {
            Config.Settings.LoadBasic();

            IContainer testContainer = Config.Autofac.Configure();
            using ILifetimeScope scope = testContainer.BeginLifetimeScope();
            CommandManager.RegisterAll(Assembly.GetAssembly(typeof(CommandBase)));

            string line;
            string session = "GameSession";
            while ((line = Console.ReadLine()) != null)
            {
                if (line.Substring(0, 1).Equals("/") && !string.IsNullOrEmpty(line))
                {
                    string[] argss = line[1..].ToLower().Split(" ");
                    CommandManager.HandleCommand(new GameCommandTrigger(argss, session));
                }
            }
        }
    }

    public class Parameter<T> : IParameter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public T Value { get; private set; }
        public Type ValueType { get; set; }

        public Parameter(string name, string description = "", T defaultValue = default)
        {
            Name = name;
            Description = description;
            Value = defaultValue;
        }

        public void SetValue(string str) => Value = (T)ConvertString(str);

        public void SetDefaultValue() => Value = (T)ConvertString(string.Empty);

        public object ConvertString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Value;
            }
            if (Value is string)
            {
                return value;
            }
            return Convert.ChangeType(value, typeof(T));
        }

        object IParameter.DefaultValue => Value;
    }

    public interface IParameter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        object DefaultValue { get; }

        object ConvertString(string value);
        void SetValue(string str);
        void SetDefaultValue();
    }

    public interface IParameter<out T> : IParameter
    {
        T Value { get; }
    }

    public abstract class CommandTrigger
    {
        public string[] Args { get; private set; }
        public CommandBase BoundCommand { get; private set; }
        internal Dictionary<string, IParameter> CommandsParametersByName { get; private set; }

        public CommandTrigger(string[] args)
        {
            Args = args;
            CommandsParametersByName = new Dictionary<string, IParameter>();
        }

        public virtual T Get<T>(string name)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter != null && CommandsParametersByName.TryGetValue(name, out IParameter value))
            {
                return value is T t ? t : throw new ArgumentException(value.ToString());
            }
            throw new ArgumentException(name);
        }

        public void BindToComand(CommandBase command)
        {
            if (Args.Length == 0)
            {
                return;
            }

            BoundCommand = command;

            List<IParameter> definedParam = new(BoundCommand.Parameters);
            int count = 0;

            foreach (string arg in Args)
            {
                if (command.Aliases.Any(x => x == arg))
                {
                    continue;
                }
                definedParam[count].SetValue(arg);
                count++;
            }

            CommandsParametersByName = BoundCommand.Parameters.ToDictionary(entry => entry.Name);
        }
    }

    public abstract class CommandBase
    {
        public string[] Aliases { get; protected set; }
        public string Description { get; set; }
        public List<IParameter> Parameters { get; protected set; }

        public CommandBase()
        {
            Parameters = new List<IParameter>();
        }

        public void AddParameter<T>(string argument, string description = "", T defaultValue = default) => Parameters.Add(new Parameter<T>(argument, description, defaultValue));

        public abstract void Execute(CommandTrigger trigger);

        public override string ToString() => GetType().Name;
    }

    public class CommandManager
    {
        public IDictionary<string, CommandBase> CommandsByAlias { get; set; }

        public CommandManager()
        {
            CommandsByAlias = new Dictionary<string, CommandBase>();
        }

        public void RegisterAll(Assembly assembly)
        {
            Log.Information("Registering Commands...");

            if (assembly == null)
            {
                Log.Error("Current Assembly was null.");
                throw new ArgumentNullException(nameof(assembly));
            }
            IEnumerable<Type> callTypes = assembly.GetTypes().Where(entry => !entry.IsAbstract);

            foreach (Type commandType in callTypes)
            {
                if (commandType.IsSubclassOf(typeof(CommandBase)))
                {
                    RegisterCommand(commandType);
                    Log.Information(commandType.Name + " was registered.");
                }
            }
            Log.Information("Commands loaded.".Pastel("#aced66"));
        }

        private void RegisterCommand(Type commandType)
        {
            if (Activator.CreateInstance(commandType) is not CommandBase instanceCommand)
            {
                Log.Error("Cannot create a new instance of {0}", commandType);
                throw new Exception(commandType.Name);
            }

            if (instanceCommand.Aliases == null)
            {
                Log.Error("Cannot register Command {0}, Aliases is null", commandType.Name);
                throw new Exception(instanceCommand.ToString());
            }

            foreach (string alias in instanceCommand.Aliases)
            {
                if (!CommandsByAlias.TryGetValue(alias, out CommandBase command))
                {
                    CommandsByAlias[alias.ToLower()] = instanceCommand;
                }
                else
                {
                    Log.Error("Found two Commands with Alias \"{0}\": {1} and {2}", alias, command, instanceCommand);
                }
            }
        }

        public void HandleCommand(CommandTrigger trigger)
        {
            if (trigger == null)
            {
                Log.Error("No CommandTrigger were found.");
                return;
            }

            CommandBase command = GetCommand(trigger.Args[0]);

            if (command == null || !command.Aliases.Any(x => x == trigger.Args[0]))
            {
                Log.Error("No Command were found with the current alias.");
                return;
            }

            trigger.BindToComand(command);
            command.Execute(trigger);
        }

        public CommandBase GetCommand(string alias)
        {
            CommandsByAlias.TryGetValue(alias, out CommandBase command);
            return command;
        }
    }

    public class GameCommandTrigger : CommandTrigger
    {
        public string Session { get; private set; }

        public GameCommandTrigger(string[] args, string session) : base(args)
        {
            Session = session;
        }
    }

    public abstract class InGameCommand : CommandBase
    {
        public override void Execute(CommandTrigger trigger)
        {
            if (!(trigger is GameCommandTrigger))
            {
                Log.Error("This command can only be executed in game.");
                return;
            }

            Execute(trigger as GameCommandTrigger);
        }

        public abstract void Execute(GameCommandTrigger trigger);
    }

    public class MapCommand : InGameCommand
    {
        public MapCommand()
        {
            Aliases = new[]
            {
                "map"
            };
            Description = "Give informations about a map";
            AddParameter<int>("id", "The map id.");
            AddParameter<int>("instance", "The instance id.");
        }

        public override void Execute(GameCommandTrigger trigger)
        {
            int mapId = trigger.Get<int>("id");
            int instanceId = trigger.Get<int>("instance");

            Console.WriteLine($"Map id: {mapId}\nInstance id: {instanceId} and this is {trigger.Session}");
        }
    }
}
