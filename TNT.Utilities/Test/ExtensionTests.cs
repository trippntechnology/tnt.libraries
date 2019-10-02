using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities;

namespace Test
{
	[TestClass]
	public class ExtensionTests
	{
		[TestMethod]
		public void Extensions_Let()
		{
			var result = (new FooExtension() { Value = 7 }).Let(it =>
			{
				it.Value = 10;
				return 20;
			});

			Assert.AreEqual(20, result);
		}

		[TestMethod]
		public void Extensions_Also()
		{
			var result = (new FooExtension() { Value = 7 }).Also(it =>
		 {
			 it.Value = 10;
		 });

			Assert.AreEqual(10, result?.Value);
		}
	}

	class FooExtension
	{
		public int Value { get; set; }
	}
}
