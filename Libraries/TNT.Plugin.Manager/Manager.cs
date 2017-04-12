using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace TNT.Plugin.Manager
{
	/// <summary>
	/// Method signature for when a tool tip changes
	/// </summary>
	/// <param name="hint"></param>
	public delegate void ToolTipChangedEventHandler(string hint);

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
		/// Event that is fired when a hint is changed
		/// </summary>
		private ToolTipChangedEventHandler _OnToolTipChanged;

		/// <summary>
		/// Initialization constructor
		/// </summary>
		/// <param name="controls">Application's controls</param>
		/// <param name="onClickHandler">Application's <see cref="EventHandler"/></param>
		/// <param name="toolTipChangedEventHandler">Application's event handler for handling a tool tip change event</param>
		public Manager(ControlCollection controls, EventHandler onClickHandler, ToolTipChangedEventHandler toolTipChangedEventHandler)
		{
			this._Controls = controls;
			this._OnClick = onClickHandler;
			this._OnToolTipChanged = toolTipChangedEventHandler;
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
		/// <param name="pluginDirectory">Directory where plugins are located</param>
		public void Register(string pluginDirectory)
		{

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
					MergePlugin(plugin);
				}
			}

		}

		/// <summary>
		/// Merges the plugin into the MenuStrip and ToolStrip
		/// </summary>
		/// <param name="plugin"><see cref="Plugin"/> to register with the <see cref="Manager"/></param>
		private void MergePlugin(Plugin plugin)
		{
			ToolStrip appMenuStrip = (ToolStrip)_Controls.Find(plugin.MenuStripName, true).FirstOrDefault();
			ToolStrip appToolStrip = (ToolStrip)_Controls.Find(plugin.ToolStripName, true).FirstOrDefault();

			MenuStrip ms = plugin.GetMenuStrip();
			ToolStrip ts = plugin.GetToolStrip();

			plugin.SetOnClickEvent(_OnClick);
			plugin.OnToolTipChanged = _OnToolTipChanged;

			if (appMenuStrip != null && ms != null)
			{
				bool result = ToolStripManager.Merge(ms, appMenuStrip);
			}

			if (appToolStrip != null && ts != null)
			{
				bool result = ToolStripManager.Merge(ts, appToolStrip);
			}
		}
	}
}
