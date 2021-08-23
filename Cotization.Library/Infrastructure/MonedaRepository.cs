using Cotization.Library.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cotization.Library.Infrastructure
{
	public class MonedaRepository: Repository<Moneda>, IMonedaRepository
	{
		public MonedaRepository(CotizacionContext context): base(context) { }

		public async Task<Moneda> GetMonedaByIdAsync(string id)
		{
			return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
