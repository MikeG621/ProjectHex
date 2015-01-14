/*
 * Idmr.ProjectHex.ProjectFile.dll, Project definition library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
 * with this file, You can obtain one at http://mozilla.org/MPL/2.0/
 *
 * Version: 0.1.4
 */
 
/* CHANGELOG
 * v0.1.4, 130910
 * [UPD] License
 * v0.1.3, 130701
 * [ADD] Serializable
 * v0.1.1, 130421
 */
 
using System;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
		/// <summary>Object for items that fail type declaration when loading the <see cref="ProjectFile"/>.</summary>
		[Serializable]
		public class ErrorVar : Var
		{
			/// <summary>Initializes a new placeholder item</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="name">The name of the item for identification purposes.</param>
			public ErrorVar(VarCollection parent, string name)
			{
				_parent = parent;
				_type = VarType.Error;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				Name = name;
				_parent.isLoading = loading;
			}
		}
	}
}