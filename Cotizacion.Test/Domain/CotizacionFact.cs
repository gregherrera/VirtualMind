using Cotizacion.Api.Controllers;
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
	public class CotizacionFact
	{
		private readonly DbContextOptions<CotizacionContext> options = new DbContextOptionsBuilder<CotizacionContext>()
			.UseInMemoryDatabase(databaseName: "Test")
			.Options;

		private readonly CotizacionContext _context = null;
		private readonly CotizacionController _controller = null;
		private readonly MonedaRepository _monedaRepository = null;

		public CotizacionFact()
		{
			_context = new CotizacionContext(options);
			_monedaRepository = new MonedaRepository(_context);
			_controller = new CotizacionController(_monedaRepository);
		}

		private async Task DbSeed()
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

		[Fact]
		public async Task GetCotizaciones_Returns_OK_Result_Async()
		{
			// Arrange
			await DbSeed();

			// Act
			var okResult = await _controller.GetCotizaciones();

			// Assert
			Assert.IsType<OkObjectResult>(okResult.Result);
		}

		[Fact]
		public async Task GetCotizacion_Returns_OK_Result_ById_Async()
		{
			// Arrange
			await DbSeed();

			// Act
			var okResult = await _controller.GetCotizacion("dolar");

			// Assert
			Assert.IsType<OkObjectResult>(okResult.Result);
		}

		[Fact]
		public async Task GetCotizacion_Returns_BadRequest_Result_ById_Async()
		{
			// Arrange
			await DbSeed();

			// Act
			var badRequestResult = await _controller.GetCotizacion("");

			// Assert
			Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
		}

		[Fact]
		public async Task GetCotizacion_Returns_NotFound_ById_Async()
		{
			// Arrange
			await DbSeed();

			// Act
			var notFoundResult = await _controller.GetCotizacion("peso");

			// Assert
			Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
		}

		[Fact]
		public async Task GetCotizacion_Return_RightItem_ById_Async()
		{
			// Arrange
			await DbSeed();

			// Act
			var okResult = (await _controller.GetCotizacion("dolar")).Result as OkObjectResult;

			var cotizacion = (okResult.Value as MyResponse).Data as Api.Controllers.Cotizacion;

			// Assert
			Assert.IsType<MyResponse>(okResult.Value);

			Assert.Equal("dolar", cotizacion.Moneda);
		}
	}
}
