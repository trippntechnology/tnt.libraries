using System;
using System.Collections.Generic;

namespace TNT.Collections
{
	/// <summary>
	/// Represents a graph node where the object on the node is type N and the edges have
	/// objects of type E
	/// </summary>
	/// <typeparam name="N">Node's object type</typeparam>
	/// <typeparam name="E">Edge's object type</typeparam>
	public class GraphNode<N, E>
	{
		/// <summary>
		/// Object bound to the node
		/// </summary>
		public N Object { get; protected set; }

		/// <summary>
		/// Edges connected to this node
		/// </summary>
		public List<GraphEdge<E, N>> Edges { get; internal set; }

		internal bool Visited { get; set; }

		#region Constructors

		/// <summary>
		/// Constructor. Initializes the node's object with obj
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when obj is null</exception>
		/// <param name="obj">Object to set on the node</param>
		public GraphNode(N obj)
		{
			if ((object)obj == null)
			{
				throw new ArgumentNullException("obj");
			}

			Object = obj;
			Edges = new List<GraphEdge<E, N>>();
		}

		#endregion

		#region Equality Methods

		/// <summary>
		/// Operator == for objects of type GraphNode&lt;N, E&gt;
		/// </summary>
		/// <param name="gn1">First comparer</param>
		/// <param name="gn2">Second comparer</param>
		/// <returns>True if equal, false otherwise</returns>
		public static bool operator ==(GraphNode<N, E> gn1, GraphNode<N, E> gn2)
		{
			if (ReferenceEquals(gn1, gn2))
			{
				return true;
			}

			if ((object)gn1 == null || (object)gn2 == null)
			{
				return false;
			}

			object o1 = gn1.Object as object;
			object o2 = gn2.Object as object;

			return o1 == o2;
		}

		/// <summary>
		/// Operator != for objects of type GraphNode&lt;N, E&gt;
		/// </summary>
		/// <param name="gn1">First comparer</param>
		/// <param name="gn2">Second comparer</param>
		/// <returns>True if not equal, false otherwise</returns>
		public static bool operator !=(GraphNode<N, E> gn1, GraphNode<N, E> gn2)
		{
			return !(gn1 == gn2);
		}

		/// <summary>
		/// Operator == for objects of type GraphNode&lt;N, E&gt; and type N
		/// </summary>
		/// <param name="gn">First comparer</param>
		/// <param name="obj">Second comparer</param>
		/// <returns>True if equal, false otherwise</returns>
		public static bool operator ==(GraphNode<N, E> gn, N obj)
		{
			if ((object)gn == null || (object)obj == null)
			{
				return false;
			}

			object o1 = gn.Object as object;
			object o2 = obj as object;

			return o1 == o2;
		}

		/// <summary>
		/// Operator == for objects of type GraphNode&lt;N, E&gt; and type N
		/// </summary>
		/// <param name="gn">First comparer</param>
		/// <param name="obj">Second comparer</param>
		/// <returns>True if equal, false otherwise</returns>
		public static bool operator ==(N obj, GraphNode<N, E> gn)
		{
			return gn == obj;
		}

		/// <summary>
		/// Operator != for objects of type GraphNode&lt;N, E&gt; and type N
		/// </summary>
		/// <param name="gn">First comparer</param>
		/// <param name="obj">Second comparer</param>
		/// <returns>True if not equal, false otherwise</returns>
		public static bool operator !=(GraphNode<N, E> gn, N obj)
		{
			return !(gn == obj);
		}

		/// <summary>
		/// Operator != for objects of type GraphNode&lt;N, E&gt; and type N
		/// </summary>
		/// <param name="gn">First comparer</param>
		/// <param name="obj">Second comparer</param>
		/// <returns>True if not equal, false otherwise</returns>
		public static bool operator !=(N obj, GraphNode<N, E> gn)
		{
			return gn != obj;
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

			GraphNode<N, E> node = obj as GraphNode<N, E>;

			if (node == null)
			{
				return false;
			}

			object o1 = node.Object as object;
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
	}
}
