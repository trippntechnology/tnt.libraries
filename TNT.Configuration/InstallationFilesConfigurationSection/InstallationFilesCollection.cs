using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TNT.Configuration
{
	/// <summary>
	/// Installation files collection
	/// </summary>
	public class InstallationFilesCollection : BaseConfigurationElementCollection<FileElement>
	{
		/// <summary>
		/// Gets the name used to identify this collection of elements in the configuration file when 
		/// overridden in a derived class.
		/// </summary>
		protected override string ElementName { get { return "File"; } }

		/// <summary>
		/// Gets the element key for a specified configuration element
		/// </summary>
		/// <param name="element">File element whose AppID should be returned</param>
		/// <returns>Object that acts as the AppID for the file element</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FileElement)element).AppID;
		}
	}
}
