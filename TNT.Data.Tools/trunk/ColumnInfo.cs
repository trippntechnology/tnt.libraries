using System;

namespace TNT.Data.Tools
{
	public abstract class ColumnInfo
	{
		/// <summary>
		/// Gets or sets the name of the column
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the Type of the column
		/// </summary>
		public Type DataType { get; set; }

		/// <summary>
		/// Retrieves a value for the column for the current row
		/// </summary>
		/// <param name="source">The object representing the current row</param>
		/// <returns>The value for the column</returns>
		public abstract object Retrieve(object source);
	}
}
