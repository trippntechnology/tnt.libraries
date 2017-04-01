﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
		/// <param name="text">Text to display</param>
		/// <param name="image">Image to display</param>
		/// <param name="toolTipText">ToolTipText to display</param>
		/// <returns></returns>
		protected ToolStripItem CreateToolStripItem<T>(string text, Image image, string toolTipText) where T : ToolStripItem, new()
		{
			ToolStripItem item = new T();

			item.Text = text;
			item.Image = image;
			item.ToolTipText = toolTipText;
			item.Tag = this;

			_ToolStripItems.Add(item);

			return item;
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
		/// Implement to associate an action with the <see cref="Plugin"/>
		/// </summary>
		/// <param name="owner">Calling application's window</param>
		/// <param name="content">Content from the application that can be accessed</param>
		public abstract void Execute(IWin32Window owner, IApplicationData content);

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
	}
}
