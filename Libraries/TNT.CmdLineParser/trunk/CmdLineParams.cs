using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace TNT.CmdLineParser
{
	/// <summary>
	/// Parses and validate command line arguments given a parameter validators.
	/// </summary>
	public class CmdLineParams
	{
		#region Members

		/// <summary>
		/// List of key/value pair representing value parameters
		/// </summary>
		private Dictionary<string, string> m_ValueParams = new Dictionary<string, string>();

		/// <summary>
		/// List of strings representing flag parameters
		/// </summary>
		private List<string> m_FlagParams = new List<string>();

		/// <summary>
		/// List representing the parameters validators
		/// </summary>
		private List<Parameter> m_ParameterValidators = new List<Parameter>();

		/// <summary>
		/// Indicates the parameter delimiter
		/// </summary>
		private string m_ParamDelimiter = "/";

		#endregion

		#region Properties

		/// <summary>
		/// Gets the value associated with the key
		/// </summary>
		/// <param name="key">Key associated with the value</param>
		/// <returns>The value associated with the key if key exists</returns>
		/// <exception cref="ArgumentException"></exception>
		public string this[string key]
		{
			get
			{
				if (!HasValueParameter(key.ToLower()))
				{
					throw new ArgumentException("Parameter not available.");
				}

				return m_ValueParams[key.ToLower()];
			}
		}

		/// <summary>
		/// Gets the parameter delimiter
		/// </summary>
		public string ParameterDelimiter
		{
			get { return m_ParamDelimiter; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cmdLineParams">String representing the command line arguments</param>
		/// <param name="paramDelimiter">Indicates the parameter delimiter. '/' is default.</param>
		public CmdLineParams(string cmdLineParams, string paramDelimiter)
		{
			m_ParamDelimiter = paramDelimiter;
			Parse(cmdLineParams);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cmdLineParams">String representing the command line arguments</param>
		public CmdLineParams(string cmdLineParams)
		{
			Parse(cmdLineParams);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cmdLineParams">Array of strings that represent the command line arguments</param>
		public CmdLineParams(string[] cmdLineParams)
		{
			Parse(string.Join(" ", cmdLineParams));
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cmdLineParams">Array of strings that represent the command line arguments</param>
		/// <param name="paramDelimiter">Indicates the parameter delimiter. '/' is default.</param>
		public CmdLineParams(string[] cmdLineParams, string paramDelimiter)
		{
			m_ParamDelimiter = paramDelimiter;
			Parse(string.Join(" ", cmdLineParams));
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Parses the arguments and places them in either m_FlagParams or m_ValueParams.
		/// </summary>
		/// <param name="cmdLineArgs">List of values that can be parsed</param>
		private void Parse(string cmdLineArgs)
		{
			MatchCollection matches = Regex.Matches(cmdLineArgs, string.Format("{0}(?<flag>[^ ]*)(?<value>[^{0}]*)", m_ParamDelimiter), RegexOptions.Multiline);

			foreach (Match match in matches)
			{
				if (match.Success && match.Groups.Count > 1)
				{
					string flag = match.Groups["flag"].ToString().Trim().ToLower();
					string value = match.Groups["value"].ToString().Trim();

					if (string.IsNullOrEmpty(value))
					{
						// This is just flag
						m_FlagParams.Add(flag);
					}
					else
					{
						// This is a flag/value pair
						m_ValueParams.Add(flag, value);
					}
				}
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Checks if there is a value parameter specified by the key value
		/// </summary>
		/// <param name="key">Key of the value parameter</param>
		/// <returns>True if value parameter with key exists, false otherwise.</returns>
		public bool HasValueParameter(string key)
		{
			return m_ValueParams.ContainsKey(key.ToLower()) && !string.IsNullOrEmpty(m_ValueParams[key.ToLower()]);
		}

		/// <summary>
		/// Checks if there is a flag parameter specified by the key value
		/// </summary>
		/// <param name="key">Key of the flag paramter</param>
		/// <returns>True if flag parameter with key exists, false otherwise.</returns>
		public bool HasFlagParameter(string key)
		{
			return m_FlagParams.Contains(key.ToLower());
		}

		/// <summary>
		/// Validates the parameters.
		/// </summary>
		/// <returns>True if all parameters are valid</returns>
		/// <exception cref="ArgumentException">ArgumentException</exception>
		public bool ValidateParameters()
		{
			if (m_ParameterValidators.Count > 0)
			{
				Dictionary<string, string> additionalParams = new Dictionary<string, string>();

				// Check for valid parameters
				foreach (string key in m_ValueParams.Keys)
				{
					Parameter parameter = m_ParameterValidators.Find(e => { return e.Name.ToLower() == key || e.ShortName.ToLower() == key; });

					if (parameter == null)
					{
						throw new ArgumentException(string.Format("Invalid parameter '{0}' specified.", key));
					}
					else if (parameter.ShortName == key)
					{
						// Add equivolent name
						additionalParams.Add(parameter.Name, m_ValueParams[key]);
					}
					else if (parameter.Name == key && !string.IsNullOrEmpty(parameter.ShortName))
					{
						// Add equivolent short name 
						additionalParams.Add(parameter.ShortName, m_ValueParams[key]);
					}
				}

				foreach (string key in additionalParams.Keys)
				{
					if (!m_ValueParams.ContainsKey(key))
					{
					m_ValueParams.Add(key, additionalParams[key]);
					}
				}
			}

			List<string> additionalFlags = new List<string>();

			foreach (string flag in m_FlagParams)
			{
				Parameter parameter = m_ParameterValidators.Find(e => { return e.Name.ToLower() == flag || e.ShortName.ToLower() == flag; });

				if (parameter == null)
				{
					throw new ArgumentException(string.Format("Invalid parameter '{0}' specified.", flag));
				}
				else if (parameter.ShortName == flag)
				{
					// Add the name as well
					additionalFlags.Add(parameter.Name);
				}
				else if (parameter.Name == flag && !string.IsNullOrEmpty(parameter.ShortName))
				{
					// Add the short name as well
					additionalFlags.Add(parameter.ShortName);
				}
			}

			m_FlagParams.AddRange(additionalFlags);

			// Get list of optional parameters with default values and add them if they weren't provided
			List<ValueParameter> optParams = (from p in m_ParameterValidators where p is ValueParameter && !string.IsNullOrEmpty((p as ValueParameter).DefaultValue) select p as ValueParameter).ToList();

			foreach (ValueParameter optParam in optParams)
			{
				if (!HasValueParameter(optParam.Name) || (!string.IsNullOrEmpty(optParam.ShortName) && !HasValueParameter(optParam.ShortName)))
				{
					m_ValueParams.Add(optParam.Name, optParam.DefaultValue);

					if (!string.IsNullOrEmpty(optParam.ShortName))
					{
						m_ValueParams.Add(optParam.ShortName, optParam.DefaultValue);
					}
				}
			}

			// Get list of all required parameters
			List<ValueParameter> reqParams = (from p in m_ParameterValidators where p is ValueParameter && (p as ValueParameter).IsRequired select p as ValueParameter).ToList<ValueParameter>();

			// Verify that the required parameters exist
			foreach (ValueParameter reqParam in reqParams)
			{
				if (!m_ValueParams.ContainsKey(reqParam.Name.ToLower()))
				{
					throw new ArgumentException(string.Format("Required parameter '{0}{1}' was not specified.", m_ParamDelimiter, reqParam.Name.ToLower()));
				}

				// Validate the values are correctly typed.
				reqParam.Validate(m_ValueParams[reqParam.Name.ToLower()]);
			}

			// Verify that the non-required parameters have valid values
			List<ValueParameter> nonreqParams = (from p in m_ParameterValidators where p is ValueParameter && (!string.IsNullOrEmpty((p as ValueParameter).DefaultValue) || !(p as ValueParameter).IsRequired) select p as ValueParameter).ToList<ValueParameter>();

			foreach (ValueParameter valParam in nonreqParams)
			{
				string key = valParam.Name.ToLower();
				if (m_ValueParams.ContainsKey(key))
				{
					valParam.Validate(m_ValueParams[key]);
				}
			}

			return true;
		}

		/// <summary>
		/// Adds a parameter that will be used to validate parameters
		/// </summary>
		/// <param name="parameter">Parameter to add</param>
		public void AddValidationParameter(Parameter parameter)
		{
			m_ParameterValidators.Add(parameter);
		}

		/// <summary>
		/// Gets the usage text. Usage text is generated by obtaining the assembly's properties and displaying the 
		/// validation parameters descriptions.
		/// </summary>
		/// <returns>Usage</returns>
		public string GetUsage()
		{
			StringBuilder usageTxt = new StringBuilder();
			Assembly asm = Assembly.GetEntryAssembly();

			// This was added for unit tests. For some reason there isn't an EntryAssembly so use
			if (asm == null)
			{
				asm = Assembly.GetCallingAssembly();
			}

			AssemblyProductAttribute apa = ((AssemblyProductAttribute)asm.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]);
			AssemblyDescriptionAttribute ada = ((AssemblyDescriptionAttribute)asm.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]);
			AssemblyCopyrightAttribute acra = ((AssemblyCopyrightAttribute)asm.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]);
			AssemblyCompanyAttribute aca = ((AssemblyCompanyAttribute)asm.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]);

			usageTxt.AppendFormat("{0} version {1}\n", ada.Description, asm.GetName().Version.ToString());
			usageTxt.AppendLine(acra.Copyright);
			usageTxt.AppendLine();
			usageTxt.AppendLine("Usage:");
			usageTxt.AppendLine();

			usageTxt.AppendFormat("  {0}", apa.Product);

			// Get list of required parameters
			List<ValueParameter> reqParams = (from p in m_ParameterValidators where (p as ValueParameter) != null && (p as ValueParameter).IsRequired select p as ValueParameter).ToList<ValueParameter>();

			foreach (Parameter p in reqParams)
			{
				// List all required parameters
				usageTxt.AppendFormat(" {0}{1}", m_ParamDelimiter, p.AsArgument());
			}

			usageTxt.AppendFormat("\n\n");

			foreach (Parameter p in m_ParameterValidators)
			{
				usageTxt.AppendFormat("    {0}\n", p.Usage(m_ParamDelimiter));
			}

			usageTxt.AppendLine();

			if (reqParams.Count > 0)
			{
				usageTxt.Append("    *Required");
			}

			return usageTxt.ToString();
		}

		#endregion
	}
}
