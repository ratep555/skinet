using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    //we want this only to be usable from the classes that derive from our baseentity
    public interface IGenericRepository<T> where T : BaseEntity
    {
         Task<T> GetByIdAsync(int id);
         Task<IReadOnlyList<T>> ListAllAsync();

         Task<T> GetEntityWithSpec(ISpecification<T> spec);
         Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
         Task<int> CountAsync(ISpecification<T> spec);
         //none of these down methods are asynchronous because we wont be adding
         //them to the database
         void Add(T entity);
         void Update(T entity);
         void Delete(T entity);
    }
}