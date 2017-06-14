using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace dotnetmvc.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext dbContext;
        internal DbSet<TEntity> dbSet;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public virtual IQueryable<T> Query<T>(Func<IQueryable<TEntity>, IQueryable<T>> query)
        {
            return QuerySet(this.dbSet, query);
        }

        public static IQueryable<T> QuerySet<T>(IQueryable<TEntity> set, Func<IQueryable<TEntity>, IQueryable<T>> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            if (set == null)
            {
                throw new ArgumentNullException("set");
            }

            return query(set);
        }

        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {

                foreach (var includeProperty in includeProperties.Split
                    (new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query);
                }
                else
                {
                    return query;
                }
            }
            else
            {
                return null;
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return dbSet.Add(entity).Entity;
        }

        public virtual IEnumerable<TEntity> InsertRange(List<TEntity> entities)
        {
            dbSet.AddRange(entities);
            return entities;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Update which retrieves and modifies an attached entity if unsavedEntity is detached from the context.
        /// Entities which are attached to the context will automatically have changes detected by Entity Framework.
        /// As of EF 6.1: SetValues does NOT cascade through the object graph (changes on navigation property objects are not captured)
        /// </summary>
        /// <param name="unsavedEntity">The entity with changes to be saved.</param>
        /// <param name="unsavedEntityId">The entity id.</param>
        /// <returns>An entity attached to the context.</returns>
        public virtual TEntity UpdateValuesIfDetached(TEntity unsavedEntity, int unsavedEntityId)
        {
            if (dbContext.Entry(unsavedEntity).State == EntityState.Detached)
            {
                TEntity attachedEntity = GetByID(unsavedEntityId);
                dbContext.Entry(attachedEntity).CurrentValues.SetValues(unsavedEntity);
                return attachedEntity;
            }
            return unsavedEntity;
        }
    }
}