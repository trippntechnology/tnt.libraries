using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TNT.Plugin;

namespace Test
{
	/// <summary>
	/// Plugin that tests the IPlugin interface
	/// </summary>
	public class TestPlugin : Plugin
	{
		/// <summary>
		/// Text displayed by the plugin
		/// </summary>
		public override string Text
		{
			get { return "Test"; }
		}

		/// <summary>
		/// Tool tip text associated to the plugin
		/// </summary>
		public override string ToolTipText
		{
			get { return "This is a test"; }
		}

		/// <summary>
		/// Image associated with the plugin
		/// </summary>
		public override Image Image
		{
			get
			{
				Stream s = this.GetType().Assembly.GetManifestResourceStream("Test.Images.SecondarySource.png");
				return s == null ? null : new Bitmap(s);
			}
		}

		public override object Execute(IWin32Window owner, object parameter, bool isLicensed)
		{
			return this.Execute(owner, parameter);
		}

		/// <summary>
		/// Displays a message box
		/// </summary>
		/// <param name="parameter">Parameter</param>
		/// <returns>Null</returns>
		protected override object Execute(IWin32Window owner, object parameter)
		{
			MessageBox.Show(owner, "This is a message");
			return null;
		}
	}
}
