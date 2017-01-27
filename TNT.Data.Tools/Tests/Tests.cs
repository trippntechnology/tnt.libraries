using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using TNT.Data.Tools;

namespace Tests
{
	[TestClass]
	public class Tests
	{
		protected static QueryHelper CreateQueryHelper { get { return new QueryHelper("sqlite"); } }

		[ClassInitialize]

		public static void Initialize(TestContext tc)
		{
			using (QueryHelper qh = CreateQueryHelper)
			{
				qh.ExecuteQuery("create table test(col1, col2, col3, col4)", CommandType.Text);

				StringBuilder sql = new StringBuilder();
				sql.Append("insert into test values(@col1, @col2, @col3, @col4)");

				for (int i = 1; i< 10; i++)
				{
					List<DbParameter> parms = new List<DbParameter>();

					parms.Add(qh.CreateParameter("@col1", DbType.AnsiString, string.Concat("row", i)));
					parms.Add(qh.CreateParameter("@col2", DbType.AnsiString, string.Concat("row", i)));
					parms.Add(qh.CreateParameter("@col3", DbType.AnsiString, string.Concat("row", i)));
					parms.Add(qh.CreateParameter("@col4", DbType.AnsiString, string.Concat("row", i)));

					qh.ExecuteQuery(sql.ToString(), CommandType.Text, parms);
				}
			}
		}

		[ClassCleanup]
		public static void CleanUp()
		{
			using (QueryHelper qh = CreateQueryHelper)
			{
				qh.ExecuteQuery("drop table test", CommandType.Text);
			}
		}

		[TestMethod]
		public void TestMethod1()
		{
			using (QueryHelper qh = new QueryHelper("sqlite"))
			{
				qh.ExecuteQuery("select * from test", CommandType.Text, null, dr =>
					{
						if (dr.Read())
						{
							Assert.AreEqual("row1", dr.GetString("col3"));
						}
						else
						{
							Assert.Fail("No records found");
						}
					});
			}
		}
	}
}
