using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnetmvc.DataAccess
{
    public interface IRepository<TEntity>
     where TEntity : class
    {
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        IQueryable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        IQueryable<T> Query<T>(Func<IQueryable<TEntity>, IQueryable<T>> query);
        TEntity GetByID(object id);
        TEntity Insert(TEntity entity);
        IEnumerable<TEntity> InsertRange(List<TEntity> entities);
        TEntity UpdateValuesIfDetached(TEntity unsavedEntity, int unsavedEntityId);
    }
}