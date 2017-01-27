using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using TNT.Utilities;

namespace Test
{
	[TestClass]
	public class TokenTests
	{
		[TestMethod]
		public void Token_Create_Size()
		{
			string token = Token.Create(8);
			Assert.AreEqual(8, token.Length);

			token = Token.Create(15);
			Assert.AreEqual(15, token.Length);
		}

		[TestMethod]
		public void Token_Create_Sections()
		{
			string token = Token.Create(1, 4);
			Assert.IsTrue(Regex.IsMatch(token, "^[a-zA-Z0-9]{4}$"));

			token = Token.Create(2, 4);
			Assert.IsTrue(Regex.IsMatch(token, "^[a-zA-Z0-9]{4}(-[a-zA-Z0-9]{4}){1}$"));

			token = Token.Create(3, 4);
			Assert.IsTrue(Regex.IsMatch(token, "^[a-zA-Z0-9]{4}(-[a-zA-Z0-9]{4}){2}$"));
		}

		[TestMethod]
		public void Token_Create_Length_0()
		{
			string token = Token.Create(0);
			Assert.IsTrue(string.IsNullOrEmpty(token));
		}

		[TestMethod]
		public void Token_Create_Section_Count_0()
		{
			string token = Token.Create(0, 4);
			Assert.IsTrue(string.IsNullOrEmpty(token));
		}

		[TestMethod]
		public void Token_Create_Not_Equal_Sections()
		{
			string token = Token.Create(2, 4);
			string[] sections = token.Split('-');

			Assert.AreNotEqual(sections[0], sections[1]);
		}

		[TestMethod]
		public void Token_Using_Custom_Chars()
		{
			char[] customCharacters = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

			string token = Token.Create(5, customCharacters);
			int output;

			Assert.IsTrue(int.TryParse(token, out output));
		}
	}
}
