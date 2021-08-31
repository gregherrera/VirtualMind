using Cotizacion.Api.Response;
using Cotization.Library.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cotizacion.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CotizacionController : ControllerBase
	{
		private readonly IMonedaRepository monedaRepository = null;

		public CotizacionController(IMonedaRepository _moneda_repository)
		{
			this.monedaRepository = _moneda_repository;
		}

		[HttpGet]
		public async Task<ActionResult<Moneda>> GetCotizaciones()
		{
			MyResponse response = new MyResponse();

			try
			{
				using (var httpClient = new HttpClient())
				{
					var coins = await monedaRepository.GetAll().ToListAsync();
					List<Cotizacion> _cotizacion = new List<Cotizacion>();

					foreach (var coin in coins)
					{
						using (var resp = await httpClient.GetAsync(coin.Url))
						{
							string apiResponse = await resp.Content.ReadAsStringAsync();
							var cotizacionList = JsonConvert.DeserializeObject<List<string>>(apiResponse);

							_cotizacion.Add(new Cotizacion() {
								Moneda = coin.Id,
								Descripcion = coin.Descripcion,
								Valor = Convert.ToString(Convert.ToDecimal(cotizacionList[1]) * coin.Factor)
							});							
						}
					}

					response.Data = _cotizacion;
					response.Success = 1;
				}
			}
			catch (Exception ex)
			{
				response.Message = ex.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Moneda>> GetCotizacion(long id)
		{
			MyResponse response = new MyResponse();

			try
			{
				if (id.Equals(DBNull.Value) || id <= 0)
				{
					response.Message = "You must provide a type of coin to proceed with the quotation.";
					return BadRequest(response);
				}
				else
				{
					var coin = await monedaRepository.GetMonedaByIdAsync(id);

					if (coin == null)
					{
						response.Message = "At the moment, there's no quotation for this type of coin.";
						return NotFound(response);
					}
					else
					{
						using (var httpClient = new HttpClient())
						{
							using (var resp = await httpClient.GetAsync(coin.Url))
							{
								string apiResponse = await resp.Content.ReadAsStringAsync();
								var cotizacionList = JsonConvert.DeserializeObject<List<string>>(apiResponse);

								Cotizacion _cotizacion = new Cotizacion();
								_cotizacion.Moneda = id;
								_cotizacion.Descripcion = coin.Descripcion;
								_cotizacion.Valor = Convert.ToString(Convert.ToDecimal(cotizacionList[1]) * coin.Factor);

								response.Data = _cotizacion;
								response.Success = 1;
							}
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

	public class Cotizacion
	{
		public long Moneda { get; set; }
		public string Descripcion { get; set; }
		public string Valor { get; set; }
	}
}
