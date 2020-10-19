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
 
namespace Idmr.ProjectHex
{
	partial class MainForm
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
			this.opnFile = new System.Windows.Forms.OpenFileDialog();
			this.menuMain = new System.Windows.Forms.MenuStrip();
			this.miFile = new System.Windows.Forms.ToolStripMenuItem();
			this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.miTools = new System.Windows.Forms.ToolStripMenuItem();
			this.miProjectEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsMainContainer = new System.Windows.Forms.ToolStripContainer();
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.rtMain = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tvFile = new System.Windows.Forms.TreeView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmdNodeValue = new System.Windows.Forms.Button();
			this.txtNodeOffset = new System.Windows.Forms.TextBox();
			this.txtNodeValue = new System.Windows.Forms.TextBox();
			this.cboNodeType = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.lblNodeString = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblDouble = new System.Windows.Forms.Label();
			this.lblSingle = new System.Windows.Forms.Label();
			this.lblLong = new System.Windows.Forms.Label();
			this.lblInt = new System.Windows.Forms.Label();
			this.lblShort = new System.Windows.Forms.Label();
			this.lblByte = new System.Windows.Forms.Label();
			this.lblBool = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.menuMain.SuspendLayout();
			this.tsMainContainer.ContentPanel.SuspendLayout();
			this.tsMainContainer.SuspendLayout();
			this.tsMain.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// opnFile
			// 
			this.opnFile.DefaultExt = "tie";
			this.opnFile.Filter = "Mission Files|*.tie";
			this.opnFile.FileOk += new System.ComponentModel.CancelEventHandler(this.opnFile_FileOk);
			// 
			// menuMain
			// 
			this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miTools});
			this.menuMain.Location = new System.Drawing.Point(0, 0);
			this.menuMain.Name = "menuMain";
			this.menuMain.Size = new System.Drawing.Size(1023, 24);
			this.menuMain.TabIndex = 0;
			this.menuMain.Text = "menuStrip1";
			// 
			// miFile
			// 
			this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpen});
			this.miFile.Name = "miFile";
			this.miFile.Size = new System.Drawing.Size(37, 20);
			this.miFile.Text = "&File";
			// 
			// miOpen
			// 
			this.miOpen.Name = "miOpen";
			this.miOpen.Size = new System.Drawing.Size(103, 22);
			this.miOpen.Text = "&Open";
			this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
			// 
			// miTools
			// 
			this.miTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miProjectEditor});
			this.miTools.Name = "miTools";
			this.miTools.Size = new System.Drawing.Size(46, 20);
			this.miTools.Text = "&Tools";
			// 
			// miProjectEditor
			// 
			this.miProjectEditor.Name = "miProjectEditor";
			this.miProjectEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.miProjectEditor.Size = new System.Drawing.Size(185, 22);
			this.miProjectEditor.Text = "Project &Editor";
			this.miProjectEditor.Click += new System.EventHandler(this.miProjectEditor_Click);
			// 
			// tsMainContainer
			// 
			// 
			// tsMainContainer.ContentPanel
			// 
			this.tsMainContainer.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.tsMainContainer.ContentPanel.Controls.Add(this.tsMain);
			this.tsMainContainer.ContentPanel.Size = new System.Drawing.Size(1012, 27);
			this.tsMainContainer.LeftToolStripPanelVisible = false;
			this.tsMainContainer.Location = new System.Drawing.Point(0, 521);
			this.tsMainContainer.Name = "tsMainContainer";
			this.tsMainContainer.RightToolStripPanelVisible = false;
			this.tsMainContainer.Size = new System.Drawing.Size(1012, 27);
			this.tsMainContainer.TabIndex = 1;
			this.tsMainContainer.Text = "toolStripContainer1";
			this.tsMainContainer.TopToolStripPanelVisible = false;
			// 
			// tsMain
			// 
			this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
			this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.tsMain.Size = new System.Drawing.Size(89, 25);
			this.tsMain.TabIndex = 0;
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(86, 22);
			this.toolStripLabel1.Text = "toolStripLabel1";
			// 
			// rtMain
			// 
			this.rtMain.DetectUrls = false;
			this.rtMain.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtMain.Location = new System.Drawing.Point(201, 43);
			this.rtMain.Name = "rtMain";
			this.rtMain.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.rtMain.Size = new System.Drawing.Size(614, 477);
			this.rtMain.TabIndex = 2;
			this.rtMain.Text = "00000000 00                                              .";
			this.rtMain.SelectionChanged += new System.EventHandler(this.rtMain_SelectionChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(200, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(496, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Address  0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F  Dump";
			// 
			// tvFile
			// 
			this.tvFile.Location = new System.Drawing.Point(0, 43);
			this.tvFile.Name = "tvFile";
			this.tvFile.Size = new System.Drawing.Size(194, 472);
			this.tvFile.TabIndex = 4;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmdNodeValue);
			this.groupBox1.Controls.Add(this.txtNodeOffset);
			this.groupBox1.Controls.Add(this.txtNodeValue);
			this.groupBox1.Controls.Add(this.cboNodeType);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.lblNodeString);
			this.groupBox1.Location = new System.Drawing.Point(821, 27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(191, 150);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Current Node";
			// 
			// cmdNodeValue
			// 
			this.cmdNodeValue.Location = new System.Drawing.Point(159, 69);
			this.cmdNodeValue.Name = "cmdNodeValue";
			this.cmdNodeValue.Size = new System.Drawing.Size(26, 23);
			this.cmdNodeValue.TabIndex = 4;
			this.cmdNodeValue.Text = "...";
			this.cmdNodeValue.UseVisualStyleBackColor = true;
			// 
			// txtNodeOffset
			// 
			this.txtNodeOffset.Location = new System.Drawing.Point(64, 97);
			this.txtNodeOffset.MaxLength = 8;
			this.txtNodeOffset.Name = "txtNodeOffset";
			this.txtNodeOffset.Size = new System.Drawing.Size(121, 20);
			this.txtNodeOffset.TabIndex = 5;
			// 
			// txtNodeValue
			// 
			this.txtNodeValue.Location = new System.Drawing.Point(64, 71);
			this.txtNodeValue.Name = "txtNodeValue";
			this.txtNodeValue.Size = new System.Drawing.Size(89, 20);
			this.txtNodeValue.TabIndex = 3;
			// 
			// cboNodeType
			// 
			this.cboNodeType.FormattingEnabled = true;
			this.cboNodeType.Location = new System.Drawing.Point(64, 44);
			this.cboNodeType.Name = "cboNodeType";
			this.cboNodeType.Size = new System.Drawing.Size(121, 21);
			this.cboNodeType.TabIndex = 2;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(6, 100);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(35, 13);
			this.label12.TabIndex = 1;
			this.label12.Text = "Offset";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 74);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(34, 13);
			this.label11.TabIndex = 1;
			this.label11.Text = "Value";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 47);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(31, 13);
			this.label10.TabIndex = 1;
			this.label10.Text = "Type";
			// 
			// lblNodeString
			// 
			this.lblNodeString.AutoSize = true;
			this.lblNodeString.Location = new System.Drawing.Point(6, 20);
			this.lblNodeString.Name = "lblNodeString";
			this.lblNodeString.Size = new System.Drawing.Size(47, 13);
			this.lblNodeString.TabIndex = 0;
			this.lblNodeString.Text = "ToString";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblDouble);
			this.groupBox2.Controls.Add(this.lblSingle);
			this.groupBox2.Controls.Add(this.lblLong);
			this.groupBox2.Controls.Add(this.lblInt);
			this.groupBox2.Controls.Add(this.lblShort);
			this.groupBox2.Controls.Add(this.lblByte);
			this.groupBox2.Controls.Add(this.lblBool);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(821, 317);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(202, 198);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Current Position As...";
			// 
			// lblDouble
			// 
			this.lblDouble.Location = new System.Drawing.Point(67, 120);
			this.lblDouble.Name = "lblDouble";
			this.lblDouble.Size = new System.Drawing.Size(112, 13);
			this.lblDouble.TabIndex = 3;
			this.lblDouble.Text = "0";
			// 
			// lblSingle
			// 
			this.lblSingle.Location = new System.Drawing.Point(67, 107);
			this.lblSingle.Name = "lblSingle";
			this.lblSingle.Size = new System.Drawing.Size(112, 13);
			this.lblSingle.TabIndex = 3;
			this.lblSingle.Text = "0";
			// 
			// lblLong
			// 
			this.lblLong.Location = new System.Drawing.Point(67, 81);
			this.lblLong.Name = "lblLong";
			this.lblLong.Size = new System.Drawing.Size(129, 26);
			this.lblLong.TabIndex = 3;
			this.lblLong.Text = "0 (0)";
			// 
			// lblInt
			// 
			this.lblInt.Location = new System.Drawing.Point(67, 55);
			this.lblInt.Name = "lblInt";
			this.lblInt.Size = new System.Drawing.Size(129, 26);
			this.lblInt.TabIndex = 3;
			this.lblInt.Text = "0 (0)";
			// 
			// lblShort
			// 
			this.lblShort.Location = new System.Drawing.Point(99, 42);
			this.lblShort.Name = "lblShort";
			this.lblShort.Size = new System.Drawing.Size(86, 13);
			this.lblShort.TabIndex = 3;
			this.lblShort.Text = "0 (0)";
			// 
			// lblByte
			// 
			this.lblByte.Location = new System.Drawing.Point(99, 29);
			this.lblByte.Name = "lblByte";
			this.lblByte.Size = new System.Drawing.Size(86, 13);
			this.lblByte.TabIndex = 3;
			this.lblByte.Text = "0 (0)";
			// 
			// lblBool
			// 
			this.lblBool.Location = new System.Drawing.Point(99, 16);
			this.lblBool.Name = "lblBool";
			this.lblBool.Size = new System.Drawing.Size(86, 13);
			this.lblBool.TabIndex = 3;
			this.lblBool.Text = "False";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 120);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 13);
			this.label8.TabIndex = 2;
			this.label8.Text = "Double";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 107);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(36, 13);
			this.label7.TabIndex = 2;
			this.label7.Text = "Single";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 81);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(70, 26);
			this.label6.TabIndex = 2;
			this.label6.Text = "Long (Unsigned)";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 55);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 26);
			this.label5.TabIndex = 2;
			this.label5.Text = "Int (Unsigned)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 42);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(86, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Short (Unsigned)";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 29);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Byte (Signed)";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Boolean";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(-1, 27);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(65, 13);
			this.label9.TabIndex = 7;
			this.label9.Text = "Project Tree";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1023, 549);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tvFile);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rtMain);
			this.Controls.Add(this.tsMainContainer);
			this.Controls.Add(this.menuMain);
			this.MainMenuStrip = this.menuMain;
			this.Name = "MainForm";
			this.Text = "ProjectHex - <project> - <binary>";
			this.menuMain.ResumeLayout(false);
			this.menuMain.PerformLayout();
			this.tsMainContainer.ContentPanel.ResumeLayout(false);
			this.tsMainContainer.ContentPanel.PerformLayout();
			this.tsMainContainer.ResumeLayout(false);
			this.tsMainContainer.PerformLayout();
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog opnFile;
		private System.Windows.Forms.MenuStrip menuMain;
		private System.Windows.Forms.ToolStripMenuItem miFile;
		private System.Windows.Forms.ToolStripContainer tsMainContainer;
		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.RichTextBox rtMain;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TreeView tvFile;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblDouble;
		private System.Windows.Forms.Label lblSingle;
		private System.Windows.Forms.Label lblLong;
		private System.Windows.Forms.Label lblInt;
		private System.Windows.Forms.Label lblShort;
		private System.Windows.Forms.Label lblByte;
		private System.Windows.Forms.Label lblBool;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtNodeOffset;
		private System.Windows.Forms.TextBox txtNodeValue;
		private System.Windows.Forms.ComboBox cboNodeType;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label lblNodeString;
		private System.Windows.Forms.Button cmdNodeValue;
		private System.Windows.Forms.ToolStripMenuItem miOpen;
		private System.Windows.Forms.ToolStripMenuItem miTools;
		private System.Windows.Forms.ToolStripMenuItem miProjectEditor;
	}
}

