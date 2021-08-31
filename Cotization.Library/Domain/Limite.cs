using System;
using System.Collections.Generic;

#nullable disable

namespace Cotization.Library.Domain
{
    public partial class Limite
    {
        public long Id { get; set; }
        public long IdMoneda { get; set; }
        public long IdUsuario { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public decimal Monto { get; set; }

        public virtual Moneda Moneda { get; set; }
    }
}
