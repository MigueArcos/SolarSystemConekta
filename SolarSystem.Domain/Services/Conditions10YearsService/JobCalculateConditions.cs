using SolarSystem.Domain.DataAccessLayer.UnitOfWork;
using SolarSystem.Domain.Models;
using SolarSystem.Domain.Services.SolarSystemService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Domain.Services.Conditions10YearsService {
	public class JobCalculateConditions : IJobCalculateConditions {
		private readonly IUnitOfWork unitOfWork;
		private readonly SolarSystemWeatherCalculator solarSystemWeatherCalculator;
		public JobCalculateConditions(IUnitOfWork unitOfWork) {
			this.unitOfWork = unitOfWork;
			solarSystemWeatherCalculator = new SolarSystemWeatherCalculator();
		}

		public async Task<bool> UploadConditionsToDatabase(int howManyDays) {
			var repo = unitOfWork.Repository<MeteorologicalConditions>();
			await repo.ClearAll();
			var tasks = new List<Task<bool>>();
			for (var i = 0; i < howManyDays; i++){
				var conditionsForDay = solarSystemWeatherCalculator.GetConditionsForDay(i);
				tasks.Add(repo.Create(conditionsForDay.ToDictionary(i)));
			}
			await Task.WhenAll(tasks);
			return true;
		}
	}
}
