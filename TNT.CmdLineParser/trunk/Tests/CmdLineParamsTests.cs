using System;
using System.Collections.Generic;
using System.IO;
using TNT.CmdLineParser;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class CmdLineParamsTests
	{
		[TestFixtureSetUp]
		public void Setup()
		{
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
		}

		[Test]
		public void CmdLineArrayDefaultFlagIndicator()
		{
			string[] parms = { "/?", "/f", "File Name.txt", "/G", "/C", "10" };

			CmdLineParams clp = new CmdLineParams(parms);

			Assert.True(clp.HasFlagParameter("g"));
			Assert.True(clp.HasFlagParameter("G"));
			Assert.True(clp.HasFlagParameter("?"));
			Assert.False(clp.HasFlagParameter("f"));
			Assert.True(clp.HasValueParameter("F"));
			Assert.True(clp.HasValueParameter("f"));
			Assert.AreEqual("File Name.txt", clp["F"]);
			Assert.AreEqual("File Name.txt", clp["f"]);

			Assert.Throws<System.ArgumentException>(delegate { string value = clp["/q"]; });
		}

		[Test]
		public void CmdLineArrayCustomFlagIndicator()
		{
			string[] parms = { "-?", "-f", "File Name.txt", "-G", "-C", "10" };

			CmdLineParams clp = new CmdLineParams(parms, "-");

			Assert.AreEqual("-", clp.ParameterDelimiter);

			Assert.True(clp.HasFlagParameter("g"));
			Assert.True(clp.HasFlagParameter("G"));
			Assert.True(clp.HasFlagParameter("?"));
			Assert.False(clp.HasFlagParameter("f"));
			Assert.True(clp.HasValueParameter("F"));
			Assert.True(clp.HasValueParameter("f"));
			Assert.AreEqual("File Name.txt", clp["F"]);
			Assert.AreEqual("File Name.txt", clp["f"]);
		}

		[Test]
		public void ValidateParameters2()
		{
			string[] parms = { "/?", "/f", "File Name.txt", "/G", "/C", "10" };

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new ValueParameter("f", ""));
			clp.AddValidationParameter(new FlagParameter("G", ""));
			clp.AddValidationParameter(new ValueParameter("C", ""));
			clp.AddValidationParameter(new ValueParameter("optional", "", "value"));

			try
			{
				Assert.Throws<System.ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (System.ArgumentException ae)
			{
				Assert.AreEqual("Invalid parameter '?' specified.", ae.Message);
			}

			clp.AddValidationParameter(new FlagParameter("?", ""));

			Assert.IsTrue(clp.ValidateParameters());

			// Added required value parameter
			clp.AddValidationParameter(new ValueParameter("A", "required"));

			try
			{
				Assert.Throws<System.ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (System.ArgumentException ae)
			{
				Assert.AreEqual("Required parameter '/a' was not specified.", ae.Message);
			}
		}

		[Test]
		public void ValidateParameters3()
		{
			string[] parms = { "/?", "/G", "/C", "10" };

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new FlagParameter("?", ""));
			clp.AddValidationParameter(new ValueParameter("f", ""));
			clp.AddValidationParameter(new FlagParameter("G", ""));
			clp.AddValidationParameter(new ValueParameter("C", ""));
			clp.AddValidationParameter(new ValueParameter("optional", "", "value"));

			try
			{
				Assert.Throws<System.ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (System.ArgumentException ae)
			{
				Assert.AreEqual("Required parameter '/f' was not specified.", ae.Message);
			}
		}

		[Test]
		public void ValidateParameters4()
		{
			string[] parms = { "/?", "/f", "File Name.txt", "/G", "/C", "10" };

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new FlagParameter("?", ""));
			clp.AddValidationParameter(new FlagParameter("G", ""));
			clp.AddValidationParameter(new ValueParameter("C", ""));
			clp.AddValidationParameter(new ValueParameter("optional", "", false));

			try
			{
				Assert.Throws<System.ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (System.ArgumentException ae)
			{
				Assert.AreEqual("Invalid parameter 'f' specified.", ae.Message);
			}

			clp.AddValidationParameter(new ValueParameter("f", ""));

			Assert.IsTrue(clp.ValidateParameters());

			// Added required value parameter
			clp.AddValidationParameter(new ValueParameter("A", "required"));

			try
			{
				Assert.Throws<System.ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (System.ArgumentException ae)
			{
				Assert.AreEqual("Required parameter '/a' was not specified.", ae.Message);
			}
		}

		[Test]
		public void ValidateParameters5()
		{
			string[] parms = { "/intparam1", "100", "/intparam2", "1a", "/intparam3", "2a" };

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new IntValueParameter("intparam1", ""));
			clp.AddValidationParameter(new IntValueParameter("intparam2", ""));
			clp.AddValidationParameter(new IntValueParameter("intparam3", "", 333));

			try
			{
				Assert.Throws<System.FormatException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (Exception ex)
			{
				Assert.AreEqual("The 'intparam2' parameter expects an integer value.", ex.Message);
			}

			// Fix intparam2

			parms[3] = "1";

			clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new IntValueParameter("intparam1", ""));
			clp.AddValidationParameter(new IntValueParameter("intparam2", ""));
			clp.AddValidationParameter(new IntValueParameter("intparam3", "", 333));

			try
			{
				Assert.Throws<System.FormatException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (Exception ex)
			{
				Assert.AreEqual("The 'intparam3' parameter expects an integer value.", ex.Message);
			}
		}

		[Test]
		public void GetUsage()
		{
			CmdLineParams clp = new CmdLineParams("");

			clp.AddValidationParameter(new ValueParameter("in", "in file"));
			clp.AddValidationParameter(new ValueParameter("out", "out file"));
			clp.AddValidationParameter(new ValueParameter("ext", "indicates extension of output", "txt"));
			clp.AddValidationParameter(new FlagParameter("p", "pause"));
			clp.AddValidationParameter(new FlagParameter("?", "help"));

			string usage = clp.GetUsage();

			Assert.AreEqual("CmdLineParams Unit Tests version 1.0.0.0\nCopyright © Medicity 2009\r\n\r\nUsage:\r\n\r\n  Tests /in <in file> /out <out file>\n\n    /in* - in file\n    /out* - out file\n    /ext - indicates extension of output (Default: txt)\n    /p - pause\n    /? - help\n\r\n    *Required", usage);
		}

		[Test]
		public void TestRequiredProperty()
		{
			string[] parms = { "/parm1", "One", "/parm2", "Two", "/parm3", "3" };

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new ValueParameter("parm1", "Parameter1"));
			clp.AddValidationParameter(new ValueParameter("parm2", "Parameter2", "two"));
			clp.AddValidationParameter(new IntValueParameter("parm3", "Parameter3", false));
			clp.AddValidationParameter(new ValueParameter("parm4", "Parameter4", false));

			string usage = clp.GetUsage();
			Assert.AreEqual("CmdLineParams Unit Tests version 1.0.0.0\nCopyright © Medicity 2009\r\n\r\nUsage:\r\n\r\n  Tests /parm1 <Parameter1>\n\n    /parm1* - Parameter1\n    /parm2 - Parameter2 (Default: two)\n    /parm3 - Parameter3\n    /parm4 - Parameter4\n\r\n    *Required", usage);

			clp.ValidateParameters();
		}

		[Test]
		public void EnumValueParameterTest()
		{
			string[] parms = { "/cmd1", "DECRYPfT", "/file", "c:\\file.txt", "/to", "you@mail.com", "/from", "me@mail.com" };
			List<EnumValue> enumValues = new List<EnumValue>();

			enumValues.Add(new EnumValue("DECRYPT", "Decrypts email contained in file and verifies signature."));
			enumValues.Add(new EnumValue("ENCRYPT", "Signs and encrypts email contained in file."));

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new EnumValueParameter("cmd1", "The command", enumValues));
			clp.AddValidationParameter(new ValueParameter("file", "The file", false));
			clp.AddValidationParameter(new ValueParameter("to", "To", false));
			clp.AddValidationParameter(new ValueParameter("from", "From", false));
			clp.AddValidationParameter(new IntValueParameter("int", "Int desc", 10));
			clp.AddValidationParameter(new EnumValueParameter("cmd2", "The command", enumValues, false));
			clp.AddValidationParameter(new EnumValueParameter("cmd3", "The command", enumValues, "DECRYPT"));

			try
			{
				Assert.Throws<ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual("The 'cmd1' parameter expects one of the following values: DECRYPT, ENCRYPT", ae.Message);
			}

			string usage = clp.GetUsage();
			Assert.AreEqual("CmdLineParams Unit Tests version 1.0.0.0\nCopyright © Medicity 2009\r\n\r\nUsage:\r\n\r\n  Tests /cmd1 <The command>\n\n    /cmd1* - The command\r\n      DECRYPT - Decrypts email contained in file and verifies signature.\n      ENCRYPT - Signs and encrypts email contained in file.\n    /file - The file\n    /to - To\n    /from - From\n    /int - Int desc (Default: 10)\n    /cmd2 - The command\r\n      DECRYPT - Decrypts email contained in file and verifies signature.\n      ENCRYPT - Signs and encrypts email contained in file.\n    /cmd3 - The command (Default: DECRYPT)\r\n      DECRYPT - Decrypts email contained in file and verifies signature.\n      ENCRYPT - Signs and encrypts email contained in file.\n\r\n    *Required", usage);
		}

		[Test]
		public void PathParameterTest()
		{
			string path = "PathParameterTest";

			if (Directory.Exists(path))
			{
				Directory.Delete(path, true);
			}

			string[] parms = { "/path", path };

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new PathParameter("path", "Path"));

			try
			{
				Assert.Throws<ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual("The 'path' parameter expects a valid path.", ae.Message);
			}

			Directory.CreateDirectory(path);

			try
			{
				clp.ValidateParameters();
			}
			catch (ArgumentException ae)
			{
				Assert.Fail(ae.Message);
			}

			clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new PathParameter("path", "Path"));
			clp.AddValidationParameter(new PathParameter("path1", "Path", false));

			try
			{
				clp.ValidateParameters();
			}
			catch (ArgumentException ae)
			{
				Assert.Fail(ae.Message);
			}

			clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new PathParameter("path", "Path"));
			clp.AddValidationParameter(new PathParameter("path1", "Path", ".\\"));

			try
			{
				clp.ValidateParameters();
				Assert.AreEqual(".\\", clp["path1"]);
			}
			catch (ArgumentException ae)
			{
				Assert.Fail(ae.Message);
			}
		}

		[Test]
		public void FileParameterTest()
		{
			string outFileName = @"FileParameterTest\outfile.txt";
			string inFileName = @"FileParameterTest\infile.txt";
			string path = Path.GetDirectoryName(inFileName);

			if (Directory.Exists(path))
			{
				Directory.Delete(path, true);
			}

			string[] parms = { "/input", inFileName };

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new FileParameter("input", "in file"));

			try
			{
				Assert.Throws<ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(string.Format("The directory, '{0}', does not exist.", path), ae.Message);
			}

			Directory.CreateDirectory(path);

			try
			{
				Assert.Throws<ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(string.Format("The file, '{0}', does not exist", inFileName), ae.Message);
			}

			File.Create(inFileName);

			try
			{
				clp.ValidateParameters();
			}
			catch (ArgumentException ae)
			{
				Assert.Fail(ae.Message);
			}

			parms = new string[0];

			clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new FileParameter("output", "out file", false));

			try
			{
				clp.ValidateParameters();
			}
			catch (ArgumentException ae)
			{
				Assert.Fail(ae.Message);
			}

			clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new FileParameter("output", "out file", outFileName));

			try
			{
				Assert.Throws<ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(string.Format("The file, '{0}', does not exist", outFileName), ae.Message);
			}

			clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new FileParameter("output", "out file", false, false));

			try
			{
				clp.ValidateParameters();
			}
			catch (ArgumentException ae)
			{
				Assert.Fail(ae.Message);
			}
		}

		[Test]
		public void DateTimeParameterTest()
		{
			string[] parms = { "/dt1", "a", "/dt2", "10-3-1971" };

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new DateTimeParameter("datetime1", "The date and/or time", false) { ShortName = "dt1" });
			clp.AddValidationParameter(new DateTimeParameter("datetime2", "The date and/or time") { ShortName = "dt2" });
			clp.AddValidationParameter(new DateTimeParameter("datetime3", "The date and/or time", "1:33pm") { ShortName = "dt3" });

			try
			{
				Assert.Throws<System.FormatException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (Exception ex)
			{
				Assert.AreEqual("The 'datetime1' parameter expects a valid date and/or time.", ex.Message);
			}

			// Valid time
			parms = new string[] { "/dt1", "1:33pm" };

			clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new DateTimeParameter("datetime1", "The date and/or time", false) { ShortName = "dt1" });

			try
			{
				clp.ValidateParameters();
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			// Valid date
			parms = new string[] { "/dt1", "10-3-1971" };

			clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new DateTimeParameter("datetime1", "The date and/or time", false) { ShortName = "dt1" });

			try
			{
				clp.ValidateParameters();
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			// Valid date/time
			parms = new string[] { "/dt1", "10-3-1971 7:31 pm" };

			clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new DateTimeParameter("datetime1", "The date and/or time", false) { ShortName = "dt1" });

			try
			{
				clp.ValidateParameters();
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void ShortNameFlagTest()
		{
			CmdLineParams clp = new CmdLineParams(new string[] { "/p1", "/param2", "/param3" });

			clp.AddValidationParameter(new FlagParameter("param1", "Parameter one") { ShortName = "p1" });
			clp.AddValidationParameter(new FlagParameter("param2", "Parameter two") { ShortName = "p2" });
			clp.AddValidationParameter(new FlagParameter("param3", "Parameter three"));

			try
			{
				clp.ValidateParameters();
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.True(clp.HasFlagParameter("param1"));
			Assert.True(clp.HasFlagParameter("p1"));

			Assert.True(clp.HasFlagParameter("param2"));
			Assert.True(clp.HasFlagParameter("p2"));

			Assert.True(clp.HasFlagParameter("param3"));
			Assert.False(clp.HasFlagParameter("p3"));

			string usage = clp.GetUsage();
			Assert.AreEqual("CmdLineParams Unit Tests version 1.0.0.0\nCopyright © Medicity 2009\r\n\r\nUsage:\r\n\r\n  Tests\n\n    /param1 - Parameter one (Short name: /p1)\n    /param2 - Parameter two (Short name: /p2)\n    /param3 - Parameter three\n\r\n", usage);
		}

		[Test]
		public void ShortNameValueParameterTest()
		{
			CmdLineParams clp = new CmdLineParams(new string[] { "/p1", "p1 value", "/param3", "p3 value", "/param4", "p4 value" });

			clp.AddValidationParameter(new ValueParameter("param1", "Parameter one") { ShortName = "p1" });
			clp.AddValidationParameter(new ValueParameter("param2", "Parameter two", "p2 value") { ShortName = "p2" });
			clp.AddValidationParameter(new ValueParameter("param3", "Parameter three", false) { ShortName = "p3" });
			clp.AddValidationParameter(new ValueParameter("param4", "Parameter four", false));

			try
			{
				clp.ValidateParameters();
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.True(clp.HasValueParameter("param1"));
			Assert.True(clp.HasValueParameter("p1"));
			Assert.True(clp.HasValueParameter("param2"));
			Assert.True(clp.HasValueParameter("p2"));
			Assert.True(clp.HasValueParameter("param3"));
			Assert.True(clp.HasValueParameter("p3"));
			Assert.True(clp.HasValueParameter("param4"));
			Assert.False(clp.HasValueParameter("p4"));

			Assert.AreEqual("p1 value", clp["param1"]);
			Assert.AreEqual("p1 value", clp["p1"]);

			Assert.AreEqual("p2 value", clp["param2"]);
			Assert.AreEqual("p2 value", clp["p2"]);

			Assert.AreEqual("p3 value", clp["param3"]);
			Assert.AreEqual("p3 value", clp["p3"]);

			Assert.AreEqual("p4 value", clp["param4"]);
			Assert.Throws<System.ArgumentException>(delegate { string tmp = clp["p4"]; });

			string usage = clp.GetUsage();
			Assert.AreEqual("CmdLineParams Unit Tests version 1.0.0.0\nCopyright © Medicity 2009\r\n\r\nUsage:\r\n\r\n  Tests /param1 <Parameter one>\n\n    /param1* - Parameter one (Short name: /p1)\n    /param2 - Parameter two (Short name: /p2) (Default: p2 value)\n    /param3 - Parameter three (Short name: /p3)\n    /param4 - Parameter four\n\r\n    *Required", usage);
		}

		[Test]
		public void ShortNameFileParameterTest()
		{
			string inFileName = @"ShortNameFileParameterTest\infile.txt";
			string path = Path.GetDirectoryName(inFileName);

			if (Directory.Exists(path))
			{
				Directory.Delete(path, true);
			}

			string[] parms = { "/input", inFileName };

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new FileParameter("input", "in file") { ShortName = "i" });

			Directory.CreateDirectory(path);
			File.Create(inFileName);

			try
			{
				clp.ValidateParameters();
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.IsTrue(clp.HasValueParameter("input"));
			Assert.IsTrue(clp.HasValueParameter("i"));
		}

		[Test]
		public void NullEmptyDefaultValueTest()
		{
			string[] parms = { };

			CmdLineParams clp = new CmdLineParams(parms);

			clp.AddValidationParameter(new ValueParameter("parm1", "desc1", string.Empty));

			try
			{
				Assert.Throws<ArgumentException>(delegate { clp.ValidateParameters(); });
				clp.ValidateParameters();
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Required parameter '/parm1' was not specified.", ex.Message);
			}
		}
	}
}
