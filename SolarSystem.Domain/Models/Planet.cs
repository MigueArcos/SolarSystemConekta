namespace SolarSystem.Domain.Models {
	public class Planet {
		public string Name { get; set; }
		public double DistanceToSunInKm { get; set; }
		public double SpeedDegPerDay { get; set; }
		public bool IsClockWiseRotation { get; set; } 
	}
}
