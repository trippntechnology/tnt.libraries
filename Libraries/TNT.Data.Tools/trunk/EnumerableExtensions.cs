using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace TNT.Data.Tools
{
	public static class EnumerableExtensions
	{
		public static DataTable ToDataTable<T>(this IEnumerable<T> source, params ColumnInfo<T>[] columns)
		{
			return ToDataTable(source, CultureInfo.CurrentCulture, columns);
		}

		public static DataTable ToDataTable<T>(this IEnumerable<T> source, CultureInfo culture, params ColumnInfo<T>[] columns)
		{
			var dt = new DataTable()
			{
				Locale = culture,
			};

			if( columns != null && columns.Length > 0 )
			{
				foreach( var c in columns )
					dt.Columns.Add(new DataColumn(c.Name, c.DataType));

				foreach( var r in source )
					dt.Rows.Add(columns.Select(c => c.Retrieve(r)).ToArray());
			}
			else
			{
				dt.Columns.Add(new DataColumn(null, typeof(T)));

				foreach( var r in source )
					dt.Rows.Add(r);
			}

			return dt;
		}
	}
}
