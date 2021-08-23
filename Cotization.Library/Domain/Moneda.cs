using System;
using System.Collections.Generic;

#nullable disable

namespace Cotization.Library.Domain
{
    public partial class Moneda
    {
        public Moneda()
        {
            Compras = new HashSet<Compra>();
            Limites = new HashSet<Limite>();
        }

        public string Id { get; set; }
        public string Url { get; set; }
        public decimal Factor { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Limite> Limites { get; set; }
    }
}
