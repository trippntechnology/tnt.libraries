using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using TNT.Utilities;

namespace Test
{
	[TestClass]
	public class ApplicationRegistryTests
	{
		ApplicationRegistry m_AppReg = null;

		[TestInitialize]
		public void Setup()
		{
			string coName = "Tripp'n Technology";
			string tntKey = string.Concat(@"SOFTWARE\", coName);
			string appName = "TNT.Utilities.Tests";

			// Check if the appName key already exists
			RegistryKey tntRegKey = Registry.CurrentUser.CreateSubKey(tntKey);

			try
			{
				tntRegKey.DeleteSubKeyTree(appName);
			}
			catch { }

			m_AppReg = new ApplicationRegistry(Registry.CurrentUser, coName, appName);

			Assert.IsNotNull(m_AppReg);
		}

		[TestMethod]
		public void ReadWriteBoolean()
		{
			try
			{
				string keyName = "BooleanTest";
				string subKeyName = "SubKey";

				Assert.IsTrue(m_AppReg.ReadBoolean(keyName, true));

				m_AppReg.WriteBoolean(keyName, true);
				Assert.IsTrue(m_AppReg.ReadBoolean(keyName, false));

				m_AppReg.WriteBoolean(keyName, false);
				Assert.IsFalse(m_AppReg.ReadBoolean(keyName, true));

				Assert.IsTrue(m_AppReg.ReadBoolean(keyName, keyName, true));

				m_AppReg.WriteBoolean(subKeyName, keyName, true);
				Assert.IsTrue(m_AppReg.ReadBoolean(subKeyName, keyName, false));

				m_AppReg.WriteBoolean(subKeyName, keyName, false);
				Assert.IsFalse(m_AppReg.ReadBoolean(subKeyName, keyName, true));

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void ReadWriteInt()
		{
			try
			{
				string keyName = "IntegerTest";
				string subKeyName = "SubKey";

				Assert.AreEqual(12, m_AppReg.ReadInteger(keyName, 12));

				m_AppReg.WriteInteger(keyName, 25);
				Assert.AreEqual(25, m_AppReg.ReadInteger(keyName, 12));

				Assert.AreEqual(12, m_AppReg.ReadInteger(subKeyName, keyName, 12));

				m_AppReg.WriteInteger(subKeyName, keyName, 25);
				Assert.AreEqual(25, m_AppReg.ReadInteger(subKeyName, keyName, 12));
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void ReadWriteString()
		{
			try
			{
				string keyName = "StringTest";
				string subKeyName = "SubKey";
				string value = "Test String";
				string defValue = "Default value";

				Assert.AreEqual(defValue, m_AppReg.ReadString(keyName, defValue));

				m_AppReg.WriteString(keyName, value);
				Assert.AreEqual(value, m_AppReg.ReadString(keyName, defValue));

				Assert.AreEqual(defValue, m_AppReg.ReadString(subKeyName, keyName, defValue));

				m_AppReg.WriteString(subKeyName, keyName, value);
				Assert.AreEqual(value, m_AppReg.ReadString(subKeyName, keyName, defValue));
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void ReadWriteStringList()
		{
			try
			{
				string keyName = "StringListTest";

				Assert.AreEqual(m_AppReg.ReadStringList(keyName).Count, 0);

				List<string> expected = new List<string>(new string[] { "one", "two", "three", "four", "five" });

				m_AppReg.WriteStringList(keyName, expected);

				CollectionAssert.AreEqual(expected, m_AppReg.ReadStringList(keyName));
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void ReadWriteIntList()
		{
			try
			{
				string keyName = "IntegerListTest";
				Assert.AreEqual(m_AppReg.ReadList<int>(keyName).Count, 0);

				List<int> expected = new List<int>(new int[] { -2, -1, 0, 1, 2 });

				m_AppReg.WriteList<int>(keyName, expected);

				CollectionAssert.AreEqual(expected, m_AppReg.ReadList<int>(keyName));
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void ReadWriteToolStripItems()
		{
			try
			{
				string keyName = "ToolStripItemsTest";
				ToolStrip ts = new ToolStrip();
				ToolStripItemCollection tsic = new ToolStripItemCollection(ts, new ToolStripItem[0]);
				m_AppReg.ReadToolStripItems(keyName, tsic);

				Assert.IsTrue(tsic == null || tsic.Count == 0);

				tsic = new ToolStripItemCollection(ts, new ToolStripItem[] { new ToolStripMenuItem("One"), new ToolStripMenuItem("Two"), new ToolStripMenuItem("Three") });

				m_AppReg.WriteToolStripItems(keyName, tsic);

				ToolStripItemCollection newTSIC = new ToolStripItemCollection(ts, new ToolStripItem[0]);

				m_AppReg.ReadToolStripItems(keyName, newTSIC);

				Assert.AreEqual(tsic.Count, newTSIC.Count);

				for (int index = 0; index < tsic.Count; index++)
				{
					Assert.AreEqual(tsic[index].Text, newTSIC[index].Text);
				}
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void LoadSaveFormState()
		{
			try
			{
				Form defForm = new Form();
				defForm.Name = "TestForm";
				Form form = new Form();
				form.Name = "TestForm";

				m_AppReg.LoadFormState(form);

				Assert.AreEqual(defForm.Width, form.Width);
				Assert.AreEqual(defForm.Height, form.Height);
				Assert.AreEqual(defForm.Top, form.Top);
				Assert.AreEqual(defForm.Left, form.Left);
				Assert.AreEqual(defForm.WindowState, form.WindowState);

				defForm.Width = 777;
				defForm.Height = 983;
				defForm.Top = 34;
				defForm.Left = 87;
				defForm.WindowState = FormWindowState.Normal;

				m_AppReg.SaveFormState(defForm);

				m_AppReg.LoadFormState(form);

				Assert.AreEqual(defForm.Width, form.Width);
				Assert.AreEqual(defForm.Height, form.Height);
				Assert.AreEqual(defForm.Top, form.Top);
				Assert.AreEqual(defForm.Left, form.Left);
				Assert.AreEqual(defForm.WindowState, form.WindowState);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void ReadWriteBytes()
		{
			try
			{
				string keyName = "BinaryTest";

				Assert.IsNull(m_AppReg.ReadBytes(keyName));

				byte[] bytes = ASCIIEncoding.ASCII.GetBytes(keyName);
				m_AppReg.WriteBytes(keyName, bytes);

				byte[] readBytes = m_AppReg.ReadBytes(keyName);

				string readString = Encoding.UTF8.GetString(readBytes);

				Assert.AreEqual(keyName, readString);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void ReadWriteObject()
		{
			try
			{
				string keyName = "ObjectTest";

				TestObject to = new TestObject()
				{
					intValue = 10,
					stringValue = "ten"
				};

				m_AppReg.WriteObject(keyName, to);

				TestObject newTO = m_AppReg.ReadObject<TestObject>(keyName);

				Assert.AreEqual(to, newTO);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}

	[Serializable]
	public class TestObject
	{
		public int intValue { get; set; }
		public string stringValue { get; set; }

		public override bool Equals(object obj)
		{
			TestObject to = obj as TestObject;

			if (to == null)
			{
				return false;
			}

			return intValue == to.intValue && stringValue == to.stringValue;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
