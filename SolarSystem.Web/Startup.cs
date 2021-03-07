using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolarSystem.Domain.Services.SolarSystemService;
using Google.Cloud.Firestore;
using SolarSystem.Domain.DataAccessLayer.UnitOfWork;

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
			services.AddScoped<FirestoreDb>(s => FirestoreDb.Create(Configuration.GetValue<string>("Firestore:ProjectId")));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<ISolarSystemService, SolarSystemService>();
			services.AddControllersWithViews().AddJsonOptions(options => {
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

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
