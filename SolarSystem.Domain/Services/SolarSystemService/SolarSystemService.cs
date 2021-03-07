using SolarSystem.Domain.DataAccessLayer.Repository;
using SolarSystem.Domain.DataAccessLayer.UnitOfWork;
using SolarSystem.Domain.Models;
using System.Collections.Generic;
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

		public async Task<string> GetWeatherForDay(int day) {
			return (await GetConditionsForDay(day))["weather"].ToString();
		}
	}
}
