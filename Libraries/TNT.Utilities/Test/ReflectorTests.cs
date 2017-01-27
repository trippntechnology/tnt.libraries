using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNT.Utilities;
using TNT.Utilities.CustomAttributes;

namespace Test
{
	[TestClass]
	public class ReflectorTests
	{
		[TestMethod]
		public void Test1()
		{
			TestClass tc = new TestClass() { EnumProperty = Enum1.Value2, IntProperty = 41, NoAttributes = "None", StringProperty = "Forty-one" };
			Reflector<TestClass> reflector = new Reflector<TestClass>(tc);

			Assert.AreEqual(Enum1.Value2, tc.EnumProperty);
			Assert.AreEqual(41, tc.IntProperty);
			Assert.AreEqual("None", tc.NoAttributes);
			Assert.AreEqual("Forty-one", tc.StringProperty);

			var props = (from p in reflector.Properties orderby p.Category, p.DisplayName select p).ToList();

			Assert.AreEqual(4, props.Count());
			Assert.IsTrue(string.IsNullOrEmpty(props[0].Category));
			Assert.AreEqual("Cat1", props[1].Category);
			Assert.AreEqual("Cat1", props[2].Category);
			Assert.AreEqual("Cat2", props[3].Category);

			Assert.AreEqual("NoAttributes", props[0].DisplayName);
			Assert.AreEqual("Int Property", props[1].DisplayName);
			Assert.AreEqual("String Property", props[2].DisplayName);
			Assert.AreEqual("Enumeration Property", props[3].DisplayName);

			Assert.AreEqual("None", props[0].Value);
			Assert.AreEqual(41, props[1].Value);
			Assert.AreEqual("Forty-one", props[2].Value);
			Assert.AreEqual(Enum1.Value2, props[3].Value);

			props = (from p in reflector.Properties orderby p.Priority select p).ToList();

			Assert.AreEqual("Cat2", props[0].Category);
			Assert.AreEqual("Cat1", props[1].Category);
			Assert.IsTrue(string.IsNullOrEmpty(props[2].Category));
			Assert.AreEqual("Cat1", props[3].Category);

			List<string> orderedCats = reflector.GetCategoriesByPriority();

			Assert.AreEqual(3, orderedCats.Count());
			Assert.AreEqual("Cat2", orderedCats[0]);
			Assert.AreEqual("Cat1", orderedCats[1]);
			Assert.AreEqual("", orderedCats[2]);
		}
	}

	public enum Enum1
	{
		Value1,
		Value2,
		Value3
	}

	public class TestClass
	{
		[PropertyReflectorAttribute(Priority = 11)]
		[System.ComponentModel.Category("Cat1")]
		[DisplayName("Int Property")]
		public int IntProperty { get; set; }

		[PropertyReflectorAttribute()]
		[System.ComponentModel.Category("Cat2")]
		[DisplayName("Enumeration Property")]
		public Enum1 EnumProperty { get; set; }

		[PropertyReflectorAttribute(Priority = 2)]
		public string NoAttributes { get; set; }

		[PropertyReflectorAttribute(Priority = 1)]
		[System.ComponentModel.Category("Cat1")]
		[DisplayName("String Property")]
		public string StringProperty { get; set; }

		public int IntNoReflect { get; set; }

		public string StringNoReflect { get; set; }
	}
}
