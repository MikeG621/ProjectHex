/*
 * Idmr.ProjectHex.ProjectFile.dll, Project definition library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in GPL.txt
 * Version: 0.1
 */
 
/* CHANGELOG
 * [ADD] Serializable, operators
 * v0.1, XXXXXX
 */
 
using System;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
		/// <summary>Object for unsigned single-byte items.</summary>
		[Serializable]
		public class ByteVar : Var
		{	
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public ByteVar(VarCollection parent) : base(parent)
			{
				_type = VarType.Byte;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="defaultValue">The starting value of the item</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public ByteVar(VarCollection parent, string defaultValue) : base(parent, defaultValue)
			{
				_type = VarType.Byte;
			}
			#endregion constructors
			
			/// <summary>Gets or sets the final value.</summary>
			/// <exception cref="ArgumentOutOfRangeException"><see cref="RawValue"/> falls outside <b>0 to 255</b>.</exception>
			public byte Value
			{
				get { return Byte.Parse(_value.ToString()); }
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
				get { return "1"; }
				set { throw new InvalidOperationException(_fixedLengthMsg); }
			}
			
			#region operators
			/// <summary>Converts an unsigned byte to a double-precision value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A double-precision object.</returns>
			public static implicit operator DoubleVar(ByteVar var)
			{
				DoubleVar nv = new DoubleVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToDouble(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (DoubleVar)(ByteVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to a signed 32-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 32-bit integer object.</returns>
			public static implicit operator IntVar(ByteVar var)
			{
				IntVar nv = new IntVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToInt32(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (IntVar)(ByteVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to a signed 64-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 64-bit integer object.</returns>
			public static implicit operator LongVar(ByteVar var)
			{
				LongVar nv = new LongVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToInt64(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (LongVar)(ByteVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to a signed byte.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed byte object.</returns>
			public static implicit operator SByteVar(ByteVar var)
			{
				SByteVar nv = new SByteVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToSByte(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (SByteVar)(ByteVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to a signed 16-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 16-bit integer object.</returns>
			public static implicit operator ShortVar(ByteVar var)
			{
				ShortVar nv = new ShortVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToInt16(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ShortVar)(ByteVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to a single-precision value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A single-precision object.</returns>
			public static implicit operator SingleVar(ByteVar var)
			{
				SingleVar nv = new SingleVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToSingle(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (SingleVar)(ByteVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to an unsigned 32-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 32-bit integer object.</returns>
			public static implicit operator UIntVar(ByteVar var)
			{
				UIntVar nv = new UIntVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToUInt32(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (UIntVar)(ByteVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to an unsigned 64-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 64-bit integer object.</returns>
			public static implicit operator ULongVar(ByteVar var)
			{
				ULongVar nv = new ULongVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToUInt64(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ULongVar)(ByteVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to an unsigned 16-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 16-bit integer object.</returns>
			public static implicit operator UShortVar(ByteVar var)
			{
				UShortVar nv = new UShortVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToUInt16(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (UShortVar)(ByteVar)var[i];
				}
				return nv;
			}
			#endregion operators
		}
	}
}