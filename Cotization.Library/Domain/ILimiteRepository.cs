using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cotization.Library.Domain
{
	public interface ILimiteRepository: IRepository<Limite>
	{
		Task<Limite> GetLimiteByIdAsync(long id);
	}
}
