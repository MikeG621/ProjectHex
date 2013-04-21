using System;
using System.Collections.Generic;
using Idmr.Common;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
		public class VarCollection : ResizableCollection<Var>
		{
			internal ProjectFile _parentFile;
			internal ProjectFile.Var _parentVar;
			internal string[] _names = null;	// names attribute is stored here before Binary is loaded

			#region constructors
			/// <summary>Creates an empty Collection.</summary>
			/// <remarks>No limit to collection size, initial capactiy is 25.</remarks>
			internal VarCollection(ProjectFile parent)
			{
				_parentFile = parent;
				_items = new List<Var>(25);
				Tag = null;
			}

			/// <summary>Creates an empty Collection.</summary>
			/// <exception cref="ArgumentOutOfRangeException"><i>quantity</i> is less than zero.</exception>
			/// <remarks>No limit to collection size, initial capacity is set to <i>quantity</i>.</remarks>
			internal VarCollection(ProjectFile parent, int quantity)
			{
				_parentFile = parent;
				try { _items = new List<Var>(quantity); }
				catch (ArgumentOutOfRangeException) { throw; }
				Tag = null;
			}

			/// <summary>Creates an empty Collection.</summary>
			/// <remarks>No limit to collection size, initial capactiy is 25.</remarks>
			internal VarCollection(Var parent)
			{
				_parentVar = parent;
				_parentFile = _parentVar._parent._parentFile;
				_items = new List<Var>(25);
				Tag = null;
			}

			/// <summary>Creates an empty Collection</summary>
			/// <remarks>No limit to collection size, initial capacity is set to <i>quantity</i></remarks>
			internal VarCollection(Var parent, int quantity)
			{
				_parentVar = parent;
				_parentFile = _parentVar._parent._parentFile;
				_items = new List<Var>(quantity);
				Tag = null;
			}
			#endregion constructors

			#region public methods
			/// <summary>Provides the index of the item with the provided ID</summary>
			/// <param name="id">The ID to search for</param>
			/// <returns>The index of the item with the same ID value if found, otherwise <b>-1</b></returns>
			/// <remarks>Intended to be used when searching <see cref="ProjectFile.Types"/>, use <see cref="GetIndexByTag"/> as a general-purpose search function</remarks>
			public int GetIndexByID(int id)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].ID == id) return i;
				return -1;
			}
			
			/// <summary>Provides the item with the provided ID</summary>
			/// <param name="id">The ID to search for</param>
			/// <returns>The matching item, otherwise <b>-1</b></returns>
			/// <remarks>Intended to be used when searching <see cref="ProjectFile.Types"/>, use <see cref="GetItemByTag"/> as a general-purpose search function</remarks>
			public Var GetItemByID(int id)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].ID == id) return _items[i];
				return null;
			}
			
			/// <summary>Finds the index of the item with the specified <see cref="Var.Tag"/> value</summary>
			/// <param name="tag">User-defined data</param>
			/// <returns>The index of the first matching item, otherwise <b>-1</b></returns>
			public int GetIndexByTag(object tag)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].Tag != null && _items[i].Tag.ToString() == tag.ToString()) return i;
				return -1;
			}
			
			/// <summary>Finds the item with the specified <see cref="Var.Tag"/> value</summary>
			/// <param name="tag">User-defined data</param>
			/// <returns>The first matching item, otherwise <b>null</b></returns>
			public Var GetItemByTag(object tag)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].Tag != null && _items[i].Tag.ToString() == tag.ToString()) return _items[i];
				return null;
			}
			
			/// <summary>Finds the the first index of the item with the specified type</summary>
			/// <param name="type">The type to search for</param>
			/// <returns>The index of the first item with the matching type, otherwise <b>-1</b></returns>
			public int GetIndexByType(VarType type)
			{
				for (int i = 0; i < Count; i++)
					if (_items[i].Type == type) return i;
				return -1;
			}
			
			/// <summary>Finds the the first item of the item with the specified type</summary>
			/// <param name="type">The type to search for</param>
			/// <returns>The first matching item, otherwise <b>null</b></returns>
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
				if (!item._parent.isLoading && item.IsDynamic) throw new ArgumentException("Cannot add existing items that use dynamic variables");
				item._parent = this;
				int added = _add(item);
				if (item.Type == VarType.Definition && added != -1 && !_isLoading)
				{
					_items[added]._id = _parentFile._nextID;
					_parentFile._nextID++;
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
						_items[added]._id = _parentFile._nextID;
						_parentFile._nextID++;
					}
				}
				else if (type == VarType.Double) added = _add(new DoubleVar(this));
				else if (type == VarType.Int) added = _add(new IntVar(this));
				else if (type == VarType.Long) added = _add(new LongVar(this));
				else if (type == VarType.SByte) added = _add(new SByteVar(this));
				else if (type == VarType.Short) added = _add(new ShortVar(this));
				else if (type == VarType.Single) added = _add(new SingleVar(this));
				else if (type == VarType.String) added = _add(new StringVar(this, "false"));
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
					_items[added]._id = _parentFile._nextID;
					_parentFile._nextID++;
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
			/// <exception cref="InvalidOperationException"><i>value</i> is smaller than <see cref="Count"/> and <i>allowTruncate</i> is <b>false</b></exception>
			/// <remarks>If the Collection expands, the new items will be <see cref="VarType.Var"/>. When truncating the Collection, items will be lost starting from the last index.</remarks>
			public override void SetCount(int value, bool allowTruncate)
			{
				if (value == Count) return;
				else if (value < Count)
				{
					if (!allowTruncate) throw new InvalidOperationException("Reducing 'value' will cause data loss");
					else
					{
						if (value == 0) _items.Clear();
						else
							while(Count > value) _removeAt(Count - 1);
					}
				}
				else
					while(Count < value) Add(new Var(this));
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
				string check = indexCheck(_parentFile.Properties, index, "Property ", false);
				if (check == "") check = indexCheck(_parentFile.Types, index, "Type ", true);
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
				bool loading = _isLoading;
				_isLoading = true;
				int id = -1;
				int count = Count;
				int pos = startingOffset;
				ProjectFile.Var definition = null;
				if (_parentVar != null && _parentVar.Type == VarType.Collection)
				{
					id = _parentVar.ID;
					definition = _parentFile.Types.GetItemByID(id);
					count = definition.Quantity;
				}
				for (int i = 0; i < count; i++)
				{
					if (id != -1)
					{
						Add(definition.Values[i].Type);
						this[i]._name = definition.Values[i]._name;
						this[i].Tag = this[i].ToString();
						this[i]._length = definition.Values[i]._length;
						this[i]._offset = definition.Values[i]._offset;
						this[i]._quantity = definition.Values[i]._quantity;
						this[i]._condition = definition.Values[i]._condition;
						this[i]._comment = definition.Values[i]._comment;
						this[i]._id = definition.Values[i]._id;
						this[i]._default = definition.Values[i]._default;
						if (this[i].Type == VarType.Bool)
						{
							((BoolVar)this[i]).TrueValue = ((BoolVar)definition.Values[i]).TrueValue;
							((BoolVar)this[i]).FalseValue = ((BoolVar)definition.Values[i]).FalseValue;
						}
						if (this[i].Type == VarType.String)
							((StringVar)this[i]).NullTermed = ((StringVar)definition.Values[i]).NullTermed;
						this[i].Values = definition.Values[i].Values;
						if (this[i].Values != null)
						{
							this[i].Values._parentFile = _parentFile;
							this[i].Values._parentVar = this[i];
						}
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
										this[i].Values[j].RawValue = rawData[pos++];
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
										((ByteVar)this[i].Values[j]).Value = rawData[pos++];
									}
								}
								break;
							case VarType.Collection:
								for (int j = 0; j < this[i].Quantity; j++)
								{
									this[i].Values.Add(new CollectionVar(this[i].Values, this[i].ID));
									this[i].Values[j].Values = new VarCollection(this[i].Values[j]);
									pos = this[i].Values[j].Values.Populate(rawData, pos);
								}
								break;
							case VarType.Double:
								if (this[i].Quantity == 0) this[i].RawValue = BitConverter.ToDouble(rawData, pos);
								else
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.Double);
										this[i].Values[j].RawValue = BitConverter.ToDouble(rawData, pos);
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
										this[i].Values[j].RawValue = BitConverter.ToInt32(rawData, pos);
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
										this[i].Values[j].RawValue = BitConverter.ToInt64(rawData, pos);
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
										this[i].Values[j].RawValue = (sbyte)rawData[pos++];
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
										this[i].Values[j].RawValue = BitConverter.ToInt16(rawData, pos);
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
										this[i].Values[j].RawValue = BitConverter.ToSingle(rawData, pos);
										pos += 4;
									}
								}
								break;
							case VarType.String:
								if (this[i].Quantity == 0 && Int32.Parse(Equation.Evaluate(ParseDynamicValues(this, this[i].RawLength))) != 0) this[i].RawValue = ArrayFunctions.ReadStringFromArray(rawData, pos, Int32.Parse(Equation.Evaluate(ParseDynamicValues(this, this[i].RawLength))));
								else if (this[i].Quantity != 0)
								{
									for (int j = 0; j < this[i].Quantity; j++)
									{
										this[i].Values.Add(VarType.String);
										if (this[i].RawLength != "0") this[i].Values[j].RawValue = ArrayFunctions.ReadStringFromArray(rawData, pos, Int32.Parse(Equation.Evaluate(ParseDynamicValues(this, this[i].RawLength))));
										this[i].Values[j].RawLength = this[i].RawLength;
										pos += Int32.Parse(Equation.Evaluate(ParseDynamicValues(this, this[i].RawLength)));
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
										this[i].Values[j].RawValue = BitConverter.ToUInt32(rawData, pos);
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
										this[i].Values[j].RawValue = BitConverter.ToUInt64(rawData, pos);
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
										this[i].Values[j].RawValue = BitConverter.ToUInt16(rawData, pos);
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
									try { this[i].Values[n].Name = this[i].Values._names[n]; }
									catch { this[i].Values[n].Name = n.ToString(); System.Diagnostics.Debug.WriteLine("Names count mismatch"); }
								}
								else this[i].Values[n].Name = n.ToString();
								this[i].Values[n].Tag = this[i].Values[n].ToString();
							}
							this[i].Values.isLoading = false;
						}
						else pos += this[i].Length;
					}
				}
				if (definition != null && definition.RawLength != "-1")
					pos = startingOffset + Int32.Parse(definition.RawLength);
				_isLoading = loading;
				return pos;
			}
			#endregion public methods

			#region public properties
			/// <summary>A single item within the collection</summary>
			/// <param name="label">The identifying string of the item in the form of "<see cref="Var.Type">Type</see><see cref="Var.Name"/></b>"</param>
			/// <exception cref="ArgumentException">Resource not found</exception>
			/// <returns>The item matching <i>label</i>, otherwise <b>null</b></returns>
			/// <remarks><i>label</i> is the same format as <see cref="Var.ToString()"/></remarks>
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

			public object Tag { get; set; }
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
			
			/// <summary>Internal property for access within the rest of ProjectFile</summary>
			internal bool isLoading
			{
				get { return _isLoading; }
				set { _isLoading = value; }
			}
		}
	}
}