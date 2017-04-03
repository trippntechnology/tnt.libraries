using Data;
using System;
using System.Windows.Forms;
using TNT.Plugin.Manager;

namespace PluginManagerTest
{
	public partial class Form1 : Form
	{
		Manager _Manager = null;
		public Form1()
		{
			InitializeComponent();

			_Manager = new Manager(Controls, pluginOnClick, @"C:\Users\stripp\repos\csharp\Libraries\TNT.Plugin.Manager\Tests\Plugins\bin\Debug");
		}

		private void pluginOnClick(object arg1, EventArgs arg2)
		{
			ToolStripItem tsi = arg1 as ToolStripItem;
			Plugin p = tsi.Tag as Plugin;

			ApplicationData data = new ApplicationData("This is the name field in the app data");
			p.Execute(this, data);
		}

		private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
		{

		}

		private void toolStripSplitButton1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Button click");
		}
	}
}
