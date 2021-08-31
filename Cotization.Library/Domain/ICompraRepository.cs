using System.Threading.Tasks;

namespace Cotization.Library.Domain
{
	public interface ICompraRepository : IRepository<Compra>
	{
		Task<Compra> GetCompraByIdAsync(long id);
	}
}