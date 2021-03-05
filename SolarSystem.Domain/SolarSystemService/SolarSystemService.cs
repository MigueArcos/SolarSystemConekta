using SolarSystem.Domain.Configuration;
using SolarSystem.Domain.Geometry.Models;
using SolarSystem.Domain.Models;
using System;
using System.Linq;

namespace SolarSystem.Domain.SolarSystemService {
	public class SolarSystemService : ISolarSystemService {
		public Planet[] Planets => new Planet[] { 
			new Planet {
				Name = "Ferrengi",
				DistanceToSunInKm = 500,
				SpeedDegPerDay = 1,
				IsClockWiseRotation = true
			},
			new Planet {
				Name = "Betasoide",
				DistanceToSunInKm = 2000,
				SpeedDegPerDay = 3,
				IsClockWiseRotation = true
			},
			new Planet {
				Name = "Vulcano",
				DistanceToSunInKm = 1000,
				SpeedDegPerDay = 5,
				IsClockWiseRotation = false
			}
		};

		public Point GetPlanetPosition(Planet planet, int day) {
			int clockwiseFactor = planet.IsClockWiseRotation ? 1 : -1;
			return new Point {
				X = Math.Cos(day * planet.SpeedDegPerDay * Math.PI / 180 * clockwiseFactor) * planet.DistanceToSunInKm,
				Y = Math.Sin(day * planet.SpeedDegPerDay * Math.PI / 180 * clockwiseFactor) * planet.DistanceToSunInKm,
			};
		}
		public bool LineTouchOrigin(Point pointA, Point pointB) {
			return Math.Abs(pointA.X * (pointB.Y - pointA.Y) - pointA.Y * (pointB.X - pointA.X)) <= Constants.MaxDecimalTolerance;
		}
		public MeteorologicalConditions GetConditionsForDay(int day) {
			var meteorologicalConditions = new MeteorologicalConditions {
				Planets = Planets
			};
			var planetPositions = Planets.ToDictionary(p => p.Name, p => GetPlanetPosition(p, day));
			meteorologicalConditions.PlanetPositions = planetPositions;
			Triangle triangle = new Triangle(
				planetPositions[Planets[0].Name],
				planetPositions[Planets[1].Name],
				planetPositions[Planets[2].Name]
			);
			var triangleArea = triangle.GetArea();
			// Is a Line
			if (triangleArea <= Constants.MaxDecimalTolerance) {
				WeatherType weather = LineTouchOrigin(triangle.PointA, triangle.PointB) ? WeatherType.Drought : WeatherType.Nice;
				meteorologicalConditions.Weather = weather.ToString();
			}
			// Is a triangle
			else{
				WeatherType weather = triangle.ContainsPoint(new Point { X = 0, Y = 0}) ? WeatherType.Rainy : WeatherType.Normal;
				if (weather == WeatherType.Rainy){
					meteorologicalConditions.TrianglePerimeter = triangle.GetPerimeter();
				}
				meteorologicalConditions.Weather = weather.ToString();
			}
			return meteorologicalConditions;
		}

		public string GetWeatherForDay(int day) {
			return GetConditionsForDay(day).Weather;
		}
	}
}
