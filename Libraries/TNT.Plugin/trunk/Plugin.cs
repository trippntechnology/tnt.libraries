using System.Drawing;
using System.Windows.Forms;

namespace TNT.Plugin
{
	/// <summary>
	/// Base class for all TNT plugins
	/// </summary>
	public abstract class Plugin
	{
		#region Members

		/// <summary>
		/// Member representing the menu item associated with this plugin
		/// </summary>
		protected ToolStripMenuItem m_MenuItem = null;

		/// <summary>
		/// Member representing the button item associated with this plugin
		/// </summary>
		protected ToolStripButton m_ButtonItem = null;

		#endregion

		#region Abstract properties and methods

		/// <summary>
		/// Text displayed by the plugin 
		/// </summary>
		abstract public string Text { get; }

		/// <summary>
		/// Describes what the plugin does
		/// </summary>
		abstract public string ToolTipText { get; }

		/// <summary>
		/// Image associated with the plugin
		/// </summary>
		abstract public Image Image { get; }

		/// <summary>
		/// Executes the action assocated with the plugin
		/// </summary>
		/// <param name="owner">Top-level form that owns the plugin</param>
		/// <param name="parameter">Parameter containing the objects from the call that
		/// may be manipulated</param>
		/// <returns>Object representing the result</returns>
		abstract protected object Execute(IWin32Window owner, object parameter);

		#endregion

		#region Public methods

		/// <summary>
		/// Checks to see if the calling application is licensed. If it is, the abstract Execute method is 
		/// called, otherwise a message is displayed
		/// </summary>
		/// <param name="owner">Top-level form that owns the plugin</param>
		/// <param name="parameter">Parameter containing the objects from the call that
		/// may be manipulated</param>
		/// <param name="isLicensed">Indicates if the calling application is licensed</param>
		/// <returns>Object representing the result</returns>
		virtual public object Execute(IWin32Window owner, object parameter, bool isLicensed)
		{
			if (!isLicensed)
			{
				MessageBox.Show(owner, "This functionality is only available in the licensed version of the application.", "Functionality Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				return Execute(owner, parameter);
			}

			return null;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Creates the menu strip that will be merged
		/// </summary>
		virtual protected MenuStrip Menu_Strip
		{
			get
			{
				MenuStrip menuStrip = new MenuStrip();
				ToolStripMenuItem menu = new ToolStripMenuItem(DestinationMenu);

				// Causes the Menu item in this menu strip to match the merging menu strip
				menu.MergeAction = MergeAction.MatchOnly;

				// Add menu items to te menu's drop down in reverse order

				if (TrailingMenuSeparator)
				{
					menu.DropDownItems.Add(new ToolStripSeparator());
				}

				menu.DropDownItems.Add(MenuItem);

				if (LeadingMenuSeparator)
				{
					menu.DropDownItems.Add(new ToolStripSeparator());
				}

				if (MenuMergeIndex >= 0)
				{
					// Set to Insert with indexes
					foreach (ToolStripItem item in menu.DropDownItems)
					{
						item.MergeAction = MergeAction.Insert;
						item.MergeIndex = MenuMergeIndex;
					}
				}

				menuStrip.Items.Add(menu);

				return menuStrip;
			}
		}

		/// <summary>
		/// Creates the tool strip that will be merged
		/// </summary>
		virtual protected ToolStrip Tool_Strip
		{
			get
			{
				ToolStrip toolStrip = new ToolStrip();

				if (TrailingButtonSeparator)
				{
					toolStrip.Items.Add(new ToolStripSeparator());
				}

				toolStrip.Items.Add(Button);

				if (LeadingButtonSeparator)
				{
					toolStrip.Items.Add(new ToolStripSeparator());
				}

				if (ToolStripMergeIndex >= 0)
				{
					foreach (ToolStripItem item in toolStrip.Items)
					{
						item.MergeAction = MergeAction.Insert;
						item.MergeIndex = ToolStripMergeIndex;
					}
				}

				return toolStrip;
			}
		}

		/// <summary>
		/// Indicates the menu strip where the plugin should be inserted
		/// </summary>
		virtual public string DestinationMenuStrip { get; set; }

		/// <summary>
		/// Specifies the menu item where the plugin should be inserted
		/// </summary>
		virtual public string DestinationMenu { get; set; }

		/// <summary>
		/// Specifies the index where to insert the plugin into the menu items
		/// </summary>
		virtual public int MenuMergeIndex { get; set; }

		/// <summary>
		/// Specifies the tool strip where the plugin should be inserted
		/// </summary>
		virtual public string DestinationToolStrip { get; set; }

		/// <summary>
		/// Specifies the index where to insert the plugin into the tool strip
		/// </summary>
		virtual public int ToolStripMergeIndex { get; set; }

		/// <summary>
		/// Creates/Returns a menu item using the Text, ToolTipText, and Image properties
		/// </summary>
		virtual public ToolStripMenuItem MenuItem
		{
			get
			{
				if (m_MenuItem == null)
				{
					m_MenuItem = new ToolStripMenuItem(Text, Image);
					m_MenuItem.ToolTipText = ToolTipText;
					m_MenuItem.Tag = this;
				}

				return m_MenuItem;
			}
		}

		/// <summary>
		/// Creates/Returns a button using the Text, ToolTipText, and Image properties
		/// </summary>
		virtual public ToolStripButton Button
		{
			get
			{
				if (m_ButtonItem == null)
				{
					m_ButtonItem = new ToolStripButton(Text, Image) { DisplayStyle = ToolStripItemDisplayStyle.Image };
					m_ButtonItem.ToolTipText = ToolTipText;
					m_ButtonItem.Tag = this;
				}

				return m_ButtonItem;
			}
		}

		/// <summary>
		/// Indicates a separator should be added before the menu item representing the plugin
		/// </summary>
		virtual public bool LeadingMenuSeparator { get; set; }

		/// <summary>
		/// Indicates a separator should be added after the menu item representing the plugin
		/// </summary>
		virtual public bool TrailingMenuSeparator { get; set; }

		/// <summary>
		/// Indicates a separator should be added before the button item representing the plugin
		/// </summary>
		virtual public bool LeadingButtonSeparator { get; set; }

		/// <summary>
		/// Indicates a separator should be added after the button item representing the plugin
		/// </summary>
		virtual public bool TrailingButtonSeparator { get; set; }

		#endregion

		#region Merge Methods

		/// <summary>
		/// Merges the plugin's menu strip into the external menu strip
		/// </summary>
		/// <param name="menuStrip">External menu strip</param>
		virtual public void Merge(MenuStrip menuStrip)
		{
			if (Menu_Strip != null && menuStrip != null)
			{
				ToolStripManager.Merge(Menu_Strip, menuStrip);
			}
		}

		/// <summary>
		/// Merges the plugin's tool strip into the external tool strip
		/// </summary>
		/// <param name="toolStrip">External tool strip</param>
		virtual public void Merge(ToolStrip toolStrip)
		{
			if (Tool_Strip != null && toolStrip != null)
			{
				ToolStripManager.Merge(Tool_Strip, toolStrip);
			}
		}

		/// <summary>
		/// Merges the plugin's menu and tool strip into the external menu and tool strip
		/// </summary>
		/// <param name="menuStrip">External menu strip</param>
		/// <param name="toolStrip">External tool strip</param>
		virtual public void Merge(MenuStrip menuStrip, ToolStrip toolStrip)
		{
			Merge(menuStrip);
			Merge(toolStrip);
		}

		/// <summary>
		/// Merges the menu and tool strips
		/// </summary>
		/// <param name="controls">List of controls that contain the destination menu and tool strips</param>
		virtual public void Merge(Control.ControlCollection controls)
		{
			MenuStrip menuStrip = FindControl<MenuStrip>(controls, DestinationMenuStrip);
			ToolStrip toolStrip = FindControl<ToolStrip>(controls, DestinationToolStrip);

			Merge(menuStrip, toolStrip);
		}

		/// <summary>
		/// Finds a control with the given key
		/// </summary>
		/// <typeparam name="T">Type of control</typeparam>
		/// <param name="controls">List of controls</param>
		/// <param name="key">Key</param>
		/// <returns>Control if found, null otherwise</returns>
		protected T FindControl<T>(Control.ControlCollection controls, string key) where T : Control
		{
			if (controls == null || string.IsNullOrEmpty(key))
			{
				return null;
			}

			Control[] ctrls = controls.Find(key, true);

			return ctrls.Length > 0 ? (T)ctrls[0] : null;
		}

		#endregion
	}
}
