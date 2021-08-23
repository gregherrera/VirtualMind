using Cotizacion.Api.Controllers;
using Cotizacion.Api.Request;
using Cotizacion.Api.Response;
using Cotization.Library.Domain;
using Cotization.Library.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Cotizacion.Test.Domains
{
	public class CompraFact
	{
		private readonly DbContextOptions<CotizacionContext> options = new DbContextOptionsBuilder<CotizacionContext>()
			.UseInMemoryDatabase(databaseName: "Test")
			.Options;

		private CotizacionContext _context = null;
		private CompraController _controller = null;
		private MonedaRepository _monedaRepository = null;
		private LimiteRepository _limiteRepository = null;
		private CompraRepository _compraRepository = null;

		public CompraFact()
		{
			_context = new CotizacionContext(options);
			_monedaRepository = new MonedaRepository(_context);
			_limiteRepository = new LimiteRepository(_context);
			_compraRepository = new CompraRepository(_context);
			_controller = new CompraController(_compraRepository, _monedaRepository, _limiteRepository);
		}
		
		private async Task DbSeedMonedas()
		{
			var mon = await _monedaRepository.GetMonedaByIdAsync("dolar");

			if (mon == null)
			{
				await _monedaRepository.Add(new Moneda { Id = "dolar", Url = "https://www.bancoprovincia.com.ar/Principal/Dolar", Factor = 1 });
			}

			mon = await _monedaRepository.GetMonedaByIdAsync("real");

			if (mon == null)
			{
				await _monedaRepository.Add(new Moneda { Id = "real", Url = "https://www.bancoprovincia.com.ar/Principal/Dolar", Factor = Convert.ToDecimal(0.25) });
			}
		}

		private async Task DbSeedLimites()
		{
			var limites = new List<Limite> {
				new Limite { IdMoneda = "dolar", IdUsuario = 1, Anio = 2021, Mes = 8, Monto = Convert.ToDecimal(200.00) },
				new Limite { IdMoneda = "real", IdUsuario = 1, Anio = 2021, Mes = 8, Monto = Convert.ToDecimal(300.00) },
				new Limite { IdMoneda = "dolar", IdUsuario = 2, Anio = 2021, Mes = 8, Monto = Convert.ToDecimal(200.00) },
				new Limite { IdMoneda = "real", IdUsuario = 2, Anio = 2021, Mes = 8, Monto = Convert.ToDecimal(300.00) },
				new Limite { IdMoneda = "dolar", IdUsuario = 1, Anio = 2021, Mes = 9, Monto = Convert.ToDecimal(200.00) },
				new Limite { IdMoneda = "real", IdUsuario = 1, Anio = 2021, Mes = 9, Monto = Convert.ToDecimal(300.00) },
				new Limite { IdMoneda = "dolar", IdUsuario = 2, Anio = 2021, Mes = 9, Monto = Convert.ToDecimal(200.00) },
				new Limite { IdMoneda = "real", IdUsuario = 2, Anio = 2021, Mes = 9, Monto = Convert.ToDecimal(300.00) }
			};

			await _limiteRepository.AddRange(limites);
		}

		[Fact]
		public async Task GetCompras_Returns_NotFound_Result_Async()
		{
			// Act
			var notFoundResult = await _controller.GetCompras();

			// Assert
			Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
		}

		[Fact]
		public async Task GetCompras_Returns_Ok_Result_Async()
		{
			//Arrange
			await DbSeedMonedas();
			await DbSeedLimites();

			await _controller.Add(new MyRequest() { IdMoneda = "real", IdUsuario = 2, Monto = Convert.ToDecimal(1500.00) });

			// Act
			var okResult = await _controller.GetCompras();

			// Assert
			Assert.IsType<OkObjectResult>(okResult.Result);
		}

		[Fact]
		public async Task GetCompra_Returns_Ok_Result_ById_Async()
		{
			//Arrange
			await DbSeedMonedas();
			await DbSeedLimites();

			await _controller.Add(new MyRequest() { IdMoneda = "real", IdUsuario = 2, Monto = Convert.ToDecimal(1500.00) });

			// Act
			var okResult = await _controller.GetCompra(1);

			// Assert
			Assert.IsType<OkObjectResult>(okResult.Result);
		}

		[Fact]
		public async Task GetCompra_Returns_NotFound_Result_ById_Async()
		{
			// Act
			var notFoundResult = await _controller.GetCompra(100);

			// Assert
			Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
		}

		[Fact]
		public async Task Add_Returns_OK_Result_Async()
		{
			//Arrange
			await DbSeedMonedas();
			await DbSeedLimites();

			await _controller.Add(new MyRequest() { IdMoneda = "dolar", IdUsuario = 1, Monto = Convert.ToDecimal(1000.00) });
			await _controller.Add(new MyRequest() { IdMoneda = "dolar", IdUsuario = 1, Monto = Convert.ToDecimal(1500.00) });
			await _controller.Add(new MyRequest() { IdMoneda = "real", IdUsuario = 1, Monto = Convert.ToDecimal(2000.00) });
			await _controller.Add(new MyRequest() { IdMoneda = "real", IdUsuario = 1, Monto = Convert.ToDecimal(2500.000) });

			// Act
			var okResult = await _controller.GetCompras();

			var items = ((okResult.Result as OkObjectResult).Value as MyResponse).Data as List<Compra>;

			// Assert
			Assert.IsType<OkObjectResult>(okResult.Result);			

			Assert.IsType<List<Compra>>(items);
		}

		[Fact]
		public async Task Add_Returns_BadRequest_Result_Async()
		{
			//Arrange
			await DbSeedMonedas();
			await DbSeedLimites();

			// Act
			var badRequestResult = await _controller.Add(new MyRequest() { IdMoneda = "dolar", IdUsuario = 1, Monto = Convert.ToDecimal(25000.00) });

			// Assert
			Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
		}

		[Fact]
		public async Task Add_Returns_NotFoundRequest_Result_Async()
		{
			// Act
			var notFoundRequestResult = await _controller.Add(new MyRequest() { IdMoneda = "peso", IdUsuario = 1, Monto = Convert.ToDecimal(5000.00) });

			// Assert
			Assert.IsType<NotFoundObjectResult>(notFoundRequestResult.Result);
		}
	}
}
