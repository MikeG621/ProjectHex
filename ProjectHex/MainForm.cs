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
using System.IO;
using System.Windows.Forms;

namespace Idmr.ProjectHex
{
	public partial class MainForm : Form
	{
		BinaryFile _binary = new BinaryFile();
		ProjectEditorDialog _projectEditor = null;

		public MainForm()
		{
			InitializeComponent();
		}

		public void ApplyProject(ProjectFile project)
		{
			System.Diagnostics.Debug.WriteLine("tried to apply " + project.Name + " to active binary");
		}

		private void opnFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_binary = new BinaryFile(opnFile.FileName);
			Text = "ProjectHex - " + _binary.Project.Name + " - " + _binary.FilePath;

			string content = "";
			FileStream fs = null;
			try
			{
				fs = new FileStream(_binary.FilePath, FileMode.Open, FileAccess.Read);
				BinaryReader br = new BinaryReader(fs);

				rtMain.Text = "";
				byte[] line;

				while (fs.Position < fs.Length)
				{
					content += fs.Position.ToString("x8") + " ";
					if (fs.Length - fs.Position > 16) line = br.ReadBytes(16);
					else line = br.ReadBytes((int)(fs.Length - fs.Position));

					for (int i = 0; i < line.Length; i++)
					{
						content += line[i].ToString("x2") + " ";
					}
					for (int i = 16; i > line.Length; i--)
					{
						content += "   ";	// last line, might be truncated
					}
					for (int i = 0; i < line.Length; i++)
					{
						if (line[i] > 31) content += (char)line[i];
						else content += ".";
					}
					if (fs.Position != fs.Length) content += "\r\n";
				}

				fs.Close();
			}
			catch
			{
				if (fs != null) fs.Close();
			}
			rtMain.Text = content;
		}

		private void miOpen_Click(object sender, EventArgs e)
		{
			opnFile.ShowDialog();
		}

		private void miProjectEditor_Click(object sender, EventArgs e)
		{
			if (_projectEditor == null || !_projectEditor.IsHandleCreated) _projectEditor = new ProjectEditorDialog(_binary.Project, this);
			_projectEditor.Show();
		}
	}
}
