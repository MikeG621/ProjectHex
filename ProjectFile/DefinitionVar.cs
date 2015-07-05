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
 * v0.1.4, 130910
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
		/// <summary>Object for <see cref="ProjectFile.Types"/> declarations.</summary>
		[Serializable]
		public class DefinitionVar : Var
		{
			#region constructors
			/// <summary>Initializes a new type definition.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> that contains the item, should always be <see cref="ProjectFile.Types"/>.</param>
			/// <param name="name">The name of the type.</param>
			/// <param name="count">The total number of items in the definition.</param>
			/// <param name="id">The ID number to be used with dynamics.</param>
			/// <exception cref="ArgumentNullException"><i>name</i>, <i>count</i> or <i>id</i> are <b>null</b> or empty.</exception>
			/// <exception cref="ArgumentException"><i>count</i> is not a constant.</exception>
			/// <remarks>Values is *not* populated, only the initial Capacity is set</remarks>
			public DefinitionVar(VarCollection parent, string name, string count, string id)
			{
				if (name == "" || name == null || id == "" || id == null || count == "" || count == null)
					throw new ArgumentNullException("Definition elements require 'name', 'id' and 'count' attributes", ((name == "" || name == null) ? "name" : ((id == "" || id == null)? "id" : "count")));
				if (isDynamicText(count) || Equation.Evaluate(count) != count)
					throw new ArgumentException("'count' attribute must be constant", "count");
				_parent = parent;
				_type = VarType.Definition;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				Name = name;
				try { Values = new VarCollection(this, Int32.Parse(count)); }
				catch (FormatException x) { throw new FormatException("'count' is not a valid integer", x); }
				catch (OverflowException x) { throw new ArgumentOutOfRangeException("'count' must be between zero and " + Int32.MaxValue, x); }
				try { _id = Int32.Parse(id); }
				catch (FormatException x) { throw new FormatException("'id' is not a valid integer", x); }
				catch (OverflowException x) { throw new OverflowException("'id' must be lower than " + Int32.MaxValue, x); }
				_parent.isLoading = loading;
			}
			
			/// <summary>Initializes a blank type definition.</summary>
			/// <param name="parent">The <see cref="VarCollection"/> that contains the item, should always be <see cref="ProjectFile.Types"/>.</param>
			/// <remarks><see cref="ID"/> is initialized to the next available value per <i>parent</i>. <see cref="Values"/> is initalized as empty, <see cref="Name"/> defaults to "NewDefinition".</remarks>
			public DefinitionVar(VarCollection parent)
			{
				_parent = parent;
				_type = VarType.Definition;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				Name = "NewDefinition";
				Values = new VarCollection(this);
				_length = "-1";
				_id = _parent.parentFile._nextID;
				_parent.parentFile._nextID++;
				_parent.isLoading = loading;
			}
			#endregion

			/// <summary>Gets or sets the length definition of the item.</summary>
			/// <exception cref="ArgumentException">Value is dynamic.<br/><b>-or-</b><br/>Calculation error with static equation.</exception>
			/// <exception cref="ArgumentOutOfRangeException">Value is less than <b>1</b> and not <b>-1</b>.<br/><b>-or</b></br>Value is greater than <see cref="Int32.MaxValue"/>.</exception>
			/// <exception cref="FormatException">Value is not a valid integer.</exception>
			/// <remarks>Dynamic values are not permitted. Equations are not permitted.<br/>
			/// Default value is <b>"-1"</b>. An empty or <b>null</b> value returns to the default.</remarks>
			public override string RawLength
			{
				get { return base.RawLength; }
				set
				{
					string msg = "RawLength must be non-negative and less than " + Int32.MaxValue;
					if (value == "-1" || value == null || value == "")
					{
						_length = "-1";
						return;
					}
					if (isDynamicText(value) || firstOperation(value) != value.Length) throw new ArgumentException("Length must be constant");
					try { if (Int32.Parse(value) < 1) throw new ArgumentOutOfRangeException(msg); }
					catch (FormatException x) { throw new FormatException("Value is not a valid integer", x); }
					catch (OverflowException x) { throw new ArgumentOutOfRangeException(msg, x); }
					_length = value;
				}
			}
			/// <summary>Disabled</summary>
			/// <exception cref="InvalidOperationException">Collection items do not contain a offset</exception>
			/// <remarks>Always returns <b>null</b></remarks>
			public override string RawOffset
			{
				get { return null; }
				set { throw new InvalidOperationException(_type.ToString() +_disableAttributeMsg + "'offset'"); }
			}
			
			/// <summary>Disabled</summary>
			/// <exception cref="InvalidOperationException">Collection items do not contain a condition</exception>
			/// <remarks>Always returns <b>null</b></remarks>
			public override string RawCondition
			{
				get { return null; }
				set { throw new InvalidOperationException(_type.ToString() +_disableAttributeMsg + "'condition'"); }
			}
			
			/// <summary>Disabled</summary>
			/// <exception cref="InvalidOperationException">Collection items do not contain a value</exception>
			/// <remarks>Always returns <b>null</b></remarks>
			public override object RawValue
			{
				get { return null; }
				set { throw new InvalidOperationException(_type.ToString() +_disableAttributeMsg + "'value'"); }
			}
			
			/// <summary>Disabled</summary>
			/// <exception cref="InvalidOperationException">Collection items do not contain a default</exception>
			/// <remarks>Always returns <b>null</b></remarks>
			public override object DefaultValue
			{
				get { return null; }
				set { throw new InvalidOperationException(_type.ToString() +_disableAttributeMsg + "'default'"); }
			}
			
			/// <summary>Gets or sets the quantity definition for <see cref="Values"/>.</summary>
			/// <exception cref="ArgumentNullException">Value is <b>null</b> or empty.</exception>
			/// <exception cref="ArgumentOutOfRangeException">Value is not between zero and <see cref="Int32.MaxValue"/>.</exception>
			/// <exception cref="ArgumentException">Value is not a constant.</exception>
			/// <exception cref="FormatException">Value is not a valid integer.</exception>
			/// <remarks>Values are based on <see cref="Values.Count"/>. If <see cref="Values"/> expands, the new items will be <see cref="VarType.Var"/>. When truncating, items will be lost starting from the last index.<br/>
			/// Dynamic values are not permitted. Equations are not permitted.</remarks>
			public override string RawQuantity
			{
				get { return Values.Count.ToString(); }
				set
				{
					string msg = "Quantity must be positive and less than " + Int32.MaxValue;
					if (value == null || value == "") throw new ArgumentNullException(msg);
					if (isDynamicText(value) || firstOperation(value) != value.Length) throw new ArgumentException("Quantity must be constant");
					try { if (Int32.Parse(value) <= 0) throw new ArgumentOutOfRangeException(msg); }
					catch (FormatException x) { throw new FormatException("Value is not a valid integer", x); }
					catch (OverflowException x) { throw new ArgumentOutOfRangeException(msg, x); }
					Values.SetCount(Int32.Parse(value), true);
				}
			}
			
			/// <summary>Gets the quantity definition for <see cref="Values"/></summary>
			public override int Quantity { get { return Values.Count; } }
		}
	}
}