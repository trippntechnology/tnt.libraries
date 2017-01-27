using System;
using NUnit.Framework;
using TNT.Collections;

namespace Tests
{
	[TestFixture]
	public class GraphEdgeTests
	{
		[Test]
		public void OperatorEqualTest()
		{
			object eo1 = new object();
			object eo2 = new object();

			object no1 = new object();
			object no2 = new object();
			object no3 = new object();

			GraphNode<object, object> gn1 = new GraphNode<object, object>(no1);
			GraphNode<object, object> gn2 = new GraphNode<object, object>(no2);
			GraphNode<object, object> gn3 = new GraphNode<object, object>(no3);


			GraphEdge<object, object> ge1 = new GraphEdge<object, object>(eo1, gn1, gn2);
			GraphEdge<object, object> ge2 = new GraphEdge<object, object>(eo1, gn1, gn2);
			GraphEdge<object, object> ge3 = new GraphEdge<object, object>(eo2, gn2, gn3);

			Assert.IsFalse(ge1 == null);
			Assert.IsTrue(ge1 == ge1);
			Assert.IsTrue(ge1 == ge2);
			Assert.IsFalse(ge1 == ge3);

			Assert.IsTrue(ge1 == eo1);
			Assert.IsFalse(ge1 == eo2);

			Assert.IsTrue(eo1 == ge1);
			Assert.IsFalse(eo2 == ge2);

			Assert.IsTrue(ge1 != null);
			Assert.IsFalse(ge1 != ge1);
			Assert.IsFalse(ge1 != ge2);
			Assert.IsTrue(ge1 != ge3);

			Assert.IsFalse(ge1 != eo1);
			Assert.IsTrue(ge1 != eo2);

			Assert.IsFalse(eo1 != ge1);
			Assert.IsTrue(eo2 != ge2);
		}

		[Test]
		public void EqualsTest()
		{
			object eo1 = new object();
			object eo2 = new object();

			object no1 = new object();
			object no2 = new object();
			object no3 = new object();

			GraphNode<object, object> gn1 = new GraphNode<object, object>(no1);
			GraphNode<object, object> gn2 = new GraphNode<object, object>(no2);
			GraphNode<object, object> gn3 = new GraphNode<object, object>(no3);


			GraphEdge<object, object> ge1 = new GraphEdge<object, object>(eo1, gn1, gn2);
			GraphEdge<object, object> ge2 = new GraphEdge<object, object>(eo1, gn1, gn2);
			GraphEdge<object, object> ge3 = new GraphEdge<object, object>(eo2, gn2, gn3);

			Assert.IsTrue(ge1.Equals(ge1));
			Assert.IsTrue(ge1.Equals(ge2));
			Assert.IsFalse(ge1.Equals(eo1));
			Assert.IsFalse(ge1.Equals(null));
			Assert.IsFalse(ge1.Equals(ge3));
		}

		[Test]
		public void GetHashCodeTest()
		{
			object eo1 = new object();
			object no1 = new object();
			object no2 = new object();

			GraphNode<object, object> gn1 = new GraphNode<object, object>(no1);
			GraphNode<object, object> gn2 = new GraphNode<object, object>(no2);

			GraphEdge<object, object> ge1 = new GraphEdge<object, object>(eo1, gn1, gn2);

			Assert.AreEqual(eo1.GetHashCode(), ge1.GetHashCode());
		}

		[Test]
		public void ConstructorTests()
		{
			object eo1 = new object();
			object no1 = new object();
			object no2 = new object();

			GraphNode<object, object> gn1 = new GraphNode<object, object>(no1);
			GraphNode<object, object> gn2 = new GraphNode<object, object>(no2);

			try
			{
				Assert.Throws<ArgumentNullException>(delegate { new GraphEdge<object, object>(null, gn1, gn2); });
				new GraphEdge<object, object>(null, gn1, gn2);
			}
			catch (ArgumentNullException ane)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: obj", ane.Message);
			}

			try
			{
				Assert.Throws<ArgumentNullException>(delegate { new GraphEdge<object, object>(eo1, null, gn2); });
				new GraphEdge<object, object>(eo1, null, gn2);
			}
			catch (ArgumentNullException ane)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: node1", ane.Message);
			}

			try
			{
				Assert.Throws<ArgumentNullException>(delegate { new GraphEdge<object, object>(eo1, gn1, null); });
				new GraphEdge<object, object>(eo1, gn1, null);
			}
			catch (ArgumentNullException ane)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: node2", ane.Message);
			}

			try
			{
				Assert.Throws<ArgumentException>(delegate { new GraphEdge<object, object>(eo1, gn1, gn1); });
				new GraphEdge<object, object>(eo1, gn1, gn1);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual("Arguments node1 and node2 can not be the same node", ae.Message);
			}
		}

		[Test]
		public void GetOppositeNodeTest()
		{
				object eo1 = new object();
			object no1 = new object();
			object no2 = new object();
			object no3 = new object();

			GraphNode<object, object> gn1 = new GraphNode<object, object>(no1);
			GraphNode<object, object> gn2 = new GraphNode<object, object>(no2);
			GraphNode<object, object> gn3 = new GraphNode<object, object>(no3);

			GraphEdge<object, object> ge1 = new GraphEdge<object,object>(eo1, gn1, gn2);

			try
			{
				Assert.Throws<ArgumentNullException>(delegate { ge1.GetOppositeNode(null); });
				ge1.GetOppositeNode(null); 
			}
			catch (ArgumentNullException ane)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: node", ane.Message);
			}

			try
			{
				Assert.Throws<ArgumentException>(delegate { ge1.GetOppositeNode(gn3); });
				ge1.GetOppositeNode(gn3);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual("The node parameter is not one of the two nodes associated with this edge", ae.Message);
			}

			Assert.AreEqual(gn1, ge1.GetOppositeNode(gn2));
			Assert.AreEqual(gn2, ge1.GetOppositeNode(gn1));
		}
	}
}
