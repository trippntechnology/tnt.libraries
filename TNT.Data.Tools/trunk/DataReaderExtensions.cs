using System.Collections.Generic;
using System.Data;

namespace TNT.Data.Tools
{
	/// <summary>
	/// Provides extension methods for IDataReader
	/// </summary>
	public static class DataReaderExtensions
	{
		/// <summary>
		/// Returns an enumerator that enumerates the rows in the IDataReader
		/// </summary>
		/// <param name="dr">IDataReader to enumerate rows for</param>
		/// <returns>An enumerator that enumerates the rows in the IDataReader</returns>
		public static IEnumerable<IDataRecord> AsEnumerable(this IDataReader dr)
		{
			while( dr.Read() )
				yield return dr;
		}
	}
}
