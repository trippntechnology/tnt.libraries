using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class VersionParameterTests
	{
		[TestMethod]
		public void VersionParameter_DefaultValueConstructor()
		{
			var version = new Version("1.2.3.4");
			var sut = new VersionParameter("name", "description", version);
			Assert.AreEqual("name", sut.Name);
			Assert.AreEqual("description", sut.Description);
			Assert.IsFalse(sut.Required);
			Assert.AreEqual(version, sut.Value);
		}

		[TestMethod]
		public void VersionParameter_RequiredConstructor()
		{
			var sut = new VersionParameter("name", "description");
			Assert.AreEqual("name", sut.Name);
			Assert.AreEqual("description", sut.Description);
			Assert.IsFalse(sut.Required);
			Assert.IsNull(sut.Value);

			sut = new VersionParameter("name", "description", true);
			Assert.AreEqual("name", sut.Name);
			Assert.AreEqual("description", sut.Description);
			Assert.IsTrue(sut.Required);
			Assert.IsNull(sut.Value);
		}

		[TestMethod]
		public void VersionParameter_Syntax()
		{
			var sut = new VersionParameter("name", "description");
			Assert.AreEqual("[/name <version>]", sut.Syntax());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void VersionParameter_SetValue_Exception()
		{
			var sut = new VersionParameter("v", "description");
			try
			{
				sut.SetValue("bogus value");
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Parameter 'v' expects a string that represents a version", ex.Message);
				throw;
			}
		}

		[TestMethod]
		public void VersionParameter_SetValue()
		{
			var sut = new VersionParameter("v", "description");
			sut.SetValue("1.2.3.4");
			Assert.AreEqual(new Version("1.2.3.4"), sut.Value);
		}
	}
}
