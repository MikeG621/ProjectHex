using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Idmr.ProjectHex
{
	public partial class ProjectEditorDialog : Form
	{
		ProjectFile _project = null;

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
			txtProjectName.Text = _project.Name;
			txtWildcard.Text = _project.Wildcard;
			if (_project.Length != -1)
			{
				chkFixedLength.Checked = true;
				numFileLength.Value = _project.Length;
			}
			else chkFixedLength.Checked = false;
			// default string encoding
			chkDefNull.Checked = ProjectFile.StringVar.DefaultNullTermed;
			numDefTrue.Value = ProjectFile.BoolVar.DefaultTrueValue;
			numDefFalse.Value = ProjectFile.BoolVar.DefaultFalseValue;
			txtProjectComments.Text = _project.Comment;
		}
		#endregion

		#region controls
		private void chkFixedLength_CheckedChanged(object sender, EventArgs e)
		{
			numFileLength.Enabled = chkFixedLength.Checked;
			if (chkFixedLength.Checked) numFileLength.Focus();
		}
		#endregion
	}
}
