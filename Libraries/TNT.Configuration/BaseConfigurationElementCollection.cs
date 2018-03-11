using System.Configuration;

namespace TNT.Configuration
{
	/// <summary>
	/// Base configuration element collection
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class BaseConfigurationElementCollection<T> : ConfigurationElementCollection where T : ConfigurationElement, new()
	{
		#region Properties

		/// <summary>
		/// Get the part element associated with the key
		/// </summary>
		/// <param name="key">Part element key</param>
		/// <returns>Part element associated with the key</returns>
		virtual public new T this[string key] { get { return base.BaseGet(key) as T; } }

		/// <summary>
		///	Gets the PartElement for the specified index
		/// </summary>
		/// <param name="index">Index within the collection</param>
		/// <returns>PartElement for the specified index</returns>
		virtual public T this[int index] { get { return base.BaseGet(index) as T; } }

		#endregion

		#region Overrides

		/// <summary>
		/// Gets the type of the System.Configuration.ConfigurationElementCollection.
		/// </summary>
		public override ConfigurationElementCollectionType CollectionType { get { return ConfigurationElementCollectionType.BasicMap; } }

		/// <summary>
		/// Creates new element of type T
		/// </summary>
		/// <returns>New element of type T</returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new T();
		}

		#endregion
	}
}
