using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities.Console;

namespace Test.Console
{
	public enum e { One, Two, Three };

	[TestClass]
	public class EnumParameter_Tests
	{
		Parameters p = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			p.Add(new EnumParameter<e>("e1", "Not required")
			{
				GetEnumDescription = t =>
					{
						return t.ToString();
					}
			});
			p.Add(new EnumParameter<e>("e2", "Required", true));
			p.Add(new EnumParameter<e>("e3", "Default", e.Three));
		}

		[TestMethod]
		public void EnumParamter_GetValue()
		{
			p.ParseArgs(new string[] { "/e2", "Two" });

			EnumParameter<e> ep = p["e2"] as EnumParameter<e>;
			e value = ep.Value;

			Assert.AreEqual(e.Two, value);
		}

		[TestMethod]
		public void EnumParameter_Syntax()
		{
			Assert.AreEqual("[/e1 <e>]", p["e1"].Syntax());
			Assert.AreEqual("/e2 <e>", p["e2"].Syntax());
			Assert.AreEqual("[/e3 <e>]", p["e3"].Syntax());
		}

		[TestMethod]
		public void EnumParameter_SetValue()
		{
			p.ParseArgs(new string[] { "/e2", e.Two.ToString() });

			Assert.AreEqual(null, p["e1"].Value);
			Assert.AreEqual(e.Two, p["e2"].Value);
			Assert.AreEqual(e.Three, p["e3"].Value);
		}

		[TestMethod]
		public void EnumParameter_SetValue_Exception()
		{
			try
			{
				p.ParseArgs(new string[] { "/e2", "bogus value" });
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is ArgumentException);
			}
		}

		[TestMethod]
		public void EnumParameter_Usage()
		{
			string expected = @"  /e1        Not required

             e values:

               One - One
               Two - Two
               Three - Three
";

			Assert.AreEqual(expected, p["e1"].Usage());

			expected = @"  /e2        Required

             e values:

               One
               Two
               Three
";
			Assert.AreEqual(expected, p["e2"].Usage());

			expected = @"  /e3        Default (default: Three)

             e values:

               One
               Two
               Three
";
			Assert.AreEqual(expected, p["e3"].Usage());
		}
	}
}
