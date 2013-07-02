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
		/// <summary>Object for signed two-byte items.</summary>
		[Serializable]
		public class ShortVar : Var
		{	
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public ShortVar(VarCollection parent) : base(parent)
			{
				_type = VarType.Short;
			}
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="defaultValue">The starting value of the item</param>
			/// <remarks><see cref="RawValue"/> initializes to <b>0</b>.</remarks>
			public ShortVar(VarCollection parent, string defaultValue) : base(parent, defaultValue)
			{
				_type = VarType.Short;
			}
			#endregion constructors
			
			/// <summary>Gets or sets the final value.</summary>
			/// <exception cref="ArgumentOutOfRangeException"><see cref="RawValue"/> falls outside <b>-32,768 to 32,767</b>.</exception>
			/// <exception cref="ArgumentException">Error computing the final value.</exception>
			public short Value
			{
				get { return Int16.Parse(_value.ToString()); }
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
			/// <summary>Converts a signed byte to an unsigned byte.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned byte object.</returns>
			public static implicit operator ByteVar(ShortVar var)
			{
				ByteVar nv = new ByteVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToByte(var.Value & 0xFF);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ByteVar)(ShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to a double-precision value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A double-precision object.</returns>
			public static implicit operator DoubleVar(ShortVar var)
			{
				DoubleVar nv = new DoubleVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToDouble(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (DoubleVar)(ShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to a signed 32-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 32-bit integer object.</returns>
			public static implicit operator IntVar(ShortVar var)
			{
				IntVar nv = new IntVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToInt32(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (IntVar)(ShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to a signed 64-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed 64-bit integer object.</returns>
			public static implicit operator LongVar(ShortVar var)
			{
				LongVar nv = new LongVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToInt64(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (LongVar)(ShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to a signed byte.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A signed byte object.</returns>
			public static explicit operator SByteVar(ShortVar var)
			{
				SByteVar nv = new SByteVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToSByte(var.Value & 0xFF);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (SByteVar)(ShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to a single-precision value.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>A single-precision object.</returns>
			public static implicit operator SingleVar(ShortVar var)
			{
				SingleVar nv = new SingleVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToSingle(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (SingleVar)(ShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to an unsigned 32-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 32-bit integer object.</returns>
			public static implicit operator UIntVar(ShortVar var)
			{
				UIntVar nv = new UIntVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToUInt32(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (UIntVar)(ShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to an unsigned 64-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 64-bit integer object.</returns>
			public static implicit operator ULongVar(ShortVar var)
			{
				ULongVar nv = new ULongVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToUInt64(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (ULongVar)(ShortVar)var[i];
				}
				return nv;
			}
			
			/// <summary>Converts an unsigned byte to an unsigned 16-bit integer.</summary>
			/// <param name="var">The object to convert.</param>
			/// <returns>An unsigned 16-bit integer object.</returns>
			public static implicit operator UShortVar(ShortVar var)
			{
				UShortVar nv = new UShortVar(var._parent);
				copyAttributes(var, nv);
				nv.Value = Convert.ToUInt16(var.Value);
				if (nv.Values != null && nv.Values.Count > 0)
				{
					for (int i = 0; i < nv.Values.Count; i++)
						nv[i] = (UShortVar)(ShortVar)var[i];
				}
				return nv;
			}
			#endregion operators
		}
	}
}