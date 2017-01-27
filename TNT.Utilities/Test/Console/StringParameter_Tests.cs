using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class StringParameter_Tests
	{
		Parameters m_Params = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			m_Params.Add(new StringParameter("s1", "Not required"));
			m_Params.Add(new StringParameter("s2", "Required", true));
			m_Params.Add(new StringParameter("s3", "Default value", "Default"));
		}

		[TestMethod]
		public void StringParameter_Syntax()
		{
			Assert.AreEqual("[/s1 <string>]", m_Params["s1"].Syntax());
			Assert.AreEqual("/s2 <string>", m_Params["s2"].Syntax());
			Assert.AreEqual("[/s3 <string>]", m_Params["s3"].Syntax());
		}

		[TestMethod]
		public void StringParameter_ParseArgs()
		{
			Assert.IsFalse(m_Params.ParseArgs(new string[] { "/s2", "" }));
			Assert.IsTrue(m_Params.ParseArgs(new string[] { "/s2", "value" }));
			Assert.AreEqual("value", (m_Params["s2"] as StringParameter).Value);
		}
	}
}
