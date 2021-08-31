using Cotizacion.Api.Controllers;
using Cotizacion.Api.Request;
using Cotizacion.Api.Response;
using Cotization.Library.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Cotizacion.Test.Compra
{
	public class CompraFact: IClassFixture<CompraFixture>
	{
		CompraFixture compraFixture = null;

		private CompraController controller = null;
		private MonedaRepository monedaRepository = null;
		private LimiteRepository limiteRepository = null;
		private CompraRepository compraRepository = null;

		public CompraFact(CompraFixture compraFixture)
		{
			this.compraFixture = compraFixture;

			monedaRepository = new MonedaRepository(this.compraFixture.context);
			limiteRepository = new LimiteRepository(this.compraFixture.context);
			compraRepository = new CompraRepository(this.compraFixture.context);

			controller = new CompraController(compraRepository, monedaRepository, limiteRepository);
		}

		[Fact]
		public async Task GetCompra_Returns_NotFound_Result_ById_Async()
		{
			// Act
			var notFoundResult = await controller.GetCompra(100);

			// Assert
			Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
		}

		[Fact]
		public async Task GetCompras_Returns_NotFound_Result_Async()
		{
			//Arrange
			var compras = await compraRepository.GetAll().ToListAsync();
			await compraRepository.DeleteRange(compras);

			// Act
			var notFoundResult = await controller.GetCompras();

			// Assert
			Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
		}

		[Fact]
		public async Task GetCompras_Returns_Ok_Result_Async()
		{
			//Arrange
			await controller.Add(new MyRequest() { IdMoneda = 2, IdUsuario = 2, Monto = Convert.ToDecimal(1500.00) });

			// Act
			var okResult = await controller.GetCompras();

			// Assert
			Assert.IsType<OkObjectResult>(okResult.Result);
		}

		[Fact]
		public async Task GetCompra_Returns_Ok_Result_ById_Async()
		{
			//Arrange
			await controller.Add(new MyRequest() { IdMoneda = 2, IdUsuario = 2, Monto = Convert.ToDecimal(1500.00) });

			// Act
			var okResult = await controller.GetCompra(1);

			// Assert
			Assert.IsType<OkObjectResult>(okResult.Result);
		}

		[Fact]
		public async Task Add_Returns_OK_Result_Async()
		{
			//Arrange
			await controller.Add(new MyRequest() { IdMoneda = 1, IdUsuario = 1, Monto = Convert.ToDecimal(1000.00) });
			await controller.Add(new MyRequest() { IdMoneda = 1, IdUsuario = 1, Monto = Convert.ToDecimal(1500.00) });
			await controller.Add(new MyRequest() { IdMoneda = 1, IdUsuario = 1, Monto = Convert.ToDecimal(2000.00) });
			await controller.Add(new MyRequest() { IdMoneda = 1, IdUsuario = 1, Monto = Convert.ToDecimal(2500.000) });

			// Act
			var okResult = await controller.GetCompras();

			var items = ((okResult.Result as OkObjectResult).Value as MyResponse).Data as List<Cotization.Library.Domain.CompraDto>;

			// Assert
			Assert.IsType<OkObjectResult>(okResult.Result);			

			Assert.IsType<List<Cotization.Library.Domain.CompraDto>>(items);
		}

		[Fact]
		public async Task Add_Returns_BadRequest_Result_Async()
		{
			// Act
			var badRequestResult = await controller.Add(new MyRequest() { IdMoneda = 1, IdUsuario = 1, Monto = Convert.ToDecimal(25000.00) });

			// Assert
			Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
		}

		[Fact]
		public async Task Add_Returns_NotFoundRequest_Result_Async()
		{
			// Act
			var notFoundRequestResult = await controller.Add(new MyRequest() { IdMoneda = 3, IdUsuario = 1, Monto = Convert.ToDecimal(5000.00) });

			// Assert
			Assert.IsType<NotFoundObjectResult>(notFoundRequestResult.Result);
		}
	}
}
