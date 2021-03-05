using SolarSystem.Domain.Models;

namespace SolarSystem.Domain.SolarSystemService {
	public interface ISolarSystemService {
		public Planet[] Planets { get; }
		public string GetWeatherForDay(int day);
		public MeteorologicalConditions GetConditionsForDay(int day);
	}
}
