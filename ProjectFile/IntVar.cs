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
		/// <summary>Object for signed four-byte items.</summary>
		[Serializable]
		public class IntVar : Var
		{	
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public IntVar(VarCollection parent) : base(parent)
			{
				_type = VarType.Int;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="defaultValue">The starting value of the item</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public IntVar(VarCollection parent, string defaultValue) : base(parent, defaultValue)
			{
				_type = VarType.Int;
			}
			#endregion constructors
			
			/// <summary>Gets or sets the final value.</summary>
			/// <exception cref="ArgumentNullException"><see cref="RawValue"/> is <b>null</b> or an empty string.</exception>
			/// <exception cref="FormatException"><see cref="RawValue"/> is not a valid integer.</exception>
			/// <exception cref="OverflorException"><see cref="RawValue"/> does not fall between <see cref="Int32.MinValue"/> and <see cref="Int32.MaxValue"/>.</exception>
			public int Value
			{
				get { return Int32.Parse(_value.ToString()); }
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