﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using TNT.Utilities;

namespace Test
{
	[TestClass]
	public class FtpTests
	{
		//[TestMethod]
		public void Ftp_UploadDownloadTest()
			{
			string fileName = "Ftp_UploadDownloadTest.txt";
			string content = "The quick brown cow jumped over the lazy dog!";
			byte[] ascii = Encoding.ASCII.GetBytes(content);

			Ftp ftp = new Ftp("ftp://localhost", "FTPUser", "V1xYmfR3n6EuBiE5fAD7");
			//Ftp ftp = new Ftp("ftp://LandscapeSprinklerDesign.com/downloads/", "ppirtnevetsftp", "Ds!g313qVfy9fB");
			string description = ftp.Upload(ascii, fileName);

			Assert.AreEqual("226 Transfer complete.\r\n", description);

			using (Stream downloadStream = ftp.Download(fileName, out description))
			using (StreamReader reader = new StreamReader(downloadStream))
			{
				string results = reader.ReadToEnd();
				Assert.AreEqual(content, results);
			}

			Assert.AreEqual("125 Data connection already open; Transfer starting.\r\n", description);
		}
	}
}
