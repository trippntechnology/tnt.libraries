using System.Configuration;

namespace TNT.Configuration
{
	/// <summary>
	/// Parts configuration section
	/// </summary>
	public class PartsConfigurationSection : BaseConfigurationSection
	{
		/// <summary>
		/// Gets the Parts collection within the configuration
		/// </summary>
		[ConfigurationProperty("Parts")]
		public PartsCollection Parts { get { return (PartsCollection)this["Parts"] ?? new PartsCollection(); } }

		/// <summary>
		/// Creates an object the represents the parts configuration section within the application's configuration
		/// </summary>
		/// <returns>Instance representing the configuration's parts section</returns>
		/// <exception cref="ConfigurationMissingSectionException"/>
		public static PartsConfigurationSection Create()
		{
			return Create<PartsConfigurationSection>("TNT.Configuration.PartsConfigurationSection");
		}
	}
}
