using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    //idisposable will look for dispose method in our unitofworkclass and
    //when we finish our transaction, it will dispose of our context
    public interface IUnitOfWork : IDisposable
    {
         IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

         //this returns the number of changes to our database
         Task<int> Complete();
    }
}