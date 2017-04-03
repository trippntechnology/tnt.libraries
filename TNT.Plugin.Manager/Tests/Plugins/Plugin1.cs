using Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace Plugins
{
	public class Plugin1 : PluginBase
	{
		public override string MenuStripName { get { return "menustrip1"; } }

		public override string ToolStripName { get { return "toolstrip1"; } }

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

			menu.DropDownItems.Add((ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>("Plugin1", GetImage("Plugins.Images.application_put.png"), "Tool tip for plugin1"));

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

			toolStrip.Items.Add(CreateToolStripItem<ToolStripButton>("Plugin1", GetImage("Plugins.Images.application_put.png"), "Tool tip for plugin1"));

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

		public override void Execute(IWin32Window owner, IApplicationData content)
		{
			ApplicationData appData = content as ApplicationData;
			MessageBox.Show($"Plugin1: {appData.Name}");
		}
	}
}
