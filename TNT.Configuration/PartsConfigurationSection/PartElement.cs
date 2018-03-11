using System.Configuration;

namespace TNT.Configuration
{
	/// <summary>
	/// Part element of the parts collection
	/// </summary>
	public class PartElement : ConfigurationElement
	{
		/// <summary>
		/// Code attribute of the part element
		/// </summary>
		[ConfigurationProperty("code", IsRequired = true)]
		public string Code { get { return this["code"] as string; } }

		/// <summary>
		/// Quantity of the part associated to the code
		/// </summary>
		[ConfigurationProperty("quantity", IsRequired = true)]
		public int Quantity { get { return (int)this["quantity"]; } }
	}
}
