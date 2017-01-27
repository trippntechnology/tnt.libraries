using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Configuration;

namespace Tests
{
	[TestClass]
	public class PaletteNodeConfigurationSectionTests
	{
		[TestMethod]
		public void Test1()
		{
			PaletteNodeConfigurationSection pnSection = PaletteNodeConfigurationSection.Create();

			Assert.IsNotNull(pnSection);
			Assert.IsNotNull(pnSection.PaletteFile);

			Assert.AreEqual("File.palette", pnSection.PaletteFile.Value);
		}
	}
}
