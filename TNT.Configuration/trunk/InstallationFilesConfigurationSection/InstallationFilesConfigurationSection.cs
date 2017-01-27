using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TNT.Configuration
{
	/// <summary>
	/// Represents an installation files configuration section
	/// </summary>
	public class InstallationFilesConfigurationSection : BaseConfigurationSection
	{
		/// <summary>
		/// Gets the collection of Files
		/// </summary>
		[ConfigurationProperty("Files")]
		public InstallationFilesCollection Files{ get { return (InstallationFilesCollection)this["Files"] ?? new InstallationFilesCollection(); } }

		/// <summary>
		/// Creates a InstallationFilesConfigurationSection object representing the TNT.LSD.InstallationFilesConfigurationSection configuration section
		/// </summary>
		/// <returns>TNT.LSD.Plugin.InstallationFilesConfigurationSection</returns>
		/// <exception cref="Exception"/>
		public static InstallationFilesConfigurationSection Create()
		{
			return Create<InstallationFilesConfigurationSection>("TNT.Configuration.InstallationFilesConfigurationSection");
		}

	}
}
