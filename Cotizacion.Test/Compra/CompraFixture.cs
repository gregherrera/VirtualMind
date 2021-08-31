using Cotization.Library.Domain;
using Cotization.Library.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cotizacion.Test.Compra
{
	public class CompraFixture
	{
		private readonly DbContextOptions<CotizacionContext> options = new DbContextOptionsBuilder<CotizacionContext>()
			.UseInMemoryDatabase(databaseName: "CompraTest")
			.Options;

		public readonly CotizacionContext context = null;
		public CompraFixture()
		{
			this.context = new CotizacionContext(options);

			MonedaRepository monedaRepository = new MonedaRepository(this.context);
			LimiteRepository limiteRepository = new LimiteRepository(this.context);

			var monedas = new List<Moneda> {
				new Moneda { Descripcion = "dolar", Url = "https://www.bancoprovincia.com.ar/Principal/Dolar", Factor = 1 },
				new Moneda { Descripcion = "real", Url = "https://www.bancoprovincia.com.ar/Principal/Dolar", Factor = Convert.ToDecimal(0.25) }
			};

			Task.FromResult(monedaRepository.AddRange(monedas));

			var limitesRange = new List<Limite> {
				new Limite { IdMoneda = 1, IdUsuario = 1, Anio = 2021, Mes = 8, Monto = Convert.ToDecimal(200.00) },
				new Limite { IdMoneda = 2, IdUsuario = 1, Anio = 2021, Mes = 8, Monto = Convert.ToDecimal(300.00) },
				new Limite { IdMoneda = 1, IdUsuario = 2, Anio = 2021, Mes = 8, Monto = Convert.ToDecimal(200.00) },
				new Limite { IdMoneda = 2, IdUsuario = 2, Anio = 2021, Mes = 8, Monto = Convert.ToDecimal(300.00) },
				new Limite { IdMoneda = 1, IdUsuario = 1, Anio = 2021, Mes = 9, Monto = Convert.ToDecimal(200.00) },
				new Limite { IdMoneda = 2, IdUsuario = 1, Anio = 2021, Mes = 9, Monto = Convert.ToDecimal(300.00) },
				new Limite { IdMoneda = 1, IdUsuario = 2, Anio = 2021, Mes = 9, Monto = Convert.ToDecimal(200.00) },
				new Limite { IdMoneda = 2, IdUsuario = 2, Anio = 2021, Mes = 9, Monto = Convert.ToDecimal(300.00) }
			};

			Task.FromResult(limiteRepository.AddRange(limitesRange));
		}
	}
}
