using System;
using System.IO;
using Idmr.Common;

namespace Idmr.ProjectHex
{
	public class BinaryFile
	{
		public ProjectFile _project = new ProjectFile();
		string _path = "";
		bool _isModified = false;
		int _position = 0;
		byte[] _raw = new byte[1024];
		// TODO: jump table to link _raw to _project values?
		
		static string _notFound = " could not be found, please confirm file location";
		
		#region constructors
		/// <summary>Initializes an empty 1 KB (1024 byte) file with a blank <see cref="ProjectFile"/></summary>
		public BinaryFile() { /* do nothing */ }
		
		/// <summary>Initializes with the provided binary and searches for compatable installed projects</summary>
		/// <param name="binaryPath">The full path to the binary</param>
		/// <exception cref="System.IO.FileNotFoundException"><i>binaryPath</i> could not be located</exception>
		/// <exception cref="OverflowException"><i>binaryPath</i> is larger than the maximum allowed file size.</exception>
		/// <remarks>If a single <see cref="ProjectFile"/> is found it will be applied. If more than one project is identified, a dialog will be presented to select the initial project. If no applicable project is found the default blank ProjectFile will be used.</remarks>
		public BinaryFile(string binaryPath)
		{
			if (!File.Exists(binaryPath))
				throw new FileNotFoundException("'binaryPath'" + _notFound);
			_path = binaryPath;
			string[] matches = ProjectFile.GetProjectMatches(_path);
			if (matches.Length == 1) _project = new ProjectFile(matches[0]);
			else if (matches.Length > 1)
			{
				// TODO: selection dialog
			}
			// else zero, and it's already a blank project
			loadBinary();
		}
		
		/// <summary>Initializes with the provided binary and project</summary>
		/// <param name="binaryPath">The full path to the binary</param>
		/// <param name="projectPath">The full path to the project</param>
		/// <exception cref="System.IO.FileNotFoundException"><i>binaryPath</i> or <i>projectPath</i> could not be located</exception>
		/// <exception cref="OverflowException"><i>binaryPath</i> is larger than the maximum allowed file size.</exception>
		/// <exception cref="ArgumentException">The <see cref="ProjectFile"/> indicated by <i>projectPath</i> is not compatable with the binary</exception>
		public BinaryFile(string binaryPath, string projectPath)
		{
			if (!File.Exists(binaryPath))
				throw new FileNotFoundException("'binaryPath'" + _notFound);
			if (!File.Exists(projectPath))
				throw new FileNotFoundException("'projectPath'" + _notFound);
			_path = binaryPath;
			if (ProjectFile.CheckProjectMatch(projectPath, _path)) _project = new ProjectFile(projectPath);
			else throw new ArgumentException("Indicated project is not compatabile with the binary");
			loadBinary();
		}
		#endregion constructors
		
		/// <summary>Sets the file size and length of the raw data</summary>
		/// <param name="length">The new file size</param>
		/// <param name="truncate">Allows shrinking the file size</param>
		/// <exception cref="ArgumentOutOfRangeException"><i>length</i> is not positive</exception>
		/// <exception cref="InvalidOperationException"><i>length</i> is smaller than the current file size and <i>truncate</i> is <b>false</b><br/>
		/// <b>-or-</b><br/>The parent ProjectFile uses a fixed file size</exception>
		public void SetLength(int length, bool truncate)
		{
			if (length == _raw.Length) return;
			if (_project.Length != -1)
				throw new InvalidOperationException("Parent ProjectFile controls file size");
			if (length <= 0)
				throw new ArgumentOutOfRangeException("'length' must be positive");
			else if (length < _raw.Length)
			{
				if (!truncate)
					throw new InvalidOperationException("Value for 'length' would result in data loss");
				byte[] temp = _raw;
				_raw = new byte[length];
				ArrayFunctions.TrimArray(temp, 0, _raw);
				_isModified = true;
			}
			else
			{
				byte[] temp = _raw;
				_raw = new byte[length];
				ArrayFunctions.WriteToArray(temp, _raw, 0);
				_isModified = true;
			}
		}
		
		void loadBinary()
		{
			FileStream fs = File.OpenRead(_path);
			if (fs.Length > Int32.MaxValue)
				throw new OverflowException("Binary exceeds maximum allowable file size");
			_raw = new BinaryReader(fs).ReadBytes((int)fs.Length);
			fs.Close();
			_project.AssignBinary(this);
			if (_project.Properties != null) _project.Properties.Populate(_raw, 0);
			_position = 0;
		}
		
		#region props
		public string ProjectName
		{
			get { return _project.Name; }
		}
		public ProjectFile Project
		{
			get { return _project; }
		}
		/// <summary>Gets the byte value located at the specified <i>index</i></summary>
		/// <param name="index">The file offset to query</param>
		/// <exception cref="ArgumentOutOfRangeException"><i>index</i> is negative or beyond the end of file</exception>
		public byte this[int index]
		{
			get
			{
				if (index < 0 || index >= Length) throw new ArgumentOutOfRangeException("'index' must be 0 to " + (Length - 1));
				return _raw[index];
			}
		}
		/// <summary>Gets the full path to the binary file</summary>
		public string FilePath { get { return _path; } }
		/// <summary>Gets the file size of the binary file's raw data</summary>
		public int Length { get { return _raw.Length; } }
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
		public bool CurrentValueAsBool { get { return _raw[_position] == 0; } }
		/// <summary>Gets the unsigned single-byte value at the current <see cref="Position"/></summary>
		public byte CurrentValueAsByte { get { return _raw[_position]; } }
		/// <summary>Gets the signed single-byte value as the current <see cref="Position"/></summary>
		public sbyte CurrentValueAsSByte { get { return (sbyte)_raw[_position]; } }
		/// <summary>Gets the signed two-byte value at the current <see cref="Position"/></summary>
		/// <exception cref="ArgumentException">The location of <see cref="Position"/> prevents reading the required length</exception>
		public short CurrentValueAsShort { get { return BitConverter.ToInt16(_raw, _position); } }
		/// <summary>Gets the umsigned two-byte value at the current <see cref="Position"/></summary>
		public ushort CurrentValueAsUShort { get { return BitConverter.ToUInt16(_raw, _position); } }
		/// <summary>Gets the signed four-byte value at the current <see cref="Position"/></summary>
		public int CurrentValueAsInt { get { return BitConverter.ToInt32(_raw, _position); } }
		/// <summary>Gets the unsigned four-byte value at the current <see cref="Position"/></summary>
		public uint CurrentValueAsUInt { get { return BitConverter.ToUInt32(_raw, _position); } }
		/// <summary>Gets the signed eight-byte value at the current <see cref="Position"/></summary>
		public long CurrentValueAsLong { get { return BitConverter.ToInt64(_raw, _position); } }
		/// <summary>Gets the unsigned eight-byte value at the current <see cref="Position"/></summary>
		public ulong CurrentValueAsULong { get { return BitConverter.ToUInt64(_raw, _position); } }
		/// <summary>Gets the single-precision floating point four-byte value at the current <see cref="Position"/></summary>
		public float CurrentValueAsSingle { get { return BitConverter.ToSingle(_raw, _position); } }
		/// <summary>Gets the double-precision floating point eight-byte value at the current <see cref="Position"/></summary>
		public double CurrentValueAsDouble { get { return BitConverter.ToDouble(_raw, _position); } }
		#endregion
	}
}