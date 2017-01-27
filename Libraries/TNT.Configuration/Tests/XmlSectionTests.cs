using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Configuration;

namespace Tests
{
	[TestClass]
	public class XmlSectionTests
	{
		[TestMethod]
		public void XmlSection_AppSettingsTest()
		{
			AppSettings settings = XmlSection<AppSettings>.Deserialize("AppSettingsTest");

			Assert.IsTrue(settings.Classes[0] is InheritedClass1);
			Assert.IsTrue(settings.Classes[1] is InheritedClass2);
		}

		[TestMethod]
		public void XmlSection_SettingsTest()
		{
			try
			{
				Settings settings = XmlSection<Settings>.Deserialize("SettingsSection");

				Assert.AreEqual(10, settings.IntValue);
				Assert.AreEqual("Value", settings.StringValue);
				List<int> intList = new List<int>(new int[] { 1, 2, 3, 4, 5, 6 });
				CollectionAssert.AreEqual(intList, settings.IntList);
				string[] array = { "first", "second", "third" };
				CollectionAssert.AreEqual(array, settings.StringArray);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void XmlSection_ExtendedSettingsTest()
		{
			try
			{
				Settings settings = XmlSection<Settings>.Deserialize("ExtendedSettingsSection");

				settings.IntValue = 345;

				settings = XmlSection<Settings>.Deserialize("ExtendedSettingsSection");
				ExtendedSettings exSettings = settings as ExtendedSettings;

				Assert.IsNotNull(exSettings);
				Assert.AreEqual(10, exSettings.IntValue);
				Assert.AreEqual("Value", exSettings.StringValue);
				List<int> intList = new List<int>(new int[] { 1, 2, 3, 4, 5, 6 });
				CollectionAssert.AreEqual(intList, exSettings.IntList);
				string[] array = { "first", "second", "third" };
				CollectionAssert.AreEqual(array, exSettings.StringArray);
				Assert.AreEqual("The new property", exSettings.NewProperty);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void XmlSection_MissingSectionTest()
		{
			try
			{
				XmlSection<Settings>.Deserialize("bogussection");
			}
			catch (ConfigurationMissingSectionException ex)
			{
				Assert.AreEqual("The section, bogussection, could not be found within the configuration file", ex.Message);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void XmlSection_SettingsListTest()
		{
			try
			{
				List<Settings> setting = XmlSection<List<Settings>>.Deserialize("SettingsList");

				Assert.IsNotNull(setting);
				Assert.AreEqual(3, setting.Count);
				Assert.IsTrue(setting[0] is Settings);
				Assert.IsTrue(setting[1] is ExtendedSettings);
				Assert.AreEqual(1, setting[0].IntValue);
				Assert.AreEqual(2, setting[1].IntValue);
				Assert.AreEqual("The new property", (setting[1] as ExtendedSettings).NewProperty);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void XmlSection_InvalidReferenceTest()
		{
			try
			{
				Settings settings = XmlSection<Settings>.Deserialize("InvalidReference", false);
				Assert.Fail("Failed to throw ConfigurationErrorsException");
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Failed to load assembly", ex.Message);
				Assert.IsTrue(ex is ConfigurationErrorsException);
				Assert.IsTrue(ex.InnerException is FileNotFoundException);
			}
		}

		[TestMethod]
		public void XmlSection_NoReferenceTest()
		{
			try
			{
				XmlSection<Settings>.Deserialize("NoReference", false);
				Assert.Fail("Failed to throw InvalidOperationException");
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is ConfigurationErrorsException);
			}
		}

		[TestMethod]
		public void XmlSection_InvalidBaseTypeTest()
		{
			try
			{
				XmlSection<Settings>.Deserialize("InvalidBaseType", false);
				Assert.Fail("Failed to throw InvalidOperationException");
			}
			catch (Exception ex)
			{
				Assert.IsTrue(ex is ConfigurationErrorsException);
			}
		}

		[TestMethod]
		public void XmlSection_DefaultSettings()
		{
			Settings settings = XmlSection<Settings>.Deserialize("bogus_section");

			Assert.IsNotNull(settings);
			Assert.AreEqual(999, settings.IntValue);
			Assert.AreEqual("The String Value", settings.StringValue);
			Assert.IsNull(settings.IntList);
			Assert.IsNotNull(settings.StringArray);
			Assert.AreEqual(3, settings.StringArray.Length);
			CollectionAssert.AreEqual(new string[] { "one", "two", "three" }, settings.StringArray);
		}
	}

	public class Settings
	{
		public int IntValue { get; set; }
		public string StringValue { get; set; }
		public List<int> IntList { get; set; }
		public string[] StringArray { get; set; }

		
		public Settings()
		{
			IntValue = 999;
			StringValue = "The String Value";
			StringArray = new string[] { "one", "two", "three" };
		}
	}

	public class ExtendedSettings : Settings
	{
		public string NewProperty { get; set; }
	}
}