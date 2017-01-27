using System;

namespace TNT.Configuration
{
	/// <summary>
	/// Exception that indicates the configuration section is missing
	/// </summary>
	public class ConfigurationMissingSectionException : Exception
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">Message to pass with exception</param>
		public ConfigurationMissingSectionException(string message)
			: base(message)
		{
		}
	}
}
