using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using TNT.Utilities;

namespace Test
{
	[TestClass]
	public class UtilitiesTests
	{
		[TestMethod]
		public void Utilities_GetAssemblyAttribute_Test()
		{
			Assembly asm = Assembly.LoadFrom(@"test.dll");

			Assert.IsNotNull(asm);

			Attribute attr = Utilities.GetAssemblyAttribute<AssemblyCompanyAttribute>(asm);
			Assert.AreEqual("Test Company", (attr as AssemblyCompanyAttribute).Company);

			attr = Utilities.GetAssemblyAttribute<AssemblyConfigurationAttribute>(asm);
			Assert.AreEqual("Test Configuration", (attr as AssemblyConfigurationAttribute).Configuration);

			attr = Utilities.GetAssemblyAttribute<AssemblyCopyrightAttribute>(asm);
			Assert.AreEqual("Test Copyright", (attr as AssemblyCopyrightAttribute).Copyright);

			attr = Utilities.GetAssemblyAttribute<AssemblyDescriptionAttribute>(asm);
			Assert.AreEqual("Test Description", (attr as AssemblyDescriptionAttribute).Description);

			attr = Utilities.GetAssemblyAttribute<AssemblyFileVersionAttribute>(asm);
			Assert.AreEqual("2.2.2.2", (attr as AssemblyFileVersionAttribute).Version);

			//attr = Utilities.GetAssemblyAttribute<AssemblyInformationalVersionAttribute>(asm);
			//Assert.AreEqual("3.3.3.3", (attr as AssemblyInformationalVersionAttribute).InformationalVersion);

			attr = Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(asm);
			Assert.AreEqual("Test Title", (attr as AssemblyTitleAttribute).Title);

			//attr = Utilities.GetAssemblyAttribute<AssemblyVersionAttribute>(asm);
			//Assert.AreEqual("3.3.3.3", (attr as AssemblyVersionAttribute).Version);

			attr = Utilities.GetAssemblyAttribute<AssemblyProductAttribute>(asm);
			Assert.AreEqual("Test Product", (attr as AssemblyProductAttribute).Product);

			attr = Utilities.GetAssemblyAttribute<GuidAttribute>(asm);
			Assert.AreEqual("eae4b166-b50f-4f09-aa59-23d18cfb4c5a", (attr as GuidAttribute).Value);
		}

		[TestMethod]
		public void Utilities_SerializeDeserialize_Tests()
		{
			RegistrationKey regKey = new RegistrationKey() { Authorization = "AuthorizationKey", License = "LicenseKey" };
			string fileName = Path.GetTempFileName();

			Utilities.SerializeToFile<RegistrationKey>(regKey, fileName);
			RegistrationKey regKey1 = Utilities.DeserializeFromFile<RegistrationKey>(fileName);

			Assert.AreEqual(regKey, regKey1);

			try
			{
				List<string> strings = Utilities.DeserializeFromFile<List<string>>(fileName);
				Assert.Fail("InvalidOperationException expected");
			}
			catch (InvalidOperationException)
			{
			}

			File.Delete(fileName);

			try
			{
				regKey1 = Utilities.DeserializeFromFile<RegistrationKey>(fileName);
				Assert.Fail("FileNotFoundException expected");
			}
			catch (FileNotFoundException)
			{
			}
		}

		[TestMethod]
		public void Utilities_GetTypes_Test()
		{
			string assemblyFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\TNT.Utilities.dll";

			var types = Utilities.GetTypes(assemblyFile, null);

			Assert.AreEqual(57, types.Length);

			types = Utilities.GetTypes(assemblyFile, t =>
			{
				return t.IsVisible;
			});

			Assert.AreEqual(36, types.Length);
		}
	}
}
