using Microsoft.EntityFrameworkCore;
using System;
using Serilog;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using RDK.Database.Core;
using System.Threading.Tasks;
using Pastel;

namespace RDK.Database.Core
{
    public abstract class DatabaseRequest<TEntity, TContext> : IDisposable, IRepository<TEntity> where TEntity : class, IEntity where TContext : DbContext
    {
        public static TContext Context { get; set; }

        public DatabaseRequest(TContext context) => Context = context;

        public async Task<TEntity> Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            await Commit();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            TEntity entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            Context.Set<TEntity>().Remove(entity);
            await Commit();

            return entity;
        }

        public async Task<TEntity> Get(int id) => await Context.Set<TEntity>().FindAsync(id);

        public async Task<List<TEntity>> GetAll() => await Context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Commit();
            return entity;
        }

        private static async Task<bool> Commit()
        {
            try
            {
                Log.Logger.Warning("Saving changes...");
                await Context.SaveChangesAsync();
                Log.Logger.Warning("Saved!...".Pastel("#aced66"));
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error saving changes => {ex}");
                return false;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
