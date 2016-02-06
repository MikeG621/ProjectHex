/*
 * Idmr.ProjectHex.BinaryFile.dll, Raw data management library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
 * with this file, You can obtain one at http://mozilla.org/MPL/2.0/
 *
 * Version: 0.1.5
 */

/* CHANGELOG
 * v0.1.5, 150705
 * [NEW] Save
 * [NEW] working on this[].set
 * [UPD] _raw converted to ArrayList
 * [ADD] SelectionDialog
 * [FIX] CurrentAsBool correctly treats non-zero as true
 * [ADD] _jumpTable
 * v0.1.4, 130910
 * [UPD] License
 * v0.1.1, 130421
 */

using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using Idmr.Common;

// TODO: possibly change _jumpTable to Collection<Var>

namespace Idmr.ProjectHex
{
    [Serializable]
	public class BinaryFile
	{
		ProjectFile _project = new ProjectFile();
		string _path = "";
		bool _isModified = false;
		int _position = 0;
		ArrayList _raw = new ArrayList(new byte[1024]);
		ProjectFile.Var[] _jumpTable = new ProjectFile.Var[1024];
		
		static string _notFound = " could not be found, please confirm file location";
		
		#region constructors
		/// <summary>Initializes an empty 1 KB (1024 byte) file with a blank <see cref="ProjectFile"/>.</summary>
		public BinaryFile() { /* do nothing */ }
		
		/// <summary>Initializes with the provided binary and searches for compatable installed projects.</summary>
		/// <param name="binaryPath">The full path to the binary.</param>
		/// <exception cref="System.IO.FileNotFoundException"><i>binaryPath</i> could not be located.</exception>
		/// <exception cref="OverflowException"><i>binaryPath</i> is larger than the maximum allowed file size.</exception>
		/// <remarks>If a single <see cref="ProjectFile"/> is found it will be applied. If more than one project is identified, a dialog will be presented to select the initial project. If no applicable project is found or the selection dialog is canceled the default blank ProjectFile will be used.</remarks>
		public BinaryFile(string binaryPath)
		{
			if (!File.Exists(binaryPath))
				throw new FileNotFoundException("'binaryPath'" + _notFound);
			_path = binaryPath;
			System.Diagnostics.Debug.WriteLine("looking for matches...");
			string[] matches = ProjectFile.GetProjectMatches(_path);
			System.Diagnostics.Debug.WriteLine("found " + matches.Length + " matches");
			if (matches.Length == 1) _project = new ProjectFile(matches[0]);
			else if (matches.Length > 1)
			{
				SelectionDialog dlg = new SelectionDialog(matches);
				DialogResult result = dlg.ShowDialog();
				if (result == DialogResult.OK && dlg.SelectedIndex != -1)
					_project = new ProjectFile(matches[dlg.SelectedIndex]);
				// else load it as blank
			}
			// else zero, and it's already a blank project
			System.Diagnostics.Debug.WriteLine(_project.Name);
			loadBinary();
		}
		
		/// <summary>Initializes with the provided binary and project.</summary>
		/// <param name="binaryPath">The full path to the binary.</param>
		/// <param name="projectPath">The full path to the project.</param>
		/// <exception cref="System.IO.FileNotFoundException"><i>binaryPath</i> or <i>projectPath</i> could not be located.</exception>
		/// <exception cref="OverflowException"><i>binaryPath</i> is larger than the maximum allowed file size.</exception>
		/// <exception cref="ArgumentException">The <see cref="ProjectFile"/> indicated by <i>projectPath</i> is not compatable with the binary.</exception>
		public BinaryFile(string binaryPath, string projectPath)
		{
			if (!File.Exists(binaryPath))
				throw new FileNotFoundException("'binaryPath'" + _notFound);
			if (!File.Exists(projectPath))
				throw new FileNotFoundException("'projectPath'" + _notFound);
			_path = binaryPath;
			if (ProjectFile.CheckProjectMatch(projectPath, _path)) _project = new ProjectFile(projectPath);
			else throw new ArgumentException("Indicated project is not compatable with the binary");
			loadBinary();
		}
		#endregion constructors

		#region public methods
		/// <summary>Sets the file size and length of the raw data.</summary>
		/// <param name="length">The new file size.</param>
		/// <param name="truncate">Allows shrinking the file size.</param>
		/// <exception cref="ArgumentOutOfRangeException"><i>length</i> is not positive.</exception>
		/// <exception cref="InvalidOperationException"><i>length</i> is smaller than the current file size and <i>truncate</i> is <b>false</b><br/>
		/// <b>-or-</b><br/>The parent ProjectFile uses a fixed file size.</exception>
		public void SetLength(int length, bool truncate)
		{
			if (length == _raw.Count) return;
			if (_project.Length != -1)
				throw new InvalidOperationException("Parent ProjectFile controls file size");
			if (length <= 0)
				throw new ArgumentOutOfRangeException("'length' must be positive");
			else if (length < _raw.Count)
			{
				if (!truncate)
					throw new InvalidOperationException("Value for 'length' would result in data loss");
				_raw.RemoveRange(length, _raw.Count - length);
				_isModified = true;
			}
			else
			{
				_raw.AddRange(new byte[length - _raw.Count]);
				_isModified = true;
			}
		}

		/// <summary>Save the raw data.</summary>
		/// <exception cref=="Idmr.SaveFileException">Binary location has not been set and user cancelled the dialog.<br/>-or-<br/>Error during save processes, no changes to file made.</exception>
		public void Save()
		{
			if (_path == "")
			{
				SaveFileDialog save = new SaveFileDialog();
				DialogResult res = save.ShowDialog();
				if (res == DialogResult.OK) _path = save.FileName;
				else throw new SaveFileException("Binary location has not been set.");
			}

			string tempPath = _path + ".tmp";
			if (File.Exists(_path)) File.Copy(_path, tempPath);
			FileStream fs = null;
			try
			{
				fs = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Write);
				fs.Write((byte[])_raw.ToArray(typeof(byte)), 0, _raw.Count);
				fs.SetLength(fs.Position);
				fs.Close();
				File.Delete(tempPath);
			}
			catch (Exception x)
			{
				if (fs != null) fs.Close();
				if (File.Exists(tempPath)) File.Copy(tempPath, _path);
				File.Delete(tempPath);
				throw new SaveFileException(x);
			}
		}

		/// <summary>Save the raw data to a new location.</summary>
		/// <param name="filePath">The new path.</param>
		/// /// <exception cref=="Idmr.SaveFileException">Error during save processes, no changes to file made.</exception>
		public void Save(string filePath)
		{
			_path = filePath;
			Save();
		}
		#endregion
		void loadBinary()
		{
			FileStream fs = File.OpenRead(_path);
			if (fs.Length > Int32.MaxValue)
				throw new OverflowException("Binary exceeds maximum allowable file size");
			_raw.Clear();
			_raw.AddRange(new BinaryReader(fs).ReadBytes((int)fs.Length));
			fs.Close();
			_project.AssignBinary(this);
			if (_project.Properties != null) _project.Properties.Populate((byte[]) _raw.ToArray(typeof(byte)), 0);
			_jumpTable = new ProjectFile.Var[_raw.Count];
			assignToJump(_project.Properties);
			_position = 0;
		}
		
		void assignToJump(ProjectFile.VarCollection vars)
		{
			for (int i = 0; i < vars.Count; i++)
			{
				_jumpTable[vars[i].FileOffset] = vars[i];
				if (vars[i].Values != null)
					assignToJump(vars[i].Values);
			}
		}
		
		#region public props
		/// <summary>Gets the Project object applied to the file.</summary>
		public ProjectFile Project { get { return _project; } }
		/// <summary>Gets or sets the byte value located at the specified <i>index</i></summary>
		/// <param name="index">The file offset to query</param>
		/// <exception cref="ArgumentOutOfRangeException"><i>index</i> is negative or beyond the end of file</exception>
		public byte this[int index]
		{
			get
			{
				if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException("'index' must be 0 to " + (Length - 1));
				return (byte)_raw[index];
			}
			set
			{
				// TODO: get this working
				if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException("'index' must be 0 to " + (Length - 1));
				_raw[index] = value;
			}
		}
		/// <summary>Gets the full path to the binary file</summary>
		public string FilePath { get { return _path; } }
		/// <summary>Gets the file size of the binary file's raw data</summary>
		public int Length { get { return _raw.Count; } }
		/// <summary>Gets if the raw data has been changed since opening</summary>
		public bool IsModified { get { return _isModified; } }
		
		/// <summary>Gets or sets the current offset within the raw data for read/write operations</summary>
		/// <remarks>If <i>value</i> is outside the range of the raw data, <b>zero</b> or the last byte is used as appropriate</remarks>
		public int Position
		{
			get { return _position; }
			set
			{
				if (value < 0) _position = 0;
				else if (value < Length) _position = value;
				else _position = Length - 1;
			}
		}
		
		/// <summary>Gets the boolean single-byte value at the current <see cref="Position"/></summary>
		/// <remarks>Evaluates <b>zero</b> as <b>true</b>, all others are <b>false</b>.</remarks>
		public bool CurrentValueAsBool { get { return (byte)_raw[_position] != 0; } }
		/// <summary>Gets the unsigned single-byte value at the current <see cref="Position"/></summary>
		public byte CurrentValueAsByte { get { return (byte)_raw[_position]; } }
		/// <summary>Gets the signed single-byte value as the current <see cref="Position"/></summary>
		public sbyte CurrentValueAsSByte { get { return (sbyte)_raw[_position]; } }
		/// <summary>Gets the signed two-byte value at the current <see cref="Position"/></summary>
		/// <exception cref="ArgumentException">The location of <see cref="Position"/> prevents reading the required length</exception>
		public short CurrentValueAsShort { get { return BitConverter.ToInt16((byte[])_raw.ToArray(typeof(byte)), _position); } }
		/// <summary>Gets the umsigned two-byte value at the current <see cref="Position"/></summary>
		/// <exception cref="ArgumentException">The location of <see cref="Position"/> prevents reading the required length</exception>
		public ushort CurrentValueAsUShort { get { return BitConverter.ToUInt16((byte[])_raw.ToArray(typeof(byte)), _position); } }
		/// <summary>Gets the signed four-byte value at the current <see cref="Position"/></summary>
		/// <exception cref="ArgumentException">The location of <see cref="Position"/> prevents reading the required length</exception>
		public int CurrentValueAsInt { get { return BitConverter.ToInt32((byte[])_raw.ToArray(typeof(byte)), _position); } }
		/// <summary>Gets the unsigned four-byte value at the current <see cref="Position"/></summary>
		/// <exception cref="ArgumentException">The location of <see cref="Position"/> prevents reading the required length</exception>
		public uint CurrentValueAsUInt { get { return BitConverter.ToUInt32((byte[])_raw.ToArray(typeof(byte)), _position); } }
		/// <summary>Gets the signed eight-byte value at the current <see cref="Position"/></summary>
		/// <exception cref="ArgumentException">The location of <see cref="Position"/> prevents reading the required length</exception>
		public long CurrentValueAsLong { get { return BitConverter.ToInt64((byte[])_raw.ToArray(typeof(byte)), _position); } }
		/// <summary>Gets the unsigned eight-byte value at the current <see cref="Position"/></summary>
		/// <exception cref="ArgumentException">The location of <see cref="Position"/> prevents reading the required length</exception>
		public ulong CurrentValueAsULong { get { return BitConverter.ToUInt64((byte[])_raw.ToArray(typeof(byte)), _position); } }
		/// <summary>Gets the single-precision floating point four-byte value at the current <see cref="Position"/></summary>
		/// <exception cref="ArgumentException">The location of <see cref="Position"/> prevents reading the required length</exception>
		public float CurrentValueAsSingle { get { return BitConverter.ToSingle((byte[])_raw.ToArray(typeof(byte)), _position); } }
		/// <summary>Gets the double-precision floating point eight-byte value at the current <see cref="Position"/></summary>
		/// <exception cref="ArgumentException">The location of <see cref="Position"/> prevents reading the required length</exception>
		public double CurrentValueAsDouble { get { return BitConverter.ToDouble((byte[])_raw.ToArray(typeof(byte)), _position); } }
		#endregion
	}
}