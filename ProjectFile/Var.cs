/*
 * Idmr.ProjectHex.ProjectFile.dll, Project definition library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * Licensed under the GPL v3.0 or later
 * 
 * Full notice in GPL.txt
 * Version: 0.1
 */
 
/* CHANGELOG
 * [ADD] _parent get/set for _parentCollection
 * [UPD] _parent renamed to _parentCollection, isDynamicText/getDynamicSplit to static, _parentCollection no longer internal
 * [ADD] Serializable
 * v0.1, XXXXXX
 */
 
using System;
using Idmr.Common;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
		/// <summary>Base class for project items</summary>
		/// <remarks>Also used for <see cref="VarType.Undefined"/> types.</remarks>
		[Serializable]
		public class Var
		{
			protected VarCollection _parentCollection = null;
			protected object _tag = null;
			/// <summary>Flag used to indicated if the object has been changed since load.</summary> 
			protected internal bool _isModified = false;
			
			protected static string _fixedLengthMsg = "Type is of a fixed length, cannot be set";
			protected static string _parentControlMsg = "Attribute is controlled by the parent array";
			protected static string _disableAttributeMsg = " items do not contain ";
			protected static string _definitionControlMsg = "Attribute is controlled be the type definition";
			
			#region XML values
			protected internal string _name = "NewVar";
			protected internal string _offset = "-1";
			protected internal string _length = "1";
			protected internal object _value = 0;
			protected internal string _quantity = "";
			protected internal string _condition = "";
			protected internal int _id = -1;
			protected internal string _comment = "";
			protected internal object _default = null;
			protected internal VarType _type = VarType.Undefined;
			protected internal bool _isValidated = false;
			
			public VarCollection Values = null;
			#endregion

			#region constructors
			internal Var()
			{
				/* do nothing */
			}
			
			/// <summary>Initializes an Undefined item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <exception cref="ArgumentNullException"><i>parent</i> is <b>null</b>.</exception>
			internal Var(VarCollection parent)
			{
				_parent = parent;
				if (parent == null) throw new ArgumentNullException("parent cannot be null");
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = 0;
				_parent.isLoading = loading;
			}
			
			/// <summary>Initializes an Undefined item.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="defaultValue">The starting value of the item.</param>
			/// <exception cref="ArgumentNullException"><i>parent</i> is <b>null</b>.</exception>
			internal Var(VarCollection parent, string defaultValue)
			{
				_parent = parent;
				if (parent == null) throw new ArgumentNullException("parent cannot be null");
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				RawValue = 0;
				DefaultValue = defaultValue;
				_parent.isLoading = loading;
			}
			#endregion constructors

			#region public methods
			/// <summary>Returns a string representation of the item</summary>
			/// <remarks>Format is "<b><see cref="Type"/>:(Parent.Name.)<see cref="Name"/></b>".<br/>
			/// <i>Parent.Name.</i> is not included for items in <see cref="ProjectFile.Properties"/> or <see cref="VarType.Definition"/> items.<br/>
			/// <i>Parent.Name</i> nests as required to complete the breadcrumbs, allowing names such as <b>Short:FlightGroups.2.Waypoints.X.Startpoint.2</b>.</remarks>
			public override string ToString()
			{
				return Type + ":" + (_parent.parentVar != null ? _parent.parentVar.ToString().Substring(_parent.parentVar.ToString().IndexOf(':') + 1) + "." : "") + Name;
			}

			/// <summary>Gets if the item contains the specified <i>index</i> as a dynamic variable</summary>
			/// <param name="index">The dynamic variable to search for.</param>
			/// <exception cref="ArgumentOutOfRangeException"><i>index</i> falls outside the range of the parent <see cref="VarCollection"/></exception>
			/// <returns><b>true</b> if the dynamic marker for the specified <i>index</i> is found, otherwise <b>false</b>.</returns>
			public bool ContainsDynamicMarker(int index)
			{
				if (!IsDynamic) return false;
				if (index < 0 || index >= _parent.Count)
					throw new ArgumentOutOfRangeException("'index' is not valid for the parent Collection");
				return (getDynamicSplit(_default.ToString(), index) != null || getDynamicSplit(_length, index) != null || getDynamicSplit(_offset, index) != null || getDynamicSplit(_quantity, index) != null || getDynamicSplit(_value.ToString(), index) != null || getDynamicSplit(_condition, index) != null);
			}
			
			/// <summary>Updates the dynamic indexes throughout the item.</summary>
			/// <param name="oldIndex">The index value to replace.</param>
			/// <param name="newIndex">The new index value.</param>
			public void ReplaceDynamicIndex(int oldIndex, int newIndex)
			{
				if (!IsDynamic) return;
				bool updated = false;
				string[] temp = getDynamicSplit(_default.ToString(), oldIndex);
				if (temp != null)
				{
					_default = temp[0] + "$" + newIndex + temp[2];
					updated = true;
				}
				temp = getDynamicSplit(_length, oldIndex);
				if (temp != null)
				{
					_length = temp[0] + "$" + newIndex + temp[2];
					updated = true;
				}
				temp = getDynamicSplit(_offset, oldIndex);
				if (temp != null) 
				{
					_offset = temp[0] + "$" + newIndex + temp[2];
					updated = true;
				}
				temp = getDynamicSplit(_quantity, oldIndex);
				if (temp != null) 
				{
					_quantity = temp[0] + "$" + newIndex + temp[2];
					updated = true;
				}
				temp = getDynamicSplit(_value.ToString(), oldIndex);
				if (temp != null) 
				{
					_value = temp[0] + "$" + newIndex + temp[2];
					updated = true;
				}
				temp = getDynamicSplit(_condition, oldIndex);
				if (temp != null) 
				{
					_condition = temp[0] + "$" + newIndex + temp[2];
					updated = true;
				}
				if (updated) ReplaceDynamicIndex(oldIndex, newIndex);
			}
			#endregion public methods
			
			#region public properties
			/// <summary>Provides Indexer getter and setter for <see cref="Values"/></summary>
			/// <param name="index">Item index within <see cref="Values"/></param>
			/// <exception cref="ArgumentOutOfRangeException">Invalid value for <i>index</i>.</exception>
			/// <exception cref="InvalidOperationException">Attempted to set when <see cref="Values"/> has not been initialized.</exception>
			/// <returns>The <see cref="Var"/> at the specified <i>index</i>, otherwise <b>null</b>.</returns>
			public Var this[int index]
			{
				get
				{
					if (Values != null) return Values[index];
					else return null;
				}
				set
				{
					if (Values == null) throw new InvalidOperationException("Values has not been initialized");
					Values[index] = value;
				}
			}
			/// <summary>Gets if the length definition must be calculated</summary>
			public bool HasDynamicLength { get { return isDynamicText(_length); } }
			/// <summary>Gets if the offset definition must be calculated</summary>
			public bool HasDynamicOffset { get { return isDynamicText(_offset); } }
			/// <summary>Gets if the quantity definition must be calculated</summary>
			public bool HasDynamicQuantity { get { return isDynamicText(_quantity); } }
			/// <summary>Gets if the value definition must be calculated</summary>
			public bool HasDynamicValue { get { return isDynamicText(_value.ToString()); } }
			
			/// <summary>Gets if any attribute in the item must be calculated</summary>
			public bool IsDynamic { get { return ((_default != null ? isDynamicText(_default.ToString()) : false) || HasDynamicLength || HasDynamicOffset || HasDynamicQuantity || HasDynamicValue || _condition != ""); } }
			
			/// <summary>Gets if the item or any child of <see cref="Values"/> have been modified since loading</summary>
			public bool IsModified
			{
				get
				{
					if (_isModified) return true;
					if (Values != null) _isModified = Values.IsModified;
					return _isModified;
				}
			}

			#region XML values
			/// <summary>Gets or sets if the computed value is used for validating the <see cref="ProjectFile"/> against binary files.</summary>
			/// <exception cref="InvalidOperationException">Parent <see cref="Var"/> controls value</exception>
			public bool IsValidated
			{
				get { return (_parent.parentVar != null ? _parent.parentVar._isValidated : _isValidated); }
				set
				{
					if (_parent.parentVar != null && _parent.parentVar.Type != VarType.Definition) throw new InvalidOperationException(_parentControlMsg);
					_isValidated = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
			/// <summary>Gets the item ID number</summary>
			/// <remarks>Only used for <see cref="VarType.Definition"/> and <see cref="VarType.Collection"/> types</remarks>
			public int ID { get { return _id; } }
			
			/// <summary>Gets or sets the note for the item</summary>
			public string Comment
			{
				get { return _comment; }
				set
				{
					if (value == "" || value == null) _comment = "";
					else _comment = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
			/// <summary>Gets or sets the name of the item</summary>
			/// <exception cref="InvalidOperationException">Value cannot be empty or <i>null</i></exception>
			public string Name
			{
				get { return _name; }
				set
				{
					if (value == "" || value == null)
						throw new InvalidOperationException("Name cannot be empty or null");
					else _name = value;
					if (!_parent.isLoading) _isModified = true;
				}
			}
			/// <summary>Gets the item's type</summary>
			public VarType Type { get { return _type; } }
			
			/// <summary>Gets or sets the conditional definition of the item</summary>
			/// <exception cref="ArgumentException">Value is static<br/><b>-or-</b><br/>Computation error, possibly due to illegal characters or bracket mismatch. Changes will be ignored.</exception>
			/// <exception cref="ArgumentOutOfRangeException">Dynamic markers fall outside the range of the parent Collection</exception>
			/// <exception cref="InvalidOperationException">First file property cannot be conditional<br/><b>-or-</b>Parent <see cref="Var"/> controls value</exception>
			/// <remarks>Dynamic values are required.<br/>
			/// An empty or <b>null</b> value clears the conditional, resulting in an always-present value.</remarks>
			public virtual string RawCondition
			{
				get { return (_isArrayChild ? _parent.parentVar._condition : _condition); }
				set
				{
					if (value == _condition) return;
					if (_parent.parentVar != null && _parent.parentVar.Type != VarType.Definition) throw new InvalidOperationException(_parentControlMsg);
					string condition = _condition;
					if (value == "" || value == null) _condition = "";
					else if (_parent.Tag != null && _parent.Tag.ToString() == "properties" && Tag == _parent.parentFile.Properties[0].Tag)
						throw new InvalidOperationException("First property must always exist and cannot be conditional");
					else if (!isDynamicText(value))
						throw new ArgumentException("Condition attributes require dynamic values", "RawCondition.value");
					else _condition = value;
					if (!_parent.isLoading) _isModified = true;
					try { bool dummy = IsPresent; }
					catch (InvalidOperationException) { return; }
					catch (ArgumentOutOfRangeException x) { throw x; }
					catch (ArgumentException x)
					{
						_condition = condition;
						throw x;
					}
				}
			}
			/// <summary>Gets or sets the length definition of the item</summary>
			/// <exception cref="ArgumentException">Value is static<br/><b>-or-</b>Calculation error with static equation, must be positive</exception>
			/// <exception cref="InvalidOperationException">Parent <see cref="Var"/> controls value</exception>
			/// <remarks>Dynamic values are not permitted. Static equations are solved and saved as the resultant.<br/>
			/// Default value is <b>"1"</b>. An empty or <b>null</b> value returns to the default.</remarks>
			public virtual string RawLength
			{
				get { return (_isArrayChild ? _parent.parentVar._length : _length); }
				set
				{
					if (value == _length) return;
					if (_parent.parentVar != null && _parent.parentVar.Type != VarType.Definition) throw new InvalidOperationException(_parentControlMsg);
					if (value == "" || value == null) _length = "1";
					else if (isDynamicText(value)) throw new ArgumentException("RawLength cannot be dynamic");
					else
					{
						string eval = "";
						try { eval = Equation.Evaluate(value); }
						catch (ArgumentException) { throw; }
						catch (FormatException) { throw; }
						catch (OverflowException x) { throw new ArgumentOutOfRangeException("Value must be positive and less than " + Int32.MaxValue, x); }
						if (Int32.Parse(eval) < 1)
							throw new ArgumentException("Value must be positive", "RawLength.value");
						_length = eval;
					}
					if (!_parent.isLoading) _isModified = true;
				}
			}
			/// <summary>Gets or sets the offset definition of the item</summary>
			/// <exception cref="ArgumentException">Static equation does not properly evaluate to an <see cref="Int64"/>.</exception>
			/// <exception cref="ArgumentOutOfRangeException">Dynamics do not calculate to a non-negative value within the range of <see cref="Int64"/>.<br/><b>-or-</b><br/>Dynamic markers fall outside the range of the parent Collection</exception>
			/// <exception cref="FormatException">Illegal characters present in <i>value</i>.</exception>
			/// <exception cref="InvalidOperationException">Parent <see cref="Var"/> controls value.</exception>
			/// <remarks>Dynamic values are permitted. Static equations are solved and saved as the resultant.<br/>
			/// Default value is <b>"-1"</b> (consecutive). An empty or <b>null</b> value returns to the default.<br/>
			/// As a project design consideration; if given the option, constant offsets are preferred over consecutive offsets. Using constant offsets provides better type safety and a performance boost, as the offsets in the Binary require less or no calculation. If the project format is further modified and adjustments are made to previous entries a constant offset ensures the item does not shift to an unexpected location.</remarks>
			public virtual string RawOffset
			{
				get { return (_isArrayChild ? _parent.parentVar._offset : _offset); }
				set
				{
					if (value == _offset) return;
					if (_parent.parentVar != null && _parent.parentVar.Type != VarType.Definition) throw new InvalidOperationException(_parentControlMsg);
					if (value == "" || value == null) _offset = "-1";
					else if (!isDynamicText(value))
					{
						string eval = "";
						try { eval = Equation.Evaluate(value); }
						catch (ArgumentException x) { throw new ArgumentException(x.Message.Replace("Error", "Error setting RawOffset"), x); }
						catch (FormatException) { throw; }
						try { if (Int64.Parse(eval) < -1) throw new ArgumentOutOfRangeException("Value must be non-negative", "RawOffset.value"); }
						catch (ArgumentOutOfRangeException) { throw; }
						catch (FormatException x) { throw new ArgumentException("Value does not evaluate to a valid Int64", x); }
						catch (OverflowException x) { throw new ArgumentOutOfRangeException("Value must be non-negative and less than " + Int64.MaxValue, x); }
						_offset = eval;
					}
					else _offset = value;
					if (!_parent.isLoading) _isModified = true;
					try { if (LocalOffset < 0 && RawOffset != "-1") throw new ArgumentException("Warning: RawOffset dynamics calculate to a negative value"); }
					catch (InvalidOperationException) { return; }
					catch (ArgumentOutOfRangeException) { throw; }
					catch (ArgumentException x) { throw new ArgumentOutOfRangeException(x.Message); }
				}
			}
			/// <summary>Gets or sets the quantity definition for <see cref="Values"/></summary>
			/// <exception cref="ArgumentException">Calculation error with static equation, must be non-negative</exception>
			/// <exception cref="ArgumentOutOfRangeException">Dynamics calculate to a negative value<br/><b>-or-</b><br/>Dynamic markers fall outside the range of the parent Collection</exception>
			/// <remarks>Dynamic values are permitted. Static equations are solved and saved as the resultant.<br/>
			/// Default value is empty (no children). An empty, <b>null</b> or <b>"0"</b> value returns to the default.<br/>
			/// Changing this value will affect <see cref="Values"/> accordingly. Any entries in Values created by increasing the quantity will be <see cref="VarType.Var"/>.</remarks>
			public virtual string RawQuantity
			{
				get { return _quantity; }
				set
				{
					if (value == _quantity) return;
					if (value == "" || value == null) _quantity = "";
					else if (!isDynamicText(value))
					{
						string eval = Equation.Evaluate(value);
						if (eval.IndexOf("Error") != -1)
						{
							eval = eval.Replace("Error", "Error setting RawQuantity");
							throw new ArgumentException(eval, "RawQuantity.value");
						}
						if (Int64.Parse(eval) < 0)
							throw new ArgumentException("Value must be non-negative", "RawQuantity.value");
						eval = Int64.Parse(eval).ToString();
						_quantity = (eval != "0" ? eval : "");
					}
					else _quantity = value;
					if (!_parent.isLoading) _isModified = true;
					try { if (Quantity < 0) throw new ArgumentException("Warning: RawQuantity dynamics calculate to a negative value"); }
					catch (InvalidOperationException) { return; }	// Binary isn't loaded, so skip messing with Values
					catch (ArgumentOutOfRangeException x) { throw x; }	// Dynamics fail from PDV()
					catch (ArgumentException x) { throw new ArgumentOutOfRangeException(x.Message); }	// thrown above
					if (_binaryAssigned) Values.SetCount(Quantity, true);
				}
			}
			/// <summary>Gets or sets the value definition of the item.</summary>
			/// <exception cref="ArgumentException">Value is not a valid Equation.</exception>
			/// <remarks>Value is numerical, see <see cref="ProjectFile.Bool"/> and <see cref="ProjectFile.String"/> for non-numeric overrides.<br/>
			/// A value of <b>null</b> or an empty string will set to <b>0</b>.</remarks>
			public virtual object RawValue
			{
				get { return _value; }
				set
				{
					if (value == null || value.ToString() == "" || value.ToString() == "0") _value = 0;
					else
					{
						try { _value = Equation.Evaluate(value.ToString()); }
						catch (ArgumentException) { throw; }
					}
					if (!_parent.isLoading) _isModified = true;
				}
			}
			
			/// <summary>Gets or sets the default value if the item</summary>
			/// <exception cref="ArgumentOutOfRangeException">Value contains dynamic markers outside the parent collection.</exception>
			/// <exception cref="ArgumentException">Value is not a valid Equation.</exception>
			/// <remarks>Value is numerical, see <see cref="ProjectFile.Bool"/> and <see cref="ProjectFile.String"/> for non-numeric overrides.<br/>
			/// Setting to a <b>null</b> or empty string removes the default setting.</remarks>
			public virtual object DefaultValue
			{
				get { return _default; }
				set
				{
					if (value == _default) return;
					if (value == null || value.ToString() == "") _default = null;
					else
					{
						string temp = value.ToString();
						if (isDynamicText(temp) && _binaryAssigned)
							try { temp = ParseDynamicValues(_parent, temp); }
							catch (ArgumentOutOfRangeException) { throw; }
						if (!isDynamicText(temp))
						{
							try { _default = Equation.Evaluate(temp); }
							catch (ArgumentException) { throw; }
						}
						else _default = value;
					}
					if (!_parent.isLoading) _isModified = true;
				}
			}
			#endregion XML values
			
			/// <summary>Gets if <see cref="RawCondition"/> evaluates to <b>true</b> signifying the item is present in the current <see cref="BinaryFile"/></summary>
			/// <exception cref="ArgumentOutOfRangeException">Dynamic markers within <see cref="RawCondition"/> fall outside the range of the parent Collection</exception>
			/// <exception cref="InvalidOperationException">No <see cref="BinaryFile"/> has been loaded into the Project</exception>
			public bool IsPresent
			{
				get
				{
					if (_condition == "") return true;
					if (!_binaryAssigned)
						throw new InvalidOperationException(_noBinaryMsg);
					else return Conditional.Evaluate(ParseDynamicValues(_parent, _condition));
				}
			}
			
			/// <summary>Gets the calculated length of the item</summary>
			/// <exception cref="ArgumentOutOfRangeException">Dynamic markers within <see cref="RawOffset"/> fall outside the range of the parent Collection</exception>
			/// <exception cref="InvalidOperationException">No <see cref="BinaryFile"/> has been loaded into the Project</exception>
			/// <remarks>For a static <see cref="RawLength"/> value, simply returns the parsed value.<br/>
			/// Dynamic values will be calculated from the beginning of the parent Collection.</remarks>
			public int Length
			{
				get
				{
					if (!HasDynamicLength && Values == null) return Int32.Parse(RawLength);
					if (HasDynamicLength && !_binaryAssigned)
						throw new InvalidOperationException(_noBinaryMsg);
					if (Values != null)
					{
						if (_parent.parentVar == null || _parent.parentVar.Type != _type || _type == VarType.Collection &&
								(_parent.parentVar._id != _id || _parent.parentFile._types.GetItemByID(_id).RawLength == "-1"))
						{
							return Values[Values.Count - 1].LocalOffset + Values[Values.Count - 1].Length;
							// int length = 0;
							// for (int i = 0; i < Values.Count; i++) length += Values[i].Length;
							// return length;
						}
						else
						{
							if (!_parent.parentVar.HasDynamicLength) return Int32.Parse(_parent.parentVar.RawLength);
							if (!_binaryAssigned)
								throw new InvalidOperationException(_noBinaryMsg);
							return Int32.Parse(Equation.Evaluate(ParseDynamicValues(_parent.parentVar._parent, _parent.parentVar.RawLength)));
						}
					}
					return Int32.Parse(Equation.Evaluate(ParseDynamicValues(_parent, RawLength)));
				}
			}
			
			/// <summary>Gets if the evaluated <see cref="RawValue"/> matches the evaluated <see cref="DefaultValue"/>.</summary>
			/// <exception cref="InvalidOperationException">No <see cref="BinaryFile"/> has been loaded into the Project.</exception>
			/// <remarks>If no default is assigned, will return <i>true</i>.<br/><see cref="VarType.Collection"/> and <see cref="VarType.Definition"/> always return <i>true</i> as they have no values to compare. <see cref="VarType.Error"/> and <see cref="VarType.Var"/> are still compared as they are assumed to be numerical.</remarks> 
			public bool MatchesDefault
			{
				get
				{
					if (!_binaryAssigned)
						throw new InvalidOperationException(_noBinaryMsg);
					if (_default == null || _type == VarType.Collection || _type == VarType.Definition) return true;
					if (_type == VarType.String && _default.ToString() != _value.ToString()) return false;
					if (_type == VarType.Bool && Conditional.Evaluate(ParseDynamicValues(_parent, _default.ToString())) != Conditional.Evaluate(_value.ToString()))
						return false;
					else if (Equation.Evaluate(ParseDynamicValues(_parent, _default.ToString())) != Equation.Evaluate(_value.ToString()))
						return false;
					return true;
				}
			}
			
			/// <summary>Gets the calculated global offset of the item within the <see cref="BinaryFile"/></summary>
			/// <exception cref="ArgumentOutOfRangeException">Dynamic markers within <see cref="RawOffset"/> fall outside the range of the parent Collection.</exception>
			/// <exception cref="InvalidOperationException">No <see cref="BinaryFile"/> has been loaded into the Project.</exception>
			public int FileOffset { get { return LocalOffset + (_parent.parentVar != null ? _parent.parentVar.FileOffset : 0); } }
			
			/// <summary>Gets the calculated offset within the parent <see cref="VarCollection"/></summary>
			/// <exception cref="ArgumentOutOfRangeException">Dynamic markers within <see cref="RawOffset"/> fall outside the range of the parent Collection.</exception>
			/// <exception cref="FormatException"><see cref="RawOffset"/> does not evaluate to a valid <see cref="Int64"/>.</exception>
			/// <exception cref="InvalidOperationException">No <see cref="BinaryFile"/> has been loaded into the Project.</exception>
			/// <exception cref="OverflowException"><see cref="RawOffset"/> calculates to a value outside the range of <see cref="Int64"/>.</exception>
			/// <remarks>For a static <see cref="RawOffset"/> value, simply returns the parsed value.<br/>
			/// Dynamic and consecutive (<b>-1</b>) values will be calculated from the beginning of the parent Collection.</remarks>
			public int LocalOffset
			{
				get
				{
					if (!HasDynamicOffset && _offset != "-1") return Int32.Parse(_offset);
					if (!_binaryAssigned)
						throw new InvalidOperationException(_noBinaryMsg);
					if (_offset == "-1")
					{
						int offset = 0;
						object tag = _tag;
						_tag = "LocalOffset calc";
						int previousIndex = _parent.GetIndexByTag("LocalOffset calc") - 1;
						if (previousIndex != -1) offset = _parent[previousIndex].LocalOffset + (_parent[previousIndex].IsPresent ? _parent[previousIndex].Length : 0);
						_tag = tag;
						return offset;
					}
					try { return Int32.Parse(Equation.Evaluate(ParseDynamicValues(_parent, _offset))); }
					catch (FormatException) { throw; }
					catch (OverflowException) { throw; }
				}
			}
			
			/// <summary>Gets the calculated quantity of children in <see cref="Values"/></summary>
			/// <exception cref="ArgumentOutOfRangeException">Dynamic markers within <see cref="RawQuantity"/> fall outside the range of the parent Collection.</exception>
			/// <exception cref="InvalidOperationException">No <see cref="BinaryFile"/> has been loaded into the Project.</exception>
			/// <remarks>For a static <see cref="RawQuantity"/> value, simply returns the parsed value.<br/>
			/// Dynamic values will be calculated from the beginning of the parent Collection.</remarks>
			public virtual int Quantity
			{
				get
				{
					if (_quantity == "") return 0;
					if (!HasDynamicQuantity) return Int32.Parse(_quantity);
					if (!_binaryAssigned)
						throw new InvalidOperationException(_noBinaryMsg);
					return Int32.Parse(Equation.Evaluate(ParseDynamicValues(_parent, _quantity)));
				}
			}

			/// <summary>Gets or sets the object that contains user-defined information</summary>
			/// <remarks>This exists solely for programatic purposes, value is not stored in the binary or project file. Defaults to the output of <see cref="ToString()"/>.</remarks>
			public object Tag
			{
				get { return _tag; }
				set { _tag = value; }
			}
			#endregion public properties
			
			protected static bool isDynamicText(string value) { return (value != null ? value.Contains("$") : false); }
			
			protected static string[] getDynamicSplit(string input, int index)
			{
				if (isDynamicText(input))
				{
					int marker = input.IndexOf("$");
					string left = input.Substring(0, marker);
					string middle = input.Substring(marker + 1, firstOperation(input.Substring(marker + 1)));
					string right = input.Substring(left.Length + middle.Length + 1);
					if (middle == index.ToString()) return new string[] { left, middle, right };
					else
					{
						string[] temp = getDynamicSplit(right, index);
						if (temp != null) temp[0] = left + "$" + middle + temp[0];
						return temp;
					}
				}
				else return null;
			}
			
			protected static void copyAttributes(Var read, Var write)
			{
				write._name = read._name;
				write._offset = read._offset;
				write._length = read._length;
				write._value = read._value;
				write._quantity = read._quantity;
				write._condition = read._condition;
				write._default = read._default;
				write._isValidated = read._isValidated;
				write._id = read._id;
				write._comment = read._comment;
				if (read.Values != null)
				{
					if (read.Values.parentVar != null)
						write.Values = new VarCollection(write, read.Values.Count);
					else
						write.Values = new VarCollection(write._parent.parentFile, read.Values.Count);
					for (int i = 0; i < read.Values.Count; i++)
						write.Values.Add(read[i].Type);		// placeholder, typically gets overridden with a new type
				}
			}
			
			protected bool _binaryAssigned { get { return _parent.parentFile._binary != null; } }

			protected bool _isArrayChild { get { return _parent.parentVar != null && _parent.parentVar._id == _id; } }
			
			/// <summary>Gets or sets the parent collection.</summary>
			/// <remarks>Setting will propogate through <see cref="Values"/> if necessary.</remarks>
			protected internal VarCollection _parent
			{
				get { return _parentCollection; }
				set
				{
					_parentCollection = value;
					if (Values != null) Values.parentFile = _parentCollection.parentFile;
					// parentVar doesn't need updating, since it's still this
				}
			}
		}
	}
}
