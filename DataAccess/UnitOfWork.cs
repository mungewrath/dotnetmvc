using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace dotnetmvc.DataAccess
{        
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext DbContext;
        private Dictionary<Type, object> Repositories;

        public UnitOfWork(DbContext DbContext)
        {
            this.DbContext = DbContext;
            Repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (Repositories.Keys.Contains(typeof(TEntity)))
                return Repositories[typeof(TEntity)] as IRepository<TEntity>;

            var repository = new Repository<TEntity>(DbContext);
            Repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public TEntity GetAddedObjectFromContext<TEntity>(TEntity previouslyAddedEntity) where TEntity : class
        {
            EntityEntry<TEntity> contextEntry = DbContext.Entry(previouslyAddedEntity);
            return contextEntry != null ? contextEntry.Entity : null;
        }

        #region Default DataAccess Commands

        /*
         * Sometimes we want the exceptions to bubble up beyond the UnitOfWork
         */
        public void SaveThrowException()
        {
            DbContext.SaveChanges();
        }

        public bool Save()
        {
            bool success = false;
            try
            {
                DbContext.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                Console.Write("UnitOfWork.Save(): Failed with exception of type {0}: {1}", ex.GetType().FullName, ex.Message);
            }

            return success;
        }

        /// <summary>
        /// Updates the modifiedon and modifiedby properties of all add or updated entities if they have those columns.
        /// Declared as virtual purely so it can be overridden in tests.
        /// </summary>
        /// <param name="BCUserId"></param>
        /// <returns></returns>
        public virtual bool ConcurrencyAwareSave(int BCUserId)
        {
            foreach (
                EntityEntry entity in
                    DbContext.ChangeTracker.Entries()
                        .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                try
                {
                    entity.Property("modifiedon").CurrentValue = DateTime.Now;
                    entity.Property("modifiedby").CurrentValue = BCUserId;
                }
                catch (Exception)
                {
                    // The entity in question doesn't have a modifiedby and/or modifiedon property
                }
            }

            return Save();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.DbContext != null)
                {
                    Console.Write("UnitOfWork: Disposing of DBContext");
                    this.DbContext.Dispose();
                    this.DbContext = null;
                }
                else
                {
                    Console.Write("UnitOfWork: DBContext already disposed - dispose called more than once");
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}