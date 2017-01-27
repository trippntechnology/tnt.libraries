using System;
using System.IO;

namespace TNT.CmdLineParser
{
	/// <summary>
	/// Parameter that represents a file
	/// </summary>
	public class FileParameter : ValueParameter
	{
		#region Properties

		/// <summary>
		/// Indicates whether or not validation should check for the existance of a file.
		/// </summary>
		public bool ValidateFile { get; set; }

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		public FileParameter(string name, string description)
			: base(name, description)
		{
			ValidateFile = true;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="required">Indicates whether or not the parameter is required. Default is true.</param>
		public FileParameter(string name, string description, bool required)
			: base(name, description, required)
		{
			ValidateFile = true;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="required">Indicates whether or not the parameter is required. Default is true.</param>
		/// <param name="validateFile">Specifies whether or not to validate that the file currently exists.
		/// Default is true.</param>
		public FileParameter(string name, string description, bool required, bool validateFile)
			: base(name, description, required)
		{
			ValidateFile = validateFile;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		/// <param name="defaultValue">Indicates the default value to use if one is provided</param>
		public FileParameter(string name, string description, string defaultValue)
			: base(name, description, defaultValue)
		{
			ValidateFile = true;
		}

		/// <summary>
		/// Validates that the value associated with this parameter is a valid path and if 
		/// ValidateFile is true, validates the file exists.
		/// </summary>
		/// <param name="value">Value to validate</param>
		/// <exception cref="ArgumentException"></exception>
		public override void Validate(string value)
		{
			base.Validate(value);

			try
			{
				// First check if the path is valid. This would be the case where the file specified 
				// might be created later so just validate that he path exists.
				string path = Path.GetDirectoryName(value);

				// Check path to make sure it exists
				if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
				{
					throw new ArgumentException(string.Format("The directory, '{0}', does not exist.", path));
				}

				if (ValidateFile)
				{
					if (!File.Exists(value))
					{
						throw new ArgumentException(string.Format("The file, '{0}', does not exist", value));
					}
				}
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}
		}

	}
}
