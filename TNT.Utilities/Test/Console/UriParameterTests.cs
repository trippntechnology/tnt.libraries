using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TNT.Utilities.Console;

namespace Tests.Console
{
	[TestClass]
	public class UriParameterTests
	{
		[TestMethod]
		public void UriParameter_Constructor()
		{
			string[] args = new string[] { "/p1", "http://domain.com/index.html" };
			UriParameter up = new UriParameter("name", "description");

			Assert.AreEqual("name", up.Name);
			Assert.AreEqual("description", up.Description);
			Assert.IsNull(up.DefaultValue);
			Assert.IsFalse(up.Required);
			Assert.IsNull(up.Validate);
			Assert.IsNull(up.Value);

			up.SetValue("http://domain.com/index.html");

			Assert.IsTrue(up.Value is Uri);

			try
			{
				up.SetValue("invalid uri");
				Assert.Fail();
			}
			catch (UriFormatException)
			{
			}

			try
			{
				up.SetValue(string.Empty);
				Assert.Fail();
			}
			catch (ArgumentException)
			{
			}

			up = new UriParameter("name", "description", new Uri("http://default.value.com/index.html"));

			Assert.AreEqual("name", up.Name);
			Assert.AreEqual("description", up.Description);
			Assert.IsNotNull(up.DefaultValue);
			Assert.IsTrue(up.DefaultValue is Uri);
			Assert.IsFalse(up.Required);
			Assert.IsNull(up.Validate);
			Assert.IsNotNull(up.Value);
		}

		[TestMethod]
		public void UriParameter_Syntax()
		{
			UriParameter up = new UriParameter("Name", "Description");
			Assert.AreEqual("[/Name <uri>]", up.Syntax());

			up = new UriParameter("Name", "Description", true);
			Assert.AreEqual("/Name <uri>", up.Syntax());
		}
	}
}
