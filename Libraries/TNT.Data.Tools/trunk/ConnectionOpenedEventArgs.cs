using System;
using System.Data.Common;

namespace TNT.Data.Tools
{
	public class ConnectionOpenedEventArgs : EventArgs
	{
		public ConnectionOpenedEventArgs(DbConnection connection)
		{
			Connection = connection;
		}

		public DbConnection Connection { get; private set; }
	}
}
