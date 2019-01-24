using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities.Console;

namespace Test
{
	public enum TestEnum { One, Two, Three, Four, Five };

	[TestClass]
	public class ParametersTests
	{
		[TestMethod]
		public void Parameters_Add_NotUnique()
		{
			Parameters parms = new Parameters();

			parms.Add(new FlagParameter("f1", "f1 description"));

			try
			{
				parms.Add(new StringParameter("f1", ""));
				Assert.Fail();
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual("Parameter 'f1' already exists", ae.Message);
			}
		}

		[TestMethod]
		public void Parameters_ParseArgs_NotValid()
		{
			Parameters parms = new Parameters();

			parms.Add(new FlagParameter("f1", "f1 description"));
			parms.Add(new StringParameter("s1", "s1 description"));

			Assert.IsFalse(parms.ParseArgs(new string[] { "/i1" }));
		}

		[TestMethod]
		public void Parameters_ParseArgs_PostValidator()
		{
			Parameters parms = new Parameters();
			var postValidatorCalled = false;

			parms.Add(new FlagParameter("f1", "f1 description"));
			parms.Add(new StringParameter("s1", "s1 description"));

			Assert.IsTrue(parms.ParseArgs(new string[] { "/s1", "param1", "/f1" },(obj)=>
			{
				Assert.AreEqual(2, obj.Count);
				postValidatorCalled = true;
			}));

			Assert.IsTrue(postValidatorCalled);
		}

		private void validator(Parameters obj)
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void Parameters_FlagParameter_Exists_Tests()
		{
			Parameters parms = new Parameters();

			parms.Add(new FlagParameter("f1", "Description for f1"));
			parms.Add(new FlagParameter("f2", "Description for f2"));

			parms.ParseArgs(new string[] { "/f1" });

			Assert.IsTrue(parms.FlagExists("f1"));
			Assert.IsTrue(parms.FlagExists("F1"));
			Assert.IsFalse(parms.FlagExists("f2"));
		}

		[TestMethod]
		public void Parameters_Duplicate_Args_Tests()
		{
			Parameters p = new Parameters();

			p.Add(new StringParameter("s1", "Description for s1"));
			p.Add(new StringParameter("s2", "Description for s2"));

			Assert.IsFalse(p.ParseArgs(new string[] { "/s2", "value1", "/s2", "value2" }));
		}

		[TestMethod]
		public void Parameters_Usage()
		{
			Parameters p = new Parameters();

			p.Add(new StringParameter("s1", "s1 description"));
			p.Add(new StringParameter("s2", "s2 description", "default"));

			p.ParseArgs(new string[] { "/s1", "value1", "/s2", "value2" });
			Assert.AreEqual("Test Description version 1.1.1.1\nTest Copyright\r\n\r\n  Test [/s1 <string>] [/s2 <string>]\r\n\r\n  /s1        s1 description\r\n  /s2        s2 description (default: default)\r\n", p.Usage());
		}

		[TestMethod]
		public void Parameters_Usage_MissingRequired()
		{
			Parameters p = new Parameters();

			p.Add(new StringParameter("s1", "s1 description", true));

			try
			{
				p.ParseArgs(new string[] { });
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is ArgumentException);
			}
		}
	}
}
