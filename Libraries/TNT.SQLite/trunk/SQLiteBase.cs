using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;

namespace TNT.SQLite
{
	/// <summary>
	/// Base class for all SQLite classes. Implements methods for accessing SQLite database
	/// </summary>
	public class SQLiteBase
	{
		#region Members

		/// <summary>
		/// Connection string
		/// </summary>
		protected static string m_ConnectionString = string.Empty;

		#endregion

		#region Properties

		/// <summary>
		/// Initializes the connection string if empty and returns it
		/// </summary>
		protected static string ConnectionString
		{
			get
			{
				if (string.IsNullOrEmpty(m_ConnectionString))
				{
					m_ConnectionString = ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString;
				}

				return m_ConnectionString;
			}
		}

		/// <summary>
		/// Returns a SQLite Connection associated with the connection string
		/// </summary>
		protected static SQLiteConnection Connection
		{
			get
			{
				SQLiteConnection conn = new SQLiteConnection(ConnectionString);
				conn.Open();

				return conn;
			}
		}

		#endregion

		#region Execution Methods

		/// <summary>
		/// Executes a non-query sql statement
		/// </summary>
		/// <param name="sql">Sql statement</param>
		/// <returns>Number of rows effected</returns>
		protected static int Execute(string sql)
		{
			return Execute(sql, new List<DbParameter>());
		}

		/// <summary>
		/// Executes a non-query sql statement
		/// </summary>
		/// <param name="sql">Sql statement</param>
		/// <param name="parms">Parameters</param>
		/// <returns>Number of rows effected</returns>
		protected static int Execute(string sql, List<DbParameter> parms)
		{
			using (SQLiteConnection conn = Connection)
			using (SQLiteCommand cmd = conn.CreateCommand())
			{
				cmd.CommandText = sql;

				foreach (DbParameter parm in parms)
				{
					cmd.Parameters.Add(parm);
				}

				return cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Executes a query sql statement with results
		/// </summary>
		/// <typeparam name="T">Type of result</typeparam>
		/// <param name="sql">Sql statement</param>
		/// <param name="dataReaderProcessor">Processes the data reader</param>
		/// <returns>Results of type T</returns>
		protected static T Execute<T>(string sql, Func<DbDataReader, T> dataReaderProcessor)
		{
			using (SQLiteConnection conn = Connection)
			using (SQLiteCommand cmd = conn.CreateCommand())
			{
				cmd.CommandText = sql;

				using (SQLiteDataReader dr = cmd.ExecuteReader())
				{
					return dataReaderProcessor(dr);
				}
			}
		}

		/// <summary>
		/// Returns the first column of the first row cast to type T
		/// </summary>
		/// <typeparam name="T">Type of value expected</typeparam>
		/// <param name="sql">Query</param>
		/// <returns>The first column of the first row cast to type T</returns>
		protected static T ExecuteScalar<T>(string sql)
		{
			using(SQLiteConnection conn = Connection)
			using (SQLiteCommand cmd = conn.CreateCommand())
			{
				cmd.CommandText = sql;

				object obj = cmd.ExecuteScalar();

				if (IsDBNull(obj))
				{
					obj = default(T);
				}
				
				return (T)obj;
			}
		}

		#endregion

		#region Getters

		/// <summary>
		/// Gets the Guid associated with the column name
		/// </summary>
		/// <param name="dr">Data reader</param>
		/// <param name="columnName">Column name</param>
		/// <returns>Guid associated with the column name if exists, Guid.Empty otherwise</returns>
		protected static Guid GetGuid(DbDataReader dr, string columnName)
		{
			int ordinal = dr.GetOrdinal(columnName);
			Guid guid = Guid.Empty;

			if (ordinal > -1 && !IsDBNull(dr[columnName]))
			{
				guid = dr.GetGuid(ordinal);
			}

			return guid;
		}

		/// <summary>
		/// Gets the string associated with the column name
		/// </summary>
		/// <param name="dr">Data reader</param>
		/// <param name="columnName">Column name</param>
		/// <returns>String associated with the column name if exists, string.Empty otherwise</returns>
		protected static string GetString(DbDataReader dr, string columnName)
		{
			int ordinal = dr.GetOrdinal(columnName);
			string value = string.Empty;

			if (ordinal > -1 && !IsDBNull(dr[columnName]))
			{
				value = dr.GetString(ordinal);
			}

			return value;
		}

		/// <summary>
		/// Gets the Uri associated with the column name
		/// </summary>
		/// <param name="dr">Data reader</param>
		/// <param name="columnName">Column name</param>
		/// <returns>Uri associated with the column name if exists, null otherwise</returns>
		protected static Uri GetUri(DbDataReader dr, string columnName)
		{
			string value = GetString(dr, columnName);
			Uri uri = null;

			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					uri = new Uri(value);
				}
				catch (UriFormatException)
				{
				}
			}

			return uri;
		}

		/// <summary>
		/// Gets the Version associated with the column name
		/// </summary>
		/// <param name="dr">Data reader</param>
		/// <param name="columnName">Column name</param>
		/// <returns>Version associated with the column name if exists, null o	therwise</returns>
		protected static Version GetVersion(DbDataReader dr, string columnName)
		{
			string value = GetString(dr, columnName);
			Version version = null;

			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					version = new Version(value);
				}
				catch (ArgumentException)
				{
				}
			}

			return version;
		}

		/// <summary>
		/// Gets the DateTime associated with the column name
		/// </summary>
		/// <param name="dr">Data reader</param>
		/// <param name="columnName">Column name</param>
		/// <returns>DateTime associated with the column name if exists, DateTime.MinValue otherwise</returns>
		protected static DateTime GetDateTime(DbDataReader dr, string columnName)
		{
			int ordinal = dr.GetOrdinal(columnName);
			DateTime dateTime = DateTime.MinValue;

			try
			{
				if (ordinal > -1 && !IsDBNull(dr[columnName]))
				{
					dateTime = dr.GetDateTime(ordinal);
				}
			}
			catch (FormatException)
			{
			}

			return dateTime;
		}

		/// <summary>
		/// Gets the int value associated with the column name
		/// </summary>
		/// <param name="dr">Data reader</param>
		/// <param name="columnName">Column name</param>
		/// <returns>Int value associated with the column name</returns>
		protected static int GetInt(DbDataReader dr, string columnName)
		{
			int ordinal = dr.GetOrdinal(columnName);
			int value = 0;

			if (ordinal > -1 && !IsDBNull(dr[columnName]))
			{
				try
				{
					value = dr.GetInt32(ordinal);
				}
				catch (InvalidCastException)
				{
				}
			}

			return value;
		}

		/// <summary>
		/// Gets the long value associated with the column name
		/// </summary>
		/// <param name="dr">Data reader</param>
		/// <param name="columnName">Column name</param>
		/// <returns>Long value associated with the column name</returns>
		protected static long GetLong(DbDataReader dr, string columnName)
		{
			int ordinal = dr.GetOrdinal(columnName);
			long value = 0;

			if (ordinal > -1 && !IsDBNull(dr[columnName]))
			{
				try
				{
					value = dr.GetInt64(ordinal);
				}
				catch (InvalidCastException)
				{
				}
			}

			return value;
		}

		#endregion

		/// <summary>
		/// Check if the obj is of type DBNull
		/// </summary>
		/// <param name="obj">Object</param>
		/// <returns>True if obj is of type DBNull, false otherwise</returns>
		protected static bool IsDBNull(object obj)
		{
			return obj.GetType() == typeof(System.DBNull);
		}

		/// <summary>
		/// Formats the DateTime value to be compatible with SQLite DateTime type
		/// </summary>
		/// <param name="dateTime">DateTime value</param>
		/// <returns>DateTime formatted to be compatible with SQLite</returns>
		protected static string Format(DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
		}
	}
}
