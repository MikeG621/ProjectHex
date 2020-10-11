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
 * [ADD] added update<T>(,) for titlebar marking
 * [UPD] init default encoding
 */

using System;
using System.Text;
using System.Windows.Forms;

namespace Idmr.ProjectHex
{
	public partial class ProjectEditorDialog : Form
	{
		bool _loading = false;
		readonly MainForm _parent;

		public ProjectEditorDialog(ProjectFile project, MainForm parent)
		{
			LoadedProject = project;
			_parent = parent;

			InitializeComponent();

			startup();
		}

		public ProjectEditorDialog(string projectFile, MainForm parent)
		{
			LoadedProject = new ProjectFile(projectFile);
			_parent = parent;

			InitializeComponent();

			startup();
		}

		public ProjectFile LoadedProject { get; private set; } = null;

		#region methods
		void loadProject()
		{
			_loading = true;
			Text = "Project Editor - " + LoadedProject.Name;
			txtProjectName.Text = LoadedProject.Name;
			txtWildcard.Text = LoadedProject.Wildcard;
			if (LoadedProject.Length != -1)
			{
				chkFixedLength.Checked = true;
				numFileLength.Value = LoadedProject.Length;
			}
			else chkFixedLength.Checked = false;
			cboDefEncoding.SelectedIndex = getValueFromEncoding(ProjectFile.StringVar.DefaultEncoding);
			chkDefNull.Checked = ProjectFile.StringVar.DefaultNullTermed;
			numDefTrue.Value = ProjectFile.BoolVar.DefaultTrueValue;
			numDefFalse.Value = ProjectFile.BoolVar.DefaultFalseValue;
			txtProjectComments.Text = LoadedProject.Comment;
			lstItems.Items.Clear();
			foreach (ProjectFile.Var v in LoadedProject.Properties)
			{
				string item = formatItem(v);
				lstItems.Items.Add(item);
			}
			cboCollType.Items.Clear();
			foreach (ProjectFile.DefinitionVar v in LoadedProject.Types)
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

		T update<T>(T oldValue, T newValue)
		{
			if (!Text.Contains("*") && oldValue.ToString() != newValue.ToString()) Text += "*";
			return newValue;
		}

		/// <summary>Scans <see cref="ProjectFile.Properties"/> for <see cref="ProjectFile.VarType.Error"/> and <see cref="ProjectFile.VarType.Undefined"/> and adjusts the title text accordingly.</summary>
		void checkTypes()
		{
			string err = " !ERROR!";
			bool error = false;
			foreach (ProjectFile.Var prop in LoadedProject.Properties)
				if (prop.Type == ProjectFile.VarType.Error || prop.Type == ProjectFile.VarType.Undefined)
					error = true;
			if (!error) Text = Text.Replace(err, "");
			else if (!Text.Contains(err)) Text += err;
		}

		void startup()
		{
			// placeholder
		}
		#endregion methods

		#region controls
		private void form_Resize(object sender, EventArgs e)
		{
			pnlSettings.Left = Width - 386;
			lstItems.Width = Width - 626;
		}

		#region menu
		private void miNew_Click(object sender, EventArgs e)
		{
			if (Text.Contains("*"))
			{
				DialogResult res = MessageBox.Show("Project has been modified, do you wish to save?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (res == DialogResult.Cancel) return;
				else if (res == DialogResult.Yes) LoadedProject.SaveProject(); // if FileName isn't defined, SaveProject will call a save dialog
			}
			LoadedProject = new ProjectFile();
		}

		private void miOpen_Click(object sender, EventArgs e)
		{
			DialogResult res = opnProject.ShowDialog();
			if (res == DialogResult.OK)
			{
				LoadedProject = new ProjectFile(opnProject.FileName);
				loadProject();
			}
		}

		private void miSave_Click(object sender, EventArgs e)
		{
			if (Text.Contains("*"))
			{
				LoadedProject.SaveProject();	// if FileName isn't defined, SaveProject will call a save dialog
				Text = Text.Replace("*", "");
			}
		}

		private void miSaveAs_Click(object sender, EventArgs e)
		{
			DialogResult res = savProject.ShowDialog();
			if (res == DialogResult.OK)
			{
				LoadedProject.SaveProject(savProject.FileName);
				Text = Text.Replace("*", "");
			}
		}

		private void miApply_Click(object sender, EventArgs e)
		{
			if (Text.Contains("*"))
			{
				DialogResult res = MessageBox.Show("Project has been modified, must be saved prior to application.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				if (res == DialogResult.Cancel) return;
				else if (res == DialogResult.OK) LoadedProject.SaveProject();
			}
			_parent.ApplyProject(LoadedProject);
		}

		private void miClose_Click(object sender, EventArgs e)
		{
			if (Text.Contains("*"))
			{
				DialogResult res = MessageBox.Show("Project has been modified, do you wish to save?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (res == DialogResult.Cancel) return;
				else if (res == DialogResult.Yes) LoadedProject.SaveProject(); // if FileName isn't defined, SaveProject will call a save dialog
			}
			Close();
		}

		private void miType_Click(object sender, EventArgs e)
		{
			TypeEditorDialog dlgType = new TypeEditorDialog(LoadedProject);
			dlgType.ShowDialog();
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
			if (lstItems.SelectedIndex == -1 || LoadedProject.Properties == null) return;
			ProjectFile.Var v = LoadedProject.Properties[lstItems.SelectedIndex];
			txtOffset.Text = (v.RawOffset != "-1" ? v.RawOffset : "");
			cboType.SelectedIndex = (int)v.Type;
			txtName.Text = v.Name;
			chkInput.Checked = (v.ID != -1 && v.Type != ProjectFile.VarType.Collection);
			lblID.Text = "ID: " + (chkInput.Checked ? v.ID.ToString() : "0");
			chkValidate.Checked = v.IsValidated;
			txtDefault.Text = (v.DefaultValue != null ? v.DefaultValue.ToString() : "");
			txtComment.Text = v.Comment;
			if (v.Values != null)
			{
				chkArray.Checked = true;
				txtArrayQty.Text = v.RawQuantity;
				txtArrayNames.Text = (v.Values.Names != null ? string.Join(",", v.Values.Names) : "");
				txtArrayNames.Enabled = !v.HasDynamicQuantity;
			}
			else chkArray.Checked = false;
			if (v.Type == ProjectFile.VarType.Bool)
			{
				numBoolFalse.Value = ((ProjectFile.BoolVar)v).FalseValue;
				numBoolTrue.Value = ((ProjectFile.BoolVar)v).TrueValue;
			}
			if (v.Type == ProjectFile.VarType.Collection) cboCollType.SelectedIndex = LoadedProject.Types.GetIndexByID(v.ID);
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
			if (LoadedProject == null || !chkFixedLength.Checked) return;
			LoadedProject.Length = (long)numFileLength.Value;
		}

		private void txtProjectComments_Leave(object sender, EventArgs e)
		{
			if (LoadedProject == null) return;
			LoadedProject.Comment = update(LoadedProject.Comment, txtProjectComments.Text);
		}
		private void txtProjectName_Leave(object sender, EventArgs e)
		{
			if (LoadedProject == null || LoadedProject.Name == txtProjectName.Text) return;	// making the exception to pre-emptive check due to Name validation within ProjectFile
			txtProjectName.Text = txtProjectName.Text.Replace("*", "");
			LoadedProject.Name = txtProjectName.Text;
			Text = "Project Editor - " + LoadedProject.Name + "*";
		}
		private void txtWildcard_Leave(object sender, EventArgs e)
		{
			if (LoadedProject == null) return;
			LoadedProject.Wildcard = update(LoadedProject.Wildcard, txtWildcard.Text);
		}

		#region project defaults
		private void cboDefEncoding_Leave(object sender, EventArgs e)
		{
			if (LoadedProject == null) return;
			ProjectFile.StringVar.DefaultEncoding = update(ProjectFile.StringVar.DefaultEncoding, getEncodingFromValue(cboDefEncoding.SelectedIndex));
		}

		private void chkDefNull_Leave(object sender, EventArgs e)
		{
			if (LoadedProject == null) return;
			ProjectFile.StringVar.DefaultNullTermed = update(ProjectFile.StringVar.DefaultNullTermed, chkDefNull.Checked);
		}

		private void numDefTrue_Leave(object sender, EventArgs e)
		{
			if (LoadedProject == null) return;
			ProjectFile.BoolVar.DefaultTrueValue = update(ProjectFile.BoolVar.DefaultTrueValue, (byte)numDefTrue.Value);
		}
		private void numDefFalse_Leave(object sender, EventArgs e)
		{
			if (LoadedProject == null) return;
			ProjectFile.BoolVar.DefaultFalseValue = update(ProjectFile.BoolVar.DefaultFalseValue, (byte)numDefFalse.Value);
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
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			//TODO: convert types and save
			checkTypes();
		}

		private void chkArray_CheckedChanged(object sender, EventArgs e)
		{
			grpArray.Enabled = chkArray.Checked;
			if (_loading || LoadedProject == null || lstItems.SelectedIndex == -1) return;
			lstItems.Items[lstItems.SelectedIndex] = formatItem(LoadedProject.Properties[lstItems.SelectedIndex]);
		}
		private void chkInput_CheckedChanged(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			lblID.Text = "ID: " + (chkInput.Checked ? LoadedProject.Properties[lstItems.SelectedIndex].ID.ToString() : "0");
		}
		private void chkInput_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			ProjectFile.Var v = LoadedProject.Properties[lstItems.SelectedIndex];
			int oldID = v.ID;
			if ((oldID == -1 && !chkInput.Checked) || (oldID != -1 && chkInput.Checked)) return;
			if (chkInput.Checked) LoadedProject.Properties.AssignNextID(v);
			else LoadedProject.Properties.RemoveID(v);
			lstItems.Items[lstItems.SelectedIndex] = formatItem(v);
		}
		private void chkValidate_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			LoadedProject.Properties[lstItems.SelectedIndex].IsValidated = update(LoadedProject.Properties[lstItems.SelectedIndex].IsValidated, chkValidate.Checked);
			lstItems.Items[lstItems.SelectedIndex] = formatItem(LoadedProject.Properties[lstItems.SelectedIndex]);
		}

		private void txtComment_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			LoadedProject.Properties[lstItems.SelectedIndex].Comment = update(LoadedProject.Properties[lstItems.SelectedIndex].Comment, txtComment.Text);
		}
		private void txtDefault_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			string old = (LoadedProject.Properties[lstItems.SelectedIndex].DefaultValue != null ? LoadedProject.Properties[lstItems.SelectedIndex].DefaultValue.ToString() : "");
			LoadedProject.Properties[lstItems.SelectedIndex].DefaultValue = update(old, txtDefault.Text);
			lstItems.Items[lstItems.SelectedIndex] = formatItem(LoadedProject.Properties[lstItems.SelectedIndex]);
		}
		private void txtName_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			LoadedProject.Properties[lstItems.SelectedIndex].Name = update(LoadedProject.Properties[lstItems.SelectedIndex].Name, txtName.Text);
			lstItems.Items[lstItems.SelectedIndex] = formatItem(LoadedProject.Properties[lstItems.SelectedIndex]);
		}
		private void txtOffset_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			LoadedProject.Properties[lstItems.SelectedIndex].RawOffset = update(LoadedProject.Properties[lstItems.SelectedIndex].RawOffset, txtOffset.Text);
			lstItems.Items[lstItems.SelectedIndex] = formatItem(LoadedProject.Properties[lstItems.SelectedIndex]);
		}
		#endregion general item
		#region type-specific stuff
		private void cboCollType_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			int newType = -1;
			for (int i = 0; i < LoadedProject.Types.Count; i++)
				if (LoadedProject.Types[i].Name == cboCollType.Text)
				{
					newType = i;
					break;
				}
			if (newType == -1)
			{
				cboCollType.SelectedIndex = LoadedProject.Types.GetIndexByID(LoadedProject.Properties[lstItems.SelectedIndex].ID);
				return;
			}
			((ProjectFile.CollectionVar)LoadedProject.Properties[lstItems.SelectedIndex]).ChangeID(update(LoadedProject.Properties[lstItems.SelectedIndex].ID, LoadedProject.Types[newType].ID));
			lstItems.Items[lstItems.SelectedIndex] = formatItem(LoadedProject.Properties[lstItems.SelectedIndex]);
		}
		private void cboEncoding_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			try { ((ProjectFile.StringVar)LoadedProject.Properties[lstItems.SelectedIndex]).Encoding = update(((ProjectFile.StringVar)LoadedProject.Properties[lstItems.SelectedIndex]).Encoding, getEncodingFromValue(cboEncoding.SelectedIndex)); }
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				cboEncoding.SelectedIndex = getValueFromEncoding(((ProjectFile.StringVar)LoadedProject.Properties[lstItems.SelectedIndex]).Encoding);
			}
		}

		private void chkNullTermed_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			try { ((ProjectFile.StringVar)LoadedProject.Properties[lstItems.SelectedIndex]).NullTermed = update(((ProjectFile.StringVar)LoadedProject.Properties[lstItems.SelectedIndex]).NullTermed, chkNullTermed.Checked); }
			catch (InvalidOperationException x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				chkNullTermed.Checked = ((ProjectFile.StringVar)LoadedProject.Properties[lstItems.SelectedIndex]).NullTermed;
			}
		}

		private void numBoolTrue_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			try { ((ProjectFile.BoolVar)(LoadedProject.Properties[lstItems.SelectedIndex])).TrueValue = update(((ProjectFile.BoolVar)(LoadedProject.Properties[lstItems.SelectedIndex])).TrueValue, (byte)numBoolTrue.Value); }
			catch (InvalidOperationException x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				numBoolTrue.Value = ((ProjectFile.BoolVar)(LoadedProject.Properties[lstItems.SelectedIndex])).TrueValue;
			}
			// not doing validation here, waiting for Save/Apply, that way it doesn't puke and require an intermediary value if swapping
		}
		private void numBoolFalse_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			try { ((ProjectFile.BoolVar)(LoadedProject.Properties[lstItems.SelectedIndex])).FalseValue = update(((ProjectFile.BoolVar)(LoadedProject.Properties[lstItems.SelectedIndex])).FalseValue, (byte)numBoolFalse.Value); }
			catch (InvalidOperationException x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				numBoolFalse.Value = ((ProjectFile.BoolVar)(LoadedProject.Properties[lstItems.SelectedIndex])).FalseValue;
			}
		}

		private void txtArrayNames_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			ProjectFile.Var v = LoadedProject.Properties[lstItems.SelectedIndex];
			string[] oldNames = v.Values.Names;
			try
			{
				string[] names = txtArrayNames.Text.Split(',');
				for (int i = 0; i < v.Quantity; i++)
				{
					try { v.Values.Names[i] = update(v.Values.Names[i], names[i]); }
					catch
					{
						v.Values.Names[i] = update(v.Values.Names[i], i.ToString());
						System.Diagnostics.Debug.WriteLine("Names assignment count mismatch: " + v.ToString() + "[" + i + "]");
					}
				}
			}
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				v.Values.Names = oldNames;
			}
			txtArrayNames.Text = (v.Values.Names != null ? string.Join(",", v.Values.Names) : "");
		}
		private void txtArrayQty_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			ProjectFile.Var v = LoadedProject.Properties[lstItems.SelectedIndex];
			string[] oldNames = v.Values.Names;
			try
			{
				v.RawQuantity = update(v.RawQuantity, txtArrayQty.Text);
				if (v.HasDynamicQuantity)
				{
					v.Values.Names = null;
					txtArrayNames.Text = "";
				}
				else if (oldNames != null)
				{
					v.Values.Names = new string[v.Quantity];
					for (int i = 0; i < v.Values.Names.Length; i++)
					{
						try { v.Values.Names[i] = oldNames[i]; }
						catch
						{
							v.Values.Names[i] = i.ToString();
							System.Diagnostics.Debug.WriteLine("Names truncated: " + v.ToString() + "[" + i + "]");
						}
					}
				}
			}
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtArrayQty.Text = v.RawQuantity;
				v.Values.Names = oldNames;
			}
			txtArrayNames.Enabled = !v.HasDynamicQuantity;
			txtArrayNames.Text = (v.Values.Names != null ? string.Join(",", v.Values.Names) : "");
			lstItems.Items[lstItems.SelectedIndex] = formatItem(v);
		}
		private void txtLength_Leave(object sender, EventArgs e)
		{
			if (lstItems.SelectedIndex == -1 || LoadedProject == null || LoadedProject.Properties == null) return;
			try { ((ProjectFile.StringVar)LoadedProject.Properties[lstItems.SelectedIndex]).RawLength = update(((ProjectFile.StringVar)LoadedProject.Properties[lstItems.SelectedIndex]).RawLength, txtLength.Text); }
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				txtLength.Text = LoadedProject.Properties[lstItems.SelectedIndex].RawLength;
			}
		}
		#endregion type-specific stuff

		#endregion controls
	}
}
