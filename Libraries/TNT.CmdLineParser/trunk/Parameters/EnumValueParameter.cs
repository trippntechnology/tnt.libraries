using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNT.CmdLineParser
{
	/// <summary>
	/// Represents a parameters that only accepts certain values.
	/// </summary>
	public class EnumValueParameter : ValueParameter
	{
		/// <summary>
		/// List of valid values
		/// </summary>
		private List<EnumValue> m_ValidEnumValues = null;

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="validEnumValues">List of valid EnumValues</param>
		public EnumValueParameter(string name, string description, List<EnumValue> validEnumValues)
			: base(name, description)
		{
			m_ValidEnumValues = validEnumValues;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="validEnumValues">List of valid EnumValues</param>
		/// <param name="required">Indicates whether or not the parameter is required. Default is true.</param>
		public EnumValueParameter(string name, string description, List<EnumValue> validEnumValues, bool required)
			: base(name, description, required)
		{
			m_ValidEnumValues = validEnumValues;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="validEnumValues">List of valid EnumValues</param>
		/// <param name="defaultValue">Indicates the default value to use if one is provided</param>
		/// <exception cref="ArgumentException"></exception>
		public EnumValueParameter(string name, string description, List<EnumValue> validEnumValues, string defaultValue)
			: base(name, description, defaultValue)
		{
			m_ValidEnumValues = validEnumValues;

			// Verify the defaultValue is in the list
			Validate(defaultValue);
		}

		#endregion

		/// <summary>
		/// Validates that the value associated with this parameter is a valid integer
		/// </summary>
		/// <param name="value">Value to validate</param>
		/// <exception cref="FormatException"></exception>
		public override void Validate(string value)
		{
			base.Validate(value);

			// Verify that value is valid
			if (!m_ValidEnumValues.Exists(v => { return v.Name == value; }))
			{
				throw new ArgumentException(string.Format("The '{0}' parameter expects one of the following values: {1}", base.Name, string.Join(", ", (from v in m_ValidEnumValues select v.Name).ToArray())));
			}
		}

		/// <summary>
		/// Returns the usage
		/// </summary>
		/// <param name="delimiter">Delimiter</param>
		/// <returns>Text describing how to use the parameter</returns>
		public override string Usage(string delimiter)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(base.Usage(delimiter));
			for (int index = 0; index < m_ValidEnumValues.Count; index++)
			{
				sb.AppendFormat("{0}      {1} - {2}", index > 0 ? "\n" : "", m_ValidEnumValues[index].Name, m_ValidEnumValues[index].Description);
			}

			return sb.ToString();
		}
	}
}
