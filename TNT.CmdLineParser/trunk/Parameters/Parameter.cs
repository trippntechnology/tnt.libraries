using System.Collections.Generic;

namespace TNT.CmdLineParser
{
	/// <summary>
	/// Abstract base class for parameter types
	/// </summary>
	public abstract class Parameter
	{
		#region Properties

		/// <summary>
		/// Parameter name
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Short name of the parameter name
		/// </summary>
		public string ShortName { get; set; }

		/// <summary>
		/// Parameter description
		/// </summary>
		public string Description { get; private set; }

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		public Parameter(string name, string description)
		{
			Name = name;
			Description = description;
			ShortName = string.Empty;
		}

		/// <summary>
		/// Returns a string representing how the argument should be used as an argument
		/// </summary>
		/// <returns></returns>
		virtual public string AsArgument()
		{
			return string.Format("{0} <{1}>", Name, Description);
		}

		/// <summary>
		/// Returns the usage
		/// </summary>
		/// <param name="delimiter">Delimiter</param>
		/// <returns>Text describing how to use the parameter</returns>
		virtual public string Usage(string delimiter)
		{
			return string.Format("{0}{1} - {2}{3}", delimiter, Name, Description, string.IsNullOrEmpty(ShortName) ? "" : string.Format(" (Short name: {0}{1})", delimiter, ShortName)); 
		}
	}
}
