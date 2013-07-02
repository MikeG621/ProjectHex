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
			/*ProjectFile proj = new ProjectFile();
			proj.LoadProject("Projects\\TIEMission.xml", false);
			System.Diagnostics.Debug.WriteLine("loaded");*/
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
				file = new BinaryFile(opnFile.FileName);
				lblOutput.Text = file.ProjectName;
				//ProjectFile.SByteVar sb;
				//ProjectFile.ShortVar old = (ProjectFile.ShortVar)file.Project.Properties[8].Values[0].Values[53].Values[0].Values[0];
				System.Diagnostics.Debug.WriteLine("rawr");
			}
			catch { throw; }
		}
	}
}
