using Cotizacion.Api.Request;
using Cotizacion.Api.Response;
using Cotization.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cotizacion.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompraController : ControllerBase
	{
		private readonly ICompraRepository compraRepository = null;
		private readonly IMonedaRepository monedaRepository = null;
		private readonly ILimiteRepository limiteRepository = null;

		public CompraController(ICompraRepository _compraRepository, IMonedaRepository _monedaRepository, ILimiteRepository _limiteRepository)
		{
			this.compraRepository = _compraRepository;
			this.monedaRepository = _monedaRepository;
			this.limiteRepository = _limiteRepository;
		}

		[HttpGet]
		public async Task<ActionResult<CompraDto>> GetCompras()
		{
			MyResponse response = new MyResponse();

			try
			{	
				var compras = await compraRepository.GetAll().OrderByDescending(c => c.Id)
					.Select(c => new CompraDto { Id = c.Id,
												 IdUsuario = c.IdUsuario, 
												 IdMoneda = c.IdMoneda, 
												 Fecha = c.Fecha, 
												 Tasa = c.Tasa, 
												 Monto = c.Monto, 
												 Valor = c.Valor, 
												 MonedaDescripcion = c.Moneda.Descripcion })
					.ToListAsync();

				if (compras.Count == 0)
				{
					response.Message = "At the moment, there's no purchase to show.";
					return NotFound(response);
				}

				response.Data = compras;
				response.Success = 1;
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Compra>> GetCompra(long id)
		{
			MyResponse response = new MyResponse();

			try
			{
				if (id.Equals(DBNull.Value) || id <= 0)
				{
					response.Message = "You must provide a purchase id to proceed with the query.";
					return BadRequest(response);
				}
				else
				{
					var compras = await compraRepository.GetCompraByIdAsync(id);

					if (compras == null)
					{
						response.Message = "The id you provided does not exists, please check it or try with another one.";
						return NotFound(response);
					}

					response.Success = 1;
					response.Data = compras;
				}
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<Compra>> Add(MyRequest request)
		{
			MyResponse response = new MyResponse();

			try
			{
				if (request.IdMoneda.Equals(DBNull.Value) || request.IdMoneda <= 0)
				{
					response.Message = "You should provide a type of coin for this transaction.";
					return BadRequest(response);
				}
				else if (request.IdUsuario.Equals(DBNull.Value) || request.IdUsuario <= 0)
				{
					response.Message = "You should provide the use who is buying the coins.";
					return BadRequest(response);
				}
				else if (request.Monto.Equals(DBNull.Value) || request.Monto <= 0 )
				{
					response.Message = "You should provide the amount to proceed with this transaction.";
					return BadRequest(response);
				}
				else
				{
					var coin = await monedaRepository.GetMonedaByIdAsync(request.IdMoneda);

					if (coin == null)
					{
						response.Message = "The coin that you have specified does not exists.";
						return NotFound(response);
					}
					else
					{
						var limit = await limiteRepository.GetAll().FirstOrDefaultAsync(
										l => l.IdMoneda.Equals(request.IdMoneda) &&
										l.IdUsuario == request.IdUsuario &&
										l.Anio == DateTime.Now.Year &&
										l.Mes == DateTime.Now.Month
									 );

						if (limit == null)
						{
							response.Message = "The monthly exchange limit for this user have not been configured.";
							return NotFound(response);
						}

						var totalMonth = await compraRepository.GetAll().Where(
											c => c.IdMoneda.Equals(request.IdMoneda) &&
											c.IdUsuario == request.IdUsuario &&
											c.Fecha.Year == DateTime.Now.Year &&
											c.Fecha.Month == DateTime.Now.Month
										).SumAsync(m => m.Valor);

						Cotizacion _cotizacion = new Cotizacion();

						using (var httpClient = new HttpClient())
						{
							using (var resp = await httpClient.GetAsync(coin.Url))
							{
								string apiResponse = await resp.Content.ReadAsStringAsync();
								var cotizacionList = JsonConvert.DeserializeObject<List<string>>(apiResponse);

								
								_cotizacion.Valor = Convert.ToString(Convert.ToDecimal(cotizacionList[1]) * coin.Factor);
							}
						}

						if (totalMonth >= limit.Monto || totalMonth + (request.Monto / Convert.ToDecimal(_cotizacion.Valor)) >= limit.Monto)
						{
							response.Message = "The amount exceed the monthly limit this user has for this coin.";
							return BadRequest(response);
						}
						else
						{
							Compra compra = new Compra() {
								IdMoneda = request.IdMoneda,
								IdUsuario = request.IdUsuario,
								Fecha = DateTime.Now,
								Tasa = Convert.ToDecimal(_cotizacion.Valor),
								Monto = request.Monto,
								Valor = request.Monto / Convert.ToDecimal(_cotizacion.Valor)
							};

							await compraRepository.Add(compra);

							response.Success = 1;
						}
					}
				}
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
