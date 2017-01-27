namespace TNT.Wizards
{
	partial class Wizard
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
			this._Panels = new System.Windows.Forms.Panel();
			this._Previous = new System.Windows.Forms.Button();
			this._Next = new System.Windows.Forms.Button();
			this._Finish = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _Panels
			// 
			this._Panels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._Panels.Location = new System.Drawing.Point(0, 0);
			this._Panels.Name = "_Panels";
			this._Panels.Size = new System.Drawing.Size(486, 352);
			this._Panels.TabIndex = 0;
			// 
			// _Previous
			// 
			this._Previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._Previous.Enabled = false;
			this._Previous.Location = new System.Drawing.Point(318, 358);
			this._Previous.Name = "_Previous";
			this._Previous.Size = new System.Drawing.Size(75, 23);
			this._Previous.TabIndex = 0;
			this._Previous.Text = "Previous";
			this._Previous.UseVisualStyleBackColor = true;
			this._Previous.Click += new System.EventHandler(this.ButtonClick);
			// 
			// _Next
			// 
			this._Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._Next.Location = new System.Drawing.Point(399, 358);
			this._Next.Name = "_Next";
			this._Next.Size = new System.Drawing.Size(75, 23);
			this._Next.TabIndex = 1;
			this._Next.Text = "Next";
			this._Next.UseVisualStyleBackColor = true;
			this._Next.Click += new System.EventHandler(this.ButtonClick);
			// 
			// _Finish
			// 
			this._Finish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._Finish.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._Finish.Location = new System.Drawing.Point(399, 358);
			this._Finish.Name = "_Finish";
			this._Finish.Size = new System.Drawing.Size(75, 23);
			this._Finish.TabIndex = 2;
			this._Finish.Text = "Finish";
			this._Finish.UseVisualStyleBackColor = true;
			this._Finish.Click += new System.EventHandler(this.ButtonClick);
			// 
			// Wizard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(486, 393);
			this.Controls.Add(this._Finish);
			this.Controls.Add(this._Next);
			this.Controls.Add(this._Previous);
			this.Controls.Add(this._Panels);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Wizard";
			this.Text = "Wizard";
			this.Load += new System.EventHandler(this.FormLoad);
			this.ResumeLayout(false);

		}

		#endregion

		protected System.Windows.Forms.Panel _Panels;
		protected System.Windows.Forms.Button _Previous;
		protected System.Windows.Forms.Button _Next;
		protected System.Windows.Forms.Button _Finish;


	}
}