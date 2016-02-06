namespace Idmr.ProjectHex
{
	partial class ProjectEditorDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			this.tvItems = new System.Windows.Forms.TreeView();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.cmdUp = new System.Windows.Forms.Button();
			this.cmdDown = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tvItems
			// 
			this.tvItems.Location = new System.Drawing.Point(12, 12);
			this.tvItems.Name = "tvItems";
			this.tvItems.Size = new System.Drawing.Size(121, 245);
			this.tvItems.TabIndex = 0;
			// 
			// cmdAdd
			// 
			this.cmdAdd.Location = new System.Drawing.Point(12, 263);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(55, 23);
			this.cmdAdd.TabIndex = 1;
			this.cmdAdd.Text = "&Add";
			this.cmdAdd.UseVisualStyleBackColor = true;
			// 
			// cmdRemove
			// 
			this.cmdRemove.Location = new System.Drawing.Point(12, 292);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(55, 23);
			this.cmdRemove.TabIndex = 2;
			this.cmdRemove.Text = "&Remove";
			this.cmdRemove.UseVisualStyleBackColor = true;
			// 
			// cmdUp
			// 
			this.cmdUp.Location = new System.Drawing.Point(73, 263);
			this.cmdUp.Name = "cmdUp";
			this.cmdUp.Size = new System.Drawing.Size(60, 23);
			this.cmdUp.TabIndex = 3;
			this.cmdUp.Text = "Move &Up";
			this.cmdUp.UseVisualStyleBackColor = true;
			// 
			// cmdDown
			// 
			this.cmdDown.Location = new System.Drawing.Point(73, 292);
			this.cmdDown.Name = "cmdDown";
			this.cmdDown.Size = new System.Drawing.Size(60, 23);
			this.cmdDown.TabIndex = 4;
			this.cmdDown.Text = "Move &Dn";
			this.cmdDown.UseVisualStyleBackColor = true;
			// 
			// ProjectEditorDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(634, 326);
			this.Controls.Add(this.cmdDown);
			this.Controls.Add(this.cmdUp);
			this.Controls.Add(this.cmdRemove);
			this.Controls.Add(this.cmdAdd);
			this.Controls.Add(this.tvItems);
			this.Name = "ProjectEditorDialog";
			this.Text = "Project Editor - <project name>";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView tvItems;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.Button cmdRemove;
		private System.Windows.Forms.Button cmdUp;
		private System.Windows.Forms.Button cmdDown;
	}
}