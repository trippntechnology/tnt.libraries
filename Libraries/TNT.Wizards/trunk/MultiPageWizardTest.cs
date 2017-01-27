
namespace TNT.Wizards
{
	public partial class MultiPageWizardTest : Wizard
	{
		public MultiPageWizardTest()
		{
			InitializeComponent();
		}

		protected override bool CanChangePanel(WizardPanel panel)
		{
			return textBox1.Text == "Yes";
		}
	}
}
