using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
//using Core.Specifications;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
    //sa genericrepository smanjujemo količinu koda
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity    
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //we are using set, we dont know which entity it is
            //hover over set i pogledaj definiciju
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            //we are passing iqueriable ApplySpecification, so we can apply further methods
            //we return the first product that matches our specification
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        //lekcija 37, 38, niš ne kužiš:)
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
          public async Task<int> CountAsync(ISpecification<T> spec)
        {
            //we want the number of results
            return await ApplySpecification(spec).CountAsync();
        }
        //we have access to T, this will be replaced with product for example
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            //we are using set method to set the type of property this is
            //T is for example product
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public void Add(T entity)
        {
            //hover over add
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}