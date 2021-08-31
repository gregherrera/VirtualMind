using Cotization.Library.Domain;
using Cotization.Library.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cotizacion.Test.Cotizacion
{
	public class CotizacionFixture
	{
		private readonly DbContextOptions<CotizacionContext> options = new DbContextOptionsBuilder<CotizacionContext>()
			.UseInMemoryDatabase(databaseName: "CotizacionTest")
			.Options;

		public readonly CotizacionContext context = null;

		public CotizacionFixture()
		{
			this.context = new CotizacionContext(options);

			MonedaRepository monedaRepository = new MonedaRepository(this.context);

			var monedas = new List<Moneda> {
				new Moneda { Descripcion = "dolar", Url = "https://www.bancoprovincia.com.ar/Principal/Dolar", Factor = 1 },
				new Moneda { Descripcion = "real", Url = "https://www.bancoprovincia.com.ar/Principal/Dolar", Factor = Convert.ToDecimal(0.25) }
			};

			Task.FromResult(monedaRepository.AddRange(monedas));
		}
	}
}
