using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class DateTimeParameter_Tests
	{
		Parameters m_Params = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			m_Params.Add(new DateTimeParameter("d1", "Not required"));
			m_Params.Add(new DateTimeParameter("d2", "Required", true));
			m_Params.Add(new DateTimeParameter("d3", "Default", Convert.ToDateTime("12-11-7")));
		}

		[TestMethod]
		public void DateTimeParameter_Syntax()
		{
			Assert.AreEqual("[/d1 <datetime>]", m_Params["d1"].Syntax());
			Assert.AreEqual("/d2 <datetime>", m_Params["d2"].Syntax());
			Assert.AreEqual("[/d3 <datetime>]", m_Params["d3"].Syntax());
		}

		[TestMethod]
		public void DateTimeParameter_SetValue()
		{
			m_Params.ParseArgs(new string[] { "/d2", "12-11-7" });
			Assert.AreEqual(Convert.ToDateTime("12-11-7"), m_Params["d2"].Value);
		}

		[TestMethod]
		public void DateTimeParameter_GetValue()
		{
			m_Params.ParseArgs(new string[] { "/d2", "12-11-7" });
			Assert.IsNull((m_Params["d1"] as DateTimeParameter).Value);
			Assert.AreEqual(Convert.ToDateTime("12-11-7"), (m_Params["d2"] as DateTimeParameter).Value);
		}

		[TestMethod]
		public void DateTimeParameter_SetValue_Exception()
		{
			Assert.IsFalse(m_Params.ParseArgs(new string[] { "/d2", "invalid datetime" }));
		}
	}
}
