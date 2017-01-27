using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Configuration;

namespace Tests
{
	[TestClass]
	public class CADSettingsConfigurationSectionTest
	{
		[TestMethod]
		public void Test1()
		{
			CADSettingsConfigurationSection section = CADSettingsConfigurationSection.Create();

			Color color = Color.FromArgb(Int32.Parse(section.GridColor, System.Globalization.NumberStyles.HexNumber));

			Assert.AreEqual(Color.FromArgb(0, 255, 255, 255), color);

			Assert.AreEqual(200, section.HeightInFeet);
			Assert.AreNotEqual(300, section.HeightInFeet);

			Assert.IsTrue(section.ShowLegend);

			string culinaryPSI = section.CulinaryPSI;
			Assert.AreEqual("> 60 PSI", culinaryPSI);

			Assert.IsNotNull(section.Parts["TE075"]);
			Assert.IsNotNull(section.Parts["CE721QT"]);
			Assert.IsNotNull(section.Parts["CEP70PT"]);
			Assert.IsNull(section.Parts["Bogus"]);
		}
	}
}
