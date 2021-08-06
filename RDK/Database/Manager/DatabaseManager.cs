using Serilog;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Pastel;
using RDK.Database.Core;

namespace RDK.Database.Manager
{
    public class DatabaseManager : DatabaseRequest<IEntity, DatabaseContext>
    {
        public DatabaseManager(DatabaseContext context) : base(context) => Context = context;

        private static bool Exists() => ((RelationalDatabaseCreator)Context.Database.GetService<IDatabaseCreator>()).Exists();

        private static void CreateDatabase() => Context.Database.EnsureCreated();

        public static void InitDatabase()
        {
            if (Exists())
            {
                Log.Information("Database already exists.");
                return;
            }
            Log.Information("Creating database...");
            CreateDatabase();

            Log.Information("Database created.".Pastel("#aced66"));
        }
    }
}
