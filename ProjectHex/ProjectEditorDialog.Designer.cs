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
			this.cmdAdd = new System.Windows.Forms.Button();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.cmdUp = new System.Windows.Forms.Button();
			this.cmdDown = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cboType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.chkInput = new System.Windows.Forms.CheckBox();
			this.lblID = new System.Windows.Forms.Label();
			this.chkArray = new System.Windows.Forms.CheckBox();
			this.grpArray = new System.Windows.Forms.GroupBox();
			this.txtArrayNames = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtArrayQty = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.chkValidate = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtDefault = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txtProjectName = new System.Windows.Forms.TextBox();
			this.txtWildcard = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.cboDefEncoding = new System.Windows.Forms.ComboBox();
			this.chkDefNull = new System.Windows.Forms.CheckBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.numDefTrue = new System.Windows.Forms.NumericUpDown();
			this.numDefFalse = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtProjectComments = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.lstItems = new System.Windows.Forms.ListBox();
			this.numFileLength = new System.Windows.Forms.NumericUpDown();
			this.chkFixedLength = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.txtOffset = new System.Windows.Forms.TextBox();
			this.grpBool = new System.Windows.Forms.GroupBox();
			this.label15 = new System.Windows.Forms.Label();
			this.numBoolFalse = new System.Windows.Forms.NumericUpDown();
			this.label16 = new System.Windows.Forms.Label();
			this.numBoolTrue = new System.Windows.Forms.NumericUpDown();
			this.grpCollection = new System.Windows.Forms.GroupBox();
			this.cboCollType = new System.Windows.Forms.ComboBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.grpString = new System.Windows.Forms.GroupBox();
			this.txtLength = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.cboEncoding = new System.Windows.Forms.ComboBox();
			this.label19 = new System.Windows.Forms.Label();
			this.chkNullTermed = new System.Windows.Forms.CheckBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.miFile = new System.Windows.Forms.ToolStripMenuItem();
			this.miNew = new System.Windows.Forms.ToolStripMenuItem();
			this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.miSave = new System.Windows.Forms.ToolStripMenuItem();
			this.miSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.miApply = new System.Windows.Forms.ToolStripMenuItem();
			this.miClose = new System.Windows.Forms.ToolStripMenuItem();
			this.miTools = new System.Windows.Forms.ToolStripMenuItem();
			this.miType = new System.Windows.Forms.ToolStripMenuItem();
			this.pnlSettings = new System.Windows.Forms.Panel();
			this.opnProject = new System.Windows.Forms.OpenFileDialog();
			this.savProject = new System.Windows.Forms.SaveFileDialog();
			this.grpArray.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numDefTrue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDefFalse)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numFileLength)).BeginInit();
			this.grpBool.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBoolFalse)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numBoolTrue)).BeginInit();
			this.grpCollection.SuspendLayout();
			this.grpString.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.pnlSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdAdd
			// 
			this.cmdAdd.Location = new System.Drawing.Point(234, 279);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(90, 23);
			this.cmdAdd.TabIndex = 1;
			this.cmdAdd.Text = "&Add item";
			this.cmdAdd.UseVisualStyleBackColor = true;
			// 
			// cmdRemove
			// 
			this.cmdRemove.Location = new System.Drawing.Point(234, 308);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(90, 23);
			this.cmdRemove.TabIndex = 2;
			this.cmdRemove.Text = "&Remove item";
			this.cmdRemove.UseVisualStyleBackColor = true;
			// 
			// cmdUp
			// 
			this.cmdUp.Location = new System.Drawing.Point(330, 279);
			this.cmdUp.Name = "cmdUp";
			this.cmdUp.Size = new System.Drawing.Size(90, 23);
			this.cmdUp.TabIndex = 3;
			this.cmdUp.Text = "Move item &Up";
			this.cmdUp.UseVisualStyleBackColor = true;
			// 
			// cmdDown
			// 
			this.cmdDown.Location = new System.Drawing.Point(330, 308);
			this.cmdDown.Name = "cmdDown";
			this.cmdDown.Size = new System.Drawing.Size(90, 23);
			this.cmdDown.TabIndex = 4;
			this.cmdDown.Text = "Move item &Dn";
			this.cmdDown.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Type*";
			// 
			// cboType
			// 
			this.cboType.FormattingEnabled = true;
			this.cboType.Location = new System.Drawing.Point(43, 46);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(121, 21);
			this.cboType.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 76);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Name*";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(43, 73);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(121, 20);
			this.txtName.TabIndex = 7;
			// 
			// chkInput
			// 
			this.chkInput.AutoSize = true;
			this.chkInput.Location = new System.Drawing.Point(6, 99);
			this.chkInput.Name = "chkInput";
			this.chkInput.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chkInput.Size = new System.Drawing.Size(91, 17);
			this.chkInput.TabIndex = 8;
			this.chkInput.Text = "Used as input";
			this.chkInput.UseVisualStyleBackColor = true;
			// 
			// lblID
			// 
			this.lblID.AutoSize = true;
			this.lblID.Location = new System.Drawing.Point(104, 100);
			this.lblID.Name = "lblID";
			this.lblID.Size = new System.Drawing.Size(30, 13);
			this.lblID.TabIndex = 9;
			this.lblID.Text = "ID: 0";
			// 
			// chkArray
			// 
			this.chkArray.AutoSize = true;
			this.chkArray.Location = new System.Drawing.Point(6, 195);
			this.chkArray.Name = "chkArray";
			this.chkArray.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chkArray.Size = new System.Drawing.Size(50, 17);
			this.chkArray.TabIndex = 10;
			this.chkArray.Text = "Array";
			this.chkArray.UseVisualStyleBackColor = true;
			// 
			// grpArray
			// 
			this.grpArray.Controls.Add(this.txtArrayNames);
			this.grpArray.Controls.Add(this.label4);
			this.grpArray.Controls.Add(this.txtArrayQty);
			this.grpArray.Controls.Add(this.label3);
			this.grpArray.Enabled = false;
			this.grpArray.Location = new System.Drawing.Point(6, 218);
			this.grpArray.Name = "grpArray";
			this.grpArray.Size = new System.Drawing.Size(155, 87);
			this.grpArray.TabIndex = 11;
			this.grpArray.TabStop = false;
			this.grpArray.Text = "Array Settings";
			// 
			// txtArrayNames
			// 
			this.txtArrayNames.Location = new System.Drawing.Point(9, 61);
			this.txtArrayNames.Name = "txtArrayNames";
			this.txtArrayNames.Size = new System.Drawing.Size(140, 20);
			this.txtArrayNames.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 44);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Names";
			// 
			// txtArrayQty
			// 
			this.txtArrayQty.Location = new System.Drawing.Point(35, 19);
			this.txtArrayQty.Name = "txtArrayQty";
			this.txtArrayQty.Size = new System.Drawing.Size(114, 20);
			this.txtArrayQty.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(27, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Qty*";
			// 
			// chkValidate
			// 
			this.chkValidate.AutoSize = true;
			this.chkValidate.Location = new System.Drawing.Point(6, 122);
			this.chkValidate.Name = "chkValidate";
			this.chkValidate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chkValidate.Size = new System.Drawing.Size(114, 17);
			this.chkValidate.TabIndex = 13;
			this.chkValidate.Text = "Used for validation";
			this.chkValidate.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 148);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Default";
			// 
			// txtDefault
			// 
			this.txtDefault.Location = new System.Drawing.Point(53, 145);
			this.txtDefault.Name = "txtDefault";
			this.txtDefault.Size = new System.Drawing.Size(111, 20);
			this.txtDefault.TabIndex = 15;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 31);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(73, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "Project name*";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 57);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "Matching files";
			// 
			// txtProjectName
			// 
			this.txtProjectName.Location = new System.Drawing.Point(90, 28);
			this.txtProjectName.Name = "txtProjectName";
			this.txtProjectName.Size = new System.Drawing.Size(121, 20);
			this.txtProjectName.TabIndex = 17;
			// 
			// txtWildcard
			// 
			this.txtWildcard.Location = new System.Drawing.Point(90, 54);
			this.txtWildcard.Name = "txtWildcard";
			this.txtWildcard.Size = new System.Drawing.Size(121, 20);
			this.txtWildcard.TabIndex = 18;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 22);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(81, 13);
			this.label9.TabIndex = 20;
			this.label9.Text = "String encoding";
			// 
			// cboDefEncoding
			// 
			this.cboDefEncoding.FormattingEnabled = true;
			this.cboDefEncoding.Items.AddRange(new object[] {
            "ASCII",
            "UTF8",
            "Unicode",
            "BigEndianUnicode",
            "UTF32"});
			this.cboDefEncoding.Location = new System.Drawing.Point(93, 19);
			this.cboDefEncoding.Name = "cboDefEncoding";
			this.cboDefEncoding.Size = new System.Drawing.Size(97, 21);
			this.cboDefEncoding.TabIndex = 21;
			this.cboDefEncoding.Text = "ASCII";
			// 
			// chkDefNull
			// 
			this.chkDefNull.AutoSize = true;
			this.chkDefNull.Location = new System.Drawing.Point(5, 46);
			this.chkDefNull.Name = "chkDefNull";
			this.chkDefNull.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chkDefNull.Size = new System.Drawing.Size(124, 17);
			this.chkDefNull.TabIndex = 22;
			this.chkDefNull.Text = "String null-terminated";
			this.chkDefNull.UseVisualStyleBackColor = true;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(8, 71);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(58, 13);
			this.label10.TabIndex = 23;
			this.label10.Text = "True value";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(8, 96);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(61, 13);
			this.label11.TabIndex = 23;
			this.label11.Text = "False value";
			// 
			// numDefTrue
			// 
			this.numDefTrue.Hexadecimal = true;
			this.numDefTrue.Location = new System.Drawing.Point(75, 69);
			this.numDefTrue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numDefTrue.Name = "numDefTrue";
			this.numDefTrue.Size = new System.Drawing.Size(43, 20);
			this.numDefTrue.TabIndex = 24;
			this.numDefTrue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numDefFalse
			// 
			this.numDefFalse.Hexadecimal = true;
			this.numDefFalse.Location = new System.Drawing.Point(75, 94);
			this.numDefFalse.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numDefFalse.Name = "numDefFalse";
			this.numDefFalse.Size = new System.Drawing.Size(43, 20);
			this.numDefFalse.TabIndex = 24;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.numDefFalse);
			this.groupBox1.Controls.Add(this.cboDefEncoding);
			this.groupBox1.Controls.Add(this.numDefTrue);
			this.groupBox1.Controls.Add(this.chkDefNull);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Location = new System.Drawing.Point(15, 105);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(196, 130);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Project defaults";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(12, 243);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(91, 13);
			this.label8.TabIndex = 26;
			this.label8.Text = "Project comments";
			// 
			// txtProjectComments
			// 
			this.txtProjectComments.Location = new System.Drawing.Point(12, 259);
			this.txtProjectComments.Multiline = true;
			this.txtProjectComments.Name = "txtProjectComments";
			this.txtProjectComments.Size = new System.Drawing.Size(199, 66);
			this.txtProjectComments.TabIndex = 27;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(234, 31);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(84, 13);
			this.label12.TabIndex = 29;
			this.label12.Text = "Project structure";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(3, 4);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(76, 13);
			this.label13.TabIndex = 29;
			this.label13.Text = "Item properties";
			// 
			// lstItems
			// 
			this.lstItems.FormattingEnabled = true;
			this.lstItems.Items.AddRange(new object[] {
            "0: short Magic = -1*",
            "2: short NumFlightGroups ($1)",
            "4: short NumMessages ($2)",
            "6: short NumGlobalGoals = 3 ($3)*",
            "10: byte Officers",
            "13: bool CapturedOnEject",
            "24: string EndOfMissionMessages[6]",
            "410: string IffNames[4]",
            "458: collection<1> FlightGroups[$1]",
            "collection<2> Messages[$2]",
            "collection<4> GlobalGoals[$3]",
            "collection<5> Briefing",
            "collection<6> PreMissionQuestions[10]",
            "collection<7> PostMissionQuestions[10]"});
			this.lstItems.Location = new System.Drawing.Point(234, 47);
			this.lstItems.Name = "lstItems";
			this.lstItems.ScrollAlwaysVisible = true;
			this.lstItems.Size = new System.Drawing.Size(222, 225);
			this.lstItems.TabIndex = 30;
			// 
			// numFileLength
			// 
			this.numFileLength.Enabled = false;
			this.numFileLength.Hexadecimal = true;
			this.numFileLength.Location = new System.Drawing.Point(90, 80);
			this.numFileLength.Maximum = new decimal(new int[] {
            -1,
            2147483647,
            0,
            0});
			this.numFileLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFileLength.Name = "numFileLength";
			this.numFileLength.Size = new System.Drawing.Size(121, 20);
			this.numFileLength.TabIndex = 32;
			this.numFileLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numFileLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// chkFixedLength
			// 
			this.chkFixedLength.AutoSize = true;
			this.chkFixedLength.Location = new System.Drawing.Point(12, 81);
			this.chkFixedLength.Name = "chkFixedLength";
			this.chkFixedLength.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chkFixedLength.Size = new System.Drawing.Size(72, 17);
			this.chkFixedLength.TabIndex = 33;
			this.chkFixedLength.Text = "Fixed Len";
			this.chkFixedLength.UseVisualStyleBackColor = true;
			this.chkFixedLength.CheckedChanged += new System.EventHandler(this.chkFixedLength_CheckedChanged);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(6, 22);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(35, 13);
			this.label14.TabIndex = 34;
			this.label14.Text = "Offset";
			// 
			// txtOffset
			// 
			this.txtOffset.Location = new System.Drawing.Point(43, 19);
			this.txtOffset.Name = "txtOffset";
			this.txtOffset.Size = new System.Drawing.Size(121, 20);
			this.txtOffset.TabIndex = 35;
			// 
			// grpBool
			// 
			this.grpBool.Controls.Add(this.label15);
			this.grpBool.Controls.Add(this.numBoolFalse);
			this.grpBool.Controls.Add(this.label16);
			this.grpBool.Controls.Add(this.numBoolTrue);
			this.grpBool.Enabled = false;
			this.grpBool.Location = new System.Drawing.Point(170, 4);
			this.grpBool.Name = "grpBool";
			this.grpBool.Size = new System.Drawing.Size(156, 69);
			this.grpBool.TabIndex = 36;
			this.grpBool.TabStop = false;
			this.grpBool.Text = "Bool Settings";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(6, 16);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(58, 13);
			this.label15.TabIndex = 23;
			this.label15.Text = "True value";
			// 
			// numBoolFalse
			// 
			this.numBoolFalse.Hexadecimal = true;
			this.numBoolFalse.Location = new System.Drawing.Point(73, 39);
			this.numBoolFalse.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numBoolFalse.Name = "numBoolFalse";
			this.numBoolFalse.Size = new System.Drawing.Size(43, 20);
			this.numBoolFalse.TabIndex = 24;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(6, 41);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(61, 13);
			this.label16.TabIndex = 23;
			this.label16.Text = "False value";
			// 
			// numBoolTrue
			// 
			this.numBoolTrue.Hexadecimal = true;
			this.numBoolTrue.Location = new System.Drawing.Point(73, 14);
			this.numBoolTrue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numBoolTrue.Name = "numBoolTrue";
			this.numBoolTrue.Size = new System.Drawing.Size(43, 20);
			this.numBoolTrue.TabIndex = 24;
			this.numBoolTrue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// grpCollection
			// 
			this.grpCollection.Controls.Add(this.cboCollType);
			this.grpCollection.Controls.Add(this.label18);
			this.grpCollection.Enabled = false;
			this.grpCollection.Location = new System.Drawing.Point(170, 79);
			this.grpCollection.Name = "grpCollection";
			this.grpCollection.Size = new System.Drawing.Size(156, 49);
			this.grpCollection.TabIndex = 37;
			this.grpCollection.TabStop = false;
			this.grpCollection.Text = "Collection Settings";
			// 
			// cboCollType
			// 
			this.cboCollType.FormattingEnabled = true;
			this.cboCollType.Location = new System.Drawing.Point(43, 19);
			this.cboCollType.Name = "cboCollType";
			this.cboCollType.Size = new System.Drawing.Size(107, 21);
			this.cboCollType.TabIndex = 1;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(6, 22);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(35, 13);
			this.label18.TabIndex = 0;
			this.label18.Text = "Type*";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(6, 174);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(51, 13);
			this.label17.TabIndex = 38;
			this.label17.Text = "Comment";
			// 
			// txtComment
			// 
			this.txtComment.Location = new System.Drawing.Point(63, 171);
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(101, 20);
			this.txtComment.TabIndex = 15;
			// 
			// grpString
			// 
			this.grpString.Controls.Add(this.txtLength);
			this.grpString.Controls.Add(this.label20);
			this.grpString.Controls.Add(this.cboEncoding);
			this.grpString.Controls.Add(this.label19);
			this.grpString.Controls.Add(this.chkNullTermed);
			this.grpString.Enabled = false;
			this.grpString.Location = new System.Drawing.Point(170, 134);
			this.grpString.Name = "grpString";
			this.grpString.Size = new System.Drawing.Size(156, 100);
			this.grpString.TabIndex = 39;
			this.grpString.TabStop = false;
			this.grpString.Text = "String Settings";
			// 
			// txtLength
			// 
			this.txtLength.Location = new System.Drawing.Point(52, 19);
			this.txtLength.Name = "txtLength";
			this.txtLength.Size = new System.Drawing.Size(98, 20);
			this.txtLength.TabIndex = 1;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(6, 71);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(52, 13);
			this.label20.TabIndex = 0;
			this.label20.Text = "Encoding";
			// 
			// cboEncoding
			// 
			this.cboEncoding.FormattingEnabled = true;
			this.cboEncoding.Items.AddRange(new object[] {
            "UTF-8"});
			this.cboEncoding.Location = new System.Drawing.Point(64, 68);
			this.cboEncoding.Name = "cboEncoding";
			this.cboEncoding.Size = new System.Drawing.Size(85, 21);
			this.cboEncoding.TabIndex = 21;
			this.cboEncoding.Text = "UTF-8";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(6, 22);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(40, 13);
			this.label19.TabIndex = 0;
			this.label19.Text = "Length";
			// 
			// chkNullTermed
			// 
			this.chkNullTermed.AutoSize = true;
			this.chkNullTermed.Location = new System.Drawing.Point(6, 45);
			this.chkNullTermed.Name = "chkNullTermed";
			this.chkNullTermed.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chkNullTermed.Size = new System.Drawing.Size(96, 17);
			this.chkNullTermed.TabIndex = 22;
			this.chkNullTermed.Text = "Null-terminated";
			this.chkNullTermed.UseVisualStyleBackColor = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miTools});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 40;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// miFile
			// 
			this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew,
            this.miOpen,
            this.miSave,
            this.miSaveAs,
            this.miApply,
            this.miClose});
			this.miFile.Name = "miFile";
			this.miFile.Size = new System.Drawing.Size(37, 20);
			this.miFile.Text = "&File";
			// 
			// miNew
			// 
			this.miNew.Name = "miNew";
			this.miNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.miNew.Size = new System.Drawing.Size(193, 22);
			this.miNew.Text = "&New";
			// 
			// miOpen
			// 
			this.miOpen.Name = "miOpen";
			this.miOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.miOpen.Size = new System.Drawing.Size(193, 22);
			this.miOpen.Text = "&Open...";
			this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
			// 
			// miSave
			// 
			this.miSave.Name = "miSave";
			this.miSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.miSave.Size = new System.Drawing.Size(193, 22);
			this.miSave.Text = "&Save";
			// 
			// miSaveAs
			// 
			this.miSaveAs.Name = "miSaveAs";
			this.miSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.miSaveAs.Size = new System.Drawing.Size(193, 22);
			this.miSaveAs.Text = "Save &as...";
			// 
			// miApply
			// 
			this.miApply.Name = "miApply";
			this.miApply.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.miApply.Size = new System.Drawing.Size(193, 22);
			this.miApply.Text = "A&pply";
			// 
			// miClose
			// 
			this.miClose.Name = "miClose";
			this.miClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.miClose.Size = new System.Drawing.Size(193, 22);
			this.miClose.Text = "&Close";
			// 
			// miTools
			// 
			this.miTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miType});
			this.miTools.Name = "miTools";
			this.miTools.Size = new System.Drawing.Size(47, 20);
			this.miTools.Text = "&Tools";
			// 
			// miType
			// 
			this.miType.Name = "miType";
			this.miType.Size = new System.Drawing.Size(142, 22);
			this.miType.Text = "&Type Editor...";
			// 
			// pnlSettings
			// 
			this.pnlSettings.Controls.Add(this.txtName);
			this.pnlSettings.Controls.Add(this.grpBool);
			this.pnlSettings.Controls.Add(this.label17);
			this.pnlSettings.Controls.Add(this.grpString);
			this.pnlSettings.Controls.Add(this.txtOffset);
			this.pnlSettings.Controls.Add(this.grpCollection);
			this.pnlSettings.Controls.Add(this.label14);
			this.pnlSettings.Controls.Add(this.label13);
			this.pnlSettings.Controls.Add(this.label1);
			this.pnlSettings.Controls.Add(this.label2);
			this.pnlSettings.Controls.Add(this.cboType);
			this.pnlSettings.Controls.Add(this.chkInput);
			this.pnlSettings.Controls.Add(this.lblID);
			this.pnlSettings.Controls.Add(this.chkArray);
			this.pnlSettings.Controls.Add(this.grpArray);
			this.pnlSettings.Controls.Add(this.chkValidate);
			this.pnlSettings.Controls.Add(this.label5);
			this.pnlSettings.Controls.Add(this.txtDefault);
			this.pnlSettings.Controls.Add(this.txtComment);
			this.pnlSettings.Location = new System.Drawing.Point(462, 27);
			this.pnlSettings.Name = "pnlSettings";
			this.pnlSettings.Size = new System.Drawing.Size(336, 313);
			this.pnlSettings.TabIndex = 41;
			// 
			// opnProject
			// 
			this.opnProject.DefaultExt = "prj";
			this.opnProject.Filter = "Project files |*.prj";
			// 
			// savProject
			// 
			this.savProject.DefaultExt = "prj";
			this.savProject.FileName = "NewProjectFile";
			this.savProject.Filter = "Project files|*.prj";
			// 
			// ProjectEditorDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 337);
			this.Controls.Add(this.pnlSettings);
			this.Controls.Add(this.chkFixedLength);
			this.Controls.Add(this.numFileLength);
			this.Controls.Add(this.lstItems);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.txtProjectComments);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtWildcard);
			this.Controls.Add(this.txtProjectName);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cmdDown);
			this.Controls.Add(this.cmdUp);
			this.Controls.Add(this.cmdRemove);
			this.Controls.Add(this.cmdAdd);
			this.Controls.Add(this.menuStrip1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1000, 376);
			this.MinimumSize = new System.Drawing.Size(780, 376);
			this.Name = "ProjectEditorDialog";
			this.Text = "Project Editor - <project name>";
			this.grpArray.ResumeLayout(false);
			this.grpArray.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numDefTrue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDefFalse)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numFileLength)).EndInit();
			this.grpBool.ResumeLayout(false);
			this.grpBool.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBoolFalse)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numBoolTrue)).EndInit();
			this.grpCollection.ResumeLayout(false);
			this.grpCollection.PerformLayout();
			this.grpString.ResumeLayout(false);
			this.grpString.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.pnlSettings.ResumeLayout(false);
			this.pnlSettings.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.Button cmdRemove;
		private System.Windows.Forms.Button cmdUp;
		private System.Windows.Forms.Button cmdDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.CheckBox chkInput;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.CheckBox chkArray;
		private System.Windows.Forms.GroupBox grpArray;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkValidate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtDefault;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtProjectName;
		private System.Windows.Forms.TextBox txtWildcard;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cboDefEncoding;
		private System.Windows.Forms.CheckBox chkDefNull;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown numDefTrue;
		private System.Windows.Forms.NumericUpDown numDefFalse;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtProjectComments;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ListBox lstItems;
		private System.Windows.Forms.NumericUpDown numFileLength;
		private System.Windows.Forms.CheckBox chkFixedLength;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox txtOffset;
		private System.Windows.Forms.TextBox txtArrayNames;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtArrayQty;
		private System.Windows.Forms.GroupBox grpBool;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown numBoolFalse;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.NumericUpDown numBoolTrue;
		private System.Windows.Forms.GroupBox grpCollection;
		private System.Windows.Forms.ComboBox cboCollType;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.GroupBox grpString;
		private System.Windows.Forms.TextBox txtLength;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.ComboBox cboEncoding;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.CheckBox chkNullTermed;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem miFile;
		private System.Windows.Forms.ToolStripMenuItem miNew;
		private System.Windows.Forms.ToolStripMenuItem miOpen;
		private System.Windows.Forms.ToolStripMenuItem miSave;
		private System.Windows.Forms.ToolStripMenuItem miSaveAs;
		private System.Windows.Forms.ToolStripMenuItem miClose;
		private System.Windows.Forms.ToolStripMenuItem miTools;
		private System.Windows.Forms.Panel pnlSettings;
		private System.Windows.Forms.ToolStripMenuItem miApply;
		private System.Windows.Forms.ToolStripMenuItem miType;
		private System.Windows.Forms.OpenFileDialog opnProject;
		private System.Windows.Forms.SaveFileDialog savProject;
	}
}