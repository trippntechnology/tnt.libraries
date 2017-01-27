using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml.Serialization;
using TNT.Configuration;

namespace Tests
{
	[TestClass]
	public class ElementsWithAttributesTest
	{
		[TestMethod]
		public void ElementsWithAttributes_Test1()
		{
			ElementsWithAttributesSettings settings = XmlSection<ElementsWithAttributesSettings>.Deserialize("ElementsWithAttributesTest");

			Assert.AreEqual("One", settings.AClasses[0].Value);
			Assert.AreEqual("Two", settings.AClasses[1].Value);
		}
	}

	public class ElementsWithAttributesSettings
	{
		public List<ClassA> AClasses { get; set; }
	}

	public class ClassA
	{
		[XmlAttribute]
		public string Value { get; set; }
	}

	public class ClassB : ClassA
	{

	}
}
