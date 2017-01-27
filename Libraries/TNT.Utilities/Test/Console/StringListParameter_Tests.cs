using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities.Console;

namespace Test.Console
{
	[TestClass]
	public class StringListParameter_Tests
	{
		Parameters m_Params = new Parameters();

		[TestInitialize]
		public void Initialize()
		{
			m_Params.Add(new StringListParameter("s1", "Not Required"));
			m_Params.Add(new StringListParameter("s2", "Required", true));
		}

		[TestMethod]
		public void StringParameter_Syntax()
		{
			Assert.AreEqual("[/s1 <string[]>]", m_Params["s1"].Syntax());
			Assert.AreEqual("/s2 <string[]>", m_Params["s2"].Syntax());
		}

		[TestMethod]
		public void StringParameter_ParseArgs()
		{
			Assert.IsFalse(m_Params.ParseArgs(new string[] { "/s2", "" }));
			Assert.IsTrue(m_Params.ParseArgs(new string[] { "/s2", "value" }));
			CollectionAssert.AreEqual(new string[] { "value" }, (m_Params["s2"] as StringListParameter).Value);

			m_Params.RemoveAt(0);
			m_Params.Add(new StringListParameter("s2", "Required", true));

			Assert.IsTrue(m_Params.ParseArgs(new string[] { "/s2", "value one, value two, value three" }));
			CollectionAssert.AreEqual(new string[] { "value one", "value two", "value three" }, (m_Params["s2"] as StringListParameter).Value);
		}
	}
}
