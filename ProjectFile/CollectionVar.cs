/*
 * Idmr.ProjectHex.ProjectFile.dll, Project definition library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
 * with this file, You can obtain one at http://mozilla.org/MPL/2.0/
 *
 * Version: 0.1.5+
 */

/* CHANGELOG
 * [UPD] changed type class references to normal type
 * v0.1.4, 130910
 * [ADD] DeepCopy
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
		/// <summary>Object for containers of multiple <see cref="Var"/> types.</summary>
		[Serializable]
		public class CollectionVar : Var
		{
			#region constructors
			/// <summary>Initializes a new item.</summary>
			/// <param name="parent">The collection the item belongs to.</param>
			/// <param name="id">The ID of the definition used.</param>
			/// <exception cref="ArgumentNullException"><i>id</i> is <b>null</b>.</exception>
			/// <exception cref="FormatException"><i>id</i> is not a valid integer value.</exception>
			/// <exception cref="OverflowException"><i>id</i> represents a number outside the range of Int32.</exception>
			/// <exception cref="ArgumentOutOfRangeException">No <see cref="ProjectFile.Properties"/> with an ID of <i>id</i> were found.</exception>
			public CollectionVar(VarCollection parent, string id)
			{
				if (id == "" || id == null)
					throw new ArgumentNullException("Collection items require 'id' attribute", "id");
				try { _id = int.Parse(id); }
				catch (FormatException x) { throw new FormatException("'id' is not a valid integer", x); }
				catch (OverflowException x) { throw new OverflowException("'id' must be lower than " + int.MaxValue, x); }
				// projects have to be written with child definitions at the top. IDs can be whatever, but new child Collections must be inserted above whatever parent wants to use it
				// ie: if I have a Square, and I decide that I want to make it out of Lines, the new Line definition must be inserted before Square in _parent
				if (!parent.isLoading && parent.parentFile.Types.GetIndexByID(_id) == -1)
					throw new ArgumentOutOfRangeException("ProjectFile Type definition with specified ID not found");
				_parent = parent;
				_type = VarType.Collection;
			}
			
			/// <summary>Initializes a new item</summary>
			/// <param name="parent">The collection the item belongs to</param>
			/// <remarks>ID must be set manually</remarks>
			internal CollectionVar(VarCollection parent)
			{
				_parent = parent;
				_type = VarType.Collection;
			}
			
			/// <summary>Initializes a new item</summary>
			/// <param name="parent">The collection the item belongs to</param>
			/// <param name="id">The ID of the definition used</param>
			/// <exception cref="ArgumentOutOfRangeException">No <see cref="ProjectFile.Properties"/> with an ID of <i>id</i> were found</exception>
			public CollectionVar(VarCollection parent, int id)
			{
				if (!parent.isLoading && parent.parentFile.Types.GetIndexByID(id) == -1)
					throw new ArgumentOutOfRangeException("ProjectFile Type definition with specified ID not found");
				_parent = parent;
				_type = VarType.Collection;
				_id = id;
			}
			#endregion constructors

			public override object DeepCopy()
			{
				CollectionVar newVar = new CollectionVar(_parent);
				copyAttributes(this, newVar);
				newVar._tag = _tag;
				newVar._parent = _parent;
				if (newVar.Values != null)
					for (int i = 0; i < newVar.Values.Count; i++)
					{
						if (Values[i]._type == VarType.Bool) newVar[i].Values[i] = (BoolVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.Byte) newVar[i].Values[i] = (ByteVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.Collection) newVar.Values[i] = (CollectionVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.Double) newVar[i].Values[i] = (DoubleVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.Int) newVar[i].Values[i] = (IntVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.Long) newVar[i].Values[i] = (LongVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.SByte) newVar[i].Values[i] = (SByteVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.Short) newVar[i].Values[i] = (ShortVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.Single) newVar[i].Values[i] = (SingleVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.String) newVar[i].Values[i] = (StringVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.UInt) newVar[i].Values[i] = (UIntVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.ULong) newVar[i].Values[i] = (ULongVar)Values[i].DeepCopy();
						else if (Values[i]._type == VarType.UShort) newVar[i].Values[i] = (UShortVar)Values[i].DeepCopy();
					}
				return newVar;
			}

			/// <summary>Disabled</summary>
			/// <exception cref="InvalidOperationException">Collection items do not contain a value</exception>
			/// <remarks>Always returns <b>null</b></remarks>
			public override object RawValue
			{
				get { return null; }
				set { throw new InvalidOperationException(_type.ToString() +_disableAttributeMsg + "'value'"); }
			}
			
			/// <summary>Gets the raw length definition per the appropriate <see cref="DefinitionVar"/>.</summary>
			/// <exception cref="InvalidOperationException">Cannot directly set the length of Collection items.</exception>
			/// <remarks>Returns the RawLength value of the origination DefinitionVar.</remarks>
			public override string RawLength
			{
				get { return _parent.parentFile._types.GetItemByID(_id).RawLength; }
				set { throw new InvalidOperationException(_definitionControlMsg + "'length'"); }
			}
			
			/// <summary>Disabled</summary>
			/// <exception cref="InvalidOperationException">Collection items do not contain default values</exception>
			/// <remarks>Always returns <b>null</b></remarks>
			public override object DefaultValue
			{
				get { return null; }
				set { throw new InvalidOperationException(_type.ToString() +_disableAttributeMsg + "'default'"); }
			}
		}
	}
}