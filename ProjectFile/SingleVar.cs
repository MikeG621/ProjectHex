/*
 * Idmr.ProjectHex.ProjectFile.dll, Project definition library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in GPL.txt
 * Version: 0.1
 */
 
/* CHANGELOG
 * [ADD] Serializable
 * v0.1, XXXXXX
 */
 
using System;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
		/// <summary>Object for single-precision (four-byte) floating point items.</summary>
		[Serializable]
		public class SingleVar : Var
		{
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public SingleVar(VarCollection parent) : base(parent)
			{
				_type = VarType.Single;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="defaultValue">The starting value of the item</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public SingleVar(VarCollection parent, string defaultValue) : base(parent, defaultValue)
			{
				_type = VarType.Single;
			}
			#endregion constructors

			/// <summary>Gets or sets the final value.</summary>
			/// <exception cref="ArgumentOutOfRangeException"><see cref="RawValue"/> falls outside <b>±3.4e38</b> (approx).</exception>
			/// <exception cref="ArgumentException">Error computing the final value.</exception>
			public float Value
			{
				get { return Single.Parse(_value.ToString()); }
				set
				{
					_value = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}

			/// <summary>Gets the byte length of the item</summary>
			/// <exception cref="InvalidOperationException">Type is a fixed length, cannot be set</exception>
			public override string RawLength
			{
				get { return "4"; }
				set { throw new InvalidOperationException(_fixedLengthMsg); }
			}
		}
	}
}
