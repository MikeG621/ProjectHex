/*
 * Idmr.ProjectHex.ProjectFile.dll, Project definition library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
 * with this file, You can obtain one at http://mozilla.org/MPL/2.0/
 *
 * Version: 0.1.4+
 */
 
/* CHANGELOG
 * [ADD] SetBytes
 * [ADD] min/max
 * [UPD] out of range default in ctor throws exception
 * v0.1.4, 130910
 * [ADD] operators, DeepCopy()
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
		/// <summary>Object for double-precision (eight-byte) floating point items.</summary>
		[Serializable]
		public class DoubleVar : Var
		{
			double _minValue = Double.MinValue;
			double _maxValue = Double.MaxValue;
			
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public DoubleVar(VarCollection parent) : base(parent)
			{
				_type = VarType.Double;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="minValue">The lower bound of the item.</param>
			/// <param name="maxValue">The upper bound of the item.</param>
			/// <param name="defaultValue">The starting value of the item</param>
			/// <exception cref="ArgumentOutOfRangeException"><i>minValue</i>, <i>maxValue</i> or <i>defaultValue</i> fall outside <b>±1.8e308</b> (approx).</exception>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public DoubleVar(VarCollection parent, string minValue, string maxValue, string defaultValue) : base(parent, defaultValue)
			{
				if (minValue != null && minValue != "") _minValue = Double.Parse(minValue);
				if (maxValue != null && maxValue != "") _maxValue = Double.Parse(maxValue);
				Double.Parse(defaultValue);
				_type = VarType.Double;
			}
			#endregion constructors

			public override object DeepCopy()
			{
				DoubleVar newVar = new DoubleVar(_parent);
				copyAttributes(this, newVar);
				newVar._tag = _tag;
				newVar._parent = _parent;
				if (newVar.Values != null)
					for (int i = 0; i < newVar.Values.Count; i++)
						newVar.Values[i] = (DoubleVar)Values[i].DeepCopy();
				return newVar;
			}

			/// <summary>Sets <see cref="Value"/> using a byte array.</summary>
			/// <exception cref="ArgumentException">'buffer' does not have a length of <b>8</b>.</exception>
			/// <exception cref="ArgumentOutOfRangeException">The value falls outside <see cref="MinimumValue"/> to <see cref="MaximumValue"/>.</exception>
			public override void SetBytes(byte[] buffer)
			{
				if (buffer.Length != 8) throw new ArgumentException("'buffer' must have a length of 8.");
				Value = BitConverter.ToDouble(buffer);
			}
			
			/// <summary>Gets or sets the final value.</summary>
			/// <exception cref="ArgumentNullException"><see cref="RawValue"/> is <b>null</b> or an empty string.</exception>
			/// <exception cref="ArgumentOutOfRangeException"><i>value</i> falls outside <see cref="MinimumValue"/> to <see cref="MaximumValue"/>.</exception>
			/// <exception cref="FormatException"><see cref="RawValue"/> is not a valid double.</exception>
			/// <exception cref="OverflowException"><see cref="RawValue"/> does not fall between <see cref="Double.MinValue"/> and <see cref="Double.MaxValue"/>.</exception>
			public double Value
			{
				get { return Double.Parse(_value.ToString()); }
				set
				{
					if (value < _minValue || value > _maxValue)
						throw new ArgumentOutOfRangeException("value falls outside " + _minValue + " to " + _maxValue);
					_value = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}

			/// <summary>Gets the byte length of the item</summary>
			/// <exception cref="InvalidOperationException">Type is a fixed length, cannot be set</exception>
			public override string RawLength
			{
				get { return "8"; }
				set { throw new InvalidOperationException(_fixedLengthMsg); }
			}

			/// <summary>Gets or sets the minimum allowable value.</summary>
			/// <exception cref="InvalidOperationException">Attribute is controlled by parent</exception>
			/// <remarks>Defaults to <see cref="Double.MinValue"/>.<br/>
			/// If part of an array, gets the parent's attribute. Attempting to set results in an exception.</remarks>
			public double MinimumValue
			{
				get
				{
					if (IsChild) return ((DoubleVar)_parent.parentVar)._minValue;
					return _minValue;
				}
				set
				{
					if (IsChild) throw new InvalidOperationException(_parentControlMsg);
					_minValue = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}

			/// <summary>Gets if the minimum value has been changed.</summary>
			/// <remarks>Always returns <b>false</b> if part of an array.</remarks>
			public bool UseMinValue { get { return (_minValue != Double.MinValue); } }

			/// <summary>Gets or sets the maximum allowable value.</summary>
			/// <exception cref="InvalidOperationException">Attribute is controlled by parent</exception>
			/// <remarks>Defaults to <see cref="Double.MaxValue"/>.<br/>
			/// If part of an array, gets the parent's attribute. Attempting to set results in an exception.</remarks>
			public double MaximumValue
			{
				get
				{
					if (IsChild) return ((DoubleVar)_parent.parentVar)._maxValue;
					return _maxValue;
				}
				set
				{
					if (IsChild) throw new InvalidOperationException(_parentControlMsg);
					_maxValue = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
			/// <summary>Gets if the maximum value has been changed.</summary>
			/// <remarks>Always returns <b>false</b> if part of an array.</remarks>
			public bool UseMaxValue { get { return (_maxValue != Double.MaxValue); } }
			
			#region operators
			/// <summary>Converts a double-precision value to a boolean value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A boolean object.</returns>
			/// <remarks>Assumes default value for <see cref="BoolVar.FalseValue"/> after isolating the first 8 bits.</remarks>
			public static explicit operator BoolVar(DoubleVar var)
			{
				BoolVar nv = new BoolVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = BitConverter.GetBytes(var.Value)[0] != 0;
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (BoolVar)(DoubleVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a double-precision value to an unsigned byte.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned byte object.</returns>
			/// <remarks>New value is composed of the first 8 bits from the original.</remarks>
			public static explicit operator ByteVar(DoubleVar var)
			{
				ByteVar nv = new ByteVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = BitConverter.GetBytes(var.Value)[0];
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ByteVar)(DoubleVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a double-precision value to a signed 32-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 32-bit integer object.</returns>
			/// <remarks>New value is composed of the first 32 bits from the original.</remarks>
			public static explicit operator IntVar(DoubleVar var)
			{
				IntVar nv = new IntVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = BitConverter.ToInt32(BitConverter.GetBytes(var.Value), 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (IntVar)(DoubleVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a double-precision value to a signed 64-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 64-bit integer object.</returns>
			public static explicit operator LongVar(DoubleVar var)
			{
				LongVar nv = new LongVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = BitConverter.ToInt64(BitConverter.GetBytes(var.Value), 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (LongVar)(DoubleVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a double-precision value to a signed byte.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed byte object.</returns>
			/// <remarks>New value is composed of the first 8 bits from the original.</remarks>
			public static explicit operator SByteVar(DoubleVar var)
			{
				SByteVar nv = new SByteVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = (sbyte)BitConverter.GetBytes(var.Value)[0];
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (SByteVar)(DoubleVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a double-precision value to a signed 16-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 16-bit integer object.</returns>
			/// <remarks>New value is composed of the first 16 bits from the original.</remarks>
			public static explicit operator ShortVar(DoubleVar var)
			{
				ShortVar nv = new ShortVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = BitConverter.ToInt16(BitConverter.GetBytes(var.Value), 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ShortVar)(DoubleVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a double-precision value to a single-precision value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A single-precision object.</returns>
			/// <remarks>New value is composed of the first 32 bits from the original.</remarks>
			public static explicit operator SingleVar(DoubleVar var)
			{
				SingleVar nv = new SingleVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = BitConverter.ToSingle(BitConverter.GetBytes(var.Value), 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (SingleVar)(DoubleVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a double-precision value to an unsigned 32-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 32-bit integer object.</returns>
			/// <remarks>New value is composed of the first 32 bits from the original.</remarks>
			public static explicit operator UIntVar(DoubleVar var)
			{
				UIntVar nv = new UIntVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = BitConverter.ToUInt32(BitConverter.GetBytes(var.Value), 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (UIntVar)(DoubleVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a double-precision value to an unsigned 64-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 64-bit integer object.</returns>
			public static explicit operator ULongVar(DoubleVar var)
			{
				ULongVar nv = new ULongVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = BitConverter.ToUInt64(BitConverter.GetBytes(var.Value), 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ULongVar)(DoubleVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts a double-precision value to an unsigned 16-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 16-bit integer object.</returns>
			/// <remarks>New value is composed of the first 16 bits from the original.</remarks>
			public static explicit operator UShortVar(DoubleVar var)
			{
				UShortVar nv = new UShortVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = BitConverter.ToUInt16(BitConverter.GetBytes(var.Value), 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (UShortVar)(DoubleVar)var[i];
				}
				return nv;
			}
			#endregion operators
		}
	}
}
