using System;

namespace SolarSystem.Domain.Geometry.Models {
	public class Line {
		public Point Point1 { get; set; }
		public Point Point2 { get; set; }
		public double DistanceX { get; set; }
		public double DistanceY { get; set; }
		public Line(Point point1, Point point2) {
			Point1 = point1;
			Point2 = point2;
		}
		public double GetLineSize() {
			return Math.Sqrt(Math.Pow(Point2.Y - Point1.Y, 2) + Math.Pow(Point2.X - Point1.X, 2));
		}
	}
}
