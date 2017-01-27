using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities;

namespace Test
{
	using System;
	using System.Management;
	using ManagementObjects = List<Dictionary<string, string>>;

	[TestClass]
	public class RegistrationTests
	{
		[TestMethod]
		public void GenerateSHA1HashTest()
		{
			Assert.AreEqual("OPAPhzjiQdrqbzf29VroQU17Ahk=", Registration.GenerateSHA1Hash("Lorem ipsum dolor sit amet"));
			Assert.AreEqual("wSvFipxMDuPkAVHAXTFTQOU16eY=", Registration.GenerateSHA1Hash("Lorem ipsum dolor sit amet, erexit per te in deinde"));
			Assert.AreEqual("43wA7VnwV/LeKHX8ZJBSPXaheRc=", Registration.GenerateSHA1Hash("Lorem ipsum dolor sit amet, natura omnes Hellenicus dixisset alia gaudio hoc ait Cumque persequatur sic nec appellarer in fuerat"));
		}

		[TestMethod]
		public void ValidateKeyTest()
		{
			Assert.IsTrue(Registration.ValidateHash("Lorem ipsum dolor sit amet", "OPAPhzjiQdrqbzf29VroQU17Ahk="));
			Assert.IsTrue(Registration.ValidateHash("Lorem ipsum dolor sit amet, erexit per te in deinde", "wSvFipxMDuPkAVHAXTFTQOU16eY="));
			Assert.IsTrue(Registration.ValidateHash("Lorem ipsum dolor sit amet, natura omnes Hellenicus dixisset alia gaudio hoc ait Cumque persequatur sic nec appellarer in fuerat", "43wA7VnwV/LeKHX8ZJBSPXaheRc="));

			Assert.IsFalse(Registration.ValidateHash("Bogus seed", "OPAPhzjiQdrqbzf29VroQU17Ahk="));
		}

		[TestMethod]
		public void GetVolumeSerialNumberTest()
		{
			Assert.AreEqual("0AD6DF7A", Registration.GetVolumeSerialNumber());
		}

		[TestMethod]
		public void GetManagementObjectsTest()
		{
			try
			{
				Registration.GetManagementObjects("select from win32_logicaldisk");
			}
			catch (ManagementException me)
			{
				Assert.AreEqual("Invalid query", me.Message.Trim());
			}
			catch (Exception)
			{
				Assert.Fail();
			}

			ManagementObjects objs = Registration.GetManagementObjects("select * from win32_logicaldisk");

			Assert.IsNotNull(objs);
			Assert.IsTrue(objs.Count > 0);

			Assert.AreEqual("Local Fixed Disk", objs[0]["Description"]);
		}

		[TestMethod]
		public void GenerateKeyTest()
		{
			string key = string.Empty;

			Assert.IsTrue(string.IsNullOrEmpty(Registration.GenerateKey(0, 0)));

			Assert.IsTrue(Regex.IsMatch(Registration.GenerateKey(1, 0), "^[A-Z]$"));
			Assert.IsTrue(Regex.IsMatch(Registration.GenerateKey(2, 0), "^[A-Z]{2}$"));
			Assert.IsTrue(Regex.IsMatch(Registration.GenerateKey(2, 1), "^[A-Z]-[A-Z]$"));
			Assert.IsTrue(Regex.IsMatch(Registration.GenerateKey(4, 1), "^[A-Z](-[A-Z]){3}$"));
			Assert.IsTrue(Regex.IsMatch(Registration.GenerateKey(4, 0), "^[A-Z]{4}$"));
			Assert.IsTrue(Regex.IsMatch(Registration.GenerateKey(4, 3), "^[A-Z]{4}$"));

			Assert.IsTrue(Regex.IsMatch(Registration.GenerateKey(20, 4), "^[A-Z]{4}(-[A-Z]{4}){4}$"));

			List<string> keys = new List<string>();

			for (int index = 0; index < 10000; index++)
			{
				key = Registration.GenerateKey(20, 4);

				if (keys.Contains(key))
				{
					Assert.Fail("Failed at index {0} with {1}", index, key);
				}

				keys.Add(key);
			}
		}
	}
}
