using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace TNT.Data.Tools
{
	public class QueryHelper : IDisposable
	{
		private DbProviderFactory m_fact;
		private string            m_cstr;
		private DbConnection      m_conn;
		private DbTransaction     m_tran;
		private bool              m_perst;

		public QueryHelper(string connectionStringName) : this(connectionStringName, false) { }

		public QueryHelper(string providerName, string connectionString) : this(providerName, connectionString, false) { }

		public QueryHelper(string connectionStringName, bool persistant)
		{
			var cstr = ConfigurationManager.ConnectionStrings[connectionStringName];

			if( cstr == null )
				throw new ArgumentException("The given connection string does not exist.", "connectionStringName");

			m_cstr  = cstr.ConnectionString;
			m_fact  = DbProviderFactories.GetFactory(cstr.ProviderName);
			m_perst = persistant;

			CommandTimeout = 30;

			if( persistant )
			{
				m_conn = m_fact.CreateConnection();

				m_conn.ConnectionString = m_cstr;

				m_conn.Open();

				if( ConnectionOpened != null )
					ConnectionOpened(this, new ConnectionOpenedEventArgs(m_conn));
			}
		}

		public QueryHelper(string providerName, string connectionString, bool persistant) : this(DbProviderFactories.GetFactory(providerName), connectionString, persistant) { }

		public QueryHelper(DbProviderFactory providerFactory, string connectionString) : this(providerFactory, connectionString, false) { }

		public QueryHelper(DbProviderFactory providerFactory, string connectionString, bool persistant)
		{
			m_cstr  = connectionString;
			m_fact  = providerFactory;
			m_perst = persistant;

			CommandTimeout = 30;

			if( persistant )
			{
				m_conn = m_fact.CreateConnection();

				m_conn.ConnectionString = m_cstr;

				m_conn.Open();

				if( ConnectionOpened != null )
					ConnectionOpened(this, new ConnectionOpenedEventArgs(m_conn));
			}
		}

		public event EventHandler<ConnectionOpenedEventArgs> ConnectionOpened;

		public bool TransactionActive
		{
			get { return m_tran != null; }
		}

		public int CommandTimeout { get; set; }

		public void BeginTransaction()
		{
			BeginTransaction(IsolationLevel.Unspecified);
		}

		public void BeginTransaction(IsolationLevel level)
		{
			if( m_tran != null )
				throw new InvalidOperationException("A transaction is already in progress.");

			if( m_conn == null )
			{
				m_conn = m_fact.CreateConnection();

				m_conn.ConnectionString = m_cstr;

				m_conn.Open();
			}

			if( ConnectionOpened != null )
				ConnectionOpened(this, new ConnectionOpenedEventArgs(m_conn));

			m_tran = m_conn.BeginTransaction(level);
		}

		public void CommitTransaction()
		{
			if( m_tran == null )
				throw new InvalidOperationException("A transaction is not currently in progress.");

			try
			{
				m_tran.Commit();

				if( !m_perst )
					m_conn.Close();
			}
			finally
			{
				try
				{
					m_tran.Dispose();

					if( !m_perst )
						m_conn.Dispose();
				}
				catch( DbException )
				{
				}
				catch( ObjectDisposedException )
				{
				}
				catch( NullReferenceException )
				{
				}

				m_tran = null;

				if( !m_perst )
					m_conn = null;
			}
		}

		public void RollbackTransaction()
		{
			if( m_tran == null )
				throw new InvalidOperationException("A transaction is not currently in progress.");

			try
			{
				m_tran.Rollback();

				if( !m_perst )
					m_conn.Close();
			}
			finally
			{
				try
				{
					m_tran.Dispose();

					if( !m_perst )
						m_conn.Dispose();
				}
				catch( DbException )
				{
				}
				catch( ObjectDisposedException )
				{
				}
				catch( NullReferenceException )
				{
				}

				m_tran = null;

				if( !m_perst )
					m_conn = null;
			}
		}

		public DbParameter CreateParameter()
		{
			return m_fact.CreateParameter();
		}

		public DbParameter CreateParameter(string name)
		{
			var parm = m_fact.CreateParameter();

			parm.ParameterName = name;
			
			return parm;
		}

		public DbParameter CreateParameter(string name, DbType type)
		{
			var parm = m_fact.CreateParameter();

			parm.ParameterName = name;
			parm.DbType        = type;

			return parm;
		}

		public DbParameter CreateParameter(string name, DbType type, int size)
		{
			var parm = m_fact.CreateParameter();

			parm.ParameterName = name;
			parm.DbType        = type;
			parm.Size          = size;

			return parm;
		}

		public DbParameter CreateParameter(string name, DbType type, object value)
		{
			var parm = m_fact.CreateParameter();

			parm.ParameterName = name;
			parm.DbType        = type;
			parm.Value         = value;

			return parm;
		}

		public DbParameter CreateParameter(string name, DbType type, int size, object value)
		{
			var parm = m_fact.CreateParameter();

			parm.ParameterName = name;
			parm.DbType        = type;
			parm.Size          = size;
			parm.Value         = value;

			return parm;
		}

		public void ExecuteQuery(string text, CommandType commandType)
		{
			ExecuteCommand(text, commandType, null, dr => true);
		}

		public void ExecuteQuery(string text, CommandType commandType, IEnumerable<DbParameter> parameters)
		{
			ExecuteCommand(text, commandType, parameters, dr => true);
		}

		public void ExecuteQuery(string text, CommandType commandType, IEnumerable<DbParameter> parameters, Action<DbDataReader> resultAction)
		{
			ExecuteCommand(text, commandType, parameters, dr => { resultAction(dr); return true; });
		}

		public T ExecuteQuery<T>(string text, CommandType commandType, IEnumerable<DbParameter> parameters, Func<DbDataReader, T> resultAction)
		{
			return ExecuteCommand(text, commandType, parameters, resultAction);
		}

		public IEnumerable<T> EnumerateRecords<T>(string text, CommandType commandType, IEnumerable<DbParameter> parameters, Func<IDataRecord, T> selector)
		{
			foreach( var rec in EnumerateRecords(text, commandType, parameters) )
				yield return selector(rec);
		}

		public IEnumerable<IDataRecord> EnumerateRecords(string text, CommandType commandType, IEnumerable<DbParameter> parameters)
		{
			if( m_conn != null )
			{
				using( var cmd = m_conn.CreateCommand() )
				{
					cmd.CommandType    = commandType;
					cmd.CommandText    = text;
					cmd.CommandTimeout = CommandTimeout;

					if( m_tran != null )
						cmd.Transaction = m_tran;

					if( parameters != null )
					{
						foreach( var parm in parameters )
							cmd.Parameters.Add(parm);
					}

					using( var dr = cmd.ExecuteReader() )
					{
						while( dr.Read() )
							yield return dr;

						dr.Close();
					}

					cmd.Parameters.Clear();
				}
			}
			else
			{
				using( var conn = m_fact.CreateConnection() )
				{
					conn.ConnectionString = m_cstr;

					conn.Open();

					if( ConnectionOpened != null )
						ConnectionOpened(this, new ConnectionOpenedEventArgs(conn));

					using( var cmd = conn.CreateCommand() )
					{
						cmd.CommandType    = commandType;
						cmd.CommandText    = text;
						cmd.CommandTimeout = CommandTimeout;

						if( parameters != null )
						{
							foreach( var parm in parameters )
								cmd.Parameters.Add(parm);
						}

						using( var dr = cmd.ExecuteReader() )
						{
							while( dr.Read() )
								yield return dr;

							dr.Close();
						}

						cmd.Parameters.Clear();
					}

					conn.Close();
				}
			}
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if( disposing )
			{
				if( m_tran != null )
				{
					m_tran.Dispose();
					m_tran = null;
				}

				if( m_conn != null )
				{
					m_conn.Dispose();
					m_conn = null;
				}
			}
		}

		private T ExecuteCommand<T>(string query, CommandType commandType, IEnumerable<DbParameter> parameters, Func<DbDataReader, T> resultAction)
		{
			if( m_conn != null )
			{
				using( var cmd = m_conn.CreateCommand() )
					return ExecuteCommand(query, commandType, cmd, parameters, resultAction);
			}
			else
			{
				T ret = default(T);

				using( var conn = m_fact.CreateConnection() )
				{
					conn.ConnectionString = m_cstr;

					conn.Open();

					if( ConnectionOpened != null )
						ConnectionOpened(this, new ConnectionOpenedEventArgs(conn));

					using( var cmd = conn.CreateCommand() )
						ret = ExecuteCommand(query, commandType, cmd, parameters, resultAction);

					conn.Close();
				}

				return ret;
			}
		}

		private T ExecuteCommand<T>(string query, CommandType commandType, DbCommand cmd, IEnumerable<DbParameter> parameters, Func<DbDataReader, T> resultAction)
		{
			cmd.CommandType    = commandType;
			cmd.CommandText    = query;
			cmd.CommandTimeout = CommandTimeout;

			if( m_tran != null )
				cmd.Transaction = m_tran;

			if( parameters != null )
			{
				foreach( var parm in parameters )
					cmd.Parameters.Add(parm);
			}

			T ret = default(T);

			if( resultAction != null )
			{
				using( var dr = cmd.ExecuteReader() )
					ret = resultAction(dr);
			}
			else
			{
				cmd.ExecuteNonQuery();
			}

			cmd.Parameters.Clear();

			return ret;
		}
	}
}
