using System.Configuration;

namespace TNT.Configuration
{
	/// <summary>
	/// Collection of part elements defined in the parts section
	/// </summary>
	public class PartsCollection : BaseConfigurationElementCollection<PartElement>
	{
		/// <summary>
		/// Gets the name used to identify this collection of elements in the configuration file when 
		/// overridden in a derived class.
		/// </summary>
		protected override string ElementName { get { return "Part"; } }

		/// <summary>
		/// Gets the element key for a specified configuration element
		/// </summary>
		/// <param name="element">Part element whose Code should be returned</param>
		/// <returns>Object that acts as the code for the part element</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((PartElement)element).Code;
		}
	}
}
