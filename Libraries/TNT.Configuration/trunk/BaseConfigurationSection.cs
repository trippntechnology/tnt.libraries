using System.Configuration;

namespace TNT.Configuration
{
	/// <summary>
	/// Base configuration section
	/// </summary>
	public class BaseConfigurationSection : ConfigurationSection
	{
		/// <summary>
		/// Creates an object the represents the configuration section specified in the section name within the 
		/// application's configuration
		/// </summary>
		/// <typeparam name="T">Type of BaseConfigurationSection that section name represents</typeparam>
		/// <param name="sectionName">Name of section that represents the configuration section.</param>
		/// <returns>Instance representing the configuration section</returns>
		/// <exception cref="ConfigurationMissingSectionException"/>
		public static T Create<T>(string sectionName)
		{
			// Force a re-read of the section from disk
			ConfigurationManager.RefreshSection(sectionName);

			T baseConfigSection = (T)ConfigurationManager.GetSection(sectionName);

			if (baseConfigSection == null)
			{
				throw new ConfigurationMissingSectionException(string.Format("The section, {0}, could not be found within the configuration file", sectionName));
			}

			return baseConfigSection;
		}
	}
}
