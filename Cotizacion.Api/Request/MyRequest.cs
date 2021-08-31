namespace Cotizacion.Api.Request
{
	public class MyRequest
	{
		public int Id { get; set; }
		public int IdUsuario { get; set; }
		public long IdMoneda { get; set; }
		public decimal Monto { get; set; }
	}
}
