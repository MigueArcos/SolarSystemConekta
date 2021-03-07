using SolarSystem.Domain.Geometry.Models;
using System.Collections.Generic;
using System.Linq;

namespace SolarSystem.Domain.Models {
	public class MeteorologicalConditions {
		public string Weather { get; set; }
		public Planet[] Planets { get; set; }
		public Dictionary<string, Point> PlanetPositions { get; set; }
		public double TrianglePerimeter { get; set; }

		public Dictionary<string, object> ToDictionary(int day){
			return new Dictionary<string, object> {
				["id"] = $"{day + 1}",
				["day"] = day,
				["weather"] = Weather,
				["trianglePerimeter"] = TrianglePerimeter,
				["planetPositions"] = PlanetPositions.ToDictionary(p => p.Key, p => new { x = p.Value.X, y = p.Value.Y } ),
				["planets"] = Planets.Select(p => new {
					name = p.Name,
					speedDegPerDay = p.SpeedDegPerDay,
					distanceToSunInKm = p.DistanceToSunInKm,
					isClockWiseRotation = p.IsClockWiseRotation
				}).ToList()
			};
		}
	}
}
