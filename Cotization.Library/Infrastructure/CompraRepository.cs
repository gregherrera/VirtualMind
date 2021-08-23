using Cotization.Library.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cotization.Library.Infrastructure
{
	public class CompraRepository: Repository<Compra>, ICompraRepository
	{
		public CompraRepository(CotizacionContext context) : base(context) { }

		public async Task<Compra> GetCompraByIdAsync(long id)
		{
			return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
