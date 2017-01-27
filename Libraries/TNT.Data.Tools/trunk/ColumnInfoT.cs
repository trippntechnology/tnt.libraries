using System;

namespace TNT.Data.Tools
{
	public class ColumnInfo<T> : ColumnInfo
	{
		/// <summary>
		/// Initializes a new instance of the ColumnInfo class
		/// </summary>
		public ColumnInfo() { }

		/// <summary>
		/// Initializes a new instance of the ColumnInfo class
		/// </summary>
		/// <param name="name">The name of the column</param>
		/// <param name="type">The type of the column</param>
		/// <param name="retriever">A delegate for retrieving values for each row</param>
		public ColumnInfo(string name, Type type, Func<T, object> retriever)
		{
			Name      = name;
			DataType  = type;
			Retriever = retriever;
		}

		/// <summary>
		/// Gets or sets a delegate for retrieving values for each row
		/// </summary>
		public Func<T, object> Retriever { get; set; }

		/// <summary>
		/// Retrieves a value for the column for the current row
		/// </summary>
		/// <param name="source">The object representing the current row</param>
		/// <returns>The value for the column</returns>
		public override object Retrieve(object source)
		{
			return Retriever((T)source);
		}

		/// <summary>
		/// Retrieves a value for the column for the current row
		/// </summary>
		/// <param name="source">The object representing the current row</param>
		/// <returns>The value for the column</returns>
		public object Retrieve(T source)
		{
			return Retriever(source);
		}
	}
}
