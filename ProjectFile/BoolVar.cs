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
 * [ADD] SetBytes
 * [ADD] IsChild implementation
 * v0.1.4, 130910
 * [ADD] operators, DefaultTrueValue, DefaultFalseValue, DeepCopy
 * [UPD] _trueValue and _falseValue now use Default*
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
		/// <summary>Object for boolean items</summary>
		[Serializable]
		public class BoolVar : Var
		{
		
			byte _trueValue = DefaultTrueValue;
			byte _falseValue = DefaultFalseValue;
			
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="trueValue">The raw byte value stored in the file for <b>true</b>. Accepted values are <b>-1</b> to <b>255</b>. Default is <b>1</b>.</param>
			/// <param name="falseValue">The raw byte value stored in the file for <b>false</b>. Accepted values are <b>-1</b> to <b>255</b>. Default is <b>0</b>.</param>
			/// <exception cref="OverflowException"><i>trueValue</i> or <i>falseValue</i> is less than <b>-1</b> or greater than <b>255</b>.</exception>
			/// <remarks><see cref="RawValue"/> initializes to <b>false</b>.<br/>
			/// If <i>trueValue</i> or <i>falseValue</i> are <b>null</b> or cannot be parsed, default values are used.<br/>
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
			/// <exception cref="OverflowException"><i>trueValue</i> or <i>falseValue</i> is less than <b>-1</b> or greater than <b>255</b>.</exception>
			/// <remarks><see cref="RawValue"/> initializes to <b>false</b>.<br/>
			/// If <i>trueValue</i> or <i>falseValue</i> are <b>null</b> or cannot parsed, default values are used.<br/>
			/// Values of <b>-1</b> are converted to <b>255</b>.<br/>
			/// For <see cref="DefaultValue"/> to be set to <b>true</b>, <i>defaultValue</i> must match <see cref="TrueValue"/> or "<b>true</b>" (case-insensitive).  All other inputs will be read as <b>false</b>.</remarks>
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
			/// <remarks><see cref="RawValue"/> initializes to <b>false</b>.<br/>
			/// For <see cref="DefaultValue"/> to be set to <b>true</b>, <i>defaultValue</i> must match <see cref="TrueValue"/> or "<b>true</b>" (case-insensitive).  All other inputs will be read as <b>false</b>.</remarks>
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
			/// <remarks><see cref="RawValue"/> initializes to <b>false</b>.<br/>
			/// For <see cref="DefaultValue"/> to be set to <b>true</b>, <i>defaultValue</i> must match <see cref="TrueValue"/> or "<b>true</b>" (case-insensitive).  All other inputs will be read as <b>false</b>.</remarks>
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

			public override object DeepCopy()
			{
				BoolVar newVar = new BoolVar(_parent, _trueValue, _falseValue);
				copyAttributes(this, newVar);
				newVar._tag = _tag;
				newVar._parent = _parent;
				if (newVar.Values != null)
					for (int i = 0; i < newVar.Values.Count; i++)
						newVar.Values[i] = (BoolVar)Values[i].DeepCopy();
				return newVar;
			}
			
			/// <summary>Sets <see cref="RawValue"/> using a byte array.</summary>
			/// <exception cref="ArgumentException">'buffer' does not have a length of <b>1</b>.</exception>
			/// <remarks>Only a numerical value matching <see cref="TrueValue"/> will set to <b>true</b>, all others will set to <b>false</b>.</remarks>
			public override void SetBytes(byte[] buffer)
			{
				if (buffer.Length != 1) throw new ArgumentException("'buffer' must have a length of 1.");
				if (buffer[0] == TrueValue) _value = true;
				else _value = false;
			}

			/// <summary>When not specified in the ctor, the initial value for <see cref="TrueValue"/>.</summary>
			/// <remarks>Default value is <b>1</b>.</remarks>
			static public byte DefaultTrueValue = 1;
			
			/// <summary>When not specified in the ctor, the initial value for <see cref="FalseValue"/>.</summary>
			/// <remarks>Default value is <b>0</b>.</remarks>
			static public byte DefaultFalseValue = 0;
			
			/// <summary>Gets or sets the value of the item.</summary>
			/// <exception cref="ArgumentException">Value does not evaluate to a valid boolean.</exception>
			/// <remarks>A value of <b>null</b> or an empty string will set to <b>false</b>.<br/>
			/// A value of <see cref="TrueValue"/> or <see cref="FalseValue"/> will set to the appropriate value.</remarks>
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
			/// A value of <see cref="TrueValue"/> or <see cref="FalseValue"/> will set to the appropriate value.</remarks>
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
			/// <remarks>Defaults to the value of <see cref="DefaultFalseValue"/>.<br/>
			/// If part of an array, gets the parent's attribute. Attempting to set results in an exception.</remarks>
			public byte FalseValue
			{
				get
				{
					if (IsChild) return ((BoolVar)_parent.parentVar)._falseValue;
					return _falseValue;
				}
				set
				{
					if (IsChild) throw new InvalidOperationException(_parentControlMsg);
					_falseValue = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
			
			/// <summary>Gets or sets the numerical value for <b>true</b>.</summary>
			/// <exception cref="InvalidOperationException">Attribute is controlled by parent</exception>
			/// <remarks>Defaults to the value of <see crf="DefaultTrueValue"/>.<br/>
			/// If part of an array, gets the parent's attribute. Attempting to set results in an exception.</remarks>
			public byte TrueValue
			{
				get
				{
					if (IsChild) return ((BoolVar)_parent.parentVar)._trueValue;
					return _trueValue;
				}
				set
				{
					if (IsChild) throw new InvalidOperationException(_parentControlMsg);
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
			
			#region operators
			/// <summary>Converts a bool to an unsigned byte.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned byte object.</returns>
			/// <remarks>The new <see cref="ByteVar.Value">Value</see> will be assigned <see cref="TrueValue"/> or <see cref="FalseValue"/> as appropriate.</remarks>
			public static implicit operator ByteVar(BoolVar var)
			{
				ByteVar nv = new ByteVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = (var.Value ? var.TrueValue : var.FalseValue);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ByteVar)(BoolVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a bool to a double-precision value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A double-precision object.</returns>
			/// <remarks>The new <see cref="DoubleVar.Value">Value</see> will be assigned <see cref="TrueValue"/> or <see cref="FalseValue"/> as appropriate before conversion.</remarks>
			public static implicit operator DoubleVar(BoolVar var)
			{
				DoubleVar nv = new DoubleVar(var._parent);
				copyAttributes(var, nv);
                byte[] a = { (var.Value ? var.TrueValue : var.FalseValue), 0, 0, 0, 0, 0, 0, 0 };
				nv.Value = BitConverter.ToDouble(a, 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (DoubleVar)(BoolVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a bool to a signed 32-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 32-bit integer object.</returns>
			/// <remarks>The new <see cref="IntVar.Value">Value</see> will be assigned <see cref="TrueValue"/> or <see cref="FalseValue"/> as appropriate.</remarks>
			public static implicit operator IntVar(BoolVar var)
			{
				IntVar nv = new IntVar(var._parent);
				copyAttributes(var, nv);
                nv.Value = (var.Value ? var.TrueValue : var.FalseValue);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (IntVar)(BoolVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a bool to a signed 64-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 64-bit integer object.</returns>
			/// <remarks>The new <see cref="LongVar.Value">Value</see> will be assigned <see cref="TrueValue"/> or <see cref="FalseValue"/> as appropriate.</remarks>
			public static implicit operator LongVar(BoolVar var)
			{
				LongVar nv = new LongVar(var._parent);
				copyAttributes(var, nv);
                nv.Value = (var.Value ? var.TrueValue : var.FalseValue);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (LongVar)(BoolVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a bool to an unsigned byte.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed byte object.</returns>
			/// <remarks>The new <see cref="SByteVar.Value">Value</see> will be assigned <see cref="TrueValue"/> or <see cref="FalseValue"/> as appropriate.</remarks>
			public static implicit operator SByteVar(BoolVar var)
			{
				SByteVar nv = new SByteVar(var._parent);
				copyAttributes(var, nv);
                nv.Value = (sbyte)(var.Value ? var.TrueValue : var.FalseValue);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (SByteVar)(BoolVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a bool to a signed 16-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 16-bit integer object.</returns>
			/// <remarks>The new <see cref="ByteVar.Value">Value</see> will be assigned <see cref="TrueValue"/> or <see cref="FalseValue"/> as appropriate.</remarks>
			public static implicit operator ShortVar(BoolVar var)
			{
				ShortVar nv = new ShortVar(var._parent);
				copyAttributes(var, nv);
                nv.Value = (var.Value ? var.TrueValue : var.FalseValue);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ShortVar)(BoolVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a bool to a single-precision value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A single-precision object.</returns>
			/// <remarks>The new <see cref="SingleVar.Value">Value</see> will be assigned <see cref="TrueValue"/> or <see cref="FalseValue"/> as appropriate before converting.</remarks>
			public static implicit operator SingleVar(BoolVar var)
			{
				SingleVar nv = new SingleVar(var._parent);
				copyAttributes(var, nv);
                byte[] a = { (var.Value ? var.TrueValue : var.FalseValue), 0, 0, 0 };
				nv.Value = BitConverter.ToSingle(a, 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (SingleVar)(BoolVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a bool to an unsigned 32-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 32-bit integer object.</returns>
			/// <remarks>The new <see cref="UIntVar.Value">Value</see> will be assigned <see cref="TrueValue"/> or <see cref="FalseValue"/> as appropriate.</remarks>
			public static implicit operator UIntVar(BoolVar var)
			{
				UIntVar nv = new UIntVar(var._parent);
				copyAttributes(var, nv);
                nv.Value = (var.Value ? var.TrueValue : var.FalseValue);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (UIntVar)(BoolVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a bool to an unsigned 64-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 64-bit integer object.</returns>
			/// <remarks>The new <see cref="ULongVar.Value">Value</see> will be assigned <see cref="TrueValue"/> or <see cref="FalseValue"/> as appropriate.</remarks>
			public static implicit operator ULongVar(BoolVar var)
			{
				ULongVar nv = new ULongVar(var._parent);
				copyAttributes(var, nv);
                nv.Value = (var.Value ? var.TrueValue : var.FalseValue);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ULongVar)(BoolVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a bool to an unsigned 16-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 16-bit integer object.</returns>
			/// <remarks>The new <see cref="UShortVar.Value">Value</see> will be assigned <see cref="TrueValue"/> or <see cref="FalseValue"/> as appropriate before.</remarks>
			public static implicit operator UShortVar(BoolVar var)
			{
				UShortVar nv = new UShortVar(var._parent);
				copyAttributes(var, nv);
                nv.Value = (var.Value ? var.TrueValue : var.FalseValue);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (UShortVar)(BoolVar)var[i];
				}
				return nv;
			}
			#endregion operators
		}
	}
}