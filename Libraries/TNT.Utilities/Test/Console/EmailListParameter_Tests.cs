using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class EmailListParameter_Tests
	{
		Parameters p = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			p.Add(new EmailListParameter("f1", "Not required"));
			p.Add(new EmailListParameter("f2", "Required", true));
		}

		[TestMethod]
		public void EmailListParameter_Syntax()
		{
			Assert.AreEqual("[/f1 <Email[]>]", p["f1"].Syntax());
			Assert.AreEqual("/f2 <Email[]>", p["f2"].Syntax());
		}

		[TestMethod]
		public void EmailListParameter_SetValue()
		{
			p.ParseArgs(new string[] { "/f2", @"addr1@domain.com, addr2@domain.com,addr3@domain.com" });
			CollectionAssert.AreEqual(new string[] { "addr1@domain.com", "addr2@domain.com", "addr3@domain.com" }, (p["f2"] as EmailListParameter).Value);
		}

		[TestMethod]
		public void EmailListParameter_SetValue_InvalidEmail()
		{
			try
			{
				Assert.IsFalse(p.ParseArgs(new string[] { "/f2", "f\"email address" }, null, false));
				Assert.Fail("Exception expected");
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is ArgumentException);
				Assert.AreEqual("The parameter 'f2' expects a comma delimited listing of email addresses", ex.Message);
			}
		}

		//[TestMethod]
		//public void EmailListParameter_SetValue_MustExist()
		//{
		//	try
		//	{
		//		Assert.IsFalse(p.ParseArgs(new string[] { "/f2", @"Resources\invalid.txt" }, null, false));
		//		Assert.Fail("Exception expected");
		//	}
		//	catch (Exception ex)
		//	{
		//		Assert.IsTrue(ex is ArgumentException);
		//		Assert.AreEqual("'Resources\\invalid.txt' does not exist", ex.Message);
		//	}
		//}
	}
}
