namespace Idmr.ProjectHex
{
	partial class TypeEditorDialog
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
			this.label1 = new System.Windows.Forms.Label();
			this.lstTypes = new System.Windows.Forms.ListBox();
			this.cmdNewType = new System.Windows.Forms.Button();
			this.cmdTypeUp = new System.Windows.Forms.Button();
			this.cmdDeleteType = new System.Windows.Forms.Button();
			this.cmdTypeDown = new System.Windows.Forms.Button();
			this.txtTypeName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.numFileLength = new System.Windows.Forms.NumericUpDown();
			this.lblTypeID = new System.Windows.Forms.Label();
			this.txtTypeComments = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.lstItems = new System.Windows.Forms.ListBox();
			this.label12 = new System.Windows.Forms.Label();
			this.cmdDown = new System.Windows.Forms.Button();
			this.cmdUp = new System.Windows.Forms.Button();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.pnlSettings = new System.Windows.Forms.Panel();
			this.txtName = new System.Windows.Forms.TextBox();
			this.grpBool = new System.Windows.Forms.GroupBox();
			this.label15 = new System.Windows.Forms.Label();
			this.numBoolFalse = new System.Windows.Forms.NumericUpDown();
			this.label16 = new System.Windows.Forms.Label();
			this.numBoolTrue = new System.Windows.Forms.NumericUpDown();
			this.label17 = new System.Windows.Forms.Label();
			this.grpString = new System.Windows.Forms.GroupBox();
			this.txtLength = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.cboEncoding = new System.Windows.Forms.ComboBox();
			this.label19 = new System.Windows.Forms.Label();
			this.chkNullTermed = new System.Windows.Forms.CheckBox();
			this.txtOffset = new System.Windows.Forms.TextBox();
			this.grpCollection = new System.Windows.Forms.GroupBox();
			this.cboCollType = new System.Windows.Forms.ComboBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cboType = new System.Windows.Forms.ComboBox();
			this.chkInput = new System.Windows.Forms.CheckBox();
			this.lblID = new System.Windows.Forms.Label();
			this.chkArray = new System.Windows.Forms.CheckBox();
			this.grpArray = new System.Windows.Forms.GroupBox();
			this.txtArrayNames = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtArrayQty = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.chkValidate = new System.Windows.Forms.CheckBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtDefault = new System.Windows.Forms.TextBox();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.cmdClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numFileLength)).BeginInit();
			this.pnlSettings.SuspendLayout();
			this.grpBool.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBoolFalse)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numBoolTrue)).BeginInit();
			this.grpString.SuspendLayout();
			this.grpCollection.SuspendLayout();
			this.grpArray.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Types";
			// 
			// lstTypes
			// 
			this.lstTypes.FormattingEnabled = true;
			this.lstTypes.Location = new System.Drawing.Point(15, 25);
			this.lstTypes.Name = "lstTypes";
			this.lstTypes.ScrollAlwaysVisible = true;
			this.lstTypes.Size = new System.Drawing.Size(132, 212);
			this.lstTypes.TabIndex = 1;
			// 
			// cmdNewType
			// 
			this.cmdNewType.Location = new System.Drawing.Point(15, 243);
			this.cmdNewType.Name = "cmdNewType";
			this.cmdNewType.Size = new System.Drawing.Size(63, 23);
			this.cmdNewType.TabIndex = 2;
			this.cmdNewType.Text = "&New";
			this.cmdNewType.UseVisualStyleBackColor = true;
			// 
			// cmdTypeUp
			// 
			this.cmdTypeUp.Location = new System.Drawing.Point(84, 243);
			this.cmdTypeUp.Name = "cmdTypeUp";
			this.cmdTypeUp.Size = new System.Drawing.Size(63, 23);
			this.cmdTypeUp.TabIndex = 3;
			this.cmdTypeUp.Text = "Mv Up";
			this.cmdTypeUp.UseVisualStyleBackColor = true;
			// 
			// cmdDeleteType
			// 
			this.cmdDeleteType.Location = new System.Drawing.Point(15, 272);
			this.cmdDeleteType.Name = "cmdDeleteType";
			this.cmdDeleteType.Size = new System.Drawing.Size(63, 23);
			this.cmdDeleteType.TabIndex = 4;
			this.cmdDeleteType.Text = "&Delete";
			this.cmdDeleteType.UseVisualStyleBackColor = true;
			// 
			// cmdTypeDown
			// 
			this.cmdTypeDown.Location = new System.Drawing.Point(84, 272);
			this.cmdTypeDown.Name = "cmdTypeDown";
			this.cmdTypeDown.Size = new System.Drawing.Size(63, 23);
			this.cmdTypeDown.TabIndex = 5;
			this.cmdTypeDown.Text = "Mv Dn";
			this.cmdTypeDown.UseVisualStyleBackColor = true;
			// 
			// txtTypeName
			// 
			this.txtTypeName.Location = new System.Drawing.Point(223, 22);
			this.txtTypeName.Name = "txtTypeName";
			this.txtTypeName.Size = new System.Drawing.Size(103, 20);
			this.txtTypeName.TabIndex = 19;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(153, 25);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 13);
			this.label6.TabIndex = 18;
			this.label6.Text = "Type name*";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(153, 48);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkBox1.Size = new System.Drawing.Size(72, 17);
			this.checkBox1.TabIndex = 35;
			this.checkBox1.Text = "Fixed Len";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// numFileLength
			// 
			this.numFileLength.Enabled = false;
			this.numFileLength.Hexadecimal = true;
			this.numFileLength.Location = new System.Drawing.Point(231, 47);
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
			this.numFileLength.Size = new System.Drawing.Size(95, 20);
			this.numFileLength.TabIndex = 34;
			this.numFileLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numFileLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblTypeID
			// 
			this.lblTypeID.AutoSize = true;
			this.lblTypeID.Location = new System.Drawing.Point(153, 72);
			this.lblTypeID.Name = "lblTypeID";
			this.lblTypeID.Size = new System.Drawing.Size(30, 13);
			this.lblTypeID.TabIndex = 37;
			this.lblTypeID.Text = "ID: 0";
			// 
			// txtTypeComments
			// 
			this.txtTypeComments.Location = new System.Drawing.Point(156, 109);
			this.txtTypeComments.Multiline = true;
			this.txtTypeComments.Name = "txtTypeComments";
			this.txtTypeComments.Size = new System.Drawing.Size(170, 66);
			this.txtTypeComments.TabIndex = 39;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(153, 93);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(82, 13);
			this.label8.TabIndex = 38;
			this.label8.Text = "Type comments";
			// 
			// lstItems
			// 
			this.lstItems.FormattingEnabled = true;
			this.lstItems.Items.AddRange(new object[] {
            "0: string Message",
            "64: collection<8> Triggers[2]",
            "72: string EditorNote",
            "88: byte Delay",
            "89: bool TriggerAndOr"});
			this.lstItems.Location = new System.Drawing.Point(332, 25);
			this.lstItems.Name = "lstItems";
			this.lstItems.ScrollAlwaysVisible = true;
			this.lstItems.Size = new System.Drawing.Size(223, 225);
			this.lstItems.TabIndex = 45;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(332, 9);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(75, 13);
			this.label12.TabIndex = 44;
			this.label12.Text = "Type structure";
			// 
			// cmdDown
			// 
			this.cmdDown.Location = new System.Drawing.Point(428, 286);
			this.cmdDown.Name = "cmdDown";
			this.cmdDown.Size = new System.Drawing.Size(90, 23);
			this.cmdDown.TabIndex = 43;
			this.cmdDown.Text = "Move item &Dn";
			this.cmdDown.UseVisualStyleBackColor = true;
			// 
			// cmdUp
			// 
			this.cmdUp.Location = new System.Drawing.Point(428, 257);
			this.cmdUp.Name = "cmdUp";
			this.cmdUp.Size = new System.Drawing.Size(90, 23);
			this.cmdUp.TabIndex = 42;
			this.cmdUp.Text = "Move item &Up";
			this.cmdUp.UseVisualStyleBackColor = true;
			// 
			// cmdRemove
			// 
			this.cmdRemove.Location = new System.Drawing.Point(332, 286);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(90, 23);
			this.cmdRemove.TabIndex = 41;
			this.cmdRemove.Text = "&Remove item";
			this.cmdRemove.UseVisualStyleBackColor = true;
			// 
			// cmdAdd
			// 
			this.cmdAdd.Location = new System.Drawing.Point(332, 257);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(90, 23);
			this.cmdAdd.TabIndex = 40;
			this.cmdAdd.Text = "&Add item";
			this.cmdAdd.UseVisualStyleBackColor = true;
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
			this.pnlSettings.Controls.Add(this.label2);
			this.pnlSettings.Controls.Add(this.label3);
			this.pnlSettings.Controls.Add(this.cboType);
			this.pnlSettings.Controls.Add(this.chkInput);
			this.pnlSettings.Controls.Add(this.lblID);
			this.pnlSettings.Controls.Add(this.chkArray);
			this.pnlSettings.Controls.Add(this.grpArray);
			this.pnlSettings.Controls.Add(this.chkValidate);
			this.pnlSettings.Controls.Add(this.label7);
			this.pnlSettings.Controls.Add(this.txtDefault);
			this.pnlSettings.Controls.Add(this.txtComment);
			this.pnlSettings.Location = new System.Drawing.Point(561, 9);
			this.pnlSettings.Name = "pnlSettings";
			this.pnlSettings.Size = new System.Drawing.Size(336, 313);
			this.pnlSettings.TabIndex = 46;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(43, 73);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(121, 20);
			this.txtName.TabIndex = 7;
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
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(6, 174);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(51, 13);
			this.label17.TabIndex = 38;
			this.label17.Text = "Comment";
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
			// txtOffset
			// 
			this.txtOffset.Location = new System.Drawing.Point(43, 19);
			this.txtOffset.Name = "txtOffset";
			this.txtOffset.Size = new System.Drawing.Size(121, 20);
			this.txtOffset.TabIndex = 35;
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
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(6, 22);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(35, 13);
			this.label14.TabIndex = 34;
			this.label14.Text = "Offset";
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Type*";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Name*";
			// 
			// cboType
			// 
			this.cboType.FormattingEnabled = true;
			this.cboType.Location = new System.Drawing.Point(43, 46);
			this.cboType.Name = "cboType";
			this.cboType.Size = new System.Drawing.Size(121, 21);
			this.cboType.TabIndex = 6;
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
			this.grpArray.Controls.Add(this.label5);
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
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(27, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "Qty*";
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
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 148);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 13);
			this.label7.TabIndex = 14;
			this.label7.Text = "Default";
			// 
			// txtDefault
			// 
			this.txtDefault.Location = new System.Drawing.Point(53, 145);
			this.txtDefault.Name = "txtDefault";
			this.txtDefault.Size = new System.Drawing.Size(111, 20);
			this.txtDefault.TabIndex = 15;
			// 
			// txtComment
			// 
			this.txtComment.Location = new System.Drawing.Point(63, 171);
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(101, 20);
			this.txtComment.TabIndex = 15;
			// 
			// cmdClose
			// 
			this.cmdClose.Location = new System.Drawing.Point(204, 181);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(75, 23);
			this.cmdClose.TabIndex = 47;
			this.cmdClose.Text = "&Close";
			this.cmdClose.UseVisualStyleBackColor = true;
			// 
			// TypeEditorDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(896, 323);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.pnlSettings);
			this.Controls.Add(this.lstItems);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.cmdDown);
			this.Controls.Add(this.cmdUp);
			this.Controls.Add(this.cmdRemove);
			this.Controls.Add(this.cmdAdd);
			this.Controls.Add(this.txtTypeComments);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.lblTypeID);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.numFileLength);
			this.Controls.Add(this.txtTypeName);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cmdTypeDown);
			this.Controls.Add(this.cmdDeleteType);
			this.Controls.Add(this.cmdTypeUp);
			this.Controls.Add(this.cmdNewType);
			this.Controls.Add(this.lstTypes);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1000, 362);
			this.MinimumSize = new System.Drawing.Size(872, 362);
			this.Name = "TypeEditorDialog";
			this.Text = "Type Editor - <project name>";
			((System.ComponentModel.ISupportInitialize)(this.numFileLength)).EndInit();
			this.pnlSettings.ResumeLayout(false);
			this.pnlSettings.PerformLayout();
			this.grpBool.ResumeLayout(false);
			this.grpBool.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBoolFalse)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numBoolTrue)).EndInit();
			this.grpString.ResumeLayout(false);
			this.grpString.PerformLayout();
			this.grpCollection.ResumeLayout(false);
			this.grpCollection.PerformLayout();
			this.grpArray.ResumeLayout(false);
			this.grpArray.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lstTypes;
		private System.Windows.Forms.Button cmdNewType;
		private System.Windows.Forms.Button cmdTypeUp;
		private System.Windows.Forms.Button cmdDeleteType;
		private System.Windows.Forms.Button cmdTypeDown;
		private System.Windows.Forms.TextBox txtTypeName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.NumericUpDown numFileLength;
		private System.Windows.Forms.Label lblTypeID;
		private System.Windows.Forms.TextBox txtTypeComments;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox lstItems;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button cmdDown;
		private System.Windows.Forms.Button cmdUp;
		private System.Windows.Forms.Button cmdRemove;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.Panel pnlSettings;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.GroupBox grpBool;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown numBoolFalse;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.NumericUpDown numBoolTrue;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.GroupBox grpString;
		private System.Windows.Forms.TextBox txtLength;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.ComboBox cboEncoding;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.CheckBox chkNullTermed;
		private System.Windows.Forms.TextBox txtOffset;
		private System.Windows.Forms.GroupBox grpCollection;
		private System.Windows.Forms.ComboBox cboCollType;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cboType;
		private System.Windows.Forms.CheckBox chkInput;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.CheckBox chkArray;
		private System.Windows.Forms.GroupBox grpArray;
		private System.Windows.Forms.TextBox txtArrayNames;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtArrayQty;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox chkValidate;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtDefault;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Button cmdClose;
	}
}