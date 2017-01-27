using System;
using System.Windows.Forms;
using TNT.Wizards;

namespace Test
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (MultiPageWizardTest tw = new MultiPageWizardTest())
			{
				tw.ShowDialog(this);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			using (SinglePageWizardTest sw = new SinglePageWizardTest())
			{
				sw.ShowDialog(this);
			}
		}
	}
}
