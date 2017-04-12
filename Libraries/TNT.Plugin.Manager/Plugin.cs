﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TNT.Plugin.Manager
{
	/// <summary>
	/// Abstract Plugin class. All plugins must derive from this class.
	/// </summary>
	public abstract class Plugin
	{
		/// <summary>
		///  <see cref="List{T}"/> of <see cref="ToolStripItem"/> that are generated by this Plugin
		/// </summary>
		protected List<ToolStripItem> _ToolStripItems = new List<ToolStripItem>();

		/// <summary>
		/// Call to set the <see cref="ToolStripItem"/> Click event
		/// </summary>
		/// <param name="onClick">External <see cref="EventHandler"/> that is triggered when the Plugin is clicked</param>
		public virtual void SetOnClickEvent(EventHandler onClick)
		{
			_ToolStripItems.ForEach(t =>
			{
				if (t is ToolStripSplitButton)
				{
					(t as ToolStripSplitButton).ButtonClick += onClick;
				}
				else
				{
					t.Click += onClick;
				}
			});
		}

		/// <summary>
		/// Creates a <see cref="ToolStripItem"/>
		/// </summary>
		/// <typeparam name="T">Type of <see cref="ToolStripItem"/> to create</typeparam>
		/// <returns></returns>
		protected ToolStripItem CreateToolStripItem<T>() where T : ToolStripItem, new()
		{
			ToolStripItem item = new T();

			item.Text = this.Text;
			item.Image = GetImage();
			item.ToolTipText = this.ToolTipText;
			item.Tag = this;

			item.MouseEnter += Item_MouseEnter;
			item.MouseLeave += Item_MouseLeave;

			_ToolStripItems.Add(item);

			return item;
		}

		/// <summary>
		/// Calls <see cref="OnToolTipChanged"/> if defined with an empty string
		/// </summary>
		/// <param name="sender"><see cref="ToolStripItem"/></param>
		/// <param name="e">Unused</param>
		protected void Item_MouseLeave(object sender, EventArgs e)
		{
			if (this.OnToolTipChanged != null)
			{
				this.OnToolTipChanged(string.Empty);
			}
		}

		/// <summary>
		/// Calls <see cref="OnToolTipChanged"/> if defined with the <paramref name="sender"/> tool tip text
		/// </summary>
		/// <param name="sender"><see cref="ToolStripItem"/></param>
		/// <param name="e">Unused</param>
		protected void Item_MouseEnter(object sender, EventArgs e)
		{
			if (this.OnToolTipChanged != null)
			{
				ToolStripItem item = sender as ToolStripItem;
				this.OnToolTipChanged(item.ToolTipText);
			}
		}

		/// <summary>
		/// Application's <see cref="MenuStrip"/> name where the plugin's <see cref="MenuStrip"/> should be merged
		/// </summary>
		public abstract string MenuStripName { get; }

		/// <summary>
		/// Application's <see cref="ToolStrip"/> name where the plugin's <see cref="MenuStrip"/> should be merged
		/// </summary>
		public abstract string ToolStripName { get; }

		/// <summary>
		/// Text to display on the plugin's <see cref="ToolStripItem"/>s
		/// </summary>
		public abstract string Text { get; }

		/// <summary>
		/// Tool tip to display on the plugin's <see cref="ToolStripItem"/>s
		/// </summary>
		public abstract string ToolTipText { get; }

		/// <summary>
		/// Name of the embedded resource that should be used for the plugin's image
		/// </summary>
		public abstract string EmbeddedResource { get; }

		/// <summary>
		/// Override when this plugin requires a license to execute
		/// </summary>
		public virtual bool LicenseRequired { get; } = false;

		/// <summary>
		/// Implement to associate an action with the <see cref="Plugin"/>
		/// </summary>
		/// <param name="owner">Calling application's window</param>
		/// <param name="content">Content from the application that can be accessed</param>
		public abstract void Execute(IWin32Window owner, IApplicationData content);

		/// <summary>
		/// Set to capture event for when the tool tip changes
		/// </summary>
		public ToolTipChangedEventHandler OnToolTipChanged { get; set; }

		/// <summary>
		/// Call to only execute if <paramref name="hasLicense"/> is true. If a license is not applicable, the caller only need
		/// to call <see cref="Execute(IWin32Window, IApplicationData)"/>.
		/// </summary>
		/// <param name="owner">Calling application's window</param>
		/// <param name="content">Content from the application that can be accessed</param>
		/// <param name="hasLicense">Indicates if the calling application has a license</param>
		public virtual void Execute(IWin32Window owner, IApplicationData content, bool hasLicense)
		{
			if (this.LicenseRequired && !hasLicense)
			{
				MessageBox.Show(owner, "You have discovered a feature only available in the the licensed versions of the appliation.", "Feature Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				this.Execute(owner, content);
			}
		}

		/// <summary>
		/// Implement to generate a <see cref="MenuStrip"/> that can be merged with the calling application
		/// </summary>
		/// <returns><see cref="MenuStrip"/></returns>
		public abstract MenuStrip GetMenuStrip();

		/// <summary>
		/// Implement to generate a <see cref="ToolStrip"/> that can be merged with the calling application
		/// </summary>
		/// <returns><see cref="ToolStrip"/></returns>
		public abstract ToolStrip GetToolStrip();

		/// <summary>
		/// Returns and image referenced by the <see cref="EmbeddedResource"/>
		/// </summary>
		/// <returns><see cref="Image"/> represented by the <see cref="EmbeddedResource"/></returns>
		protected Image GetImage()
		{
			System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetAssembly(this.GetType());
			Stream myStream = myAssembly.GetManifestResourceStream(this.EmbeddedResource);
			return new Bitmap(myStream);
		}
	}
}
