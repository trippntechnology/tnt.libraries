using System.Xml.Serialization;

namespace TNT.Configuration
{
	/// <summary>
	/// Represents a reference to an assembly
	/// </summary>
	public class Reference
	{
		/// <summary>
		/// Assembly name
		/// </summary>
		[XmlAttribute()]
		public string Assembly { get; set; }

		/// <summary>
		/// Indicate to specify a specific type
		/// </summary>
		[XmlAttribute()]
		public string BaseType { get; set; }

		/// <summary>
		/// Fully qualified type
		/// </summary>
		[XmlAttribute()]
		public string Type { get; set; }
	}
}
