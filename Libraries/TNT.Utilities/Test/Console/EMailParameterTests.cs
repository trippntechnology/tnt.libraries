using System;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class EMailParameterTests
	{
		Parameters p = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			p.Add(new EMailParameter("e1", "Not required"));
			p.Add(new EMailParameter("e2", "Required", true));
		}

		[TestMethod]
		public void EMailParameter_Syntax()
		{
			Assert.AreEqual("[/e1 <Email>]", p["e1"].Syntax());
			Assert.AreEqual("/e2 <Email>", p["e2"].Syntax());
		}

		[TestMethod]
		public void EMailParameter_SetValue()
		{
			p.ParseArgs(new string[] { "/e2", "bogus@domain.com" });
			Assert.IsNull(p["e1"].Value);
			Assert.AreEqual(new MailAddress("bogus@domain.com"), (p["e2"] as EMailParameter).Value);
		}

		[TestMethod]
		public void EMailParameter_SetValue_Exception()
		{
			Assert.IsFalse(p.ParseArgs(new string[] { "/e2", "A" }));
		}
	}
}
