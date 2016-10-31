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
				string item = formatItem(v);
				lstItems.Items.Add(item);
			}
			cboCollType.Items.Clear();
			foreach (ProjectFile.DefinitionVar v in _project.Types)
			{
				cboCollType.Items.Add(v.Name);
			}
			lstItems.SelectedIndex = 0;
		}

		string formatItem(ProjectFile.Var item)
		{
			string line = "";
			if (item.RawOffset != "-1") line += item.RawOffset + ": ";
			line += item.Type.ToString().ToLower();
			if (item.Type == ProjectFile.VarType.Collection)
				line += "<" + item.ID + ">";
			line += " " + item.Name;
			if (item.DefaultValue != null)
				line += " = " + item.DefaultValue;
			if (item.Type != ProjectFile.VarType.Collection && item.ID != -1)
				line += " ($" + item.ID + ")";
			if (item.IsValidated) line += "*";
			// this next one is array only, the previous 3 wouldn't've happened
			if (item.RawQuantity != "" && item.RawQuantity != "1")
				line += "[" + item.RawQuantity + "]";
			return line;
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
			if (lstItems.SelectedIndex == -1 || _project.Properties == null) return;
			ProjectFile.Var v = _project.Properties[lstItems.SelectedIndex];
			if (v.RawOffset != "-1") txtOffset.Text = v.RawOffset;
			else txtOffset.Text = "";
			cboType.SelectedIndex = (int)v.Type;
			txtName.Text = v.Name;
			chkInput.Checked = (v.ID != -1 && v.Type != ProjectFile.VarType.Collection);
			lblID.Text = "ID: " + (chkInput.Checked ? v.ID.ToString() : "0");
			chkValidate.Checked = v.IsValidated;
			if (v.DefaultValue != null) txtDefault.Text = v.DefaultValue.ToString();
			else txtDefault.Text = "";
			txtComment.Text = v.Comment;
			if (v.Values != null)
			{
				chkArray.Checked = true;
				txtArrayQty.Text = v.RawQuantity;
				if (v.Values.Names != null) txtArrayNames.Text = string.Join(",", v.Values.Names);
				else txtArrayNames.Text = "";
			}
			else chkArray.Checked = false;
			if (v.Type == ProjectFile.VarType.Bool)
			{
				numBoolFalse.Value = ((ProjectFile.BoolVar)v).FalseValue;
				numBoolTrue.Value = ((ProjectFile.BoolVar)v).TrueValue;
			}
			if (v.Type == ProjectFile.VarType.Collection) cboCollType.SelectedIndex = _project.Types.GetIndexByID(v.ID);
			if (v.Type == ProjectFile.VarType.String)
			{
				txtLength.Text = v.RawLength;
				chkNullTermed.Checked = ((ProjectFile.StringVar)v).NullTermed;
				// TODO: encoding
			}
			_loading = false;
		}

		private void cboType_SelectedIndexChanged(object sender, EventArgs e)
		{
			grpBool.Enabled = (cboType.SelectedIndex == (int)ProjectFile.VarType.Bool);
			grpString.Enabled = (cboType.SelectedIndex == (int)ProjectFile.VarType.String);
			bool isCollection = (cboType.SelectedIndex == (int)ProjectFile.VarType.Collection);
			grpCollection.Enabled = isCollection;
			chkInput.Enabled = !isCollection;
			txtDefault.Enabled = !isCollection;
			if (cboType.SelectedIndex == (int)ProjectFile.VarType.Error || cboType.SelectedIndex == (int)ProjectFile.VarType.Undefined)
			{
				if (!Text.Contains("*ERROR*")) Text += " *ERROR*";
			}
			// TODO: else scan for any Undefined or Error types before clearing alert
		}

		private void chkArray_CheckedChanged(object sender, EventArgs e)
		{
			grpArray.Enabled = chkArray.Checked;
			if (_loading || _project == null || lstItems.SelectedIndex == -1) return;
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}

		private void ProjectEditorDialog_Resize(object sender, EventArgs e)
		{
			pnlSettings.Left = Width - 386;
			lstItems.Width = Width - 626;
		}
		

		private void txtDefault_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			_project.Properties[lstItems.SelectedIndex].DefaultValue = txtDefault.Text;
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}

		private void txtProjectName_Leave(object sender, EventArgs e)
		{
			if (_project == null) return;
			_project.Name = txtProjectName.Text;
		}

		private void txtWildcard_Leave(object sender, EventArgs e)
		{
			if (_project == null) return;
			_project.Wildcard = txtWildcard.Text;
		}

		private void numFileLength_Leave(object sender, EventArgs e)
		{
			if (_project == null || !chkFixedLength.Checked) return;
			_project.Length = (long)numFileLength.Value;
		}

		private void cboDefEncoding_Leave(object sender, EventArgs e)
		{
			// TODO default string encoding
		}

		private void chkDefNull_Leave(object sender, EventArgs e)
		{
			if (_project == null) return;
			ProjectFile.StringVar.DefaultNullTermed = chkDefNull.Checked;
		}

		private void numDefTrue_Leave(object sender, EventArgs e)
		{
			if (_project == null) return;
			ProjectFile.BoolVar.DefaultTrueValue = (byte)numDefTrue.Value;
		}

		private void numDefFalse_Leave(object sender, EventArgs e)
		{
			if (_project == null) return;
			ProjectFile.BoolVar.DefaultFalseValue = (byte)numDefFalse.Value;
		}

		private void txtProjectComments_Leave(object sender, EventArgs e)
		{
			if (_project == null) return;
			_project.Comment = txtProjectComments.Text;
		}

		private void txtOffset_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			_project.Properties[lstItems.SelectedIndex].RawOffset = txtOffset.Text;
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}
		#endregion
	}
}
