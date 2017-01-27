using System.Text;

namespace TNT.CmdLineParser
{
	/// <summary>
	/// Parameter that expects a value
	/// </summary>
	public class ValueParameter : Parameter
	{
		#region Properties

		/// <summary>
		/// Default value to use if value isn't specified
		/// </summary>
		public string DefaultValue { get; private set; }

		/// <summary>
		/// Indicates if the parameter is required
		/// </summary>
		public bool IsRequired { get; private set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		public ValueParameter(string name, string description)
			: base(name, description)
		{
			IsRequired = true;
			DefaultValue = null;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="required">Indicates whether or not the parameter is required. Default is true.</param>
		public ValueParameter(string name, string description, bool required)
			: base(name, description)
		{
			IsRequired = required;
			DefaultValue = null;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="defaultValue">Indicates the default value to use if one is provided</param>
		public ValueParameter(string name, string description, string defaultValue)
			: base(name, description)
		{
			IsRequired = string.IsNullOrEmpty(defaultValue) ? true : false;
			DefaultValue = string.IsNullOrEmpty(defaultValue) ? null : defaultValue;
		}

		#endregion

		/// <summary>
		/// Returns the usage
		/// </summary>
		/// <param name="delimiter">Delimiter</param>
		/// <returns>Text describing how to use the parameter</returns>
		public override string Usage(string delimiter)
		{
			StringBuilder usage = new StringBuilder();

			usage.AppendFormat("{0}{1}", delimiter, Name);

			if (IsRequired)
			{
				usage.Append("*");
			}

			usage.AppendFormat(" - {0}{1}", Description, string.IsNullOrEmpty(ShortName) ? "" : string.Format(" (Short name: {0}{1})", delimiter, ShortName)); 

			if (!string.IsNullOrEmpty(DefaultValue))
			{
				usage.AppendFormat(" (Default: {0})", DefaultValue);
			}

			return usage.ToString();
		}

		/// <summary>
		/// Method to validate the parameters value
		/// </summary>
		/// <param name="value">Value to validate</param>
		public virtual void Validate(string value)
		{
		}
	}
}
