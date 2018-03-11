using System.Configuration;

namespace TNT.Configuration
{
	/// <summary>
	/// Configuration section for the PaletteNodeTreeView control
	/// </summary>
	public class PaletteNodeConfigurationSection : BaseConfigurationSection
	{
		/// <summary>
		/// Indicates the Palette File to use
		/// </summary>
		[ConfigurationProperty("PaletteFile", IsRequired=true)]
		public ConfigurationElement<string> PaletteFile { get { return (ConfigurationElement<string>)this["PaletteFile"]; } }

		/// <summary>
		/// Creates an object that represents the PaletteNodeTreeView configuration section within the application's configuration
		/// </summary>
		/// <returns>Instance representing the configuration's PaletteNodeTreeView section</returns>
		/// <exception cref="ConfigurationMissingSectionException"/>
		public static PaletteNodeConfigurationSection Create()
		{
			return Create<PaletteNodeConfigurationSection>("PaletteNodeSection");
		}
	}
}
