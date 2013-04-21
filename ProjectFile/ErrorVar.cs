using System;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
		/// <summary>Object for items that fail type declaration when loading the <see cref="ProjectFile"/>.</summary>
		public class ErrorVar : Var
		{
			/// <summary>Initializes a new placeholder item</summary>
			/// <param name="parent">The <see cref="VarCollection"/> containing the item.</param>
			/// <param name="name">The name of the item for identification purposes.</param>
			public ErrorVar(VarCollection parent, string name)
			{
				_parent = parent;
				_type = VarType.Error;
				bool loading = _parent.isLoading;
				_parent.isLoading = true;
				Name = name;
				_parent.isLoading = loading;
			}
		}
	}
}