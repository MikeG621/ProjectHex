/*
 * Idmr.ProjectHex.ProjectFile.dll, Project definition library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
 * with this file, You can obtain one at http://mozilla.org/MPL/2.0/
 *
 * Version: 0.1.4+
 */

/* CHANGELOG
 * [NEW] Save
 * [NEW] numerical type min/max read
 * [UPD] Tag no longer initialized during load
 * [UPD] StringVar loads "encoding" attribute
 * [UPD] tweaked corrupted default msgs
 * v0.1.4, 130910
 * [ADD] Read initial defaults for String and Bool
 * [UPD] License
 * v0.1.3, 130701
 * [ADD] Serializable
 * v0.1.1, 130421
 */
 
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Idmr.Common;

namespace Idmr.ProjectHex
{
	/// <summary>Object to contain the project definition.</summary>
    [Serializable]
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
		/// <summary>Next available ID number for a new Type.</summary>
		internal int _nextID = 0;
		BinaryFile _binary = null;

		static string _noBinaryMsg = "Binary file has not been loaded into the Project, dynamic values cannot be calculated.";
		static string _corruptDefaultMsg = "Corrupted default declaration, ";

		/// <summary>Type of <see cref="Var"/> objects.</summary>
		public enum VarType : byte {
			/// <summary>Placeholder for corrupted objects.</summary>
			Error,
			/// <summary>New object.</summary>
			Undefined,
			/// <summary>Boolean object.</summary>
			Bool,
			/// <summary>Unsigned 8-bit integer.</summary>
			Byte,
			/// <summary>Signed 8-bit integer.</summary>
			SByte,
			/// <summary>Signed 16-bit integer.</summary>
			Short,
			/// <summary>Unsigned 16-bit integer.</summary>
			UShort,
			/// <summary>Signed 32-bit integer.</summary>
			Int,
			/// <summary>Unsigned 32-bite integer.</summary>
			UInt,
			/// <summary>Signed 64-bit integer.</summary>
			Long,
			/// <summary>Unsigned 64-bit integer.</summary>
			ULong,
			/// <summary>Text value.</summary>
			String,
			/// <summary>Multiple child objects.</summary>
			Collection,
			/// <summary>Template collection.</summary>
			Definition,
			/// <summary>Signed 32-bit floating-point value.</summary>
			Single,
			/// <summary>Signed 64-bit floating-point value.</summary>
			Double
		}

		#region constructors
		/// <summary>Initializes a blank project.</summary>
		public ProjectFile() { /* do nothing */ }

		/// <summary>Initializes a project from file.</summary>
		/// <param name="projectPath">The full path to the file to load.</param>
		/// <exception cref="System.IO.FileNotFoundException"><i>projectFile</i> could not be located.</exception>
		/// <exception cref="XmlException">Definition load failure.</exception>
		public ProjectFile(string projectPath) { LoadProject(projectPath); }

		/// <summary>Initializes a project from file.</summary>
		/// <param name="projectPath">The full path to the file to load.</param>
		/// <param name="validationOnly">Whether or not the project is only loaded to ensure compatability.</param>
		/// <exception cref="System.IO.FileNotFoundException"><i>projectFile</i> could not be located.</exception>
		/// <exception cref="XmlException">Definition load failure.</exception>
		/// <remarks>When <i>validationOnly</i> is <b>true</b>, only project properties and items used to check compatability are loaded.</remarks>
		public ProjectFile(string projectPath, bool validationOnly) { LoadProject(projectPath, validationOnly); }
		#endregion constructors

		#region public methods
		/// <summary>Re-initializes the project definition from file.</summary>
		/// <exception cref="InvalidOperationException"><see cref="ProjectPath"/> has not been defined.</exception>
		/// <exception cref="FileNotFoundException">The original file could not be located, likely has been moved or deleted.</exception>
		/// <exception cref="XmlException">Definition load failure.</exception>
		/// <remarks>Wipes any content that may have been created when applied to a BinaryFile.</remarks>
		public void ReloadProject()
		{
			if (_projectPath == "") throw new InvalidOperationException("No project file defined");
			LoadProject(_projectPath);
		}
		
		/// <summary>Loads a project definition structure for use.</summary>
		/// <param "projectFile">Project definition to be loaded.</param>
		/// <exception cref="FileNotFoundException"><i>projectFile</i> could not be located.</exception>
		/// <exception cref="XmlException">Definition load failure.</exception>
		public void LoadProject(string projectFile) { LoadProject(projectFile, false); }

		/// <summary>Loads a project definition structure for use.</summary>
		/// <param "projectFile">Project definition to be loaded.</param>
		/// <param "validationOnly">When <b>true</b>, only processes project properties and items used to check compatability.</param>
		/// <exception cref="FileNotFoundException"><i>projectFile</i> could not be located.</exception>
		/// <exception cref="XmlException">Definition load failure.</exception>
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
					else if (reader.Name == "default")
					{
						if (reader["type"] == null) throw new XmlException(_corruptDefaultMsg + "missing type.");
						else if (reader["type"] == "string.encoding") StringVar.DefaultEncoding = System.Text.Encoding.GetEncoding(reader.ReadElementContentAsString());
						else if (reader["type"] == "string.nulltermed") StringVar.DefaultNullTermed = (reader.ReadElementContentAsString().ToLower() == "true");
						else if (reader["type"] == "bool.truevalue") BoolVar.DefaultTrueValue = Convert.ToByte(reader.ReadElementContentAsInt());
						else if (reader["type"] == "bool.falsevalue") BoolVar.DefaultFalseValue = Convert.ToByte(reader.ReadElementContentAsInt());
						else throw new XmlException(_corruptDefaultMsg + "unknown type (" + reader["type"] + ".");
					}
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

		/// <summary>Save the project definition in it's current location.</summary>
		/// <remarks>If the project has not yet been saved, will prompt via dialog for a new location.</remarks>
		/// <exception cref="ArgumentException">Project location has not been set and user cancelled save dialog.</exception>
		/// <exception cref="InvalidOperationException"><see cref="ErrorVar"/> or Undefined types detected in project structure or definitions.</exception>
		/// <exception cref=="Idmr.SaveFileException">Error during save processes, no changes to file made.</exception>
		public void SaveProject()
		{
			if (_projectPath == "")
			{
				SaveFileDialog save = new SaveFileDialog();
				save.DefaultExt = ".xml";
				save.FileName = Name;
				save.Filter = "XML Project files (.xml)|*.xml";
				DialogResult res = save.ShowDialog();
				if (res == DialogResult.OK) _projectPath = save.FileName;
				else throw new ArgumentException("Project location has not been set.");
			}
			string noSave = "Project cannot be saved:";
			string errorMsg = "";
			if (_properties.GetIndexByType(VarType.Error) != -1)
				errorMsg += "\r\nErrors in project structure";
			if (_types.GetIndexByType(VarType.Error) != -1)
				errorMsg += "\r\nErrors in definitions";
			if (_properties.GetIndexByType(VarType.Undefined) != -1)
				errorMsg += "\r\nUnspecified types in project structure";
			if (_types.GetIndexByType(VarType.Undefined) != -1)
				errorMsg += "\r\nUnspecified types in definitions";
			if (errorMsg != "") throw new InvalidOperationException(noSave + errorMsg);
			string tempPath = _projectPath + ".tmp";
			XmlWriter xw = null;
			try
			{
				if (File.Exists(_projectPath)) File.Copy(_projectPath, tempPath);
				XmlWriterSettings ws = new XmlWriterSettings();
				ws.Indent = true;
				ws.OmitXmlDeclaration = true;
				xw = XmlWriter.Create(_projectPath, ws);
				xw.WriteStartElement("project");
				#region project header
				xw.WriteElementString("name", _name);
				if (_wildcard != "*.*") xw.WriteElementString("file", _wildcard);
				if (_types != null && _types.Count > 0) xw.WriteElementString("types", _types.Count.ToString());
				xw.WriteElementString("count", _properties.Count.ToString());
				if (_length > 0) xw.WriteElementString("length", _length.ToString());
				if (_nextID > 0) xw.WriteElementString("nextid", _nextID.ToString());
				if (StringVar.DefaultEncoding != System.Text.Encoding.UTF8)
				{
					xw.WriteStartElement("default");
					xw.WriteAttributeString("type", "string.encoding");
					xw.WriteString(StringVar.DefaultEncoding.ToString());
					xw.WriteEndElement();
				}
				if (StringVar.DefaultNullTermed)
				{
					xw.WriteStartElement("default");
					xw.WriteAttributeString("type", "string.nulltermed");
					xw.WriteString("true");
					xw.WriteEndElement();
				}
				if (BoolVar.DefaultTrueValue != 1)
				{
					xw.WriteStartElement("default");
					xw.WriteAttributeString("type", "bool.truevalue");
					xw.WriteString(BoolVar.DefaultTrueValue.ToString());
					xw.WriteEndElement();
				}
				if (BoolVar.DefaultFalseValue != 0)
				{
					xw.WriteStartElement("default");
					xw.WriteAttributeString("type", "bool.falsevalue");
					xw.WriteString(BoolVar.DefaultFalseValue.ToString());
					xw.WriteEndElement();
				}
				if (_comment != "") xw.WriteElementString("comments", _comment);
				#endregion project header
				xw.WriteStartElement("structure");
				#region structure
				foreach (Var p in _properties)
				{
					VarType type = p.Type;
					xw.WriteStartElement("item");	// <item...
					if (!p.IsChild && p._offset != "-1") xw.WriteAttributeString("offset", p._offset);
					xw.WriteAttributeString("type", type.ToString().Remove(type.ToString().Length - 3).ToLower());
					xw.WriteAttributeString("name", p.Name);
					if (!p.IsChild && p._condition != "") xw.WriteAttributeString("condition", p._condition);
					if (!p.IsChild && p._isValidated)
					{
						xw.WriteAttributeString("validate", "true");
						if (p.DefaultValue == null)
							throw new ArgumentNullException("Default value must be set for validated properties");
					}
					if (p.DefaultValue != null) xw.WriteAttributeString("default", p.DefaultValue.ToString());
					if (type == VarType.Bool)
					{
						BoolVar v = (BoolVar)p;
						if (!v.IsChild && v.TrueValue != BoolVar.DefaultTrueValue) xw.WriteAttributeString("true", v.TrueValue.ToString());
						if (!v.IsChild && v.FalseValue != BoolVar.DefaultFalseValue) xw.WriteAttributeString("false", v.FalseValue.ToString());
					}
					else if (type == VarType.Byte)
					{
						ByteVar v = (ByteVar)p;
						if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
						if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
					}
					else if (type == VarType.Collection)
					{
						xw.WriteAttributeString("id", p.ID.ToString());
						xw.WriteAttributeString("qty", p.RawQuantity);
					}
					else if (type == VarType.Double)
					{
						DoubleVar v = (DoubleVar)p;
						if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
						if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
					}
					else if (type == VarType.Int)
					{
						IntVar v = (IntVar)p;
						if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
						if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
					}
					else if (type == VarType.Long)
					{
						LongVar v = (LongVar)p;
						if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
						if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
					}
					else if (type == VarType.SByte)
					{
						SByteVar v = (SByteVar)p;
						if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
						if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
					}
					else if (type == VarType.Short)
					{
						ShortVar v = (ShortVar)p;
						if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
						if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
					}
					else if (type == VarType.Single)
					{
						SingleVar v = (SingleVar)p;
						if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
						if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
					}
					else if (type == VarType.String)
					{
						StringVar v = (StringVar)p;
						if (!v.IsChild && v.Encoding != StringVar.DefaultEncoding) xw.WriteAttributeString("encoding", v.Encoding.ToString());
						if (!v.IsChild && v.NullTermed != StringVar.DefaultNullTermed) xw.WriteAttributeString("nullterm", v.NullTermed.ToString());
						if (!v.IsChild && v.RawLength != "0") xw.WriteAttributeString("length", v.RawLength);
					}
					else if (type == VarType.UInt)
					{
						UIntVar v = (UIntVar)p;
						if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
						if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
					}
					else if (type == VarType.ULong)
					{
						ULongVar v = (ULongVar)p;
						if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
						if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
					}
					else if (type == VarType.UShort)
					{
						UShortVar v = (UShortVar)p;
						if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
						if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
					}
					if (p.RawQuantity != "" && type != VarType.Collection)
					{
						xw.WriteAttributeString("qty", p.RawQuantity);
						if (p.Values._names != null) xw.WriteAttributeString("names", String.Join(",", p.Values._names));
					}
					if (p.Comment != "") xw.WriteAttributeString("comment", p.Comment);
					xw.WriteEndElement();	// />
				}
				#endregion structure
				xw.WriteEndElement();	//</structure>
				if (_types != null && _types.Count > 0)
				{
					foreach (Var t in _types)
					{
						xw.WriteStartElement("definition");	// <definition...
						xw.WriteAttributeString("name", t.Name);
						xw.WriteAttributeString("id", t.ID.ToString());
						xw.WriteAttributeString("count", t.Values.Count.ToString());
						if (t.RawLength != "-1") xw.WriteAttributeString("length", t.RawLength);
						if (t.Comment != "") xw.WriteAttributeString("comment", t.Comment);	// >
						#region definition items
						foreach (Var p in t.Values)
						{
							VarType type = p.Type;
							xw.WriteStartElement("item");	// <item...
							if (!p.IsChild && p._offset != "-1") xw.WriteAttributeString("offset", p._offset);
							xw.WriteAttributeString("type", type.ToString().Remove(type.ToString().Length - 3).ToLower());
							xw.WriteAttributeString("name", p.Name);
							if (!p.IsChild && p._condition != "") xw.WriteAttributeString("condition", p._condition);
							if (!p.IsChild && p._isValidated)
							{
								xw.WriteAttributeString("validate", "true");
								if (p.DefaultValue == null)
									throw new ArgumentNullException("Default value must be set for validated properties");
							}
							if (p.DefaultValue != null) xw.WriteAttributeString("default", p.DefaultValue.ToString());
							if (type == VarType.Bool)
							{
								BoolVar v = (BoolVar)p;
								if (!v.IsChild && v.TrueValue != BoolVar.DefaultTrueValue) xw.WriteAttributeString("true", v.TrueValue.ToString());
								if (!v.IsChild && v.FalseValue != BoolVar.DefaultFalseValue) xw.WriteAttributeString("false", v.FalseValue.ToString());
							}
							else if (type == VarType.Byte)
							{
								ByteVar v = (ByteVar)p;
								if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
								if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
							}
							else if (type == VarType.Collection)
							{
								xw.WriteAttributeString("id", p.ID.ToString());
								xw.WriteAttributeString("qty", p.RawQuantity);
							}
							else if (type == VarType.Double)
							{
								DoubleVar v = (DoubleVar)p;
								if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
								if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
							}
							else if (type == VarType.Int)
							{
								IntVar v = (IntVar)p;
								if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
								if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
							}
							else if (type == VarType.Long)
							{
								LongVar v = (LongVar)p;
								if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
								if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
							}
							else if (type == VarType.SByte)
							{
								SByteVar v = (SByteVar)p;
								if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
								if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
							}
							else if (type == VarType.Short)
							{
								ShortVar v = (ShortVar)p;
								if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
								if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
							}
							else if (type == VarType.Single)
							{
								SingleVar v = (SingleVar)p;
								if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
								if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
							}
							else if (type == VarType.String)
							{
								StringVar v = (StringVar)p;
								if (!v.IsChild && v.Encoding != StringVar.DefaultEncoding) xw.WriteAttributeString("encoding", v.Encoding.ToString());
								if (!v.IsChild && v.NullTermed != StringVar.DefaultNullTermed) xw.WriteAttributeString("nullterm", v.NullTermed.ToString());
								if (!v.IsChild && v.RawLength != "0") xw.WriteAttributeString("length", v.RawLength);
							}
							else if (type == VarType.UInt)
							{
								UIntVar v = (UIntVar)p;
								if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
								if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
							}
							else if (type == VarType.ULong)
							{
								ULongVar v = (ULongVar)p;
								if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
								if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
							}
							else if (type == VarType.UShort)
							{
								UShortVar v = (UShortVar)p;
								if (v.UseMaxValue) xw.WriteAttributeString("max", v.MaximumValue.ToString());
								if (v.UseMinValue) xw.WriteAttributeString("min", v.MinimumValue.ToString());
							}
							if (p.RawQuantity != "" && type != VarType.Collection)
							{
								xw.WriteAttributeString("qty", p.RawQuantity);
								if (p.Values._names != null) xw.WriteAttributeString("names", String.Join(",", p.Values._names));
							}
							if (p.Comment != "") xw.WriteAttributeString("comment", p.Comment);
							xw.WriteEndElement();	// />
						}
						#endregion definition items
						xw.WriteEndElement();	// </definition>
					}
				}
				xw.WriteEndElement();	// </project>
				xw.Close();
				File.Delete(tempPath);
			}
			catch (Exception x)
			{
				if (xw != null) xw.Close();
				if (File.Exists(tempPath)) File.Copy(tempPath, _projectPath);
				File.Delete(tempPath);
				throw new SaveFileException(x);
			}
		}
		
		/// <summary>Save the project definition in a new location.</summary>
		/// <param name="projectFile">New path for the project.</param>
		/// <exception cref="InvalidOperationException"><see cref="ErrorVar"/> types detected in project structure or definitions.</exception>
		/// <exception cref=="Idmr.SaveFileException">Error during save processes, no changes to file made.</exception>
		public void SaveProject(string projectFile)
		{
			_projectPath = projectFile;
			SaveProject();
		}
		
		/// <summary>Checks installed project files for valid projects for the given binary file.</summary>
		/// <param "binaryPath">Full path to the file to be edited.</param>
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
			return null;
		}

		/// <summary>Validates whether or not a given project is suitable for the given binary.</summary>
		/// <param name="projectPath">Full path to the project file.</param>
		/// <param name="binaryPath">Full path to the binary file to be edited.</param>
		/// <returns><b>true</b> is the project can be used, <b>false</b> otherwise.</returns>
		/// <remarks>If <i>projectPath</i> fails to load, automatically returns <b>false</b>.<br/>
		/// Providing the project can be loaded, validates by ensuring the filename meets <see cref="ProjectFile.Wildcard"/>, if <see cref="ProjectFile.Length"/> is defined it will compare the file size, and if a property's <see cref="Var.IsValidated"/> is <b>true</b> it will compare the binary's value against the default.<br/>
		/// The binary must pass all applicable tests for the project to be valid.<br/>
		/// The reason for the failure is output to console.</remarks>
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
		
		/// <summary>Parses <i>input</i> to replace dynamic markers with the solved values.</summary>
		/// <param name="vars">The collection to use for the indexes.</param>
		/// <param name="input">The full string with dynamic markers.</param>
		/// <exception cref="ArgumentOutOfRangeException">A marker index was detected that is outside the range of <i>vars</i>.</exception>
		/// <remarks>If <i>input</i> does not contain any markers, simply returns itself.</remarks>
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
			return input;
		}
		
		/// <summary>Sets the opened binary file.</summary>
		public void AssignBinary(Idmr.ProjectHex.BinaryFile binary) { _binary = binary; }
		#endregion public methods
		
		#region public props
		/// <summary>Gets a listing of all installed project files.</summary>
		static public string[] ProjectFileList { get { return Directory.GetFiles(Directory.GetParent(Application.ExecutablePath) + "\\Projects\\", "*.xml"); } }
		/// <summary>Gets the path to the project.</summary>
		public string ProjectPath { get { return _projectPath; } }
		/// <summary>Gets or sets the name of the project.</summary>
		/// <exception cref="ArgumentException">Value cannot match the name of another installed project.</exception>
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
		/// <summary>Gets or sets the wildcard pattern for binary filename matching.</summary>
		/// <remarks>Empty or <b>null</b> values will set to <b>"*.*"</b>.<br/>
		/// **Used for validation.</remarks>
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
		/// <summary>Gets or sets the fixed-length size for the binary file.</summary>
		/// <remarks>A value of <b>0</b> or lower will be set as <b>-1</b>.<br/>
		/// **Used for validation.</remarks>
		public long Length
		{
			get { return _length; }
			set
			{
				if (value < 1) _length = -1;
				else _length = value;
				_isModified = true;
			}
		}
		/// <summary>Gets or sets a project note.</summary>
		/// <remarks>Value only exists for reference, provides no function.</remarks>
		public string Comment
		{
			get { return _comment; }
			set { _comment = value; _isModified = true; }
		}
		/// <summary>Gets a reference to the project layout.</summary>
		public VarCollection Properties { get { return _properties; } }
		/// <summary>Gets a reference to the item templates.</summary>
		public VarCollection Types { get { return _types; } }
		/// <summary>Gets if the ProjectFile, <see cref="Properties"/> or <see cref="Types"/> have been modified since loading.</summary>
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
					vars.Add(new ByteVar(vars, structure["min"], structure["max"], defaultValue));
				else if (type == "double")
					vars.Add(new DoubleVar(vars, structure["min"], structure["max"], defaultValue));
				else if (type == "int")
					vars.Add(new IntVar(vars, structure["min"], structure["max"], defaultValue));
				else if (type == "long")
					vars.Add(new LongVar(vars, structure["min"], structure["max"], defaultValue));
				else if (type == "sbyte")
					vars.Add(new SByteVar(vars, structure["min"], structure["max"], defaultValue));
				else if (type == "short")
					vars.Add(new ShortVar(vars, structure["min"], structure["max"], defaultValue));
				else if (type == "single")
					vars.Add(new SingleVar(vars, structure["min"], structure["max"], defaultValue));
				else if (type == "string")
					vars.Add(new StringVar(vars, structure["nullterm"], structure["length"], defaultValue, structure["encoding"]));
				else if (type == "uint")
					vars.Add(new UIntVar(vars, structure["min"], structure["max"], defaultValue));
				else if (type == "ulong")
					vars.Add(new ULongVar(vars, structure["min"], structure["max"], defaultValue));
				else if (type == "ushort")
					vars.Add(new UShortVar(vars, structure["min"], structure["max"], defaultValue));
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

		/// <summary>Looks for the first non-numerical character.</summary>
		/// <param name="s">The equation string.</param>
		/// <returns>The offset of the first non-numerical character in <i>s</i>, otherwise <i>s.Length</i>.</returns>
		internal static int firstOperation(string s)
		{
			string nums = "0123456789";
			for (int offset = 0; offset < s.Length; offset++) if (nums.IndexOf(s[offset]) == -1) return offset;
			return s.Length;
		}
	}
}