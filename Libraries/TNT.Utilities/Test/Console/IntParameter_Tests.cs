using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class IntParameter_Tests
	{
		Parameters p = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			p.Add(new IntParameter("i1", "Not required"));
			p.Add(new IntParameter("i2", "Required", true) { Validate = ValidateInt });
			p.Add(new IntParameter("i3", "Default", 17));
		}

		[TestMethod]
		public void IntParameter_Syntax()
		{
			Assert.AreEqual("[/i1 <Int>]", p["i1"].Syntax());
			Assert.AreEqual("/i2 <Int>", p["i2"].Syntax());
			Assert.AreEqual("[/i3 <Int>]", p["i3"].Syntax());
		}

		[TestMethod]
		public void IntParameter_SetValue()
		{
			p.ParseArgs(new string[] { "/i2", "19" });
			Assert.IsNull(p["i1"].Value);
			Assert.AreEqual(19, p["i2"].Value);
			Assert.AreEqual(17, (p["i3"] as IntParameter).Value);
		}

		[TestMethod]
		public void IntParameter_SetValue_Exception()
		{
			Assert.IsFalse(p.ParseArgs(new string[] { "/i2", "A" }));
		}

		[TestMethod]
		public void IntParameter_WrapLines()
		{
			Parameter intParam = new IntParameter("i", "Lorem ipsum dolor sit amet, stranguillionem ubi confudit huc est cum, singulas cotidie hoc puella eius. Christe in lucem exempli paupers coniunx, se ad quia ad te ad suis. Ratione congregaverim eum ego dum autem Apolloni ex sic nec 'pectore zaetam at ipsum dolore. Allocutus ait in rei completo litus ostendam Apollonio vidit Dionysiadi Apollonius mihi. Triton testandum ecce prima luctatur in modo compungi mulierem volutpat cum magna anima Apollonium illis codicello lenonem in lucem.");

			Assert.AreEqual("  /i         Lorem ipsum dolor sit amet, stranguillionem ubi confudit huc est cum,\n             singulas cotidie hoc puella eius. Christe in lucem exempli paupers\n             coniunx, se ad quia ad te ad suis. Ratione congregaverim eum ego dum\n             autem Apolloni ex sic nec 'pectore zaetam at ipsum dolore. Allocutus\n             ait in rei completo litus ostendam Apollonio vidit Dionysiadi\n             Apollonius mihi. Triton testandum ecce prima luctatur in modo\n             compungi mulierem volutpat cum magna anima Apollonium illis codicello\n             lenonem in lucem.", intParam.Usage());
		}

		protected void ValidateInt(object obj)
		{
			Assert.AreEqual(19, obj);
		}
	}
}
