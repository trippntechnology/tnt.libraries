using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class FileListParameter_Tests
	{
		Parameters p = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			p.Add(new FileListParameter("f1", "Not required"));
			p.Add(new FileListParameter("f2", "Required", true));
		}

		[TestMethod]
		public void FileParameter_Syntax()
		{
			Assert.AreEqual("[/f1 <File[]>]", p["f1"].Syntax());
			Assert.AreEqual("/f2 <File[]>", p["f2"].Syntax());
		}

		[TestMethod]
		public void FileParameter_SetValue()
		{
			p.ParseArgs(new string[] { "/f2", @"Resources\file1.txt, Resources\file2.txt, Resources\file3.txt" });
			CollectionAssert.AreEqual(new string[] { "Resources\\file1.txt", "Resources\\file2.txt", "Resources\\file3.txt" }, (p["f2"] as FileListParameter).Value);
		}

		[TestMethod]
		public void FileParameter_SetValue_InvalidFile()
		{
			try
			{
				Assert.IsFalse(p.ParseArgs(new string[] { "/f2", "f\"dfd" }, null, false));
				Assert.Fail("Exception expected");
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is ArgumentException);
				Assert.AreEqual("The 'f2' parameters expects a valid file name", ex.Message);
			}
		}

		[TestMethod]
		public void FileParameter_SetValue_MustExist()
		{
			try
			{
				Assert.IsFalse(p.ParseArgs(new string[] { "/f2", @"Resources\invalid.txt" }, null, false));
				Assert.Fail("Exception expected");
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is ArgumentException);
				Assert.AreEqual("'Resources\\invalid.txt' does not exist", ex.Message);
			}
		}
	}
}
