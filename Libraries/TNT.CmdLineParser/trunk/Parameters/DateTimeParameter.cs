using System;

namespace TNT.CmdLineParser
{
	/// <summary>
	/// Represents a parameter that contains a date/time value
	/// </summary>
	public class DateTimeParameter : ValueParameter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		public DateTimeParameter(string name, string description)
			: base(name, description)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="required">Indicates whether or not the parameter is required. Default is true.</param>
		public DateTimeParameter(string name, string description, bool required)
			: base(name, description, required)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="defaultValue">Indicates the default value to use if one is provided</param>
		public DateTimeParameter(string name, string description, string defaultValue)
			: base(name, description, defaultValue)
		{
		}

		/// <summary>
		/// Validates the that the value is a valid DateTime
		/// </summary>
		/// <param name="value">Value to validate</param>
		/// <exception cref="FormatException"></exception>
		public override void Validate(string value)
		{
			base.Validate(value);

			// Check if value is a valid DateTime
			try
			{
				Convert.ToDateTime(value);
			}
			catch (FormatException)
			{
				throw new FormatException(string.Format("The '{0}' parameter expects a valid date and/or time.", base.Name));
			}
		}
	}
}
