using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace TNT.Math.Tests
{
	[TestClass]
	public class VectorTests
	{
		[TestMethod]
		public void ConstructorTests()
		{
			Vector v1 = new Vector(1.234, 5.678);
			Assert.AreEqual(v1.X, 1.234);
			Assert.AreEqual(v1.Y, 5.678);

			Vector v2 = new Vector(v1);
			Assert.AreEqual(v1, v2);

			Vector v3 = new Vector(new PointF(0, 0), new PointF(10, 10));
			Assert.AreEqual(new Vector(10, 10), v3);

			Assert.AreEqual(10, v3.X);
			Assert.AreEqual(10, v3.Y);

			Vector v4 = new Vector(new PointF(13, 17));

			Assert.AreEqual(new Vector(13, 17), v4);
		}

		[TestMethod]
		public void ConstructorTest_Angle()
		{
			Vector v = new Vector(new Angle(0, true), System.Math.Sqrt(100));
			Assert.AreEqual(0, System.Math.Round(v.X, 0));
			Assert.AreEqual(-10, System.Math.Round(v.Y, 0));

			v = new Vector(new Angle(45, true), System.Math.Sqrt(200));
			Assert.AreEqual(10, System.Math.Round(v.X, 0));
			Assert.AreEqual(-10, System.Math.Round(v.Y, 0));

			v = new Vector(new Angle(90, true), System.Math.Sqrt(100));
			Assert.AreEqual(10, System.Math.Round(v.X, 0));
			Assert.AreEqual(0, System.Math.Round(v.Y, 0));

			v = new Vector(new Angle(135, true), System.Math.Sqrt(200));
			Assert.AreEqual(10, System.Math.Round(v.X, 0));
			Assert.AreEqual(10, System.Math.Round(v.Y, 0));

			v = new Vector(new Angle(180, true), System.Math.Sqrt(100));
			Assert.AreEqual(0, System.Math.Round(v.X, 0));
			Assert.AreEqual(10, System.Math.Round(v.Y, 0));

			v = new Vector(new Angle(225, true), System.Math.Sqrt(200));
			Assert.AreEqual(-10, System.Math.Round(v.X, 0));
			Assert.AreEqual(10, System.Math.Round(v.Y, 0));

			v = new Vector(new Angle(270, true), System.Math.Sqrt(100));
			Assert.AreEqual(-10, System.Math.Round(v.X, 0));
			Assert.AreEqual(0, System.Math.Round(v.Y, 0));

			v = new Vector(new Angle(315, true), System.Math.Sqrt(200));
			Assert.AreEqual(-10, System.Math.Round(v.X, 0));
			Assert.AreEqual(-10, System.Math.Round(v.Y, 0));

			v = new Vector(new Angle(360, true), System.Math.Sqrt(100));
			Assert.AreEqual(0, System.Math.Round(v.X, 0));
			Assert.AreEqual(-10, System.Math.Round(v.Y, 0));

			v = new Vector(new Angle(-45, true), System.Math.Sqrt(200));
			Assert.AreEqual(-10, System.Math.Round(v.X, 0));
			Assert.AreEqual(-10, System.Math.Round(v.Y, 0));
		}

		[TestMethod]
		public void MagnitudeTest()
		{
			Vector v1 = new Vector(0, 17);
			Assert.AreEqual(17, v1.Magnitude);

			Vector v2 = new Vector(13, 0);
			Assert.AreEqual(13, v2.Magnitude);

			Vector v3 = v1 + v2;
			Assert.AreEqual(new Vector(13, 17), v3);

			Assert.AreEqual(21.40093, System.Math.Round(v3.Magnitude, 5));

			Vector v4 = new Vector(8, 12);
			Assert.AreEqual(new Vector(5, 5), v3 - v4);
			Assert.AreEqual(new Vector(-5, -5), v4 - v3);
		}

		[TestMethod]
		public void DotTest()
		{
			Vector v1 = new Vector(7, 13);
			Vector v2 = new Vector(17, 23);

			Assert.AreEqual(7 * 17 + 13 * 23, v1.Dot(v2));
		}

		[TestMethod]
		public void OperatorDotTest()
		{
			Vector v1 = new Vector(7, -13);
			Vector v2 = new Vector(17, 23);

			Assert.AreEqual(v1.Dot(v2), v1 * v2);
			Assert.AreEqual(v2.Dot(v1), v2 * v1);
		}

		[TestMethod]
		public void UnitVectorTest()
		{
			Vector v1 = new Vector(0, 0);
			Assert.AreEqual(new Vector(0, 0), v1.Unit);

			Vector v2 = new Vector(25, 0);
			Assert.AreEqual(new Vector(1, 0), v2.Unit);

			Vector v3 = new Vector(0, 25);
			Assert.AreEqual(new Vector(0, 1), v3.Unit);

			Vector v4 = new Vector(25, 25);
			Assert.AreEqual(System.Math.Round(.70710, 3), System.Math.Round(v4.Unit.X, 3));
			Assert.AreEqual(System.Math.Round(.70710, 3), System.Math.Round(v4.Unit.Y, 3));
		}

		[TestMethod]
		public void ToStringTest()
		{
			Vector v1 = new Vector(13.321, 17.754);
			Assert.AreEqual("[13.321 17.754]", v1.ToString());

			Vector v2 = new Vector(.321, .565);
			Assert.AreEqual("[0.321 0.565]", v2.ToString());
		}

		[TestMethod]
		public void OperatorMultiplyTest()
		{
			Vector v1 = new Vector(3, 7);

			Assert.AreEqual(new Vector(21, 49), v1 * 7);
			Assert.AreEqual(new Vector(27, 63), 9 * v1);
		}

		[TestMethod]
		public void OperatorAddTest()
		{
			PointF pf1 = new PointF(13, 17);
			Point p1 = new Point(13, 17);
			Vector v1 = new Vector(-7, -23);

			Assert.AreEqual(new PointF(6, -6), v1 + pf1);
			Assert.AreEqual(new PointF(6, -6), pf1 + v1);

			Point p1Ex = new Point(6, -6);
			Assert.AreEqual((PointF)p1Ex, v1 + p1);
			Assert.AreEqual((PointF)p1Ex, p1 + v1);
		}

		[TestMethod]
		public void CastingTests()
		{
			Vector v1 = new Vector(1.234, 5.678);
			PointF pf1 = v1;
			Point p1 = v1;

			Assert.AreEqual(new PointF((float)1.234, (float)5.678), pf1);
			Assert.AreEqual(new Point(1, 5), p1);
		}

		[TestMethod]
		public void AngleTests()
		{
			Vector v0 = new Vector(0, 0);
			Assert.AreEqual(new Angle(0), v0.Angle(v0));

			Vector v1 = new Vector(0, -10);
			Assert.AreEqual(Angle.Empty, v1.Angle(v0));
			Assert.AreEqual(Angle.Empty, v0.Angle(v1));

			for (double theta = 0; theta <= 180; theta += 22.5)
			{
				double thetaRad = theta / 180 * System.Math.PI;
				Vector v = new Vector(10 * System.Math.Sin(thetaRad), -10 * System.Math.Cos(thetaRad));

				Assert.AreEqual(theta, System.Math.Round(v.Angle(v1).InDegrees, 1));
			}

			for (double theta = 0; theta >= -180; theta -= 22.5)
			{
				double thetaRad = theta / 180 * System.Math.PI;
				Vector v = new Vector(10 * System.Math.Sin(thetaRad), -10 * System.Math.Cos(thetaRad));

				Assert.AreEqual(-theta, System.Math.Round(v.Angle(v1).InDegrees, 1));
			}
		}

		[TestMethod]
		public void AngleTests2()
		{
			Vector v1 = new Vector(new Angle(33, true), 1);
			Assert.AreEqual(33, System.Math.Round(v1.Angle().InDegrees));

			for (int angle = 205; angle < 220; angle++)
			{
				Vector v = new Vector(new Angle(angle, true), 1);
				Assert.AreEqual(angle, System.Math.Round(v.Angle().InDegrees));
				Angle a = v.Angle(v1, true);
				Assert.AreEqual(angle - 33, System.Math.Round(a.InDegrees));
			}
		}

		[TestMethod]
		public void RotateTest()
		{
			Vector origVector = new Vector(1, 0);
			Vector vector90 = origVector.Rotate(new Angle(90, true));
			Vector vector45 = origVector.Rotate(new Angle(45, true));

			Assert.AreEqual(0, System.Math.Round(vector90.X, 3));
			Assert.AreEqual(1, System.Math.Round(vector90.Y, 3));

			Assert.AreEqual(System.Math.Round(0.7069, 3), System.Math.Round(vector45.X, 3));
			Assert.AreEqual(System.Math.Round(0.7069, 3), System.Math.Round(vector45.Y, 3));
		}

		[TestMethod]
		public void DivideTest()
		{
			Vector vector = new Vector(27, 30);
			Vector dividend = new Vector(9, 10);

			Assert.AreEqual(dividend, vector / 3);
		}

		[TestMethod]
		public void SubtractTest()
		{
			PointF p = new PointF(21, 29);
			Vector v = new Vector(17, 11);
			Vector expected = new Vector(4, 18);

			Assert.AreEqual(expected.X, (p - v).X);
			Assert.AreEqual(expected.Y, (p - v).Y);
		}

		[TestMethod]
		public void NotEqual_Not_Vector()
		{
			var v = new Vector(17, 11);
			Assert.IsFalse(v.Equals(1));
		}

		[TestMethod]
		public void GetHash()
		{
			var v = new Vector(17,11);
			var hash = v.GetHashCode();
			Assert.AreEqual(-2141782003, hash);
		}
	}
}
