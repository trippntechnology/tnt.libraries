using System;

namespace TNT.Collections
{
	/// <summary>
	/// Represents a graph edge where object on the edge is type E and the node 
	/// objects are type N
	/// </summary>
	/// <typeparam name="E">Edge's object type</typeparam>
	/// <typeparam name="N">Node's object type</typeparam>
	public class GraphEdge<E, N>
	{
		#region Properties

		/// <summary>
		/// Object bound to the edge
		/// </summary>
		public E Object { get; protected set; }

		/// <summary>
		/// One of the two nodes connected by this edge
		/// </summary>
		public GraphNode<N, E> Node1 { get; internal set; }

		/// <summary>
		/// One of the two nodes connected by this edge
		/// </summary>
		public GraphNode<N, E> Node2 { get; internal set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <param name="obj">Object to bind to the edge</param>
		/// <param name="node1">One of two nodes that are connected by this edge</param>
		/// <param name="node2">One of two nodes that are connected by this edge</param>
		public GraphEdge(E obj, GraphNode<N, E> node1, GraphNode<N, E> node2)
		{
			if ((object)obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			else if ((object)node1 == null)
			{
				throw new ArgumentNullException("node1");
			}
			else if ((object)node2 == null)
			{
				throw new ArgumentNullException("node2");
			}
			else if (node1 == node2)
			{
				throw new ArgumentException("Arguments node1 and node2 can not be the same node");
			}

			Object = obj;
			Node1 = node1;
			Node2 = node2;
		}

		#endregion

		#region Equality Methods

		/// <summary>
		/// Operator == for objects of type GraphEdge&lt;E, N&gt; 
		/// </summary>
		/// <param name="ge1">First comparer</param>
		/// <param name="ge2">Second comparer</param>
		/// <returns>True if equal, false otherwise</returns>
		public static bool operator ==(GraphEdge<E, N> ge1, GraphEdge<E, N> ge2)
		{
			if (ReferenceEquals(ge1, ge2))
			{
				return true;
			}

			if ((object)ge1 == null || (object)ge2 == null)
			{
				return false;
			}

			object o1 = ge1.Object as object;
			object o2 = ge2.Object as object;

			return o1 == o2;
		}

		/// <summary>
		/// Operator != for objects of type GraphEdge&lt;E, N&gt;
		/// </summary>
		/// <param name="ge1">First comparer</param>
		/// <param name="ge2">Second comparer</param>
		/// <returns>True if not equal, false otherwise</returns>
		public static bool operator !=(GraphEdge<E, N> ge1, GraphEdge<E, N> ge2)
		{
			if (ReferenceEquals(ge1, ge2))
			{
				return false;
			}

			if ((object)ge1 == null || (object)ge2 == null)
			{
				return true;
			}

			object o1 = ge1.Object as object;
			object o2 = ge2.Object as object;

			return o1 != o2;
		}

		/// <summary>
		/// Operator == for objects of type GraphEdge&lt;E, N&gt; and type E
		/// </summary>
		/// <param name="ge">First comparer</param>
		/// <param name="obj">Second comparer</param>
		/// <returns>True if equal, false otherwise</returns>
		public static bool operator ==(GraphEdge<E, N> ge, E obj)
		{
			if ((object)ge == null || (object)obj == null)
			{
				return false;
			}

			object o1 = ge.Object as object;
			object o2 = obj as object;

			return o1 == o2;
		}

		/// <summary>
		/// Operator == for objects of type GraphEdge&lt;E, N&gt; and type E
		/// </summary>
		/// <param name="ge">First comparer</param>
		/// <param name="obj">Second comparer</param>
		/// <returns>True if equal, false otherwise</returns>
		public static bool operator ==(E obj, GraphEdge<E, N> ge)
		{
			return ge == obj;
		}

		/// <summary>
		/// Operator != for objects of type GraphEdge&lt;E, N&gt; and type E
		/// </summary>
		/// <param name="ge">First comparer</param>
		/// <param name="obj">Second comparer</param>
		/// <returns>True if not equal, false otherwise</returns>
		public static bool operator !=(GraphEdge<E, N> ge, E obj)
		{
			if ((object)ge == null || (object)obj == null)
			{
				return true;
			}

			object o1 = ge.Object as object;
			object o2 = obj as object;

			return o1 != o2;
		}

		/// <summary>
		/// Operator != for objects of type GraphEdge&lt;E, N&gt; and type E
		/// </summary>
		/// <param name="ge">First comparer</param>
		/// <param name="obj">Second comparer</param>
		/// <returns>True if not equal, false otherwise</returns>
		public static bool operator !=(E obj, GraphEdge<E, N> ge)
		{
			return ge != obj;
		}

		/// <summary>
		/// Determines whether the specified System.Object is equal to the current System.Object.
		/// </summary>
		/// <param name="obj">The System.Object to compare with the current System.Object.</param>
		/// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj == null)
			{
				return false;
			}

			GraphEdge<E, N> edge = obj as GraphEdge<E, N>;

			if (edge == null)
			{
				return false;
			}

			object o1 = edge.Object as object;
			object o2 = Object as object;

			return o1 == o2;
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash code for the current System.Object.</returns>
		public override int GetHashCode()
		{
			return Object.GetHashCode();
		}

		#endregion

		/// <summary>
		/// Returns the node on the other end of the edge
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <param name="node">Known node</param>
		/// <returns>Node on the other end of the edge</returns>
		public GraphNode<N, E> GetOppositeNode(GraphNode<N, E> node)
		{
			// First check this node to make sure it's valid
			if ((object)node == null)
			{
				throw new ArgumentNullException("node");
			}
			else if (node != Node1 && node != Node2)
			{
				throw new ArgumentException("The node parameter is not one of the two nodes associated with this edge");
			}

			// Find the opposite node
			if (node != Node1)
			{
				return Node1;
			}
			else
			{
				return Node2;
			}
		}

	}
}
