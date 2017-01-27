using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Configuration;

namespace Tests
{
	[TestClass]
	public class PartsConfigurationSectionTests
	{
		[TestMethod]
		public void Test1()
		{
			PartsConfigurationSection partsSection = PartsConfigurationSection.Create<PartsConfigurationSection>("MyCustomSection");

			Assert.IsNotNull(partsSection);

			Assert.IsNotNull(partsSection.Parts["TE075"]);
			Assert.IsNotNull(partsSection.Parts["CE721QT"]);
			Assert.IsNotNull(partsSection.Parts["CEP70PT"]);
			Assert.IsNull(partsSection.Parts["Bogus"]);

			partsSection = PartsConfigurationSection.Create();

			Assert.IsNotNull(partsSection);

			Assert.IsNotNull(partsSection.Parts["TE075"]);
			Assert.IsNotNull(partsSection.Parts["CE721QT"]);
			Assert.IsNotNull(partsSection.Parts["CEP70PT"]);
			Assert.IsNull(partsSection.Parts["Bogus"]);

			for (int index = 0; index < partsSection.Parts.Count; index++)
			{
				Assert.AreEqual(index + 1, partsSection.Parts[index].Quantity);
			}
		}

		[TestMethod]
		public void MissingSectionExceptionTest()
		{
			PartsConfigurationSection partsSection = null;

			try
			{
				partsSection = PartsConfigurationSection.Create<PartsConfigurationSection>("missingsection");
			}
			catch (ConfigurationMissingSectionException mse)
			{
				Assert.AreEqual("The section, missingsection, could not be found within the configuration file", mse.Message);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.ToString());
			}
		}
	}
}
