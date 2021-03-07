using SolarSystem.Domain.Models;

namespace SolarSystem.Domain.SolarSystemService {
	public interface ISolarSystemService {
		Planet[] Planets { get; }
		string GetWeatherForDay(int day);
		MeteorologicalConditions GetConditionsForDay(int day);
	}
}
