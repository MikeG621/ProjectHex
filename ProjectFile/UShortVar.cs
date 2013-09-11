/*
 * Idmr.ProjectHex.ProjectFile.dll, Project definition library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
 * with this file, You can obtain one at http://mozilla.org/MPL/2.0/
 *
 * Version: 0.0.4
 */
 
/* CHANGELOG
 * v0.0.4, 130910
 * [ADD] operators, DeepCopy()
 * [UPD] License
 * v0.0.3, 130701
 * [ADD] Serializable
 * v0.0.1, 130421
 */
 
using System;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
		/// <summary>Object for unsigned two-byte items.</summary>
		[Serializable]
		public class UShortVar : Var
		{	
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public UShortVar(VarCollection parent) : base(parent)
			{
				_type = VarType.UShort;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="defaultValue">The starting value of the item</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public UShortVar(VarCollection parent, string defaultValue) : base(parent, defaultValue)
			{
				_type = VarType.UShort;
			}
			#endregion constructors

			public override object DeepCopy()
			{
				UShortVar newVar = new UShortVar(_parent);
				copyAttributes(this, newVar);
				newVar._tag = _tag;
				newVar._parent = _parent;
				if (newVar.Values != null)
					for (int i = 0; i < newVar.Values.Count; i++)
						newVar.Values[i] = (UShortVar)Values[i].DeepCopy();
				return newVar;
			}

			/// <summary>Gets or sets the final value.</summary>
			/// <exception cref="ArgumentOutOfRangeException"><see cref="RawValue"/> falls outside <b>0 to 65,535</b>.</exception>
			/// <exception cref="ArgumentException">Error computing the final value.</exception>
			public ushort Value
			{
				get { return UInt16.Parse(_value.ToString()); }
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
				get { return "2"; }
				set { throw new InvalidOperationException(_fixedLengthMsg); }
			}
			
			#region operators
			/// <summary>Converts an unsigned short to a boolean value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A boolean object.</returns>
			/// <remarks>Assumes default values for <see cref="BoolVar.TrueValue"/> and <see cref="BoolVar.FalseValue"/> after applying a byte-mask to isolate the top byte (<b>0xFF00</b>). Meaning any non-zero value is interpreted as <b>true</b> and the numerical equivalent will be <b>1</b>.</remarks>
			public static explicit operator BoolVar(UShortVar var)
			{
				BoolVar nv = new BoolVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = (BitConverter.GetBytes(var.Value)[0] != 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (BoolVar)(UShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned short to an unsigned byte.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned byte object.</returns>
			/// <remarks>New value is the first 8 bits from the original (<b>0xFF</b> mask applied).</remarks>
			public static explicit operator ByteVar(UShortVar var)
			{
				ByteVar nv = new ByteVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = BitConverter.GetBytes(var.Value)[0];
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ByteVar)(UShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned short to a double-precision value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A double-precision object.</returns>
			public static implicit operator DoubleVar(UShortVar var)
			{
				DoubleVar nv = new DoubleVar(var._parent);
				copyAttributes(var, nv);
				byte[] s = BitConverter.GetBytes(var.Value);
				byte[] a = { s[0], s[1], 0, 0, 0, 0, 0, 0 };
				nv.Value = BitConverter.ToDouble(a, 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (DoubleVar)(UShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned short to a signed 32-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 32-bit integer object.</returns>
			public static implicit operator IntVar(UShortVar var)
			{
				IntVar nv = new IntVar(var._parent);
				copyAttributes(var, nv);
				byte[] s = BitConverter.GetBytes(var.Value);
				byte[] a = { s[0], s[1], 0, 0 };
				nv.Value = BitConverter.ToInt32(a, 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (IntVar)(UShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned short to a signed 64-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 64-bit integer object.</returns>
			public static implicit operator LongVar(UShortVar var)
			{
				LongVar nv = new LongVar(var._parent);
				copyAttributes(var, nv);
				byte[] s = BitConverter.GetBytes(var.Value);
				byte[] a = { s[0], s[1], 0, 0, 0, 0, 0, 0 };
				nv.Value = BitConverter.ToInt64(a, 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (LongVar)(UShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned short to a signed byte.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed byte object.</returns>
			/// <remarks>New value is the first 8 bits from the original (<b>0xFF</b> mask applied).</remarks>
			public static explicit operator SByteVar(UShortVar var)
			{
				SByteVar nv = new SByteVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = (sbyte)BitConverter.GetBytes(var.Value)[0];
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (SByteVar)(UShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned short to a signed 16-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 16-bit integer object.</returns>
			public static implicit operator ShortVar(UShortVar var)
			{
				ShortVar nv = new ShortVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = (short)var.Value;
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ShortVar)(UShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned short to a single-precision value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A single-precision object.</returns>
			public static implicit operator SingleVar(UShortVar var)
			{
				SingleVar nv = new SingleVar(var._parent);
				copyAttributes(var, nv);
				byte[] s = BitConverter.GetBytes(var.Value);
				byte[] a = { s[0], s[1], 0, 0 };
				nv.Value = BitConverter.ToSingle(a, 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (SingleVar)(UShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned short to an unsigned 32-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 32-bit integer object.</returns>
			public static implicit operator UIntVar(UShortVar var)
			{
				UIntVar nv = new UIntVar(var._parent);
				copyAttributes(var, nv);
				byte[] s = BitConverter.GetBytes(var.Value);
				byte[] a = { s[0], s[1], 0, 0 };
				nv.Value = BitConverter.ToUInt32(a, 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (UIntVar)(UShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned short to an unsigned 64-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 64-bit integer object.</returns>
			public static implicit operator ULongVar(UShortVar var)
			{
				ULongVar nv = new ULongVar(var._parent);
				copyAttributes(var, nv);
				byte[] s = BitConverter.GetBytes(var.Value);
				byte[] a = { s[0], s[1], 0, 0, 0, 0, 0, 0 };
				nv.Value = BitConverter.ToUInt64(a, 0);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ULongVar)(UShortVar)var[i];
				}
				return nv;
			}
			#endregion operators
		}
	}
}