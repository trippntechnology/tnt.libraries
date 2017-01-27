namespace TNT.Wizards
{
	partial class WizardPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._Caption = new System.Windows.Forms.Label();
			this._Description = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _Caption
			// 
			this._Caption.AutoSize = true;
			this._Caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._Caption.Location = new System.Drawing.Point(10, 10);
			this._Caption.Name = "_Caption";
			this._Caption.Size = new System.Drawing.Size(50, 13);
			this._Caption.TabIndex = 0;
			this._Caption.Text = "Caption";
			// 
			// _Description
			// 
			this._Description.AutoSize = true;
			this._Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._Description.Location = new System.Drawing.Point(10, 30);
			this._Description.Name = "_Description";
			this._Description.Size = new System.Drawing.Size(60, 13);
			this._Description.TabIndex = 1;
			this._Description.Text = "Description";
			// 
			// WizardPanel
			// 
			this.Controls.Add(this._Description);
			this.Controls.Add(this._Caption);
			this.Size = new System.Drawing.Size(357, 339);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _Caption;
		private System.Windows.Forms.Label _Description;
	}
}
