using RDK.Core.Reflection;
using RDK.Database.Manager;
using RDK;
using RDK.Database;
using Microsoft.EntityFrameworkCore;
using static RDK.Settings;

// Main entry of the program.

DotEnv.Load();
Config.LoadBasic();

DbContextOptionsBuilder optionsBuilder = new();
optionsBuilder.UseMySQL(Config.GetConnectionString());

object context = new DatabaseContext(optionsBuilder.Options);
DatabaseManager manager = Generator.InstanciateConstructorWithParams<DatabaseManager>(new Type[] { typeof(DatabaseContext) }, context);
manager.InitDatabase();