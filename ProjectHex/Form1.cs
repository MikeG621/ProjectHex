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
	public partial class Form1 : Form
	{
		BinaryFile file;
		public Form1()
		{
			InitializeComponent();
		}

		private void cmdOpen_Click(object sender, EventArgs e)
		{
			opnFile.ShowDialog();
		}

		private void opnFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				ProjectFile pf = new ProjectFile(Application.StartupPath + "\\projects\\tiemission.xml");
                System.Diagnostics.Debug.WriteLine("Project loaded, loading Binary...");
				file = new BinaryFile(opnFile.FileName);
				lblOutput.Text = file.ProjectName;
				//ProjectFile.SByteVar sb;
				//ProjectFile.ShortVar old = (ProjectFile.ShortVar)file.Project.Properties[8].Values[0].Values[53].Values[0].Values[0];
				System.Diagnostics.Debug.WriteLine("Binary loaded");
			}
			catch { throw; }
		}
	}
}
