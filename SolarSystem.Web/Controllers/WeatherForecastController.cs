using Microsoft.AspNetCore.Mvc;
using SolarSystem.Domain.Models;
using SolarSystem.Domain.SolarSystemService;

namespace SolarSystem.Web.Controllers {
	[ApiController]
	public class WeatherForecastController : ControllerBase {
		private readonly ISolarSystemService solarSystemService;

		public WeatherForecastController(ISolarSystemService solarSystemService) {
			this.solarSystemService = solarSystemService;
		}

		[HttpGet("clima")]
		public IActionResult GetWeatherForDay([FromQuery] int? day) {
			return Ok(
				new {
					dia = day ?? 0,
					clima = solarSystemService.GetWeatherForDay(day ?? 0)
				}
			);
		}

		[HttpGet("condiciones")]
		public MeteorologicalConditions GetConditionsForDay([FromQuery] int? day) {
			return solarSystemService.GetConditionsForDay(day ?? 0);
		}
	}
}
