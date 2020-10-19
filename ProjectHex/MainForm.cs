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
			rtMain.SelectionStart = 9;
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
			int position = 0;
			while (position < _binary.Length)
			{
				content += position.ToString("x8") + " ";
				int lineLength;
				if (_binary.Length - position > 16) lineLength = 16;
				else lineLength = _binary.Length - position;

				for (int i = 0; i < lineLength; i++)
					content += _binary[position + i].ToString("x2") + " ";
				for (int i = 16; i > lineLength; i--)
					content += "   ";   // last line, might be truncated
				for (int i = 0; i < lineLength; i++, position++)
				{
					if (_binary[position] > 32) content += (char)_binary[position];
					else content += ".";
				}
				if (position != _binary.Length) content += "\r\n";
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

		private void rtMain_SelectionChanged(object sender, EventArgs e)
		{
			//string line = rtMain.Lines[rtMain.SelectionStart / 74];
			int pos = rtMain.SelectionStart % 74;   // 74 is the full length of a complete line
			int lineStart = rtMain.SelectionStart - pos;
			if (pos < 9) pos = 9;
			if (pos < 57 && pos % 3 == 2) pos++;

			rtMain.SelectionStart = lineStart + pos;

			int offset = rtMain.SelectionStart / 74 * 16 + (pos < 57 ? (pos - 9) / 3 : pos - 57);
			txtNodeOffset.Text = offset.ToString("x8");

			_binary.Position = offset;
			lblBool.Text = _binary.CurrentValueAsBool.ToString();
			lblByte.Text = _binary.CurrentValueAsByte + " (" + _binary.CurrentValueAsSByte + ")";
			try { lblShort.Text = _binary.CurrentValueAsShort + " (" + _binary.CurrentValueAsUShort + ")"; }
			catch { lblShort.Text = "EOF"; }
			try { lblInt.Text = _binary.CurrentValueAsInt + "\r\n(" + _binary.CurrentValueAsUInt + ")"; }
			catch { lblInt.Text = "EOF"; }
			try { lblLong.Text = _binary.CurrentValueAsLong + "\r\n(" + _binary.CurrentValueAsULong + ")"; }
			catch { lblLong.Text = "EOF"; }
			try { lblSingle.Text = _binary.CurrentValueAsSingle.ToString("E8"); }
			catch { lblSingle.Text = "EOF"; }
			try { lblDouble.Text = _binary.CurrentValueAsDouble.ToString("E8"); }
			catch { lblDouble.Text = "EOF"; }
		}
	}
}
