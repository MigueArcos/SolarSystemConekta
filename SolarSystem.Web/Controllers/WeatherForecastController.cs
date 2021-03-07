using Microsoft.AspNetCore.Mvc;
using SolarSystem.Domain.Services.SolarSystemService;
using System;
using System.Threading.Tasks;

namespace SolarSystem.Web.Controllers {
	[ApiController]
	public class WeatherForecastController : BasicController {
		private readonly ISolarSystemService solarSystemService;

		public WeatherForecastController(ISolarSystemService solarSystemService) {
			this.solarSystemService = solarSystemService;
		}
		[Route("")]
		[HttpGet("clima")]
		public async Task<IActionResult> GetWeatherForDay([FromQuery] int? dia) {
			try {
				int day = dia ?? 1;
				var weatherOfDay = await solarSystemService.GetWeatherForDay(day);
				return Ok(
					new {
						dia = day,
						clima = weatherOfDay
					}
				);
			}
			catch (Exception e) {
				return DefaultCatch(e);
			}
		}

		[HttpGet("condiciones")]
		public async Task<IActionResult> GetConditionsForDay([FromQuery] int? dia) {
			try {
				int day = dia ?? 1;
				var meteorologicalConditions = await solarSystemService.GetConditionsForDay(day);
				return Ok(meteorologicalConditions);
			}
			catch (Exception e) {
				return DefaultCatch(e);
			}
		}

		[HttpGet("resumen-condiciones")]
		public async Task<IActionResult> GetConditionsSummary() {
			try {
				var conditionsSummary = await solarSystemService.GetConditionsSummary();
				return Ok(conditionsSummary);
			}
			catch (Exception e) {
				return DefaultCatch(e);
			}
		}

		[HttpGet("sistema-solar")]
		public IActionResult SolarSystem() {
			return View();
		}
	}
}
