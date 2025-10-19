using DataAccess.Data;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly DataContext _context;

		public GenericRepository(DataContext context)
        {
			_context = context;
		}
		public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

		public void Delete(T entity) => _context.Set<T>().Remove(entity);

		public async Task<ICollection<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

		public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

		public void Update(T entity) => _context.Set<T>().Update(entity);
	}
}
