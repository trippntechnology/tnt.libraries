using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class PathParameter_Tests
	{
		Parameters p = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			p.Add(new PathParameter("p1", "Not required"));
			p.Add(new PathParameter("p2", "Required", true));
			p.Add(new PathParameter("p3", "Description for P3", "c:\\"));
			p.Add(new PathParameter("p4", "Description for P4") { CreateIfMissing = true });
		}

		[TestMethod]
		public void PathParameter_Syntax()
		{
			Assert.AreEqual("[/p1 <Path>]", p["p1"].Syntax());
			Assert.AreEqual("/p2 <Path>", p["p2"].Syntax());
			Assert.AreEqual("[/p3 <Path>]", p["p3"].Syntax());
		}

		[TestMethod]
		public void PathParameter_SetValue()
		{
			p.ParseArgs(new string[] { "/p2", "c:\\" });
			Assert.AreEqual("c:\\", p["p2"].Value);
		}

		[TestMethod]
		public void PathParameter_SetValue_NonExistantPath()
		{
			Assert.IsFalse(p.ParseArgs(new string[] { "/p2", "g:\\" }));
		}

		[TestMethod]
		public void PathParameter_SetValue_InvalidPath()
		{
			Assert.IsFalse(p.ParseArgs(new string[] { "/p2", "g:\\:" }));
		}

		[TestMethod]
		public void PathParameter_CreatePath()
		{
			string path = "createdpath";

			try
			{
				Directory.Delete(path);
			}
			catch
			{
			}

			Assert.IsTrue(p.ParseArgs(new string[] { "/p2", "c:\\", "/p4", path }));
			Assert.IsTrue(Directory.Exists(path));
		}
	}
}
