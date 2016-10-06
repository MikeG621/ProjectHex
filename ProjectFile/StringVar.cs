/*
 * Idmr.ProjectHex.ProjectFile.dll, Project definition library file
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
 * [ADD] IsChild implementation
 * [REM] unused ctors
 * [ADD] ctor(VC,bl,str,str,str) for encoding
 * [UPD] Encoding now uses _parentControlMsg when applicable
 * v0.1.4, 130910
 * [ADD] Encoding, DefaultEncoding, DefaultNullTermed, DeepCopy()
 * [UPD] _encoding and _nullTermed use Default*
 * [UPD] RawLength now uses _parentControlMsg when applicable
 * [UPD] License
 * v0.1.3, 130701
 * [ADD] Serializable
 * v0.1.1, 130421
 */

using System;
using System.Text;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
		/// <summary>Object for string items</summary>
		/// <remarks>Initializes to an empty string without terminating null character.</remarks>
		[Serializable]
		public class StringVar : Var
		{
			// TODO: fully implement encoding.
			// TODO: SetBytes
            Encoding _encoding = DefaultEncoding;
			bool _nullTermed = DefaultNullTermed;
			
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <remarks><see cref="NullTermed"/> defaults to <b>false</b>, <see cref="RawLength"/> defaults to <b>0</b>.</remarks>
			public StringVar(VarCollection parent)
			{
				_parent = parent;
				_type = VarType.String;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = "";
                _length = "0";
				_parent.isLoading = loading;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="nullTermed">Whether or not the string is terminated with a null character (<b>\0</b>).</param>
			/// <param name="length">The number of characters in the string, can also be a dynamic value.</param>
			/// <remarks><see cref="RawLength"/> defaults to <b>0</b>.</remarks>
			public StringVar(VarCollection parent, bool nullTermed)
			{
				_parent = parent;
				_type = VarType.String;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = "";
				_length = "0";
				_nullTermed = nullTermed;
				_parent.isLoading = loading;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="length">The number of characters in the string including null term if applicable, can also be a dynamic value.</param>
			/// <param name="nullTermed">Whether or not the string is terminated with a null character (<b>\0</b>).</param>
			/// <param name="defaultValue">The starting value of the item.</param>
			/// <param name="encoding">The raw encoding method of the string.</param>
			/// <remarks>Any value except <b>"true"</b> (case-insensitive) for <i>nullTermed</i> is interpreted as <b>false</b>.<br/>
			/// A <b>null</> or empty value for <i>length/> results in the default length of <b>0</b>.</remarks>
			public StringVar(VarCollection parent, string nullTermed, string length, string defaultValue, string encoding)
			{
				_parent = parent;
				_type = VarType.String;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = "";
				_length = length;
				DefaultValue = defaultValue;
				if (encoding != null && encoding != "") _encoding = Encoding.GetEncoding(encoding);	// mind asplode
				_nullTermed = (nullTermed != null && nullTermed.ToLower() == "true");
				_parent.isLoading = loading;
			}
			#endregion constructors

			public override object DeepCopy()
			{
				StringVar newVar = new StringVar(_parent);
				copyAttributes(this, newVar);
				newVar._tag = _tag;
				newVar._parent = _parent;
				newVar._encoding = _encoding;
				newVar._nullTermed = _nullTermed;
				if (newVar.Values != null)
					for (int i = 0; i < newVar.Values.Count; i++)
						newVar.Values[i] = (StringVar)Values[i].DeepCopy();
				return newVar;
			}

			/// <summary>If not specified in the ctor, the initial value for <see cref="StringVar.Encoding"/>.</summary>
			/// <remarks>Default value is <b><see cref="Encoding.UTF8"/></b>.</remarks>
			static public Encoding DefaultEncoding = Encoding.UTF8;
			
			/// <summary>If not specified in the ctor, the initial value for <see cref="NullTermed"/>.</summary>
			/// <remarks>Default value is <b>false</b>.</remarks>
			static public bool DefaultNullTermed = false;
			
			/// <summary>Gets or sets if the String s terminated by a null-term (<b>\0</b>).</summary>
			/// <exception cref="InvalidOperationException">Attribute is controlled by parent.</exception>
			/// <remarks>Defaults to the value of <see cref="DefaultNullTermed"/>.<br/>
			/// If part of a string array, gets the parent's attribute. Attempting to set results in an exception.</remarks>
			public bool NullTermed
			{
				get
				{
					if (IsChild) return ((StringVar)_parent.parentVar)._nullTermed;
					return _nullTermed;
				}
				set
				{
					if (IsChild) throw new InvalidOperationException(_parentControlMsg);
					_nullTermed = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
			
			/// <summary>Gets or sets the byte length definition of the item.</summary>
			/// <exception cref="ArgumentException">Calculation error with static equation.</exception>
			/// <exception cref="ArgumentOutOfRangeException">Value calculates to a negative value.<br/>
			/// <b>-or-</b><br/>
			/// Dynamic markers fall outside the range of the parent Collection.</exception>
			/// <exception cref="InvalidOperationException">Attribute is controlled by parent.</exception>
			/// <remarks>Dynamic values are permitted. Static equations are solved and saved as the resultant.<br/>
			/// Default value is <b>"0"</b>. An empty or <b>null</b> value returns to the default.<br/>
			/// Special note that this is the <b>*byte*</b> length of the field. This is an important distinction
			/// when using UTF-16 (2 bytes per character) or similar multi-byte <see cref="System.Text.Encoding">encodings</see>.</remarks>
			public override string RawLength
			{
				get
				{
					if (IsChild) return ((StringVar)_parent.parentVar)._length;
					return _length;
				}
				set
				{
					if (IsChild) throw new InvalidOperationException(_parentControlMsg);
					if (value == "" || value == null) _length = "0";
					else if (!isDynamicText(value))
					{
						string eval = Equation.Evaluate(value);
						if (eval.IndexOf("Error") != -1)
						{
							eval = eval.Replace("Error", "Error setting RawLength");
							throw new ArgumentException(eval, "RawLength.value");
						}
						if (int.Parse(eval) < 0)
							throw new ArgumentOutOfRangeException("RawLength must not be negative");
						_length = eval;
					}
					else _length = value;
					if (!_parent.isLoading) _isModified = true;
					try { if (Length < 0) throw new ArgumentOutOfRangeException("Warning: RawLength dynamics calculate to a negative value"); }
					catch (InvalidOperationException) { return; }
					catch (ArgumentOutOfRangeException) { throw; }
					catch (ArgumentException) { throw; }
				}
			}
			
			/// <summary>Gets or sets the value definition of the item.</summary>
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
			
			/// <summary>Gets or sets the default value if the item.</summary>
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

			/// <summary>Gets or sets the encoding for <see cref="Value"/>.</summary>
			/// <exception cref="InvalidOperationException">Attribute is controlled by parent.</exception>
			/// <remarks>Defaults to the value of <see cref="DefaultEncoding"/>.<br/>
			/// Changing the value does not directly impact <see cref="RawValue"/>, but instead only affects
			/// the reading and writing of the <see cref="BinaryFile"/>. If a character is found that does not
			/// properly encode using the current value, when writing to disk it will be replaced with <b>'?'</b>.</remarks>
			public Encoding Encoding
			{
				get
				{
					if (IsChild) return ((StringVar)_parent.parentVar)._encoding;
					return _encoding;
				}
				set
				{
					if (IsChild) throw new InvalidOperationException(_parentControlMsg);
					if (value == null) _encoding = DefaultEncoding;
					else _encoding = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
		}
	}
}