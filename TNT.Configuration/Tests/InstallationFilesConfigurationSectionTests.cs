using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Configuration;

namespace Tests
{
	[TestClass]
	public class InstallationFilesConfigurationSectionTests
	{
		[TestMethod]
		public void Test1()
		{
			InstallationFilesConfigurationSection section = InstallationFilesConfigurationSection.Create();

			Assert.IsNotNull(section);

			for (int index = 0; index < 3; index++)
			{
				Assert.IsNotNull(section.Files[string.Format("appid{0}", index + 1)]);
				Assert.IsNotNull(section.Files[index]);
				Assert.AreEqual(string.Format("version{0}", index + 1), section.Files[index].Version);
				Assert.AreEqual(string.Format("url{0}", index + 1), section.Files[index].URL);
			}
		}
	}
}
