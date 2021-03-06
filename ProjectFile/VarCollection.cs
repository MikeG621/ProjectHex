﻿/*
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
 * [UPD] Names set checks and adjustment during SetCount()
 * v0.1.5, 150705
 * [UPD] Populate() implements StringVar.Encoding
 * [UPD] Add(StringVar) modified the ctor call
 * v0.1.4, 130910
 * [UPD] Var.DeepCopy() implementation
 * [UPD] StringVar arrays no long assign child's RawLength
 * [UPD] License
 * v0.1.3, 130701
 * [ADD] Serializable
 * [ADD] parentFile/Var for internal access to _parentFile/Var, propogate to _items[].Values
 * [UPD] _parentFile/Var no longer internal
 * [UPD] changed name mismatch message to include identifiers
 * [UPD] Populate() for StringVars, added stringlength
 * v0.1.1, 130421
 */

using System;
using System.Collections.Generic;
using System.Text;
using Idmr.Common;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
		/// <summary>Object to maintain multiple <see cref="Var">Vars</see>.</summary>
		[Serializable]
		public class VarCollection : ResizableCollection<Var>
		{
			ProjectFile _parentFile;
			Var _parentVar;
			internal string[] _names = null;    // names attribute is stored here before Binary is loaded
			int _nextID;

			#region constructors
			/// <summary>Creates an empty Collection.</summary>
			/// <param name="parent">The parent project</param>
			/// <remarks>No limit to collection size, initial capacity is 25.</remarks>
			internal VarCollection(ProjectFile parent)
			{
				parentFile = parent;
				_items = new List<Var>(25);
				Tag = null;
			}

			/// <summary>Creates an empty Collection.</summary>
			/// <param name="parent">The parent project</param>
			/// <param name="quantity">The initial capacity of the collection</param>
			/// <exception cref="ArgumentOutOfRangeException"><paramref name="quantity"/> is less than zero.</exception>
			/// <remarks>No limit to collection size.</remarks>
			internal VarCollection(ProjectFile parent, int quantity)
			{
				parentFile = parent;
				try { _items = new List<Var>(quantity); }
				catch (ArgumentOutOfRangeException) { throw; }
				Tag = null;
			}

			/// <summary>Creates an empty Collection.</summary>
			/// <param name="parent">The parent Var, whether for Collection or Arrays</param>
			/// <remarks>No limit to collection size, initial capacity is 25.</remarks>
			internal VarCollection(Var parent)
			{
				parentVar = parent;
				parentFile = parentVar._parent.parentFile;
				_items = new List<Var>(25);
				Tag = null;
			}

			/// <summary>Creates an empty Collection</summary>
			/// <param name="parent">The parent Var, whether for Collection or Arrays</param>
			/// <param name="quantity">The initial capacity of the collection</param>
			/// <exception cref="ArgumentOutOfRangeException"><paramref name="quantity"/> is less than zero.</exception>
			/// <remarks>No limit to collection size</remarks>
			internal VarCollection(Var parent, int quantity)
			{
				parentVar = parent;
				parentFile = parentVar._parent.parentFile;
				try { _items = new List<Var>(quantity); }
				catch (ArgumentOutOfRangeException) { throw; }
				Tag = null;
			}
			#endregion constructors

			#region public methods
			/// <summary>Provides the index of the item with the provided ID.</summary>
			/// <param name="id">The ID to search for.</param>
			/// <returns>The index of the item with the same ID value if found, otherwise <b>-1</b>.</returns>
			/// <remarks>Intended to be used when searching <see cref="Types"/>.</remarks>
			public int GetIndexByID(int id)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].ID == id) return i;
				return -1;
			}

			/// <summary>Provides the item with the provided ID.</summary>
			/// <param name="id">The ID to search for.</param>
			/// <returns>The matching item, otherwise <b>null</b>.</returns>
			/// <remarks>Intended to be used when searching <see cref="Types"/>.</remarks>
			public Var GetItemByID(int id)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].ID == id) return _items[i];
				return null;
			}

			/// <summary>Finds the index of the item with the specified <see cref="FixedSizeCollection{T}.Tag"/> value.</summary>
			/// <param name="tag">User-defined data.</param>
			/// <returns>The index of the first matching item, otherwise <b>-1</b>.</returns>
			public int GetIndexByTag(object tag)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].Tag != null && _items[i].Tag.ToString() == tag.ToString()) return i;
				return -1;
			}

			/// <summary>Finds the item with the specified <see cref="FixedSizeCollection{T}.Tag"/> value.</summary>
			/// <param name="tag">User-defined data.</param>
			/// <returns>The first matching item, otherwise <b>null</b>.</returns>
			public Var GetItemByTag(object tag)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].Tag != null && _items[i].Tag.ToString() == tag.ToString()) return _items[i];
				return null;
			}
			
			/// <summary>Finds the index of the item with the specified <see cref="Var.Name"/> value.</summary>
			/// <param name="name">Item name.</param>
			/// <returns>The index of the first matching item, otherwise <b>-1</b>.</returns>
			public int GetIndexByName(string name)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].Name == name) return i;
				return -1;
			}
			
			/// <summary>Finds the item with the specified <see cref="Var.Name"/> value.</summary>
			/// <param name="name">Item name.</param>
			/// <returns>The first matching item, otherwise <b>null</b>.</returns>
			public Var GetItemByName(string name)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].Name == name) return _items[i];
				return null;
			}
			
			/// <summary>Finds the the first index of the item with the specified type.</summary>
			/// <param name="type">The type to search for.</param>
			/// <returns>The index of the first matching item, otherwise <b>-1</b>.</returns>
			public int GetIndexByType(VarType type)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].Type == type) return i;
				return -1;
			}
			
			/// <summary>Finds the the first item of the item with the specified type.</summary>
			/// <param name="type">The type to search for.</param>
			/// <returns>The first matching item, otherwise <b>null</b>.</returns>
			public Var GetItemByType(VarType type)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].Type == type) return _items[i];
				return null;
			}

			/// <summary>Adds the given item to the end of the Collection</summary>
			/// <param name="item">The item to be added</param>
			/// <exception cref="ArgumentException"><i>item</i> uses dynamic variables</exception>
			/// <returns>The index of the added item if successfull, otherwise <b>-1</b></returns>
			/// <remarks>If <i>item</i> is a <see cref="Definition"/>, uses the next available ID per the parent <see cref="ProjectFile"/></remarks>
			public override int Add(Var item)
			{
				if (!_isLoading && item.IsDynamic)
				{
					System.Diagnostics.Debug.WriteLine(item.ToString());
					throw new ArgumentException("Cannot add existing items that use dynamic variables");
				}
				item._parent = this;
				int added = _add(item);
				if (item.Type == VarType.Definition && added != -1 && !_isLoading)
				{
					_items[added]._id = parentFile._nextID;
					parentFile._nextID++;
				}
				return added;
			}
			
			/// <summary>Adds a blank item of the specified <see cref="VarType"/> to the end of the Collection</summary>
			/// <param name="type">The VarType of the item to be added</param>
			/// <returns>The index of the added item if successfull, otherwise <b>-1</b></returns>
			/// <remarks><see cref="VarType.Calc"/> initalizes with a <see cref="Var.RawValue"/> of <b>0</b>. <see cref="VarType.String"/> initializes with a <see cref="Var.RawLength"/> of <b>0</b>.<br/>
			/// <see cref="VarType.Definition"/> uses the next available ID per the parent <see cref="ProjectFile"/>.</remarks>
			public int Add(VarType type)
			{
				int added = -1;
				if (type == VarType.Bool) added = _add(new BoolVar(this));
				else if (type == VarType.Byte) added = _add(new ByteVar(this));
				else if (type == VarType.Collection) added = _add(new CollectionVar(this));
				else if (type == VarType.Definition)
				{
					added = _add(new DefinitionVar(this));
					if (added != -1)
					{
						_items[added]._id = parentFile._nextID;
						parentFile._nextID++;
					}
				}
				else if (type == VarType.Double) added = _add(new DoubleVar(this));
				else if (type == VarType.Int) added = _add(new IntVar(this));
				else if (type == VarType.Long) added = _add(new LongVar(this));
				else if (type == VarType.SByte) added = _add(new SByteVar(this));
				else if (type == VarType.Short) added = _add(new ShortVar(this));
				else if (type == VarType.Single) added = _add(new SingleVar(this));
				else if (type == VarType.String) added = _add(new StringVar(this, false));
				else if (type == VarType.UInt) added = _add(new UIntVar(this));
				else if (type == VarType.ULong) added = _add(new ULongVar(this));
				else if (type == VarType.UShort) added = _add(new UShortVar(this));
				else added = _add(new Var(this));
				return added;
			}

			/// <summary>Inserts the given item at the specified index</summary>
			/// <param name="index">Location of the item</param>
			/// <param name="item">The item to be added</param>
			/// <exception cref="ArgumentException"><i>item</i> uses dynamic variables</exception>
			/// <returns>The index of the added item if successfull, otherwise <b>-1</b></returns>
			public override int Insert(int index, Var item)
			{
				if (item.IsDynamic) throw new ArgumentException("Cannot add existing items that use dynamic variables");
				item._parent = this;
				int added = _insert(index, item);
				if (item.Type == VarType.Definition && added != -1)
				{
					_items[added]._id = parentFile._nextID;
					parentFile._nextID++;
				}
				if (Count > 2)	// no dynamics possible before this point
				{
					for (int i = Count - 2; i >= index; i--) dynamicsFix(this, i, i + 1);
				}
				return added;
			}
			
			/// <summary>Expands or contracts the Collection, populating as necessary</summary>
			/// <param name="value">The new size of the Collection</param>
			/// <param name="allowTruncate">Controls if the Collection is allowed to get smaller</param>
			/// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative.</exception>
			/// <exception cref="InvalidOperationException"><paramref name="value"/> is smaller than <see cref="Count"/> and <paramref name="allowTruncate"/> is <b>false</b></exception>
			/// <remarks>If the Collection expands, the new items will be <see cref="VarType.Var"/>. When truncating the Collection, items will be lost starting from the last index. <see cref="Names"/> will also expand and contract, new entires will be blank.</remarks>
			public override void SetCount(int value, bool allowTruncate)
			{
				if (value < 0) throw new ArgumentOutOfRangeException("value must not be negative.");
				if (value == Count) return;
				else if (value < Count)
				{
					if (!allowTruncate) throw new InvalidOperationException("Reducing 'value' will cause data loss");
					else
					{
						if (value == 0)
						{
							_items.Clear();
							_names = null;
						}
						else
						{
							while (Count > value) _removeAt(Count - 1);
							if (_names != null)
							{
								string[] oldNames = _names;
								_names = new string[value];
								for (int i = 0; i < value; i++) _names[i] = oldNames[i];
							}
						}
					}
				}
				else
				{
					while (Count < value) Add(new Var(this));
					if (_names != null)
					{
						string[] oldNames = _names;
						_names = new string[value];
						for (int i = 0; i < oldNames.Length; i++) _names[i] = oldNames[i];
						for (int i = oldNames.Length; i < value; i++) _names[i] = "";
					}
				}
				if (!_isLoading) _isModified = true;
			}

			/// <summary>Deletes the specified item from the Collection</summary>
			/// <param name="index">Item index</param>
			/// <exception cref="ArgumentOutOfRangeException"><i>index</i> is outside the bounds of the collection.</exception>
			/// <exception cref="InvalidOperationException">The item <i>index</i> is in use elsewhere in the project. The <see cref="Exception.Message"/> will specify where it is used.</exception>
			/// <returns>Next available index.</returns>
			public int RemoveAt(int index)
			{
				if (index < 0 || index >= Count)
					throw new ArgumentOutOfRangeException("'index' must be 0-" + (Count - 1));
				string msg = "Cannot remove item, is currently in use by ";
				string check = indexCheck(parentFile.Properties, index, "Property ", false);
				if (check == "") check = indexCheck(parentFile.Types, index, "Type ", true);
				if (check != "")
					throw new InvalidOperationException(msg + check);
				if (Count > 1)
				{
					int next = _removeAt(index);
					for (int i = (index + 1); i <= Count; i++) dynamicsFix(this, i, i - 1);
					return next;
				}
				else if (Count == 1)
				{
					Clear();
					return 0;
				}
				return -1;
			}

			/// <summary>Re-initialize the contents of the Collection using raw byte data.</summary>
			/// <param name="rawData">Raw byte data from file.</param>
			/// <param name="startingOffset">Offset within <i>rawData</i> to begin reading.</param>
			/// <returns>First offset following the collection data.</returns>
			public int Populate(byte[] rawData, int startingOffset)
			{
                //System.Diagnostics.Debug.WriteLine("pop, " + startingOffset);
				bool loading = _isLoading;
				_isLoading = true;
				int id = -1;
				int count = Count;
				int pos = startingOffset;
				Var definition = null;
				if (parentVar != null && parentVar.Type == VarType.Collection)
				{
					id = parentVar.ID;
					definition = parentFile.Types.GetItemByID(id);
					count = definition.Quantity;
				}
				for (int i = 0; i < count; i++)
				{
					if (id != -1)
					{
						//System.Diagnostics.Debug.WriteLine(definition[i].Type.ToString());
						if (definition[i].Type == VarType.Bool) Add((BoolVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.Byte) Add((ByteVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.Collection) Add((CollectionVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.Double) Add((DoubleVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.Int) Add((IntVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.Long) Add((LongVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.SByte) Add((SByteVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.Short) Add((ShortVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.Single) Add((SingleVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.String) Add((StringVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.UInt) Add((UIntVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.ULong) Add((ULongVar)definition[i].DeepCopy());
						else if (definition[i].Type == VarType.UShort) Add((UShortVar)definition[i].DeepCopy());
						else Add((Var)definition[i].DeepCopy());
					}
					if (this[i].IsPresent)
					{
						if (this[i].RawOffset != "-1") pos = startingOffset + this[i].LocalOffset;
						if (this[i].Quantity != 0) this[i].Values.isLoading = true;
						switch (this[i].Type)
						{
							case VarType.Bool:
								if (this[i].Quantity == 0) this[i].RawValue = rawData[pos];
								else
								{
									for (int j = 0; j < this[i].Quantity; i++)
									{
										this[i].Values.Add(VarType.Bool);
										this[i][j].RawValue = rawData[pos++];
									}
								}
								break;
							case VarType.Byte:
								if (this[i].Quantity == 0) ((ByteVar)this[i]).Value = rawData[pos];
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.Byte);
										((ByteVar)this[i][j]).Value = rawData[pos++];
									}
								}
								break;
							case VarType.Collection:
								for (int j = 0; j < this[i].Quantity; j++)
								{
									this[i].Values.Add(new CollectionVar(this[i].Values, this[i].ID));
									this[i][j].Values = new VarCollection(this[i][j]);
									pos = this[i][j].Values.Populate(rawData, pos);
								}
								break;
							case VarType.Double:
								if (this[i].Quantity == 0) this[i].RawValue = BitConverter.ToDouble(rawData, pos);
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.Double);
										this[i][j].RawValue = BitConverter.ToDouble(rawData, pos);
										pos += 8;
									}
								}
								break;
							case VarType.Int:
								if (this[i].Quantity == 0) this[i].RawValue = BitConverter.ToInt32(rawData, pos);
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.Int);
										this[i][j].RawValue = BitConverter.ToInt32(rawData, pos);
										pos += 4;
									}
								}
								break;
							case VarType.Long:
								if (this[i].Quantity == 0) this[i].RawValue = BitConverter.ToInt64(rawData, pos);
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.Long);
										this[i][j].RawValue = BitConverter.ToInt64(rawData, pos);
										pos += 8;
									}
								}
								break;
							case VarType.SByte:
								if (this[i].Quantity == 0) this[i].RawValue = (sbyte)rawData[pos];
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.SByte);
										this[i][j].RawValue = (sbyte)rawData[pos++];
									}
								}
								break;
							case VarType.Short:
								if (this[i].Quantity == 0) this[i].RawValue = BitConverter.ToInt16(rawData, pos);
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.Short);
										this[i][j].RawValue = BitConverter.ToInt16(rawData, pos);
										pos += 2;
									}
								}
								break;
							case VarType.Single:
								if (this[i].Quantity == 0) this[i].RawValue = BitConverter.ToSingle(rawData, pos);
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.Single);
										this[i][j].RawValue = BitConverter.ToSingle(rawData, pos);
										pos += 4;
									}
								}
								break;
							case VarType.String:
								int stringLength = int.Parse(Equation.Evaluate(ParseDynamicValues(this, this[i].RawLength)));
								Encoding enc = ((StringVar)this[i]).Encoding;
								if (this[i].Quantity == 0 && stringLength != 0) this[i].RawValue = enc.GetString(rawData, pos, stringLength);
								else if (this[i].Quantity != 0)
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.String);
										if (this[i].RawLength != "0") this[i][j].RawValue = enc.GetString(rawData, pos, stringLength);
										pos += stringLength;
									}
								}
								break;
							case VarType.UInt:
								if (this[i].Quantity == 0) this[i].RawValue = BitConverter.ToUInt32(rawData, pos);
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.UInt);
										this[i][j].RawValue = BitConverter.ToUInt32(rawData, pos);
										pos += 4;
									}
								}
								break;
							case VarType.ULong:
								if (this[i].Quantity == 0) this[i].RawValue = BitConverter.ToUInt64(rawData, pos);
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.ULong);
										this[i][j].RawValue = BitConverter.ToUInt64(rawData, pos);
										pos += 8;
									}
								}
								break;
							case VarType.UShort:
								if (this[i].Quantity == 0) this[i].RawValue = BitConverter.ToUInt16(rawData, pos);
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.UShort);
										this[i][j].RawValue = BitConverter.ToUInt16(rawData, pos);
										pos += 2;
									}
								}
								break;
						}
						if (this[i].Quantity != 0)
						{
							for (int n = 0; n < this[i].Quantity; n++)
							{
								if (this[i].Values._names != null)
								{
									try { this[i][n].Name = this[i].Values._names[n]; }
									catch { this[i][n].Name = n.ToString(); System.Diagnostics.Debug.WriteLine("Names count mismatch: " + this[i].ToString() + "[" + n + "]"); }
								}
								else this[i][n].Name = n.ToString();
							}
							this[i].Values.isLoading = false;
						}
						else pos += this[i].Length;
					}
				}
				//System.Diagnostics.Debug.WriteLine("copied");
				if (definition != null && definition.RawLength != "-1")
					pos = startingOffset + int.Parse(definition.RawLength);
				_isLoading = loading;
				return pos;
			}

			/// <summary>Assign <see cref="NextID"/> to <paramref name="v"/>'s <see cref="Var.ID"/> value.</summary>
			/// <param name="v">The selected Var</param>
			/// <exception cref="InvalidOperationException">Was called on a collection other than the project Properties</exception>
			/// <remarks>To be used with Properties</remarks>
			public void AssignNextID(Var v)
			{
				if (Tag.ToString() != "properties") throw new InvalidOperationException("Only available for the Properties collection.");
				v._id = _nextID;
				_nextID++;
			}
			/// <summary>Reset the <see cref="Var.ID"/> value of <paramref name="v"/>.</summary>
			/// <param name="v">The selected Var</param>
			/// <exception cref="InvalidOperationException">Was called on a collection other than the project Properties</exception>
			/// <remarks>To be used with Properties</remarks>
			public void RemoveID(Var v)
			{
				if (Tag.ToString() != "properties") throw new InvalidOperationException("Only available for the Properties collection.");
				v._id = -1;
			}
			#endregion public methods

			#region public properties
			/// <summary>A single item within the collection</summary>
			/// <param name="label">The identifying string of the item in the form of "<b><see cref="Var.Type"/>:<see cref="Var.Name"/></b>"</param>
			/// <exception cref="ArgumentException"><paramref name="label"/> not found</exception>
			/// <returns>The item matching <paramref name="label"/>, otherwise <b>null</b></returns>
			/// <remarks><paramref name="label"/> is the same format as <see cref="Var.ToString()"/></remarks>
			public Var this[string label]
			{
				get
				{
					for (int i = 0; i < Count; i++)
						if (_items[i].ToString() == label) return _items[i];
					return null;
				}
				set
				{
					int index = -1;
					for (int i = 0; i < Count; i++)
						if (_items[i].ToString() == label) { index = i; break; }
					if (index == -1) throw new ArgumentException("label not found");
					if (!_isLoading) _isModified = true;
					_items[index] = value;
				}
			}
			/// <summary>Gets if the Collection or any child <see cref="Var">items</see> have been modified since loading</summary>
			public override bool IsModified
			{
				get
				{
					if (_isModified) return true;
					for (int i = 0; i < Count; i++)
					{
						_isModified |= _items[i].IsModified;
						if (_isModified) return true;	// due to nesting VarCollections, break the loop ASAP
					}
					return false;
				}
			}

			/// <summary>Gets or sets the list of names to be used for children.</summary>
			/// <exception cref="ArgumentException">Array size does not match <see cref="FixedSizeCollection{T}.Count"/>.</exception>
			/// <exception cref="InvalidOperationException">Attempted to set when dynamic <see cref="Var.Quantity"/> is used.</exception>
			public string[] Names
			{
				get { return _names; }
				set
				{
					if (_parentVar.HasDynamicQuantity) throw new InvalidOperationException("Dynamic Quantity, cannot set Names.");
					if (value.Length != Count) throw new ArgumentException("Names count does not match Collection count.");
					_names = value;
				}
			}

			/// <summary>Gets the next available ID number</summary>
			/// <remarks>Used in <see cref="Properties"/> to keep track of Vars used as input</remarks>
			public int NextID
			{
				get { return _nextID; }
				internal set { _nextID = value; }
			}
			#endregion public properties

			static string indexCheck(VarCollection vars, int index, string label, bool checkChildren)
			{
				for(int i = 0; i < vars.Count; i++)
				{
					if (i == index) continue;
					if (vars[i].ID == index) return label + vars[i].ToString() + ".ID";
					if (!label.Contains(",") && vars[i].ContainsDynamicMarker(index))	// comma ensures only top-level checked
						return label + vars[i].ToString() + " dynamics";
					if (checkChildren && vars[i].Values != null)
					{
						string check = indexCheck(vars[i].Values, index, label + vars[i].ToString() + ", ", true);
						if (check != "") return check;
					}
				}
				return "";
			}
			
			static void dynamicsFix(VarCollection vars, int oldIndex, int newIndex)
			{
				for (int i = 0; i < vars.Count; i++) vars[i].ReplaceDynamicIndex(oldIndex, newIndex);
			}
			
			/// <summary>Gets or sets if the collection is in a loading state.</summary>
			/// <remarks>Provides internal access to <see cref="ResizableCollection{T}._isLoading"/> for use throughout <see cref="ProjectFile"/>.</remarks>
			internal bool isLoading
			{
				get { return _isLoading; }
				set { _isLoading = value; }
			}
			
			/// <summary>Gets or sets the parent project.</summary>
			/// <remarks>Propogates value through child <see cref="Var.Values"/> entries.</remarks>
			internal ProjectFile parentFile
			{
				get { return _parentFile; }
				set
				{
					_parentFile = value;
					for (int i = 0; i < Count; i++)
						if (_items[i].Values != null) _items[i].Values.parentFile = value;
				}
			}
			
			/// <summary>Gets or sets the parent object.</summary>
			/// <remarks>Propogates value through child <see cref="Var.Values"/> entries.</remarks>
			internal Var parentVar
			{
				get { return _parentVar; }
				set
				{
					_parentVar = value;
					for (int i = 0; i < Count; i++)
						if (_items[i].Values != null) _items[i].Values.parentVar = value;
				}
			}
		}
	}
}