using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class FlagParameter_Tests
	{
		Parameters m_Params = new Parameters();

		[TestInitialize]
		public void Intialize()
		{
			m_Params.Add(new FlagParameter("f1", "Flags description"));
			m_Params.Add(new FlagParameter("f2", "Hidden syntax") { HideSyntax = true });
		}

		[TestMethod]
		public void FlagParameter_Exists()
		{
			m_Params.ParseArgs(new string[] { "/f1" });

			Assert.IsTrue(m_Params.FlagExists("f1"));
			Assert.IsTrue(m_Params.FlagExists("F1"));
			Assert.IsFalse(m_Params.FlagExists("f2"));
		}

		[TestMethod]
		public void FlagParameter_FlagWithValue()
		{
			Assert.IsFalse(m_Params.ParseArgs(new string[] { "/f1", "value" }));
		}

		[TestMethod]
		public void FlagParameter_Syntax()
		{
			Assert.AreEqual("[/f1]", m_Params.First().Syntax());
			Assert.AreEqual(string.Empty, m_Params["f2"].Syntax());
		}

		[TestMethod]
		public void FlagParameter_get_Value()
		{
			m_Params.ParseArgs(new string[] { "/f1" });
			Assert.IsTrue(string.IsNullOrEmpty(m_Params["f1"].Value as string));
		}
	}
}
