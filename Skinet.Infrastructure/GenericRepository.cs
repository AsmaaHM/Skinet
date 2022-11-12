using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skinet.Core;
using Skinet.Core.Specifications;
using Skinet.Infrastructure;
using Skinet.Infrastructure.Data;

namespace Core.Interfaces
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly SkinetContext _context;

		public GenericRepository(SkinetContext context)
		{
            _context = context; 
		}
        public async Task<T> GetByIdAsync(int id) {
            return await _context.Set<T>().FindAsync(id); 
        }
        public async Task<IReadOnlyList<T>> ListAllAsync() { 
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec) {
            return await ApplySpecification(spec).FirstOrDefaultAsync();   
        }
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec) { 
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec) {
            return await ApplySpecification(spec).CountAsync();
        }
        //void Add(T entity);
        //void Update(T entity);
        //void Delete(T entity);


        private IQueryable<T> ApplySpecification(ISpecification<T> spec) {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}