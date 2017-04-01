using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace TNT.Plugin.Manager
{
	/// <summary>
	/// Class that manages <see cref="Plugin"/>
	/// </summary>
	public class Manager
	{
		/// <summary>
		/// <see cref="EventHandler"/> that gets associated with each <see cref="ToolStripItem"/> click handler
		/// </summary>
		private EventHandler _OnClick;

		/// <summary>
		/// Application's controls
		/// </summary>
		private ControlCollection _Controls = null;

		/// <summary>
		/// Initialization constructor
		/// </summary>
		/// <param name="controls">Application's controls</param>
		/// <param name="onClickHandler">Application's <see cref="EventHandler"/></param>
		/// <param name="pluginDirectory">Directory where the plugins are located</param>
		public Manager(ControlCollection controls, EventHandler onClickHandler, string pluginDirectory)
		{
			this._Controls = controls;
			this._OnClick = onClickHandler;

			if (!Directory.Exists(pluginDirectory))
			{
				throw new DirectoryNotFoundException($"Unable to locate {pluginDirectory}.");
			}

			var files = Directory.GetFiles(pluginDirectory, "*.dll");

			foreach (string file in files)
			{
				var types = Utilities.Utilities.GetTypes(file, t =>
				{
					return HasBaseType(t, typeof(Plugin)) && !t.IsAbstract;
				});

				Plugin plugin = null;

				foreach (Type type in types)
				{
					plugin = (Plugin)Activator.CreateInstance(type);
					this.Register(plugin);
				}
			}
		}

		/// <summary>
		/// Checks to see if <paramref name="type"/> derives from <paramref name="baseType"/>
		/// </summary>
		/// <param name="type"><see cref="Type"/> to check</param>
		/// <param name="baseType"><see cref="Type"/> that represents the base class</param>
		/// <returns>True if <paramref name="type"/> derives from <paramref name="baseType"/></returns>
		protected bool HasBaseType(Type type, Type baseType)
		{
			if (type.BaseType == baseType)
			{
				return true;
			}
			else if (type.BaseType == null)
			{
				return false;
			}
			else
			{
				return HasBaseType(type.BaseType, baseType);
			}
		}

		/// <summary>
		/// Registers a <see cref="Plugin"/>
		/// </summary>
		/// <param name="plugin"><see cref="Plugin"/> to register with the <see cref="Manager"/></param>
		protected void Register(Plugin plugin)
		{
			ToolStrip appMenuStrip = (ToolStrip)_Controls.Find(plugin.MenuStripName, true).FirstOrDefault();//  (from c in _Controls where c.Name == plugin.MenuStripName select c).FirstOrDefault();
			ToolStrip appToolStrip = (ToolStrip)_Controls.Find(plugin.ToolStripName, true).FirstOrDefault();// (from c in _Controls where c.Name == plugin.ToolStripName select c).FirstOrDefault();

			MenuStrip ms = plugin.GetMenuStrip();
			ToolStrip ts = plugin.GetToolStrip();
			plugin.SetOnClickEvent(_OnClick);

			if (appMenuStrip != null && ms != null)
			{
				bool result = ToolStripManager.Merge(ms, appMenuStrip);
			}

			if (appToolStrip!= null && ts != null)
			{
				bool result = ToolStripManager.Merge(ts, appToolStrip);
			}
		}
	}
}
