using System;
using System.IO;

namespace TNT.CmdLineParser
{
	/// <summary>
	/// Parameter that represents a path
	/// </summary>
	public class PathParameter : ValueParameter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		public PathParameter(string name, string description)
			: base(name, description)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="required">Indicates whether or not the parameter is required. Default is true.</param>
		public PathParameter(string name, string description, bool required)
			: base(name, description, required)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="defaultValue">Indicates the default value to use if one is provided</param>
		public PathParameter(string name, string description, string defaultValue)
			: base(name, description, defaultValue)
		{
		}

		/// <summary>
		/// Validates that the value associated with this parameter is a valid path
		/// </summary>
		/// <param name="value">Value to validate</param>
		/// <exception cref="ArgumentException"></exception>
		public override void Validate(string value)
		{
			base.Validate(value);

			// Check if the value is a valid path
			if (!Directory.Exists(value))
			{
				throw new ArgumentException(string.Format("The '{0}' parameter expects a valid path.", base.Name));
			}
		}

	}
}
