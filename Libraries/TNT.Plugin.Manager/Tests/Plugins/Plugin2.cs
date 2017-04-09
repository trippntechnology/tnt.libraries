using System;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace Plugins
{
	public class Plugin2 : PluginBase
	{
		public override string MenuStripName => "menustrip2";

		public override string ToolStripName => "toolstrip2";

		public override MenuStrip GetMenuStrip()
		{
			MenuStrip menuStrip = new MenuStrip();
			ToolStripMenuItem menu = new ToolStripMenuItem("File");

			// Causes the Menu item in this menu strip to match the merging menu strip
			menu.MergeAction = MergeAction.MatchOnly;

			// Add menu items to te menu's drop down in reverse order

			//if (TrailingMenuSeparator)
			//{
			//	menu.DropDownItems.Add(new ToolStripSeparator());
			//}

			menu.DropDownItems.Add((ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>("Plugin2", GetImage("Plugins.Images.application_put.png"), "Tool tip for plugin2"));

			//if (LeadingMenuSeparator)
			//{
			//	menu.DropDownItems.Add(new ToolStripSeparator());
			//}

			//if (MenuMergeIndex >= 0)
			//{
			//	// Set to Insert with indexes
			//	foreach (ToolStripItem item in menu.DropDownItems)
			//	{
			//		item.MergeAction = MergeAction.Insert;
			//		item.MergeIndex = MenuMergeIndex;
			//	}
			//}

			menuStrip.Items.Add(menu);

			return menuStrip;
		}

		public override ToolStrip GetToolStrip()
		{
			ToolStrip toolStrip = new ToolStrip();

			//if (TrailingButtonSeparator)
			//{
			//	toolStrip.Items.Add(new ToolStripSeparator());
			//}

			ToolStripSplitButton button = (ToolStripSplitButton)CreateToolStripItem<ToolStripSplitButton>("Plugin2", GetImage("Plugins.Images.application_put.png"), "Tool tip for plugin2");
			toolStrip.Items.Add(button);

			button.DropDownItems.Add("One", null, OnOneClick);

			//if (LeadingButtonSeparator)
			//{
			//	toolStrip.Items.Add(new ToolStripSeparator());
			//}

			//if (ToolStripMergeIndex >= 0)
			//{
			//	foreach (ToolStripItem item in toolStrip.Items)
			//	{
			//		item.MergeAction = MergeAction.Insert;
			//		item.MergeIndex = ToolStripMergeIndex;
			//	}
			//}

			return toolStrip;
		}

		private void OnOneClick(object sender, EventArgs e)
		{
			MessageBox.Show("OneClicked");
		}

		public override void Execute(IWin32Window owner, IApplicationData content)
		{
			MessageBox.Show("Plugin2");
		}
	}
}
