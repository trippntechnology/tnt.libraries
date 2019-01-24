using System;

namespace TNT.Utilities.Console
{
	public class VersionParameter : Parameter
	{
		/// <summary>
		/// Converts base.Value to <see cref="string"/>
		/// </summary>
		public new Version Value { get { return base.Value as Version; } }

		public VersionParameter(string name, string description, Version defaultValue)
			: this(name, description)
		{
			this.DefaultValue = defaultValue;
		}

		public VersionParameter(string name, string description, bool required = false)
	: base(name, description, required)
		{
		}

		public override string Syntax()
		{
			string syntax = string.Format("/{0} <version>", Name);

			if (!Required)
			{
				syntax = string.Format("[{0}]", syntax);
			}

			return syntax;
		}

		public override void SetValue(object value)
		{
			if (!Version.TryParse(value.ToString(), out Version version))
			{
				throw new ArgumentException(string.Format("Parameter '{0}' expects a string that represents a version", this.Name));
			}

			base.SetValue(version);
		}
	}
}
