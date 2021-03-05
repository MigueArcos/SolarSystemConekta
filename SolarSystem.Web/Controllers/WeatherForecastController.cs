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
		public string GetWeatherForDay([FromQuery] int? day) {
			return solarSystemService.GetWeatherForDay(day ?? 0).ToString();
		}

		[HttpGet("condiciones")]
		public MeteorologicalConditions GetConditionsForDay([FromQuery] int? day) {
			return solarSystemService.GetConditionsForDay(day ?? 0);
		}
	}
}
