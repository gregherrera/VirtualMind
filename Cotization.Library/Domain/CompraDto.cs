using System;

namespace Cotization.Library.Domain
{
    public partial class CompraDto
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public long IdMoneda { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Tasa { get; set; }
        public decimal Monto { get; set; }
        public decimal Valor { get; set; }
		public string MonedaDescripcion { get; set; }
    }
}
