using System;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
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
				try { _id = Int32.Parse(id); }
				catch (FormatException x) { throw new FormatException("'id' is not a valid integer", x); }
				catch (OverflowException x) { throw new OverflowException("'id' must be lower than " + Int32.MaxValue, x); }
				// projects have to be written with child definitions at the top. IDs can be whatever, but new child Collections must be inserted above whatever parent wants to use it
				// ie: if I have a Square, and I decide that I want to make it out of Lines, the new Line definition must be inserted before Square in _parent
				if (!parent.isLoading && parent._parentFile.Types.GetIndexByID(_id) == -1)
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
				if (!parent.isLoading && parent._parentFile.Types.GetIndexByID(id) == -1)
					throw new ArgumentOutOfRangeException("ProjectFile Type definition with specified ID not found");
				_parent = parent;
				_type = VarType.Collection;
				_id = id;
			}
			#endregion constructors
			
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
				get { return _parent._parentFile._types.GetItemByID(_id).RawLength; }
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