
namespace TNT.CmdLineParser
{
	/// <summary>
	/// Represents the name and description of a enumerated value
	/// </summary>
	public class EnumValue
	{
		/// <summary>
		/// Name of enumerated value
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Description of enumerated value
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Name of enumerated value</param>
		/// <param name="description">Description of enumerated value</param>
		public EnumValue(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
