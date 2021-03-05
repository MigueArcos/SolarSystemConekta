using SolarSystem.Domain.Configuration;
using System;
using System.Linq;

namespace SolarSystem.Domain.Geometry.Models {
	public class Triangle {
		public Point PointA { get; private set; }
		public Point PointB { get; private set; }
		public Point PointC { get; private set; }

		public Triangle(Point pointA, Point pointB, Point pointC) {
			PointA = pointA;
			PointB = pointB;
			PointC = pointC;
		}

		public double GetArea() {
			return Math.Abs(
				(PointA.X - PointC.X) * (PointB.Y - PointA.Y) -
				(PointA.X - PointB.X) * (PointC.Y - PointA.Y)
			) / 2;
		}

		public bool ContainsPoint(Point point){
			var triangles = new Triangle[] {
				new Triangle(point, PointA, PointB),
				new Triangle(point, PointB, PointC),
				new Triangle(point, PointA, PointC),
			};
			var subtrianglesAreaSum = triangles.Sum(t => t.GetArea());
			var triangleArea = GetArea();
			return Math.Abs(triangleArea - subtrianglesAreaSum) <= Constants.MaxDecimalTolerance;
		}

		public double GetPerimeter() {
			return new Line(PointA, PointB).GetLineSize() + new Line(PointA, PointC).GetLineSize() + new Line(PointB, PointC).GetLineSize();
		}
	}
}
