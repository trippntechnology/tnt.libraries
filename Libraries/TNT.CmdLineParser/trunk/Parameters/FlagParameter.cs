
namespace TNT.CmdLineParser
{
	/// <summary>
	/// Parameter that can be provided without an associated value
	/// </summary>
	public class FlagParameter : Parameter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Parameter name</param>
		/// <param name="description">Parameter description</param>
		public FlagParameter(string name, string description)
			: base(name, description)
		{
		}
	}
}
