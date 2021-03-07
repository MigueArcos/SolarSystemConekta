using SolarSystem.Domain.Configuration;
using SolarSystem.Domain.Configuration.Extensions;
using SolarSystem.Domain.DataAccessLayer.Repository;
using SolarSystem.Domain.DataAccessLayer.UnitOfWork;
using SolarSystem.Domain.Models;
using SolarSystem.Domain.Services.Conditions10YearsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarSystem.Domain.Services.SolarSystemService {
	public class SolarSystemService : ISolarSystemService {
		private readonly IRepository<MeteorologicalConditions> repository;
		public SolarSystemService(IUnitOfWork unitOfWork) {
			this.repository = unitOfWork.Repository<MeteorologicalConditions>();
		}

		public async Task<Dictionary<string, object>> GetConditionsForDay(int day) {
			try {
				return await repository.GetById($"{day}");
			}
			catch {
				throw ErrorStatusCode.WeatherNotFoundForDay;
			}
		}

		public Task<Dictionary<string, object>> GetConditionsSummary() {
			// var allData = await repository.GetAll();
			var meteorologicalConditions = new List<(int Day, MeteorologicalConditions Conditions)>();
			var weatherCalculator = new SolarSystemWeatherCalculator();
			for (var i = 0; i < 3650; i++){
				meteorologicalConditions.Add((i + 1, weatherCalculator.GetConditionsForDay(i)));
			}
			//List<MeteorologicalConditions> meteorologicalConditions = allData.Select(i => new MeteorologicalConditions { 
			//	Weather = i["weather"].ToString(),
			//	TrianglePerimeter = double.Parse(i["trianglePerimeter"].ToString())
			//}).ToList();
			Dictionary<string, object> data = new Dictionary<string, object>();
			var groups = meteorologicalConditions.GroupBy(m => m.Conditions.Weather);
			var rainyPeriods = groups.FirstOrDefault(g => g.Key == WeatherType.Rainy.GetDescription());
			data["periodosSequia"] = groups.FirstOrDefault(g => g.Key == WeatherType.Drought.GetDescription())?.Select(m => m).Count() ?? 0;
			data["periodosOptimos"] = groups.FirstOrDefault(g => g.Key == WeatherType.Nice.GetDescription())?.Select(m => m).Count() ?? 0;
			data["periodosLluvia"] = new {
				numeroDias = rainyPeriods?.Select(m => m).Count() ?? 0,
				picoMaximo = rainyPeriods.Where(m => 
					Math.Abs(m.Conditions.TrianglePerimeter - rainyPeriods.Max(m => m.Conditions.TrianglePerimeter)) < Constants.MaxDecimalTolerance
				).Select(m => m.Day).ToList()
			};
			return Task.FromResult(data);
		}

		public async Task<string> GetWeatherForDay(int day) {
			return (await GetConditionsForDay(day))["weather"].ToString();
		}
	}
}
