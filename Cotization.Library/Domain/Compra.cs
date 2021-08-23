using System;

namespace Cotization.Library.Domain
{
    public partial class Compra
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public string IdMoneda { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Tasa { get; set; }
        public decimal Monto { get; set; }
        public decimal Valor { get; set; }

        public virtual Moneda Moneda { get; set; }
    }
}
