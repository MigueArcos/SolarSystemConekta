using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolarSystem.Domain.SolarSystemService;

namespace SolarSystem.Web {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddCors(options => {
				options.AddDefaultPolicy(builder => {
						builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				});
			});
			services.AddScoped<ISolarSystemService, SolarSystemService>();
			services.AddControllers().AddJsonOptions(options => {
				options.JsonSerializerOptions.IgnoreNullValues = true;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseCors();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
