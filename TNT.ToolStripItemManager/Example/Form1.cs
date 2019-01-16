﻿using System;
using System.Linq;
using System.Windows.Forms;
using Test.Groups;
using TNT.ToolStripItemManager;

namespace Test
{
	public partial class Form1 : Form
	{
		private One _One;
		private Two _Two;
		private Three _Three;
		private Four _Four;

		ToolStripItemGroupManager ItemGroupManager;
		ToolStripItemCheckboxGroupManager CheckboxItemGroupManager;

		public Form1()
		{
			InitializeComponent();

			ItemGroupManager = new ToolStripItemGroupManager(toolStripStatusLabel1);
			CheckboxItemGroupManager = new ToolStripItemCheckboxGroupManager(toolStripStatusLabel1);

			_One = ItemGroupManager.Create<One>(new ToolStripItem[] { oneToolStripMenuItem, toolStripButton1, aToolStripMenuItem }, oneToolStripMenuItem.Image, this);
			_Two = ItemGroupManager.Create<Two>(new ToolStripItem[] { twoToolStripMenuItem, toolStripSplitButton2, bToolStripMenuItem }, onClick: Open_OnMouseClick);
			_Three = ItemGroupManager.Create<Three>(new ToolStripItem[] { threeToolStripMenuItem, toolStripButton3, cToolStripMenuItem });
			_Four = ItemGroupManager.Create<Four>(new ToolStripItem[] { toolStripButton4 });

			CheckboxItemGroupManager.CreateHome<Left>(new ToolStripItem[] { tsb1, leftToolStripMenuItem }, externalObject: label1).Checked = true;
			CheckboxItemGroupManager.Create<Center>(new ToolStripItem[] { tsb2, centerToolStripMenuItem }, externalObject: label1);
			CheckboxItemGroupManager.Create<Right>(new ToolStripItem[] { tsb3, rightToolStripMenuItem }, externalObject: label1);
		}

		private void Open_OnMouseClick(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.ShowDialog();
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{
			var checkedItemGroup = CheckboxItemGroupManager.FirstOrDefault(i => i.Value.Checked == true).Value;
			if (checkedItemGroup != null)
			{
				checkedItemGroup.Checked = false;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			CheckboxItemGroupManager.Toggle();
		}
	}
}