/*
 * Idmr.ProjectHex.exe, Project-based hex editor
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
 * with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Version: 0.1
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Idmr.ProjectHex
{
	public partial class MainForm : Form
	{
		BinaryFile _binary;
		public MainForm()
		{
			InitializeComponent();
		}

		private void opnFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_binary = new BinaryFile(opnFile.FileName);
			this.Text = "ProjectHex - " + _binary.Project.Name + " - " + _binary.FilePath;
		}

		private void miOpen_Click(object sender, EventArgs e)
		{
			opnFile.ShowDialog();
		}

	}
}
