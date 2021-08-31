using Cotizacion.Api.Controllers;
using Cotizacion.Api.Response;
using Cotization.Library.Domain;
using Cotization.Library.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Cotizacion.Test.Cotizacion
{
	public class CotizacionFact: IClassFixture<CotizacionFixture>
	{
		CotizacionFixture cotizacionFixture = null;
		private readonly CotizacionController _controller = null;
		private readonly MonedaRepository _monedaRepository = null;

		public CotizacionFact(CotizacionFixture cotizacionFixture)
		{
			this.cotizacionFixture = cotizacionFixture;
			_monedaRepository = new MonedaRepository(cotizacionFixture.context);
			_controller = new CotizacionController(_monedaRepository);
		}

		[Fact]
		public async Task GetCotizaciones_Returns_OK_Result_Async()
		{
			// Act
			var okResult = await _controller.GetCotizaciones();

			// Assert
			Assert.IsType<OkObjectResult>(okResult.Result);
		}

		[Fact]
		public async Task GetCotizacion_Returns_OK_Result_ById_Async()
		{
			// Act
			var okResult = await _controller.GetCotizacion(1);

			// Assert
			Assert.IsType<OkObjectResult>(okResult.Result);
		}

		[Fact]
		public async Task GetCotizacion_Returns_BadRequest_Result_ById_Async()
		{
			// Act
			var badRequestResult = await _controller.GetCotizacion(-1);

			// Assert
			Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
		}

		[Fact]
		public async Task GetCotizacion_Returns_NotFound_ById_Async()
		{
			// Act
			var notFoundResult = await _controller.GetCotizacion(3);

			// Assert
			Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
		}

		[Fact]
		public async Task GetCotizacion_Return_RightItem_ById_Async()
		{
			// Act
			var okResult = (await _controller.GetCotizacion(1)).Result as OkObjectResult;

			var cotizacion = (okResult.Value as MyResponse).Data as Api.Controllers.Cotizacion;

			// Assert
			Assert.IsType<MyResponse>(okResult.Value);

			Assert.Equal(1, cotizacion.Moneda);
		}
	}
}
