using Cotization.Library.Domain;
using Cotization.Library.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cotizacion.Api
{
	public class Startup
	{
		private readonly string MyCors = "MyCors";
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddCors(options =>
			{
				options.AddPolicy(name: MyCors, policy => { policy.WithOrigins("*").AllowAnyHeader(); });
			});

			services.AddDbContext<CotizacionContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Exchange")));
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IMonedaRepository, MonedaRepository>();
			services.AddScoped<ILimiteRepository, LimiteRepository>();
			services.AddScoped<ICompraRepository, CompraRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors(MyCors);

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
