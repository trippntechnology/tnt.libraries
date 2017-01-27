using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class FileParameter_Tests
	{
		Parameters p = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			p.Add(new FileParameter("f1", "Not required"));
			p.Add(new FileParameter("f2", "Required", true));
		}

		[TestMethod]
		public void FileParameter_Syntax()
		{
			Assert.AreEqual("[/f1 <File>]", p["f1"].Syntax());
			Assert.AreEqual("/f2 <File>", p["f2"].Syntax());
		}

		[TestMethod]
		public void FileParameter_SetValue()
		{
			p.ParseArgs(new string[] { "/f2", "file.txt" });
			Assert.AreEqual("file.txt", p["f2"].Value);
		}

		[TestMethod]
		public void FileParameter_SetValue_InvalidFile()
		{
			Assert.IsFalse(p.ParseArgs(new string[] { "/f2", "f\"dfd" }));
		}

		[TestMethod]
		public void FileParameter_SetValue_MustExist()
		{
			(p["f2"] as FileParameter).MustExist = true;
			Assert.IsFalse(p.ParseArgs(new string[] { "/f2", "file.txt" }));
		}
	}
}
