using Cotization.Library.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cotization.Library.Infrastructure
{
	public class LimiteRepository: Repository<Limite>, ILimiteRepository
	{
		public LimiteRepository(CotizacionContext context) : base(context) { }

		public async Task<Limite> GetLimiteByIdAsync(long id)
		{
			return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
