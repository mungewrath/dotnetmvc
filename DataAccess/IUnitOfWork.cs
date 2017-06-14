using System;

namespace dotnetmvc.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        TEntity GetAddedObjectFromContext<TEntity>(TEntity previouslyAddedEntity) where TEntity : class;
        bool Save();
        void SaveThrowException();
        bool ConcurrencyAwareSave(int BCUserId);
    }
}