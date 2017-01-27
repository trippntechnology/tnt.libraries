using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using NUnit.Framework;
using TNT.SQLite;

namespace Tests
{
	[TestFixture]
	public class SQLiteBaseTest : SQLiteBase
	{
		[TearDown]
		public void TearDown()
		{
			Execute("drop table TestTable");
		}

		[Test]
		public void GetGuidTest()
		{
			Guid expectedGuid = Guid.NewGuid();

			Execute("create table TestTable (column1 guid, column2 guid, column3 guid)");
			Execute(string.Format("insert into TestTable (column1, column2) values('{0}', '{1}')", expectedGuid.ToString(), Guid.Empty));

			Execute<Guid>("select * from TestTable", dr =>
				{
					if (dr.Read())
					{
						Assert.AreEqual(Guid.Empty, GetGuid(dr, "bogus"));
						Assert.AreEqual(expectedGuid, GetGuid(dr, "column1"));
						Assert.AreEqual(Guid.Empty, GetGuid(dr, "column2"));
						Assert.AreEqual(Guid.Empty, GetGuid(dr, "column3"));
					}

					return Guid.Empty;
				});
		}

		[Test]
		public void getUriTest()
		{
			Execute("create table TestTable (column1 varchar, column2 varchar, column3 varchar)");
			Execute("insert into TestTable (column1, column2) values('http://trippntechnology.com', 'invalid uri')");

			Execute<Uri>("select * from TestTable", dr =>
				{
					if (dr.Read())
					{
						Assert.IsNull(GetUri(dr, "bogus"));
						Assert.AreEqual(new Uri("http://trippntechnology.com"), GetUri(dr, "column1"));
						Assert.IsNull(GetUri(dr, "column2"));
						Assert.IsNull(GetUri(dr, "column3"));
					}

					return null;
				});
		}

		[Test]
		public void GetVersionTest()
		{
			Execute("create table TestTable (column1 varchar, column2 varchar, column3 varchar)");
			Execute("insert into TestTable (column1, column2) values('1.2.3.4', 'invalid version')");

			Execute<Uri>("select * from TestTable", dr =>
			{
				if (dr.Read())
				{
					Assert.IsNull(GetVersion(dr, "bogus"));
					Assert.AreEqual(new Version("1.2.3.4"), GetVersion(dr, "column1"));
					Assert.IsNull(GetVersion(dr, "column2"));
					Assert.IsNull(GetVersion(dr, "column3"));
				}

				return null;
			});
		}

		[Test]
		public void GetDateTimeTest()
		{
			DateTime expectedDT = DateTime.Now;
			Execute("create table TestTable (column1 datetime, column2 datetime, column3 datetime)");
			Execute(string.Format("insert into TestTable (column1, column2) values('{0}', 'invalid datetime')", Format(expectedDT)));

			Execute<Uri>("select * from TestTable", dr =>
			{
				if (dr.Read())
				{
					Assert.AreEqual(DateTime.MinValue, GetDateTime(dr, "bogus"));
					Assert.AreEqual(expectedDT.ToString(), GetDateTime(dr, "column1").ToString());
					Assert.AreEqual(DateTime.MinValue, GetDateTime(dr, "column2"));
					Assert.AreEqual(DateTime.MinValue, GetDateTime(dr, "column3"));
				}

				return null;
			});
		}

		[Test]
		public void GetIntTest()
		{
			int expectedValue = (new Random()).Next(Int32.MaxValue);
			Execute("create table TestTable (column1 int, column2 int, column3 int)");
			Execute(string.Format("insert into TestTable (column1, column2) values({0}, 'invalid int')", expectedValue));

			Execute<Uri>("select * from TestTable", dr =>
			{
				if (dr.Read())
				{
					Assert.AreEqual(0, GetInt(dr, "bogus"));
					Assert.AreEqual(expectedValue, GetInt(dr, "column1"));
					Assert.AreEqual(0, GetInt(dr, "column2"));
					Assert.AreEqual(0, GetInt(dr, "column3"));
				}

				return null;
			});
		}

		[Test]
		public void GetLongTest()
		{
			long expectedValue = (long)Math.Pow(2, 45);
			Execute("create table TestTable (column1 long, column2 long, column3 long)");
			Execute(string.Format("insert into TestTable (column1, column2) values({0}, 'invalid long')", expectedValue));

			Execute<Uri>("select * from TestTable", dr =>
			{
				if (dr.Read())
				{
					Assert.AreEqual(0, GetLong(dr, "bogus"));
					Assert.AreEqual(expectedValue, GetLong(dr, "column1"));
					Assert.AreEqual(0, GetLong(dr, "column2"));
					Assert.AreEqual(0, GetLong(dr, "column3"));
				}

				return null;
			});
		}

		[Test]
		public void ExecuteWithParametersTest()
		{
			Execute("create table TestTable (column1 varchar)");

			List<DbParameter> parms = new List<DbParameter>();

			parms.Add(new SQLiteParameter("@value"));

			string[] values = { "one", "two", "three" };

			foreach (string value in values)
			{
				parms[0].Value = value;
				Assert.AreEqual(1, Execute("insert into TestTable values(@value)", parms));
			}
		}

		[Test]
		public void ExecuteScalarTest()
		{
			DateTime expectedDT = DateTime.Now;
			Execute("create table TestTable (column1 int, column2 varchar, column3 datetime, column4 varchar)");
			Execute(string.Format("insert into TestTable (column1, column2, column3) values(30, 'Text column', '{0}')", Format(expectedDT)));

			Assert.AreEqual(30, ExecuteScalar<int>("select * from TestTable"));
			Assert.AreEqual(1, ExecuteScalar<long>("select count(*) from TestTable"));
			Assert.AreEqual("Text column", ExecuteScalar<string>("select column2 from TestTable"));
			Assert.AreEqual(expectedDT.ToString(), ExecuteScalar<DateTime>("select column3 from TestTable").ToString());
			Assert.AreEqual(default(string), ExecuteScalar<string>("select column4 from TestTable"));
		}
	}
}
