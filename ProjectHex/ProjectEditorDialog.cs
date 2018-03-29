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

/* CHANGELOG
 * [UPD] init default encoding
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
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
			cboDefEncoding.SelectedIndex = getValueFromEncoding(ProjectFile.StringVar.DefaultEncoding);
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
				line += "<" + item.ID + ">";	// TODO: should probably replace this with the name
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

		/// <summary>Convert <i>enc</i> into the appropriate index for a ComboBox</summary>
		/// <param name="enc">Encoding value from a <see cref="ProjectFile.StringVar"/></param>
		/// <returns>0-4 on match, otherwise -1</returns>
		int getValueFromEncoding(Encoding enc)
		{
			if (enc == Encoding.ASCII)
				return 0;
			else if (enc == Encoding.UTF8)
				return 1;
			else if (enc == Encoding.Unicode)
				return 2;
			else if (enc == Encoding.BigEndianUnicode)
				return 3;
			else if (enc == Encoding.UTF32)
				return 4;
			else return -1;
		}
		/// <summary>Convert a ComboBox index to the appropriate Encoding</summary>
		/// <param name="value">ComboBox index</param>
		/// <returns>The correct Encoding. If there's no match (-1), returns <see cref="Encoding.UTF8"/></returns>
		Encoding getEncodingFromValue(int value)
		{
			if (value == 0)
				return Encoding.ASCII;
			//else if (value == 1)
				//return Encoding.UTF8;
			else if (value == 2)
				return Encoding.Unicode;
			else if (value == 3)
				return Encoding.BigEndianUnicode;
			else if (value == 4)
				return Encoding.UTF32;
			else return Encoding.UTF8;
		}

		void markModified() { if (!Text.Contains("*")) Text += "*"; }
		#endregion methods

		private void ProjectEditorDialog_Resize(object sender, EventArgs e)
		{
			pnlSettings.Left = Width - 386;
			lstItems.Width = Width - 626;
		}

		#region controls
		#region menu
		private void miNew_Click(object sender, EventArgs e)
		{
			// TODO: miNew
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

		private void miSave_Click(object sender, EventArgs e)
		{
			if (Text.Contains("*"))
			{
				_project.SaveProject();	// if FileName isn't defined, SaveProject will call a save dialog
				Text = Text.Replace("*", "");
			}
		}

		private void miSaveAs_Click(object sender, EventArgs e)
		{
			DialogResult res = savProject.ShowDialog();
			if (res == DialogResult.OK)
			{
				_project.SaveProject(savProject.FileName);
				if (Text.Contains("*")) Text = Text.Replace("*", "");
			}
		}

		private void miApply_Click(object sender, EventArgs e)
		{
			// TODO: miApply
		}

		private void miClose_Click(object sender, EventArgs e)
		{
			// TODO: miClose
		}

		private void miType_Click(object sender, EventArgs e)
		{
			//TODO: launch Type Editor
		}
		#endregion menu
		private void chkFixedLength_CheckedChanged(object sender, EventArgs e)
		{
			numFileLength.Enabled = chkFixedLength.Checked;
			if (chkFixedLength.Checked) numFileLength.Focus();
		}

		private void cmdAdd_Click(object sender, EventArgs e)
		{
			// TODO: cmdAdd
		}
		private void cmdRemove_Click(object sender, EventArgs e)
		{
			// TODO: cmdRemove
		}
		private void cmdUp_Click(object sender, EventArgs e)
		{
			// TODO: cmdUp
		}
		private void cmdDown_Click(object sender, EventArgs e)
		{
			// TODO: cmdDown
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
				cboEncoding.SelectedIndex = getValueFromEncoding(((ProjectFile.StringVar)v).Encoding);
			}
			_loading = false;
		}

		private void numFileLength_Leave(object sender, EventArgs e)
		{
			if (_project == null || !chkFixedLength.Checked) return;
			_project.Length = (long)numFileLength.Value;
		}

		private void txtProjectComments_Leave(object sender, EventArgs e)
		{
			if (_project == null) return;
			_project.Comment = txtProjectComments.Text;
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

		#region project defaults
		private void cboDefEncoding_Leave(object sender, EventArgs e)
		{
			if (_project == null) return;
			ProjectFile.StringVar.DefaultEncoding = getEncodingFromValue(cboDefEncoding.SelectedIndex);
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
		#endregion project defaults

		#region general items
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
				if (!Text.Contains("!ERROR!")) Text += " !ERROR!";
			}
			// TODO: else scan for any Undefined or Error types before clearing alert
		}

		private void chkArray_CheckedChanged(object sender, EventArgs e)
		{
			grpArray.Enabled = chkArray.Checked;
			if (_loading || _project == null || lstItems.SelectedIndex == -1) return;
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}
		private void chkInput_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			//TODO: chkInput, needs to calc new ID and update
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}
		private void chkValidate_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			_project.Properties[lstItems.SelectedIndex].IsValidated = chkValidate.Checked;
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}

		private void txtComment_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			_project.Properties[lstItems.SelectedIndex].Comment = txtComment.Text;
		}
		private void txtDefault_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			_project.Properties[lstItems.SelectedIndex].DefaultValue = txtDefault.Text;
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}
		private void txtName_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			_project.Properties[lstItems.SelectedIndex].Name = txtName.Text;
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}
		private void txtOffset_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			_project.Properties[lstItems.SelectedIndex].RawOffset = txtOffset.Text;
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}
		#endregion general item
		#region type-specific stuff
		private void cboCollType_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			//TODO: cboCollType, work out how to back out the ID from the index
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}
		private void cboEncoding_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			try { ((ProjectFile.StringVar)_project.Properties[lstItems.SelectedIndex]).Encoding = getEncodingFromValue(cboEncoding.SelectedIndex); }
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				cboEncoding.SelectedIndex = getValueFromEncoding(((ProjectFile.StringVar)_project.Properties[lstItems.SelectedIndex]).Encoding);
			}
		}

		private void chkNullTermed_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			try { ((ProjectFile.StringVar)_project.Properties[lstItems.SelectedIndex]).NullTermed = chkNullTermed.Checked; }
			catch (InvalidOperationException x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				chkNullTermed.Checked = ((ProjectFile.StringVar)_project.Properties[lstItems.SelectedIndex]).NullTermed;
			}
		}

		private void numBoolTrue_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			try { ((ProjectFile.BoolVar)(_project.Properties[lstItems.SelectedIndex])).TrueValue = (byte)numBoolTrue.Value; }
			catch (InvalidOperationException x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				numBoolTrue.Value = ((ProjectFile.BoolVar)(_project.Properties[lstItems.SelectedIndex])).TrueValue;
			}
			// not doing validation here, waiting for Save/Apply, that way it doesn't puke and require an intermediary value if swapping
		}
		private void numBoolFalse_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			try { ((ProjectFile.BoolVar)(_project.Properties[lstItems.SelectedIndex])).FalseValue = (byte)numBoolFalse.Value; }
			catch (InvalidOperationException x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				numBoolFalse.Value = ((ProjectFile.BoolVar)(_project.Properties[lstItems.SelectedIndex])).FalseValue;
			}
		}

		private void txtArrayNames_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			//TODO: txtArrayNames, will need validation
		}
		private void txtArrayQty_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			//TODO: txtArrayQty, will need validation
			lstItems.Items[lstItems.SelectedIndex] = formatItem(_project.Properties[lstItems.SelectedIndex]);
		}
		private void txtLength_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || _project == null || _project.Properties == null) return;
			try { ((ProjectFile.StringVar)_project.Properties[lstItems.SelectedIndex]).RawLength = txtLength.Text; }
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtLength.Text = _project.Properties[lstItems.SelectedIndex].RawLength;
			}
		}
		#endregion type-specific stuff
		#endregion controls
	}
}
