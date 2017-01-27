namespace Test.FormPlugin
{
	partial class PluginForm
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
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menu1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menu2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menu3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu1ToolStripMenuItem,
            this.menu2ToolStripMenuItem,
            this.menu3ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(115, 70);
			// 
			// menu1ToolStripMenuItem
			// 
			this.menu1ToolStripMenuItem.Name = "menu1ToolStripMenuItem";
			this.menu1ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.menu1ToolStripMenuItem.Text = "Menu 1";
			// 
			// menu2ToolStripMenuItem
			// 
			this.menu2ToolStripMenuItem.Name = "menu2ToolStripMenuItem";
			this.menu2ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.menu2ToolStripMenuItem.Text = "Menu 2";
			// 
			// menu3ToolStripMenuItem
			// 
			this.menu3ToolStripMenuItem.Name = "menu3ToolStripMenuItem";
			this.menu3ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.menu3ToolStripMenuItem.Text = "Menu 3";
			// 
			// PluginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.Name = "PluginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "PluginForm";
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menu1ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menu2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menu3ToolStripMenuItem;
	}
}