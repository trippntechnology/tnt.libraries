namespace TNT.Wizards
{
	partial class MultiPageWizardTest
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
			this.wizardPanel2 = new TNT.Wizards.WizardPanel();
			this.wizardPanel3 = new TNT.Wizards.WizardPanel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this._Panels.SuspendLayout();
			this.wizardPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _Panels
			// 
			this._Panels.Controls.Add(this.wizardPanel1);
			this._Panels.Controls.Add(this.wizardPanel3);
			this._Panels.Controls.Add(this.wizardPanel2);
			// 
			// wizardPanel1
			// 
			this.wizardPanel1.Caption = "Page 1";
			this.wizardPanel1.Controls.Add(this.textBox1);
			this.wizardPanel1.Description = "Description";
			this.wizardPanel1.Location = new System.Drawing.Point(3, 148);
			this.wizardPanel1.Name = "wizardPanel1";
			this.wizardPanel1.Size = new System.Drawing.Size(474, 204);
			this.wizardPanel1.TabIndex = 0;
			// 
			// wizardPanel2
			// 
			this.wizardPanel2.Caption = "Page 2";
			this.wizardPanel2.Description = "Description";
			this.wizardPanel2.Location = new System.Drawing.Point(36, 74);
			this.wizardPanel2.Name = "wizardPanel2";
			this.wizardPanel2.Size = new System.Drawing.Size(397, 204);
			this.wizardPanel2.TabIndex = 1;
			// 
			// wizardPanel3
			// 
			this.wizardPanel3.Caption = "Page 3";
			this.wizardPanel3.Description = "Description";
			this.wizardPanel3.Location = new System.Drawing.Point(104, 45);
			this.wizardPanel3.Name = "wizardPanel3";
			this.wizardPanel3.Size = new System.Drawing.Size(357, 260);
			this.wizardPanel3.TabIndex = 2;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(27, 70);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "Enter Yes";
			// 
			// MultiPageWizardTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(486, 393);
			this.Name = "MultiPageWizardTest";
			this._Panels.ResumeLayout(false);
			this.wizardPanel1.ResumeLayout(false);
			this.wizardPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private WizardPanel wizardPanel1;
		private WizardPanel wizardPanel2;
		private WizardPanel wizardPanel3;
		private System.Windows.Forms.TextBox textBox1;
	}
}
