using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;

        //any repositories that we use in unitofwork are gonna be stored in hashtable
        private Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

         //this returns the number of changes to our database
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            //if there are no repositories
            if(_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            //if our hashtable does not already contain entry for the entity with this specific name
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                //if we don't have repository for this particular type
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}