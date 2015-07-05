/*
 * Idmr.ProjectHex.BinaryFile.dll, Raw data management library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
 * with this file, You can obtain one at http://mozilla.org/MPL/2.0/
 *
 * Version: 0.1.5
 */

/* CHANGELOG
 * v0.1.5, 150705
 * [ADD] created
 */
using System;
using System.Windows.Forms;

namespace Idmr.ProjectHex
{
	public partial class SelectionDialog : Form
	{
		/// <summary>Initializes a new SelectionDialog.</summary>
		/// <param name="matches">The list of possible Projects that may apply to the BinaryFile.</param>
		public SelectionDialog(string[] matches)
		{
			InitializeComponent();
			lstProjects.Items.AddRange(matches);
			lstProjects.SelectedIndex = 0;
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>Gets the selected index from the ListBox.</summary>
		public int SelectedIndex { get { return lstProjects.SelectedIndex; } }
	}
}
