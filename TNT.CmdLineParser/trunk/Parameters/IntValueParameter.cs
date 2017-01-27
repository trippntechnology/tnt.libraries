using System;

namespace TNT.CmdLineParser
{
	/// <summary>
	/// Parameter that requires an integer value
	/// </summary>
	public class IntValueParameter : ValueParameter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		public IntValueParameter(string name, string description)
			: base(name, description)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="required">indicates the parameter is required</param>
		public IntValueParameter(string name, string description, bool required)
			: base(name, description, required)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="defaultValue">Specifies the default value to use if the optional value isn't provided</param>
		public IntValueParameter(string name, string description, int defaultValue)
			: base(name, description, defaultValue.ToString())
		{
		}

		/// <summary>
		/// Validates that the value associated with this parameter is a valid integer
		/// </summary>
		/// <param name="value">Value to validate</param>
		/// <exception cref="FormatException"></exception>
		public override void Validate(string value)
		{
			base.Validate(value);

			try
			{
				// Check if the value is an integer
				Convert.ToInt32(value);
			}
			catch (FormatException fe)
			{
				throw new FormatException(string.Format("The '{0}' parameter expects an integer value.", base.Name), fe);
			}
		}
	}
}
