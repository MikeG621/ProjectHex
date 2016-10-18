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
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Idmr.ProjectHex
{
	public partial class ProjectEditorDialog : Form
	{
		ProjectFile _project = null;
		bool _loading = false;

		public ProjectEditorDialog(ProjectFile project)
		{
			_project = project;

			InitializeComponent();
		}

		public ProjectEditorDialog(string projectFile)
		{
			_project = new ProjectFile(projectFile);

			InitializeComponent();
		}

		#region methods
		void loadProject()
		{
			_loading = true;
			Text = "Project Editor - " + _project.Name;
			txtProjectName.Text = _project.Name;
			txtWildcard.Text = _project.Wildcard;
			if (_project.Length != -1)
			{
				chkFixedLength.Checked = true;
				numFileLength.Value = _project.Length;
			}
			else chkFixedLength.Checked = false;
			// TODO: default string encoding
			chkDefNull.Checked = ProjectFile.StringVar.DefaultNullTermed;
			numDefTrue.Value = ProjectFile.BoolVar.DefaultTrueValue;
			numDefFalse.Value = ProjectFile.BoolVar.DefaultFalseValue;
			txtProjectComments.Text = _project.Comment;
			lstItems.Items.Clear();
			foreach (ProjectFile.Var v in _project.Properties)
			{
				string item = "";
				if (v.RawOffset != "-1") item += v.RawOffset + ": ";
				item += v.Type.ToString().ToLower();
				if (v.Type == ProjectFile.VarType.Collection)
					item += "<" + v.ID + ">";
				item += " " + v.Name;
				if (v.DefaultValue != null)
					item += " = " + v.DefaultValue;
				if (v.Type != ProjectFile.VarType.Collection && v.ID != -1)
					item += " ($" + v.ID + ")";
				if (v.IsValidated) item += "*";
				// this next one is array only, the previous 3 wouldn't've happened
				if (v.RawQuantity != "" && v.RawQuantity != "1")
					item += "[" + v.RawQuantity + "]";
				lstItems.Items.Add(item);
			}
			lstItems.SelectedIndex = 0;
		}
		#endregion

		#region controls
		private void chkFixedLength_CheckedChanged(object sender, EventArgs e)
		{
			numFileLength.Enabled = chkFixedLength.Checked;
			if (chkFixedLength.Checked) numFileLength.Focus();
		}

		private void miOpen_Click(object sender, EventArgs e)
		{
			DialogResult res = opnProject.ShowDialog();
			if (res == DialogResult.OK)
			{
				_project = new ProjectFile(opnProject.FileName);
				loadProject();
			}
		}

		private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1) return;
			ProjectFile.Var v = _project.Properties[lstItems.SelectedIndex];
			if (v.RawOffset != "-1") txtOffset.Text = v.RawOffset;
			else txtOffset.Text = "";
			cboType.SelectedIndex = (int)v.Type;
			txtName.Text = v.Name;
			chkInput.Checked = (v.ID != -1);
			lblID.Text = "ID: " + (chkInput.Checked ? v.ID.ToString() : "0");
			chkValidate.Checked = v.IsValidated;
			if (v.DefaultValue != null) txtDefault.Text = v.DefaultValue.ToString();
			else txtDefault.Text = "";
			txtComment.Text = v.Comment;
		}
		#endregion
	}
}
