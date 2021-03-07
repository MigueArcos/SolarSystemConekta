using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Domain.Services.SolarSystemService {
	public interface ISolarSystemService {
		Task<string> GetWeatherForDay(int day);
		Task<Dictionary<string, object>> GetConditionsForDay(int day);
		Task<Dictionary<string, object>> GetConditionsSummary();
	}
}
