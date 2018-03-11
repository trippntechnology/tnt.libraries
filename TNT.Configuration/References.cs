using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace TNT.Configuration
{
	/// <summary>
	/// List of references
	/// </summary>
	[XmlRoot("References")]
	public class References : List<Reference>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public References()
		{
		}

		/// <summary>
		/// Gets an array of Types represented by the list
		/// </summary>
		public Type[] Types
		{
			get
			{
				string assPath = Path.GetDirectoryName(this.GetType().Module.FullyQualifiedName);
				List<Type> types = new List<Type>();

				this.ForEach(r =>
					{
						if (!string.IsNullOrEmpty(r.Type))
						{
							Type type = Type.GetType(r.Type);

							if (type != null)
							{
								types.Add(type);
							}
						}
						else
						{
							Assembly ass = null;

							try
							{
								string path = Path.IsPathRooted(r.Assembly) ? r.Assembly : Path.Combine(assPath, r.Assembly);
								ass = Assembly.LoadFile(path);
							}
							catch (Exception ex)
							{
								throw new ConfigurationErrorsException("Failed to load assembly", ex);
							}

							if (ass != null)
							{
								Type baseType = null;

								if (!string.IsNullOrEmpty(r.BaseType))
								{
									baseType = Type.GetType(r.BaseType);

									if (baseType == null)
									{
										throw new ConfigurationErrorsException("Invalid base type specified");
									}
								}

								if (baseType != null)
								{
									types.AddRange((from t in ass.GetExportedTypes() where t.GetConstructor(new Type[0]) != null && !t.IsAbstract && InheritsFrom(t, baseType) select t).ToList<Type>());
								}
								else if (!string.IsNullOrEmpty(r.Assembly))
								{
									types.AddRange((from t in ass.GetExportedTypes() where t.GetConstructor(new Type[0]) != null && !t.IsAbstract select t).ToList<Type>());
								}
							}
						}
					});

				return types.ToArray();
			}
		}

		/// <summary>
		/// Checks if thisBaseType inherits from baseType.
		/// </summary>
		/// <param name="thisBaseType">Type that is being check</param>
		/// <param name="baseType">Base type we're lookin for</param>
		/// <returns></returns>
		private bool InheritsFrom(Type thisBaseType, Type baseType)
		{
			bool rtnValue = false;

			if (baseType == null)
			{
				// Since baseType isn't provided ignore check and return true
				rtnValue = true;
			}
			else
			{
				if (thisBaseType == baseType || thisBaseType.BaseType == baseType)
				{
					rtnValue = true;
				}
				else
				{
					// Check if base type is further down the inheritance list
					if (thisBaseType.BaseType.BaseType != null)
					{
						rtnValue = InheritsFrom(thisBaseType.BaseType, baseType);
					}
				}
			}

			return rtnValue;
		}
	}
}
