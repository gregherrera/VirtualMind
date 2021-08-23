using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cotization.Library.Domain
{	public interface IRepository<T> where T : class
	{
		IQueryable<T> GetAll();
		IQueryable<T> Find(Expression<Func<T, bool>> predicate);
		Task Add(T entity);
		Task AddRange(IEnumerable<T> entities);
	}
}
