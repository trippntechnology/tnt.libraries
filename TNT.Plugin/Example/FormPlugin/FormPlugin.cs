using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TNT.Plugin;
using System.Windows.Forms;

namespace Test.FormPlugin
{
	public class FormPlugin :Plugin
	{
		public override string Text
		{
			get { return "FormPlugin Text"; }
		}

		public override string ToolTipText
		{
			get { return "FormPlugin ToolTipText"; }
		}

		public override System.Drawing.Image Image
		{
			get { return (new PluginForm()).Icon.ToBitmap(); }
		}

		protected override object Execute(IWin32Window owner, object parameter)
		{
			PluginForm pf = new PluginForm();

			pf.ShowDialog(owner);

			return null;
		}
	}
}
