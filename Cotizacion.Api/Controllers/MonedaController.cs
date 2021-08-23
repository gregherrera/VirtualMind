using Cotizacion.Api.Response;
using Cotization.Library.Domain;
using Cotization.Library.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cotizacion.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MonedaController : ControllerBase
	{
		private readonly IMonedaRepository monedaRepository;
		public MonedaController (IMonedaRepository _monedaRepository)
		{
			this.monedaRepository = _monedaRepository;
		}

		[HttpGet]
		public async Task<ActionResult<Moneda>> GetMonedas()
		{
			MyResponse response = new MyResponse();

			try
			{
				var monedas = monedaRepository.GetAll();				

				if (await monedas.CountAsync() == 0)
				{
					response.Message = "At the moment, there's no coins to show.";
					return BadRequest(response);
				}

				response.Success = 1;
				response.Data = monedas;
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
