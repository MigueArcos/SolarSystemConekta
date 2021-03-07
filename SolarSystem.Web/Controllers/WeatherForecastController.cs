using Microsoft.AspNetCore.Mvc;
using SolarSystem.Domain.Models;
using SolarSystem.Domain.Services.SolarSystemService;

namespace SolarSystem.Web.Controllers {
	[ApiController]
	public class WeatherForecastController : Controller {
		private readonly ISolarSystemService solarSystemService;

		public WeatherForecastController(ISolarSystemService solarSystemService) {
			this.solarSystemService = solarSystemService;
		}
		[Route("")]
		[HttpGet("clima")]
		public IActionResult GetWeatherForDay([FromQuery] int? dia) {
			int day = dia ?? 1;
			return Ok(
				new {
					dia = day,
					clima = solarSystemService.GetWeatherForDay(day - 1)
				}
			);
		}

		[HttpGet("condiciones")]
		public MeteorologicalConditions GetConditionsForDay([FromQuery] int? dia) {
			int day = dia ?? 1;
			return solarSystemService.GetConditionsForDay(day - 1);
		}

		[HttpGet("sistema-solar")]
		public IActionResult SolarSystem() {
			return View();
		}
	}
}
