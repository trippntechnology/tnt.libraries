using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TNT.Wizards
{
	/// <summary>
	/// Extend to create a <see cref="Wizard"/> form
	/// </summary>
	public partial class Wizard : Form
	{
		/// <summary>
		/// Gets the current panel index
		/// </summary>
		protected int CurrentPanelIndex { get; private set; }

		private event EventHandler OnBeforePanelShow;

		/// <summary>
		/// Gets a <see cref="List"/> of <see cref="WizardPanel"/>
		/// </summary>
		protected List<WizardPanel> WizardPanels { get { return (from Control c in this._Panels.Controls where c is WizardPanel orderby c.TabIndex select c as WizardPanel).ToList(); } }

		/// <summary>
		/// Initialization constructor
		/// </summary>
		public Wizard()
		{
			InitializeComponent();
			CurrentPanelIndex = 0;
			Application.Idle += OnApplicationIdle;
		}

		/// <summary>
		/// Implement to process while appliation is idle
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnApplicationIdle(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Implement and return false to prevent the wizard from changing panels
		/// </summary>
		/// <param name="panel"><see cref="WizardPanel"/> that is being closed</param>
		/// <returns>Returns true</returns>
		protected virtual bool CanChangePanel(WizardPanel panel)
		{
			return true;
		}

		private void ButtonClick(object sender, EventArgs e)
		{
			var panels = this.WizardPanels;

			if (sender == _Next)
			{
				if (!CanChangePanel(panels[CurrentPanelIndex]))
				{
					return;
				}

				CurrentPanelIndex++;
				_Previous.Enabled = true;

				if (CurrentPanelIndex == panels.Count - 1)
				{
					_Next.Visible = false;
					_Finish.Visible = true;
					_Finish.Enabled = true;
				}
			}
			else if (sender == _Previous)
			{
				CurrentPanelIndex--;
				_Next.Enabled = true;
				_Next.Visible = true;
				_Finish.Visible = false;

				if (CurrentPanelIndex == 0)
				{
					_Previous.Enabled = false;
				}
			}

			if (this.OnBeforePanelShow != null)
			{
				this.OnBeforePanelShow(panels[CurrentPanelIndex], null);
			}

			panels[CurrentPanelIndex].BringToFront();

			if (!panels[CurrentPanelIndex].Enabled)
			{
				ButtonClick(sender, e);
			}
		}

		private void FormLoad(object sender, EventArgs e)
		{
			var panels = this.WizardPanels;

			panels.ForEach(p => p.Dock = DockStyle.Fill);

			if (panels.Count > 0)
			{
				panels[CurrentPanelIndex].BringToFront();
			}

			if (panels.Count == 1)
			{
				_Finish.Enabled = true;
				_Finish.Visible = true;
				_Next.Visible = false;
				_Previous.Visible = false;
			}
			else if (panels.Count > 1)
			{
				_Finish.Visible = false;
				_Next.Enabled = true;
				_Previous.Visible = true;
			}
		}

	}
}
