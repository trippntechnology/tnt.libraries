using System;
using System.Data;

namespace TNT.Data.Tools
{
	/// <summary>
	/// Provides extension methods for IDataRecord
	/// </summary>
	public static class DataRecordExtensions
	{
		/// <summary>
		/// Retrieves a nullable Decimal value from a IDataRecord
		/// </summary>
		/// <param name="dr">IDataRecord to retrieve the value from</param>
		/// <param name="column">Column name to retrieve a value for</param>
		/// <returns>Null or a valid Decimal value</returns>
		public static decimal? GetDecimal(this IDataRecord dr, string column)
		{
			int idx = dr.GetOrdinal(column);

			if( dr.IsDBNull(idx) )
				return null;
			else
				return Convert.ToDecimal(dr[idx]);
		}

		/// <summary>
		/// Retrieves a string value from a IDataRecord
		/// </summary>
		/// <param name="dr">IDataRecord to retrieve the value from</param>
		/// <param name="column">Column name to retrieve a value for</param>
		/// <returns>Null or a valid string value</returns>
		public static string GetString(this IDataRecord dr, string column)
		{
			int idx = dr.GetOrdinal(column);

			if( dr.IsDBNull(idx) )
				return null;
			else
				return Convert.ToString(dr[idx]);
		}

		/// <summary>
		/// Retrieves a nullable DateTime value from a IDataRecord
		/// </summary>
		/// <param name="dr">IDataRecord to retrieve the value from</param>
		/// <param name="column">Column name to retrieve a value for</param>
		/// <returns>Null or a valid DateTime value</returns>
		public static DateTime? GetDateTime(this IDataRecord dr, string column)
		{
			int idx = dr.GetOrdinal(column);

			if( dr.IsDBNull(idx) )
				return null;
			else
				return Convert.ToDateTime(dr[idx]);
		}

		/// <summary>
		/// Retrieves a nullable int value from a IDataRecord
		/// </summary>
		/// <param name="dr">IDataRecord to retrieve the value from</param>
		/// <param name="column">Column name to retrieve a value for</param>
		/// <returns>Null or a valid int value</returns>
		public static int? GetInt32(this IDataRecord dr, string column)
		{
			int idx = dr.GetOrdinal(column);

			if( dr.IsDBNull(idx) )
				return null;
			else
				return Convert.ToInt32(dr[idx]);
		}

		/// <summary>
		/// Retrieves a nullable long value from a IDataRecord
		/// </summary>
		/// <param name="dr">IDataRecord to retrieve the value from</param>
		/// <param name="column">Column name to retrieve a value for</param>
		/// <returns>Null or a valid long value</returns>
		public static long? GetInt64(this IDataRecord dr, string column)
		{
			int idx = dr.GetOrdinal(column);

			if( dr.IsDBNull(idx) )
				return null;
			else
				return Convert.ToInt64(dr[idx]);
		}

		/// <summary>
		/// Retrieves a nullable boolean value from a IDataRecord
		/// </summary>
		/// <param name="dr">IDataRecord to retrieve the value from</param>
		/// <param name="column">Column name to retrieve a value for</param>
		/// <returns>Null or a valid boolean value</returns>
		public static bool? GetBoolean(this IDataRecord dr, string column)
		{
			int idx = dr.GetOrdinal(column);

			if( dr.IsDBNull(idx) )
				return null;
			else
				return Convert.ToBoolean(dr[idx]);
		}

		/// <summary>
		/// Retrieves a nullable Guid value from an IDataRecord
		/// </summary>
		/// <param name="dr">IDataRecord to retrieve the value from</param>
		/// <param name="column">Column name to retrieve a value for</param>
		/// <returns>Null or a valid Guid value</returns>
		public static Guid? GetGuid(this IDataRecord dr, string column)
		{
			int idx = dr.GetOrdinal(column);

			if( dr.IsDBNull(idx) )
				return null;
			else
				return dr.GetGuid(idx);
		}

		/// <summary>
		/// Retrieves a byte[] value from an IDataRecord
		/// </summary>
		/// <param name="dr">IDataRecord to retrieve the value from</param>
		/// <param name="column">Column name to retrieve a value for</param>
		/// <returns>Null or a valid Guid value</returns>
		public static byte[] GetBytes(this IDataRecord dr, string column)
		{
			return GetBytes(dr, dr.GetOrdinal(column));
		}

		/// <summary>
		/// Retrieves a byte[] value from an IDataRecord
		/// </summary>
		/// <param name="dr">IDataRecord to retrieve the value from</param>
		/// <param name="index">Column index to retrieve a value for</param>
		/// <returns>Null or a valid Guid value</returns>
		public static byte[] GetBytes(this IDataRecord dr, int index)
		{
			if( dr.IsDBNull(index) )
				return null;
			else
				return (byte[])dr.GetValue(index);
		}

		/// <summary>
		/// Return whether the specified field is set to null
		/// </summary>
		/// <param name="dr">IDataRecord to retrieve the value from</param>
		/// <param name="column">The name of the field to find</param>
		/// <returns>True if the field is set to null</returns>
		public static bool IsDBNull(this IDataRecord dr, string column)
		{
			int idx = dr.GetOrdinal(column);

			return dr.IsDBNull(idx);
		}
	}
}
