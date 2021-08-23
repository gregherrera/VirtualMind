
using System.Threading.Tasks;

namespace Cotization.Library.Domain
{
	public interface IMonedaRepository: IRepository<Moneda>
	{
		Task<Moneda> GetMonedaByIdAsync(string id);
	}
}
