using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using TNT.Configuration;
using TNT.Plugin;


namespace Test
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		public string Serialize<T>(T obj, Type[] expectedTypes)
		{
			using (StringWriter sw = new StringWriter())
			using (XmlTextWriter tw = new XmlTextWriter(sw))
			{
				tw.Formatting = Formatting.Indented;

				XmlSerializer ser = new XmlSerializer(typeof(T), expectedTypes);
				ser.Serialize(tw, obj);

				return sw.ToString();
			}
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				List<Plugin> plugins = XmlSection<List<Plugin>>.Deserialize("PluginSection");

				foreach (Plugin plugin in plugins)
				{
					plugin.Merge(Controls);

					plugin.MenuItem.Click += MenuItem_Click;
					plugin.Button.Click += MenuItem_Click;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void MenuItem_Click(object sender, EventArgs e)
		{
			ToolStripItem toolStripItem = sender as ToolStripItem;
			Plugin plugin = toolStripItem.Tag as Plugin;
			plugin.Execute(this, null, LicensedButton.Checked);
		}
	}
}
