using Cotization.Library.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cotization.Library.Infrastructure
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly CotizacionContext context;
		private DbSet<T> entities;

		public Repository(CotizacionContext context)
		{
			this.context = context;
			entities = context.Set<T>();
		}

		public IQueryable<T> GetAll()
		{
			return entities;
		}

		public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
		{
			return entities.Where(predicate);
		}

		public async Task Add(T entity)
		{
			await entities.AddAsync(entity);
			await context.SaveChangesAsync();
		}
		public async Task AddRange(IEnumerable<T> entities)
		{
			await context.AddRangeAsync(entities);
			await context.SaveChangesAsync();
		}
	}
}
