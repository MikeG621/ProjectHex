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
		/// <summary>Object for boolean items</summary>
		[Serializable]
		public class BoolVar : Var
		{
		
			byte _trueValue = 1;
			byte _falseValue = 0;
			
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="trueValue">The raw byte value stored in the file for <b>true</b>. Accepted values are <b>-1</b> to <b>255</b>. Default is <b>1</b>.</param>
			/// <param name="falseValue">The raw byte value stored in the file for <b>false</b>. Accepted values are <b>-1</b> to <b>255</b>. Default is <b>0</b>.</param>
			/// <exception cref="FormatException">Unable to parse <i>trueValue</i> or <i>falseValue</i>.</exception>
			/// <exception cref="OverflowException"><i>trueValue</i> or <i>falseValue</i> is less than <b>-1</b> or greater than <b>255</b>.</exception>
			/// <remarks><see cref="RawValue"/> initializes to <b>false</b>.<br/>
			/// If <i>trueValue</i> or <i>falseValue</i> are <b>null</b>, default values are used.<br/>
			/// Values of <b>-1</b> are converted to <b>255</b>.</remarks>
			public BoolVar(VarCollection parent, string trueValue, string falseValue)
			{
				_parent = parent;
				_type = VarType.Bool;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				try { TrueValue = System.Byte.Parse(trueValue); }
				catch (ArgumentNullException) { /* do nothing */ }
				catch (FormatException) { /* do nothing */ }
				catch (OverflowException x)
				{
					if (trueValue == "-1") TrueValue = 255;
					else throw x;
				}
				try { FalseValue = System.Byte.Parse(falseValue); }
				catch (ArgumentNullException) { /* do nothing */ }
				catch (FormatException) { /* do nothing */ }
				catch (OverflowException x)
				{
					if (falseValue == "-1") FalseValue = 255;
					else throw x;
				}
				RawValue = false;
				_parent.isLoading = loading;
			}
			
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="trueValue">The raw value stored in the file for <b>true</b>. Default is <b>1</b>.</param>
			/// <param name="falseValue">The raw value stored in the file for <b>false</b>. Default is <b>0</b>.</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>false</b>.</remarks>
			public BoolVar(VarCollection parent, byte trueValue, byte falseValue)
			{
				_parent = parent;
				_type = VarType.Bool;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				TrueValue = trueValue;
				FalseValue = falseValue;
				RawValue = false;
				_parent.isLoading = loading;
			}
			
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>false</b>.</remarks>
			public BoolVar(VarCollection parent)
			{
				_parent = parent;
				_type = VarType.Bool;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = false;
				_parent.isLoading = loading;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="trueValue">The raw byte value stored in the file for <b>true</b>. Accepted values are <b>-1</b> to <b>255</b>. Default is <b>1</b>.</param>
			/// <param name="falseValue">The raw byte value stored in the file for <b>false</b>. Accepted values are <b>-1</b> to <b>255</b>. Default is <b>0</b>.</param>
			/// <param name="defaultValue">The starting value of the item</param>
			/// <exception cref="FormatException">Unable to parse <i>trueValue</i> or <i>falseValue</i>.</exception>
			/// <exception cref="OverflowException"><i>trueValue</i> or <i>falseValue</i> is less than <b>-1</b> or greater than <b>255</b>.</exception>
			/// <remarks><see cref="RawValue"/> initializes to <b>false</b>.<br/>
			/// If <i>trueValue</i> or <i>falseValue</i> are <b>null</b>, default values are used.<br/>
			/// Values of <b>-1</b> are converted to <b>255</b>.</remarks>
			public BoolVar(VarCollection parent, string trueValue, string falseValue, string defaultValue)
			{
				_parent = parent;
				_type = VarType.Bool;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				try { TrueValue = System.Byte.Parse(trueValue); }
				catch (FormatException) { /* do nothing */ }
				catch (ArgumentNullException) { /* do nothing */ }
				catch (OverflowException x)
				{
					if (trueValue == "-1") TrueValue = 255;
					else throw x;
				}
				try { FalseValue = System.Byte.Parse(falseValue); }
				catch (FormatException) { /* do nothing */ }
				catch (ArgumentNullException) { /* do nothing */ }
				catch (OverflowException x)
				{
					if (falseValue == "-1") FalseValue = 255;
					else throw x;
				}
				RawValue = false;
				DefaultValue = (defaultValue != null && (defaultValue == TrueValue.ToString() || defaultValue.ToString().ToLower() == "true"));
				_parent.isLoading = loading;
			}
			
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="trueValue">The raw value stored in the file for <b>true</b>. Default is <b>1</b>.</param>
			/// <param name="falseValue">The raw value stored in the file for <b>false</b>. Default is <b>0</b>.</param>
			/// <param name="defaultValue">The starting value of the item</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>false</b>.</remarks>
			public BoolVar(VarCollection parent, byte trueValue, byte falseValue, string defaultValue)
			{
				_parent = parent;
				_type = VarType.Bool;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				TrueValue = trueValue;
				FalseValue = falseValue;
				RawValue = false;
				DefaultValue = (defaultValue != null && (defaultValue == TrueValue.ToString() || defaultValue.ToString().ToLower() == "true"));
				_parent.isLoading = loading;
			}
			
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="defaultValue">The starting value of the item</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>false</b>.</remarks>
			public BoolVar(VarCollection parent, string defaultValue)
			{
				_parent = parent;
				_type = VarType.Bool;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = false;
				DefaultValue = (defaultValue != null && (defaultValue == TrueValue.ToString() || defaultValue.ToString().ToLower() == "true"));
				_parent.isLoading = loading;
			}
			#endregion constructors
			
			/// <summary>Gets or sets the value of the item.</summary>
			/// <exception cref="ArgumentException">Value does not evaluate to a valid boolean.</exception>
			/// <remarks>A value of <b>null</b> or an empty string will set to <b>false</b>.<br/>
			/// A value of <see cref="TrueValue"/> or <see cref="FalseValue"/> will set ot the appropriate value.</remarks>
			public override object RawValue
			{
				get { return _value; }
				set
				{
					if (value == null || value.ToString() == "" || value.ToString().ToLower() == "false" || value.ToString() == FalseValue.ToString())
						_value = false;
					else if (value.ToString() == TrueValue.ToString() || value.ToString().ToLower() == "true")
						_value = true;
					else
					{
						try { _value = Conditional.Evaluate(value.ToString()); }
						catch (ArgumentException) { throw; }
						catch (FormatException x) { throw new ArgumentException(x.Message); }
					}
					if (!_parent.isLoading) _isModified = true;
				}
			}
			
			/// <summary>Gets or sets the default value of the item.</summary>
			/// <exception cref="ArgumentOutOfRangeException">Value contains dynamic markers outside the parent collection.</exception>
			/// <exception cref="ArgumentException">Value is not a valid Conditional.<br/><b>-or-</b><br/>Value does not evaluate to a valid boolean.</exception>
			/// <remarks>A value of <b>null</b> or an empty string will remove the default value.<br/>
			/// A value of <see cref="TrueValue"/> or <see cref="FalseValue"/> will set ot the appropriate value.</remarks>
			public override object DefaultValue
			{
				get { return _default; }
				set
				{
					if (value == null || value.ToString() == "") _default = null;
					else if (value.ToString() == FalseValue.ToString()) _default = false;
					else if (value.ToString() == TrueValue.ToString()) _default = true;
					else
					{
						string temp = value.ToString();
						if (isDynamicText(temp) && _binaryAssigned)
							try { temp = ParseDynamicValues(_parent, temp); }
							catch (ArgumentOutOfRangeException) { throw; }
						if (!isDynamicText(temp))
						{
							try { _default = Conditional.Evaluate(temp); }
							catch (ArgumentException) { throw; }
							catch (FormatException x) { throw new ArgumentException(x.Message); }
						}
						else _default = value;
					}
					if (!_parent.isLoading) _isModified = true;
				}
			}
			/// <summary>Gets or sets the numerical value for <b>false</b>.</summary>
			/// <exception cref="InvalidOperationException">Attribute is controlled by parent</exception>
			/// <remarks>Default value is <b>0</b>.<br/>
			/// If part of a byte array, gets the parent's attribute. Attempting to set results in an exception.</remarks>
			public byte FalseValue
			{
				get
				{
					if (_parent._parentVar != null && _parent._parentVar.Type == VarType.Bool) return ((BoolVar)_parent._parentVar)._falseValue;
					return _falseValue;
				}
				set
				{
					if (_parent._parentVar != null && _parent._parentVar.Type == VarType.Bool) throw new InvalidOperationException(_parentControlMsg);
					_falseValue = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
			
			/// <summary>Gets or sets the numerical value for <b>true</b>.</summary>
			/// <exception cref="InvalidOperationException">Attribute is controlled by parent</exception>
			/// <remarks>Default value is <b>1</b>.<br/>
			/// If part of a byte array, gets the parent's attribute. Attempting to set results in an exception.</remarks>
			public byte TrueValue
			{
				get
				{
					if (_parent._parentVar != null && _parent._parentVar.Type == VarType.Bool) return ((BoolVar)_parent._parentVar)._trueValue;
					return _trueValue;
				}
				set
				{
					if (_parent._parentVar != null && _parent._parentVar.Type == VarType.Bool) throw new InvalidOperationException(_parentControlMsg);
					_trueValue = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
			
			/// <summary>Gets or sets the boolean value.</summary>
			/// <exception cref="ArgumentException">Error computing the conditional</exception>
			public bool Value
			{
				get { return Conditional.Evaluate(_value.ToString()); }
				set
				{
					_value = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
		}
	}
}