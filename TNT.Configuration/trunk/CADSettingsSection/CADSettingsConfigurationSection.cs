using System;
using System.Configuration;
using System.Drawing;

namespace TNT.Configuration
{
	/// <summary>
	/// Represents the configuration elements associated with the CAD Settings
	/// </summary>
	public class CADSettingsConfigurationSection : BaseConfigurationSection
	{
		/// <summary>
		/// Grid's color
		/// </summary>
		[ConfigurationProperty("GridColor")]
		public ConfigurationElement<string> GridColor { get { return (ConfigurationElement<string>)this["GridColor"]; } }

		/// <summary>
		/// Grid's alpha value
		/// </summary>
		[ConfigurationProperty("GridColorAlphaValue")]
		public ConfigurationElement<int> GridColorAlphaValue { get { return (ConfigurationElement<int>)this["GridColorAlphaValue"]; } }

		/// <summary>
		/// Indicates whether the legend should be shown
		/// </summary>
		[ConfigurationProperty("ShowLegend")]
		public ConfigurationElement<bool> ShowLegend { get { return (ConfigurationElement<bool>)this["ShowLegend"]; } }

		/// <summary>
		/// Height of the CAD represented by feet
		/// </summary>
		[ConfigurationProperty("HeightInFeet")]
		public ConfigurationElement<int> HeightInFeet { get { return (ConfigurationElement<int>)this["HeightInFeet"]; } }

		/// <summary>
		/// Width of the CAD represented by feet
		/// </summary>
		[ConfigurationProperty("WidthInFeet")]
		public ConfigurationElement<int> WidthInFeet { get { return (ConfigurationElement<int>)this["WidthInFeet"]; } }

		/// <summary>
		/// Specifies the number of lateral line drains to add per zone
		/// </summary>
		[ConfigurationProperty("LateralDrains")]
		public ConfigurationElement<int> LateralDrains { get { return (ConfigurationElement<int>)this["LateralDrains"]; } }

		/// <summary>
		/// Specifies the number of mainline drains to include in the system
		/// </summary>
		[ConfigurationProperty("MainlineDrains")]
		public ConfigurationElement<int> MainlineDrains { get { return (ConfigurationElement<int>)this["MainlineDrains"]; } }

		/// <summary>
		/// Indicates that pipe quantity should be rounded to nearest 20' lengths
		/// </summary>
		[ConfigurationProperty("RoundPipe")]
		public ConfigurationElement<bool> RoundPipe { get { return (ConfigurationElement<bool>)this["RoundPipe"]; } }

		/// <summary>
		/// Specifies the external codes should be displayed
		/// </summary>
		[ConfigurationProperty("ShowExtCodes")]
		public ConfigurationElement<bool> ShowExtCodes { get { return (ConfigurationElement<bool>)this["ShowExtCodes"]; } }

		/// <summary>
		/// Specifies the culinary S and W should be figured into the parts listing
		/// </summary>
		[ConfigurationProperty("IncludeCulinarySW")]
		public ConfigurationElement<string> IncludeCulinarySW { get { return (ConfigurationElement<string>)this["IncludeCulinarySW"]; } }

		/// <summary>
		/// Indicates the culinary PSI
		/// </summary>
		[ConfigurationProperty("CulinaryPSI")]
		public ConfigurationElement<string> CulinaryPSI { get { return (ConfigurationElement<string>)this["CulinaryPSI"]; } }

		/// <summary>
		/// Indicates the size of pipe supplied by the culinary source
		/// </summary>
		[ConfigurationProperty("CulinarySize")]
		public ConfigurationElement<string> CulinarySize { get { return (ConfigurationElement<string>)this["CulinarySize"]; } }

		/// <summary>
		/// Indicates the type of pipe supplied by the culinary source
		/// </summary>
		[ConfigurationProperty("CulinaryType")]
		public ConfigurationElement<string> CulinaryType { get { return (ConfigurationElement<string>)this["CulinaryType"]; } }

		/// <summary>
		/// Specifies the secondary S and W should be figured into the parts listing
		/// </summary>
		[ConfigurationProperty("IncludeSecondarySW")]
		public ConfigurationElement<string> IncludeSecondarySW { get { return (ConfigurationElement<string>)this["IncludeSecondarySW"]; } }

		/// <summary>
		/// Indicates the secondary PSI
		/// </summary>
		[ConfigurationProperty("SecondaryPSI")]
		public ConfigurationElement<string> SecondaryPSI { get { return (ConfigurationElement<string>)this["SecondaryPSI"]; } }

		/// <summary>
		/// Indicates the size of pipe supplied by the secondary source
		/// </summary>
		[ConfigurationProperty("SecondarySize")]
		public ConfigurationElement<string> SecondarySize { get { return (ConfigurationElement<string>)this["SecondarySize"]; } }

		/// <summary>
		/// Indicates the type of pipe supplied by the secondary source
		/// </summary>
		[ConfigurationProperty("SecondaryType")]
		public ConfigurationElement<string> SecondaryType { get { return (ConfigurationElement<string>)this["SecondaryType"]; } }

		/// <summary>
		/// Indicates whether the grid should be drawn
		/// </summary>
		[ConfigurationProperty("DrawGrid")]
		public ConfigurationElement<bool> DrawGrid { get { return (ConfigurationElement<bool>)this["DrawGrid"]; } }

		/// <summary>
		/// Indicates whether the units should be drawn
		/// </summary>
		[ConfigurationProperty("DrawUnits")]
		public ConfigurationElement<bool> DrawUnits { get { return (ConfigurationElement<bool>)this["DrawUnits"]; } }

		/// <summary>
		/// Gets the Parts collection within the configuration
		/// </summary>
		[ConfigurationProperty("Parts")]
		public PartsCollection Parts { get { return (PartsCollection)this["Parts"] ?? new PartsCollection(); } }

		/// <summary>
		/// Creates an object the represents the CAD settings section within the application's configuration
		/// </summary>
		/// <returns>Instance representing the configuration's CAD settings section</returns>
		public static CADSettingsConfigurationSection Create()
		{
			CADSettingsConfigurationSection section = null;

			try
			{
				section = Create<CADSettingsConfigurationSection>("TNT.Configuration.CADSettingsConfigurationSection");
			}
			catch
			{

			}

			return section;
		}
	}
}
