using System.Configuration;

namespace TNT.Configuration
{
	/// <summary>
	/// Represents a configuration element with a value of type T
	/// </summary>
	/// <typeparam name="T">Type represented by the value attribute</typeparam>
	public class ConfigurationElement<T> : ConfigurationElement
	{
		/// <summary>
		/// Represents the value attribute
		/// </summary>
		[ConfigurationProperty("value")]
		public T Value { get { return (T)this["value"]; } }

		/// <summary>
		/// Implicit conversion operator for type T
		/// </summary>
		/// <param name="element">Element that should be converted</param>
		/// <returns></returns>
		public static implicit operator T(ConfigurationElement<T> element)
		{
			return (T)element.Value;
		}

		/// <summary>
		/// Overriden to show the value within the object inspector
		/// </summary>
		/// <returns>String representation of the type T value</returns>
		public override string ToString()
		{
			return this["value"].ToString();
		}
	}
}
