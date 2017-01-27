using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class CERTParameter_Tests
	{
		Parameters m_Params = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			m_Params.Add(new CERTParameter("c1", "Not required"));
			m_Params.Add(new CERTParameter("c2", "Required", true));
		}

		[TestMethod]
		public void CERTParameter_Syntax()
		{
			Assert.AreEqual("[/c1 <cert>]", m_Params["c1"].Syntax());
			Assert.AreEqual("/c2 <cert>", m_Params["c2"].Syntax());
		}
	}
}
