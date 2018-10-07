using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TNT.Math.Tests
{
	[TestClass]
	public class AngleTests
	{
		[TestMethod]
		public void ConstructorTests()
		{
			//try
			//{
			//  Angle a1;
			//  Assert.Throws<ArgumentException>(delegate { a1 = new Angle(-1.1); });
			//  Assert.Throws<ArgumentException>(delegate { a1 = new Angle(1.1); });
			//  a1 = new Angle(-1.1);
			//}
			//catch (ArgumentException ae)
			//{
			//  Assert.AreEqual("The angle parameter must be between 1 and -1", ae.Message);
			//}

			Angle a1 = new Angle(System.Math.PI);
			Assert.AreEqual(System.Math.PI, a1.InRadians);
			Assert.AreEqual(180, a1.InDegrees);

			Angle a2 = new Angle(90, true);
			Assert.AreEqual(System.Math.PI / 2, a2.InRadians);
			Assert.AreEqual(90, a2.InDegrees);

			Angle a3 = new Angle(System.Math.PI / 4, false);
			Assert.AreEqual(System.Math.PI / 4, a3.InRadians);
			Assert.AreEqual(45, a3.InDegrees);

			Angle a4 = new Angle(a3);
			Assert.AreEqual(System.Math.PI / 4, a4.InRadians);
			Assert.AreEqual(45, a4.InDegrees);
		}

		[TestMethod]
		public void EmptyTest()
		{
			Assert.AreEqual(new Angle(0), Angle.Empty);
		}

		[TestMethod]
		public void ToStringTest()
		{
			Angle a1 = new Angle(247, true);
			Assert.AreEqual("247", a1.ToString());

			Angle a2 = new Angle(12.34, true);
			Assert.AreEqual("12.34", a2.ToString());
		}

		[TestMethod]
		public void OperatorAddTest()
		{
			Angle a1 = new Angle(33, true);
			Angle a2 = new Angle(42, true);

			Angle sum = a1 + a2;

			Assert.AreEqual(75, System.Math.Round(sum.InDegrees, 3));
		}

		[TestMethod]
		public void NotEqual_Not_Angle()
		{
			Angle a1 = new Angle(33, true);
			Assert.IsFalse(a1.Equals(1));
		}

		[TestMethod]
		public void GetHash()
		{
			var a1 = new Angle(33, true);
			var hash = a1.GetHashCode();
			Assert.AreEqual(-393782082, hash);
		}
	}
}
