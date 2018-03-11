using System.Configuration;

namespace TNT.Configuration
{
	/// <summary>
	/// Represents a File element
	/// </summary>
	public class FileElement : ConfigurationElement
	{
		/// <summary>
		/// Application ID attribute
		/// </summary>
		[ConfigurationProperty("appid", IsRequired = true)]
		public string AppID { get { return this["appid"] as string; } }

		/// <summary>
		/// Current version
		/// </summary>
		[ConfigurationProperty("version", IsRequired = true)]
		public string Version { get { return this["version"] as string; } }

		/// <summary>
		/// Location where file is located
		/// </summary>
		[ConfigurationProperty("url", IsRequired = true)]
		public string URL { get { return this["url"] as string; } }
	}
}
