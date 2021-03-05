using SolarSystem.Domain.Geometry.Models;
using System.Collections.Generic;

namespace SolarSystem.Domain.Models {
	public class MeteorologicalConditions {
		public string Weather { get; set; }
		public Planet[] Planets { get; set; }
		public Dictionary<string, Point> PlanetPositions { get; set; }
		public double TrianglePerimeter { get; set; }
	}
}
