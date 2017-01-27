namespace TNT.Wizards
{
	partial class SinglePageWizardTest
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.wizardPanel1 = new TNT.Wizards.WizardPanel();
			this._Panels.SuspendLayout();
			this.SuspendLayout();
			// 
			// _Panels
			// 
			this._Panels.Controls.Add(this.wizardPanel1);
			// 
			// wizardPanel1
			// 
			this.wizardPanel1.Caption = "Caption";
			this.wizardPanel1.Description = "Description";
			this.wizardPanel1.Location = new System.Drawing.Point(107, 42);
			this.wizardPanel1.Name = "wizardPanel1";
			this.wizardPanel1.Size = new System.Drawing.Size(357, 339);
			this.wizardPanel1.TabIndex = 0;
			// 
			// SinglePageWizardTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(486, 393);
			this.Name = "SinglePageWizardTest";
			this._Panels.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private WizardPanel wizardPanel1;
	}
}
