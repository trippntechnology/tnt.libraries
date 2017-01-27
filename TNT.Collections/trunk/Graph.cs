using System;
using System.Collections.Generic;
using System.Linq;

namespace TNT.Collections
{
	/// <summary>
	/// Represents a graph
	/// </summary>
	/// <typeparam name="N">Node container type</typeparam>
	/// <typeparam name="E">Edge container type</typeparam>
	public class Graph<N, E>
	{
		#region Properties

		/// <summary>
		/// List of edges
		/// </summary>
		public List<GraphEdge<E, N>> Edges { get; protected set; }

		/// <summary>
		/// List of nodes
		/// </summary>
		public List<GraphNode<N, E>> Nodes { get; protected set; }

		/// <summary>
		/// Indicates whether the graph allows circular linkds
		/// </summary>
		public bool AllowCircularLinks { get; protected set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public Graph()
		{
			AllowCircularLinks = false;
			Edges = new List<GraphEdge<E, N>>();
			Nodes = new List<GraphNode<N, E>>();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="allowCircularLinks">Indicates whether the graph allow cycles. (Default: false)</param>
		public Graph(bool allowCircularLinks)
		{
			AllowCircularLinks = allowCircularLinks;
			Edges = new List<GraphEdge<E, N>>();
			Nodes = new List<GraphNode<N, E>>();
		}

		#endregion

		/// <summary>
		/// Creates an edge between two nodes with the bound objects
		/// </summary>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <param name="edgeObj"></param>
		/// <param name="nodeObj1"></param>
		/// <param name="nodeObj2"></param>
		/// <returns></returns>
		public GraphEdge<E, N> CreateEdge(E edgeObj, N nodeObj1, N nodeObj2)
		{
			if (edgeObj == null)
			{
				throw new ArgumentNullException("edgeObj");
			}
			else if (nodeObj1 == null)
			{
				throw new ArgumentNullException("nodeObj1");
			}
			else if (nodeObj2 == null)
			{
				throw new ArgumentNullException("nodeObj2");
			}
			else if (ReferenceEquals(nodeObj1, nodeObj2))
			{
				throw new ArgumentException("Arguments nodeObj1 and nodeObj2 can not be the same object");
			}

			GraphNode<N, E> node1 = FindGraphNode(nodeObj1);
			GraphNode<N, E> node2 = FindGraphNode(nodeObj2);
			GraphEdge<E, N> edge = FindGraphEdge(edgeObj);

			if (edge != null)
			{
				// The edgeObj already is bound to an edge
				throw new ArgumentException("edgeObj is already bound to an edge");
			}

			if (node1 != null && node2 != null && !AllowCircularLinks)
			{
				// Check if the both nodes are already connected to each other.
				GraphNode<N, E> foundNode = FindConnectedNode(node1, node2, true);

				if (foundNode != null)
				{
					throw new ArgumentException("Circular link not allowed");
				}
			}

			if (node1 == null)
			{
				node1 = new GraphNode<N, E>(nodeObj1);
			}

			if (node2 == null)
			{
				node2 = new GraphNode<N, E>(nodeObj2);
			}

			GraphEdge<E, N> commonedge = (from e1 in node1.Edges join e2 in node2.Edges on e1 equals e2 select e2).SingleOrDefault();

			if (commonedge != null)
			{
				throw new ArgumentException("An edge already exists between nodeObj1 and nodeObj2");
			}

			edge = new GraphEdge<E, N>(edgeObj, node1, node2);

			Edges.Add(edge);

			node1.Edges.Add(edge);
			Nodes.Add(node1);

			node2.Edges.Add(edge);
			Nodes.Add(node2);

			return edge;
		}

		/// <summary>
		/// Finds a graph node who has the nodeObj bound
		/// </summary>
		/// <param name="nodeObj">Object that is bound to the node</param>
		/// <returns>Graph node if found, null otherwise</returns>
		public GraphNode<N, E> FindGraphNode(N nodeObj)
		{
			return Nodes.Find(n => { return n == nodeObj; });
		}

		/// <summary>
		/// Finds a graph edge who has the edgeObj bound
		/// </summary>
		/// <param name="edgeObj">Object that is bound to the edge</param>
		/// <returns></returns>
		public GraphEdge<E, N> FindGraphEdge(E edgeObj)
		{
			return Edges.Find(e => { return e == edgeObj; });
		}

		//public List<GraphNode<N, E>> FindGraphNodes(N nodeObj)
		//{
		//  GraphNode<N, E> node = new GraphNode<N, E>(nodeObj);
		//  List<GraphNode<N, E>> graphNodes = (from n in Nodes where n == node select n).ToList();

		//  return graphNodes;
		//}

		/// <summary>
		/// Finds the searchNode if it is connected to startNode
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <param name="startNode">Node to start search from</param>
		/// <param name="searchNode">Node that is being searched for</param>
		/// <param name="reset">Specifies whether or not the node's Visited property should be set to false</param>
		/// <returns>Node if found, false otherwise</returns>
		public GraphNode<N, E> FindConnectedNode(GraphNode<N, E> startNode, GraphNode<N, E> searchNode, bool reset)
		{
			if (startNode == null)
			{
				throw new ArgumentNullException("startNode");
			}
			else if (searchNode == null)
			{
				throw new ArgumentNullException("searchNode");
			}

			if (reset)
			{
				Nodes.ForEach(n => n.Visited = false);
			}

			startNode.Visited = true;

			if (startNode == searchNode)
			{
				return startNode;
			}
			else
			{
				GraphNode<N, E> foundNode = null;

				foreach (GraphEdge<E, N> edge in startNode.Edges)
				{
					GraphNode<N, E> opNode = edge.GetOppositeNode(startNode);

					if (!opNode.Visited)
					{
						foundNode = FindConnectedNode(opNode, searchNode, false);

						if (foundNode != null)
						{
							return foundNode;
						}
					}
				}
			}

			return null;
		}
	}
}
