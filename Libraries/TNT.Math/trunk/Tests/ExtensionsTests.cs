using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNT.Math.Tests
{
	[TestClass]
	public class ExtensionsTests
	{
		[TestMethod]
		public void Extensions_RoundUpToNearest_int_20()
		{
			var testCases = new List<TestCase<int>>(new TestCase<int>[]
			{
				new TestCase<int>(-1,0),
				new TestCase<int>(0,0),
				new TestCase<int>(1,20),
				new TestCase<int>(10,20),
				new TestCase<int>(20,20),
				new TestCase<int>(21,40)
			});

			var baseValue = 20;

			testCases.ForEach(t =>
			{
				Assert.AreEqual(t.expectedValue, t.testValue.RoundUpToNearest(baseValue), t.ToString());
			});
		}

		[TestMethod]
		public void Extensions_RoundUpToNearest_int_7()
		{
			var testCases = new List<TestCase<int>>(new TestCase<int>[]
			{
				new TestCase<int>(-1,0),
				new TestCase<int>(0,0),
				new TestCase<int>(1,7),
				new TestCase<int>(10,14),
				new TestCase<int>(20,21),
				new TestCase<int>(21,21),
				new TestCase<int>(22,28)
			});

			var baseValue = 7;

			testCases.ForEach(t =>
			{
				Assert.AreEqual(t.expectedValue, t.testValue.RoundUpToNearest(baseValue), t.ToString());
			});
		}

		[TestMethod]
		public void Extensions_RoundUpToNearest_double_7()
		{
			var testCases = new List<TestCase<double>>(new TestCase<double>[]
			{
				new TestCase<double>(-1.0,0.0),
				new TestCase<double>(0.0,0.0),
				new TestCase<double>(1.0,7.0),
				new TestCase<double>(10.0,14.0),
				new TestCase<double>(20.0,21.0),
				new TestCase<double>(21.0,21.0),
				new TestCase<double>(22.0,28.0)
			});

			var baseValue = 7.0;

			testCases.ForEach(t =>
			{
				Assert.AreEqual(t.expectedValue, t.testValue.RoundUpToNearest(baseValue), t.ToString());
			});
		}
	}

	class TestCase<T>
	{
		public T testValue { get; set; }
		public T expectedValue { get; set; }

		public TestCase(T testValue, T expectedValue)
		{
			this.testValue = testValue;
			this.expectedValue = expectedValue;
		}

		public override string ToString()
		{
			return $"({testValue}, {expectedValue})";
		}
	}
}
