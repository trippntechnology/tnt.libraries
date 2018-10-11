using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TNT.Collections;

namespace Tests
{
	[TestClass]
	public class GraphNodeTests
	{
		[TestMethod]
		public void OperatorEqualTest()
		{
			object o1 = new object();
			object o2 = new object();

			GraphNode<object, object> gn1 = new GraphNode<object, object>(o1);
			GraphNode<object, object> gn2 = new GraphNode<object, object>(o1);
			GraphNode<object, object> gn3 = new GraphNode<object, object>(o2);

			Assert.IsFalse(gn1 == null);
			Assert.IsTrue(gn1 == gn1);
			Assert.IsTrue(gn1 == gn2);
			Assert.IsFalse(gn1 == gn3);

			Assert.IsTrue(gn1 == o1);
			Assert.IsFalse(gn1 == o2);

			Assert.IsTrue(o1 == gn1);
			Assert.IsFalse(o2 == gn2);


			Assert.IsTrue(gn1 != null);
			Assert.IsFalse(gn1 != gn1);
			Assert.IsFalse(gn1 != gn2);
			Assert.IsTrue(gn1 != gn3);

			Assert.IsFalse(gn1 != o1);
			Assert.IsTrue(gn1 != o2);

			Assert.IsFalse(o1 != gn1);
			Assert.IsTrue(o2 != gn2);
		}

		[TestMethod]
		public void EqualsTest()
		{
			object o1 = new object();
			object o2 = new object();

			GraphNode<object, object> gn1 = new GraphNode<object, object>(o1);
			GraphNode<object, object> gn2 = new GraphNode<object, object>(o1);
			GraphNode<object, object> gn3 = new GraphNode<object, object>(o2);

			Assert.IsTrue(gn1.Equals(gn1));
			Assert.IsTrue(gn1.Equals(gn2));
			Assert.IsFalse(gn1.Equals(o1));
			Assert.IsFalse(gn1.Equals(null));
			Assert.IsFalse(gn1.Equals(gn3));
		}

		[TestMethod]
		public void GetHashCodeTest()
		{
			object o1 = new object();
			GraphNode<object, object> gn1 = new GraphNode<object, object>(o1);

			Assert.AreEqual(o1.GetHashCode(), gn1.GetHashCode());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConstructorTests()
		{
			try
			{
				new GraphNode<object, object>(null);
			}
			catch (ArgumentNullException ane)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: obj", ane.Message);
				throw;
			}
		}
	}
}
