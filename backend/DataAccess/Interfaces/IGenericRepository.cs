﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T> GetByIdAsync(int id);
		Task<ICollection<T>> GetAllAsync();
		Task AddAsync (T entity);
		void Update (T entity);
		void Delete (T entity);

	}
}
