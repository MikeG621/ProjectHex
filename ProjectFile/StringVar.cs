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
		/// <summary>Object for string items</summary>
		/// <remarks>Initializes to an empty string without terminating null character.</remarks>
		[Serializable]
		public class StringVar : Var
		{
			bool _nullTermed = false;
			
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item</param>
			/// <remarks><see cref="NullTermed"/> defaults to <b>false</b>, <see cref="RawLength"/> defaults to <b>0</b>.</remarks>
			public StringVar(VarCollection parent)
			{
				_parent = parent;
				_type = VarType.String;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = "";
				RawLength = "0";
				_parent.isLoading = loading;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="nullTermed">Whether or not the string is terminated with a null character (<b>\0</b>)</param>
			/// <param name="length">The number of characters in the string, can also be a dynamic value</param>
			/// <remarks><see cref="RawLength"/> defaults to <b>0</b>.</remarks>
			public StringVar(VarCollection parent, string nullTermed)
			{
				_parent = parent;
				_type = VarType.String;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = "";
				RawLength = "0";
				_nullTermed = (nullTermed != null && nullTermed.ToLower() == "true");
				_parent.isLoading = loading;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item</param>
			/// <param name="length">The number of characters in the string including null term if applicable, can also be a dynamic value</param>
			/// <param name="nullTermed">Whether or not the string is terminated with a null character (<b>\0</b>)</param>
			/// <remarks>A <b>null</b> or empty value for <i>nullTermed</i> is interpreted as <b>false</b>.<br/>
			/// A <b>null</> or empty value for <i>length/> results in the default length of <b>1</b>.</remarks>
			public StringVar(VarCollection parent, string nullTermed, string length)
			{
				_parent = parent;
				_type = VarType.String;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = "";
				RawLength = length;
				_nullTermed = (nullTermed != null && nullTermed.ToLower() == "true");
				_parent.isLoading = loading;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item</param>
			/// <param name="length">The number of characters in the string including null term if applicable, can also be a dynamic value</param>
			/// <param name="nullTermed">Whether or not the string is terminated with a null character (<b>\0</b>)</param>
			/// <param name="defaultValue">The starting value of the item</param>
			/// <remarks>A <b>null</b> or empty value for <i>nullTermed</i> is interpreted as <b>false</b>.<br/>
			/// A <b>null</> or empty value for <i>length/> results in the default length of <b>1</b>.</remarks>
			public StringVar(VarCollection parent, string nullTermed, string length, string defaultValue)
			{
				_parent = parent;
				_type = VarType.String;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = "";
				RawLength = length;
				DefaultValue = defaultValue;
				_nullTermed = (nullTermed != null && nullTermed.ToLower() == "true");
				_parent.isLoading = loading;
			}
			#endregion constructors
			
			/// <summary>Gets or sets if the String s terminated by a null-term (<b>\0</b>)</summary>
			/// <exception cref="InvalidOperationException">Attribute is controlled by parent</exception>
			/// <remarks>Default is <b>false</b><br/>
			/// If part of a string array, gets the parent's attribute. Attempting to set results in an exception.</remarks>
			public bool NullTermed
			{
				get
				{
					if (_parent._parentVar != null && _parent._parentVar.Type == VarType.String) return ((StringVar)_parent._parentVar)._nullTermed;
					return _nullTermed;
				}
				set
				{
					if (_parent._parentVar != null && _parent._parentVar.Type == VarType.String) throw new InvalidOperationException(_parentControlMsg);
					_nullTermed = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
			
			/// <summary>Gets or sets the length definition of the item</summary>
			/// <exception cref="ArgumentException">Calculation error with static equation, must be positive</exception>
			/// <exception cref="ArgumentOutOfRangeException">Dynamics calculate to a negative value or <b>0</b><br/><b>-or-</b><br/>Dynamic markers fall outside the range of the parent Collection</exception>
			/// <remarks>Dynamic values are permitted. Static equations are solved and saved as the resultant.<br/>
			/// Default value is <b>"0"</b>. An empty or <b>null</b> value returns to the default.</remarks>
			public override string RawLength
			{
				get { return _length; }
				set
				{
					if (value == "" || value == null) _length = "0";
					else if (!isDynamicText(value))
					{
						string eval = Equation.Evaluate(value);
						if (eval.IndexOf("Error") != -1)
						{
							eval = eval.Replace("Error", "Error setting RawLength");
							throw new ArgumentException(eval, "RawLength.value");
						}
						if (Int32.Parse(eval) < 0)
							throw new ArgumentException("Value must not be negative", "RawLength.value");
						_length = eval;
					}
					else _length = value;
					if (!_parent.isLoading) _isModified = true;
					try { if (Length < 0) throw new ArgumentException("Warning: RawLength dynamics calculate to a negative value"); }
					catch (InvalidOperationException) { return; }
					catch (ArgumentOutOfRangeException) { throw; }
					catch (ArgumentException x) { throw new ArgumentOutOfRangeException(x.Message); }
				}
			}
			
			/// <summary>Gets or sets the value definition of the item</summary>
			/// <remarks>Dynamic values and equations do not apply and are treated as static strings.</remarks>
			public override object RawValue
			{
				get { return _value; }
				set
				{
					if (value == null) _value = "";
					else _value = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
			
			/// <summary>Gets or sets the default value if the item</summary>
			/// <remarks>Dynamic values and equations do not apply and are treated as static strings.<br/>
			/// Setting to a <b>null</b> or empty string removes the default setting.</remarks>
			public override object DefaultValue
			{
				get { return _default; }
				set
				{
					if (value == null || value.ToString() == "") _default = null;
					else _default = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
		}
	}
}