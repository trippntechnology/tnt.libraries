using System.Drawing;
using System.Windows.Forms;

namespace TNT.Wizards
{
	public partial class WizardPanel : Panel
	{
		public string Caption { get { return _Caption.Text; } set { _Caption.Text = value; } }
		public string Description { get { return _Description.Text; } set { _Description.Text = value; } }

		public WizardPanel()
		{
			InitializeComponent();

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			using (var graphics = base.CreateGraphics())
			{
				graphics.DrawLine(new Pen(Color.Black), new Point(10, 50), new Point(base.Width - 10, 50));
			}
		}
	}
}
