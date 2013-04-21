using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Idmr.Common;

namespace Idmr.ProjectHex
{
	public partial class ProjectFile
	{
		string _projectPath = "";	// project file
		string _name = "";
		string _wildcard = "*.*";	// filename wildcard match
		long _length = -1;
		string _comment = "";
		VarCollection _properties = null;
		VarCollection _types = null;
		bool _isModified = false;
		internal int _nextID = 0;
		BinaryFile _binary = null;

		static string _noBinaryMsg = "Binary file has not been loaded into the Project, dynamic values cannot be calculated";

		public enum VarType { Error, Undefined, Bool, Byte, SByte, Short, UShort, Int, UInt, Long, ULong, String, Collection, Definition, Single, Double }

		#region constructors
		public ProjectFile() { /* do nothing */ }

		public ProjectFile(string projectPath) { LoadProject(projectPath); }

		public ProjectFile(string projectPath, bool validationOnly) { LoadProject(projectPath, validationOnly); }
		#endregion constructors

		#region public methods
		/// <summary>Re-initializes the project definition from file</summary>
		/// <exception cref="InvalidOperationException"><see cref="ProjectPath"/> has not been defined</exception>
		/// <remarks>Wipes any content that may have been created when applied to a BinaryFile</remarks>
		public void ReloadProject()
		{
			if (_projectPath == "") throw new InvalidOperationException("No project file defined");
			LoadProject(_projectPath);
		}
		
		/// <summary>Loads a project definition structure for use</summary>
		/// <param "projectFile">Project definition to be loaded</param>
		/// <exception cref="System.IO.FileNotFoundException"><i>projectFile</i> could not be located</exception>
		/// <exception cref="XmlException">Definition load failure</exception>
		public void LoadProject(string projectFile) { LoadProject(projectFile, false); }

		/// <summary>Loads a project definition structure for use</summary>
		/// <param "projectFile">Project definition to be loaded</param>
		/// <param "validationOnly">When <b>true</b>, only processes project properties and items used to check compatability.</param>
		/// <exception cref="System.IO.FileNotFoundException"><i>projectFile</i> could not be located</exception>
		/// <exception cref="XmlException">Definition load failure</exception>
		public void LoadProject(string projectFile, bool validationOnly)
		{
			if (!File.Exists(projectFile))
				throw new FileNotFoundException("'projectFile' could not be found, please confirm file location");
			_projectPath = projectFile;
			resetDefaults();
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreComments = true;
			settings.IgnoreWhitespace = true;
			XmlReader reader = XmlReader.Create(_projectPath, settings);

			try
			{
				reader.ReadToFollowing("project");
				reader.Read();
				while (true)
				{
					if (reader.IsEmptyElement) throw new XmlException("<project> children cannot be empty");

					if (reader.Name == "name") _name = reader.ReadElementContentAsString();
					else if (reader.Name == "file") _wildcard = reader.ReadElementContentAsString();
					else if (reader.Name == "types") _types = new VarCollection(this, reader.ReadElementContentAsInt());
					else if (reader.Name == "count") _properties = new VarCollection(this, reader.ReadElementContentAsInt());
					else if (reader.Name == "nextid") _nextID = reader.ReadElementContentAsInt();
					else if (reader.Name == "comments") _comment = reader.ReadElementContentAsString();
					else if (reader.Name == "length") _length = reader.ReadElementContentAsLong();
					else if (reader.Name == "structure")
					{
						if (_properties == null) throw new XmlException("Missing \"project > count\"");
						if (_name == "") throw new XmlException("Missing \"project > name\"");
						_properties.isLoading = true;
						_properties.Tag = "properties";
						readVars(_properties, reader.ReadSubtree(), validationOnly);
						_properties.isLoading = false;
						reader.Read();	// </structure>
						break;	// structure node must be last in project node
					}
				}
				if (validationOnly || _types == null) { reader.Close(); return; }

				_types.isLoading = true;
				_types.Tag = "types";
				for (int c = 0; c < _types.Capacity; c++)
				{
					if (reader.Name != "definition") throw new XmlException("Unexpected element, <definition> missing");
					_types.Add(new DefinitionVar(_types, reader["name"], reader["count"], reader["id"]));
					if (_types[c].ID >= _nextID) _nextID = _types[c].ID + 1;	// this mitigates missing <nextid> elements
					_types[c].RawLength = reader["length"];
					_types[c].Comment = reader["comment"];
					_types[c].Values.isLoading = true;
					readVars(_types[c].Values, reader.ReadSubtree(), false);
					_types[c].Values.isLoading = false;
					reader.Read();	// </definition>
				}
				_types.isLoading = false;
				reader.Close();
			}
			catch { reader.Close(); throw; }
		}

		// TODO: SaveProject

		/// <summary>Checks installed project files for valid projects for the given binary file</summary>
		/// <param "binaryPath">Full path to the file to be edited</param>
		/// <returns>Array of paths to applicable project definitions, <b>null</b> if none are found.</returns>
		static public string[] GetProjectMatches(string binaryPath)
		{
			string[] projects = ProjectFileList;
			string[] matches = new string[projects.Length];
			int j = 0;
			foreach (string p in projects)
				if (CheckProjectMatch(p, binaryPath)) matches[j++] = p;
			if (j > 0)
			{
				string[] returnedMatches = new string[j];
				for (int i = 0; i < j; i++) returnedMatches[i] = matches[i];
				return returnedMatches;
			}
			else return null;
		}

		/// <summary>Validates whether or not a given project is suitable for the given binary</summary>
		/// <param name="projectPath">Full path to the project file</param>
		/// <param name="binaryPath">Full path to the binary file to be edited</param>
		/// <returns><b>true</b> is the project can be used, <b>false</b> otherwise</returns>
		/// <remarks>If <i>projectPath</i> fails to load, automatically returns <b>false</b>.<br/>
		/// Providing the project can be loaded, validates by ensuring the filename meets <see cref="ProjectFile.Wildcard"/>, if <see cref="ProjectFile.Length"/> is defined it will compare the file size, and if <see cref="ProjectFile.HasMagic"/> is <b>true</b> it will compare the magic value against the binary file.<br/>
		/// The binary must pass all applicable tests for the project to be valid.</remarks>
		static public bool CheckProjectMatch(string projectPath, string binaryPath)
		{
			ProjectFile project = new ProjectFile();
			try { project.LoadProject(projectPath, true); }
			catch { return false; }
			FileStream fs = null;
			try
			{
				if (!StringFunctions.MatchesWildcard(StringFunctions.GetFileName(binaryPath), project.Wildcard)) throw new ArgumentException("wildcard fail");
				fs = File.OpenRead(binaryPath);
				if ((project.Length != -1) && (fs.Length != project.Length)) throw new ArgumentException("length fail");
				foreach (Var v in project.Properties)
				{
					if (v.IsValidated)
					{
						fs.Position = v.FileOffset;
						byte[] bytes = new BinaryReader(fs).ReadBytes(v.Length);
						bool match = true;
						if (v.Type == VarType.Byte && bytes[0].ToString() != v.DefaultValue.ToString()) match = false;
						else if (v.Type == VarType.SByte && ((sbyte)bytes[0]).ToString() != v.DefaultValue.ToString()) match = false;
						else if (v.Type == VarType.Short && BitConverter.ToInt16(bytes, 0).ToString() != v.DefaultValue.ToString()) match = false;
						else if (v.Type == VarType.UShort && BitConverter.ToUInt16(bytes, 0).ToString() != v.DefaultValue.ToString()) match = false;
						else if (v.Type == VarType.Int && BitConverter.ToInt32(bytes, 0).ToString() != v.DefaultValue.ToString()) match = false;
						else if (v.Type == VarType.UInt && BitConverter.ToUInt32(bytes, 0).ToString() != v.DefaultValue.ToString()) match = false;
						else if (v.Type == VarType.Long && BitConverter.ToInt64(bytes, 0).ToString() != v.DefaultValue.ToString()) match = false;
						else if (v.Type == VarType.ULong && BitConverter.ToUInt64(bytes, 0).ToString() != v.DefaultValue.ToString()) match = false;
						else if (v.Type == VarType.Single && BitConverter.ToSingle(bytes, 0).ToString() != v.DefaultValue.ToString()) match = false;
						else if (v.Type == VarType.Double && BitConverter.ToDouble(bytes, 0).ToString() != v.DefaultValue.ToString()) match = false;
						else if (v.Type == VarType.String && ArrayFunctions.ReadStringFromArray(bytes, 0, bytes.Length) != v.DefaultValue.ToString()) match = false;
						if (!match) throw new ArgumentException("validate fail");
					}
				}
				fs.Close();
			}
			catch (Exception x)
			{
				fs.Close();
				System.Diagnostics.Debug.WriteLine("CheckProjectMatch: " + x.Message);
				return false;
			}
			return true;
		}
		
		/// <summary>Parses <i>input</i> to replace dynamic markers with the solved values</summary>
		/// <param name="vars">The collection to use for the indexes</param>
		/// <param name="input">The full string with dynamic markers</param>
		/// <exception cref="ArgumentOutOfRangeException">A marker index was detected that is outside the range of <i>vars</i></exception>
		/// <remarks>If <i>input</i> does not contain any markers, simply returns itself</remarks>
		static public string ParseDynamicValues(VarCollection vars, string input)
		{
			//if (vars._parentFile._binary == null) throw new InvalidOperationException(_noBinaryMsg);
			if (input.Contains("$"))
			{
				int marker = input.IndexOf("$");
				string left = input.Substring(0, marker);
				string middle = input.Substring(marker + 1, firstOperation(input.Substring(marker + 1)));
				if (Int32.Parse(middle) >= vars.Count)
					throw new ArgumentOutOfRangeException("Marker of $" + middle + " is outside the range of the collection");
				string right = "";
				try { right = input.Substring(left.Length + middle.Length + 1); }
				catch (ArgumentOutOfRangeException) { /* do nothing */ }
				middle = vars[Int32.Parse(middle)].RawValue.ToString();
				return ParseDynamicValues(vars, left + middle + right);
			}
			else return input;
		}
		
		public void AssignBinary(BinaryFile binary) { _binary = binary; }
		#endregion public methods
		
		#region public props
		static public string[] ProjectFileList { get { return Directory.GetFiles(Directory.GetParent(Application.ExecutablePath) + "\\Projects\\", "*.xml"); } }
		public string ProjectPath { get { return _projectPath; } }
		public string Name
		{
			get { return _name; }
			set
			{
				foreach (string p in ProjectFileList)
				{
					if (value.ToLower() == new ProjectFile(p, true).Name.ToLower())
						throw new ArgumentException("Value must not match name of any other installed Project", "value");
				}
				_name = value;
				_isModified = true;
			}
		}
		public string Wildcard
		{
			get { return _wildcard; }
			set
			{
				if (value == "" || value == null) _wildcard = "*.*";
				else _wildcard = value;
				_isModified = true;
			}
		}
		public long Length
		{
			get { return _length; }
			set
			{
				if (value < 0) _length = -1;
				else if (value == 0) throw new ArgumentException("Value cannot be zero");
				else _length = value;
				_isModified = true;
			}
		}
		public string Comment
		{
			get { return _comment; }
			set { _comment = value; _isModified = true; }
		}
		public VarCollection Properties { get { return _properties; } }
		public VarCollection Types { get { return _types; } }
		/// <summary>Gets if the ProjectFile, <see cref="Properties"/> or <see cref="Types"/> have been modified since loading</summary>
		public bool IsModified
		{
			get
			{
				if (_properties.IsModified) _isModified = true;
				if (_types.IsModified) _isModified = true;
				return _isModified;
			}
		}
		#endregion
		
		static void readVars(VarCollection vars, XmlReader structure, bool validationOnly)
		{
			structure.ReadToDescendant("item");
			for (int i = 0; i < vars.Capacity; i++)
			{
				if (structure.Name != "item") throw new XmlException("Unexpected child element in " + (vars.Tag.ToString() == "properties" ? "<structure>" : "<definition>") + ", must be <item //>");
				// must be <item attr[] />
				if (!structure.IsEmptyElement) throw new XmlException("Item elements must be empty");
				if (!structure.HasAttributes) throw new XmlException("Item elements must contain attributes");
				if (structure["name"] == null) throw new XmlException("Item elements must have 'name' attribute");
				string type = structure["type"];
				if (type == null) throw new XmlException("Item elements must have 'type' attribute");
				type = type.ToLower();
				string defaultValue = structure["default"];
				if (structure["validate"] == "true" && (type == "bool" || type == "collection" || structure["offset"] == null || structure["offset"].Contains("$") || defaultValue == null || structure["condition"] != null))
					throw new XmlException("Validated elements must not be Bool or Collection types, must define a static `offset`, must have a defined `default` and must not use conditionals.");
				if (type == "bool")
					vars.Add(new BoolVar(vars, structure["true"], structure["false"], defaultValue));
				else if (type == "byte")
					vars.Add(new ByteVar(vars, defaultValue));
				else if (type == "double")
					vars.Add(new DoubleVar(vars, defaultValue));
				else if (type == "int")
					vars.Add(new IntVar(vars, defaultValue));
				else if (type == "long")
					vars.Add(new LongVar(vars, defaultValue));
				else if (type == "sbyte")
					vars.Add(new SByteVar(vars, defaultValue));
				else if (type == "short")
					vars.Add(new ShortVar(vars, defaultValue));
				else if (type == "single")
					vars.Add(new SingleVar(vars, defaultValue));
				else if (type == "string")
					vars.Add(new StringVar(vars, structure["nullterm"], structure["length"], defaultValue));
				else if (type == "uint")
					vars.Add(new UIntVar(vars, defaultValue));
				else if (type == "ulong")
					vars.Add(new ULongVar(vars, defaultValue));
				else if (type == "ushort")
					vars.Add(new UShortVar(vars, defaultValue));
				else if (type == "collection")
					vars.Add(new CollectionVar(vars, structure["id"]));
				else
				{
					System.Diagnostics.Debug.WriteLine("Type error: " + structure["type"]);
					vars.Add(new ErrorVar(vars, structure["name"]));
					structure.Read();
					continue;
				}
				vars[i].Name = structure["name"];
				vars[i].Tag = vars[i].ToString();
				if (structure["validate"] == "true") vars[i].IsValidated = true;
				vars[i].RawOffset = structure["offset"];
				vars[i].Comment = structure["comment"];
				vars[i].RawCondition = structure["condition"];
				vars[i].RawQuantity = structure["qty"];
				if (vars[i].Type == VarType.Collection && vars[i].RawQuantity == "")
					vars[i].RawQuantity = "1";
				if (vars[i].RawQuantity != "")
				{
					vars[i].Values = new VarCollection(vars[i]);
					if (structure["names"] != null) vars[i].Values._names = structure["names"].Split(',');
				}
				structure.Read();
			}
		}

		void resetDefaults()
		{
			_name = "";
			_wildcard = "*.*";
			_length = -1;
			_comment = "";
			_properties = null;
			_types = null;
			_isModified = false;
		}

		internal static int firstOperation(string s)
		{
			string nums = "0123456789";
			for (int offset = 0; offset < s.Length; offset++) if (nums.IndexOf(s[offset]) == -1) return offset;
			return s.Length;
		}
	}
}