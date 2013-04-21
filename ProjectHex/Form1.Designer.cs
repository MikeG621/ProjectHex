namespace Idmr.ProjectHex
{
	partial class Form1
	{
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>Clean up any resources being used.</summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmdOpen = new System.Windows.Forms.Button();
			this.lblPath = new System.Windows.Forms.Label();
			this.lblOutput = new System.Windows.Forms.Label();
			this.opnFile = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// cmdOpen
			// 
			this.cmdOpen.Location = new System.Drawing.Point(21, 40);
			this.cmdOpen.Name = "cmdOpen";
			this.cmdOpen.Size = new System.Drawing.Size(59, 26);
			this.cmdOpen.TabIndex = 0;
			this.cmdOpen.Text = "&Open...";
			this.cmdOpen.UseVisualStyleBackColor = true;
			this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
			// 
			// lblPath
			// 
			this.lblPath.AutoSize = true;
			this.lblPath.Location = new System.Drawing.Point(86, 47);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(0, 13);
			this.lblPath.TabIndex = 1;
			// 
			// lblOutput
			// 
			this.lblOutput.AutoSize = true;
			this.lblOutput.Location = new System.Drawing.Point(34, 152);
			this.lblOutput.Name = "lblOutput";
			this.lblOutput.Size = new System.Drawing.Size(35, 13);
			this.lblOutput.TabIndex = 2;
			this.lblOutput.Text = "label1";
			// 
			// opnFile
			// 
			this.opnFile.DefaultExt = "tie";
			this.opnFile.Filter = "Mission Files|*.tie";
			this.opnFile.FileOk += new System.ComponentModel.CancelEventHandler(this.opnFile_FileOk);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.lblOutput);
			this.Controls.Add(this.lblPath);
			this.Controls.Add(this.cmdOpen);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdOpen;
		private System.Windows.Forms.Label lblPath;
		private System.Windows.Forms.Label lblOutput;
		private System.Windows.Forms.OpenFileDialog opnFile;
	}
}

