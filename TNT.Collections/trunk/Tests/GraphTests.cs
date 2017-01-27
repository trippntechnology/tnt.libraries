using System;
using NUnit.Framework;
using TNT.Collections;

namespace Tests
{
	[TestFixture]
	public class GraphTests
	{
		[Test]
		public void ConstructorTests()
		{
			Graph<object, object> g = new Graph<object, object>();
			Assert.IsFalse(g.AllowCircularLinks);

			g = new Graph<object, object>(true);
			Assert.IsTrue(g.AllowCircularLinks);
		}

		[Test]
		public void CreateEdgeTestValidParameters()
		{
			object eo1 = new object();
			object eo2 = new object();
			object no1 = new object();
			object no2 = new object();

			Graph<object, object> g = new Graph<object, object>();

			try
			{
				Assert.Throws<ArgumentNullException>(delegate { g.CreateEdge(null, no1, no2); });
				g.CreateEdge(null, no1, no2);
			}
			catch (ArgumentNullException ane)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: edgeObj", ane.Message);
			}

			try
			{
				Assert.Throws<ArgumentNullException>(delegate { g.CreateEdge(eo1, null, no2); });
				g.CreateEdge(eo1, null, no2);
			}
			catch (ArgumentNullException ane)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: nodeObj1", ane.Message);
			}

			try
			{
				Assert.Throws<ArgumentNullException>(delegate { g.CreateEdge(eo1, no1, null); });
				g.CreateEdge(eo1, no1, null);
			}
			catch (ArgumentNullException ane)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: nodeObj2", ane.Message);
			}

			try
			{
				Assert.Throws<ArgumentException>(delegate { g.CreateEdge(eo1, no1, no1); });
				g.CreateEdge(eo1, no1, no1);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual("Arguments nodeObj1 and nodeObj2 can not be the same object", ae.Message);
			}
		}

		[Test]
		public void CreateEdgeTestExistingEdgeNode()
		{
			object eo1 = new object();
			object eo2 = new object();
			object no1 = new object();
			object no2 = new object();

			Graph<object, object> g = new Graph<object, object>(true);

			GraphEdge<object, object> edge = g.CreateEdge(eo1, no1, no2);

			Assert.AreEqual(eo1, edge.Object);

			try
			{
				Assert.Throws<ArgumentException>(delegate { g.CreateEdge(eo1, no1, no2); });
				g.CreateEdge(eo1, no1, no2);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual("edgeObj is already bound to an edge", ae.Message);
			}

			try
			{
				Assert.Throws<ArgumentException>(delegate { g.CreateEdge(eo2, no1, no2); });
				g.CreateEdge(eo2, no1, no2);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual("An edge already exists between nodeObj1 and nodeObj2", ae.Message);
			}
		}

		[Test]
		public void CreateEdgeTestCircularLink()
		{
			object eo1 = new object();
			object eo2 = new object();
			object eo3 = new object();
			object no1 = new object();
			object no2 = new object();
			object no3 = new object();

			Graph<object, object> g = new Graph<object, object>();

			g.CreateEdge(eo1, no1, no2);
			g.CreateEdge(eo2, no2, no3);

			try
			{
				Assert.Throws<ArgumentException>(delegate { g.CreateEdge(eo3, no3, no1); });
				g.CreateEdge(eo3, no3, no1);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual("Circular link not allowed", ae.Message);
			}
		}


		[Test]
		public void FindGraphNodeTest()
		{
			Graph<object, object> g = new Graph<object, object>();
			GraphNode<object, object> gn = g.FindGraphNode(new object());
			Assert.IsNull(gn);

			gn = g.FindGraphNode(null);
			Assert.IsNull(gn);
		}

		[Test]
		public void FindEdgeNodeTest()
		{
			Graph<object, object> g = new Graph<object, object>();
			GraphEdge<object, object> ge = g.FindGraphEdge(new object());
			Assert.IsNull(ge);

			ge = g.FindGraphEdge(null);
			Assert.IsNull(ge);
		}

		[Test]
		public void FindConnectedNodeTest()
		{
			object eo1 = new object();
			object eo2 = new object();
			object eo3 = new object();
			object eo4 = new object();
			object no1 = new object();
			object no2 = new object();
			object no3 = new object();
			object no4 = new object();

			Graph<object, object> g = new Graph<object, object>();

			GraphEdge<object, object> e1 = g.CreateEdge(eo1, no1, no2);
			GraphEdge<object, object> e2 = g.CreateEdge(eo2, no2, no3);
			GraphEdge<object, object> e3 = g.CreateEdge(eo3, no2, no4);

			try
			{
				Assert.Throws<ArgumentNullException>(delegate { g.FindConnectedNode(null, null, true); });
				g.FindConnectedNode(null, null, true);
			}
			catch (ArgumentNullException ane)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: startNode", ane.Message);
			}

			try
			{
				Assert.Throws<ArgumentNullException>(delegate { g.FindConnectedNode(e1.Node1, null, true); });
				g.FindConnectedNode(e1.Node1, null, true);
			}
			catch (ArgumentNullException ane)
			{
				Assert.AreEqual("Value cannot be null.\r\nParameter name: searchNode", ane.Message);
			}

			GraphNode<object, object> gn = g.FindConnectedNode(e1.Node1, e3.Node2, true);

			Assert.AreEqual(e3.Node2, gn);

		}
	}
}
