using System;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace Cyrez.SqliteUtil
{
	public class CsvReader : IDataReader
	{
		public CsvReader(bool header, char delimiter, string filename)
			: this(header, delimiter, null, new FileStream(filename, FileMode.Open))
		{
		}

		public CsvReader(bool header, char delimiter, Regex regex, Stream stream)
		{
			if(stream == null) throw new ArgumentNullException("stream");
			Header = header;
			Delimiter = delimiter;
			var metaChars = "^[.${*(\\+)|?<>".ToCharArray();

			string escapedDelimiter = Array.IndexOf(metaChars, delimiter) >= 0 ? @"\" + delimiter : delimiter.ToString();

			Regex = regex ?? new Regex(escapedDelimiter + "(?!(?<=(?:^|" + escapedDelimiter + @")\s*""(?:[^""]|""""|\\"")*" + escapedDelimiter + @")(?:[^""]|""""|\\"")*""\s*(?:" + escapedDelimiter + "|$))");

			_stream = stream;
			_reader = new StreamReader(_stream);
			if (Header)
				Read();
		}

		public CsvReader(string filename)
			: this(true, ',',  filename)
		{
		}

		public CsvReader(Stream stream)
			: this(true, ',', null, stream)
		{
		}

		public char Delimiter { get; private set; }
		public Regex Regex { get; set; }

		public bool Header { get; private set; }

		private Stream _stream;
		private StreamReader _reader;

		private string ReformatQuotedString(string s)
		{
			var trimed = s.Trim();
			if (trimed.StartsWith("\"") && trimed.EndsWith("\""))
			{
				//remove first and last "
				trimed = trimed.Substring(1, trimed.Length - 2);
				//Replace "" by a "
				trimed = trimed.Replace("\"\"", "\"");
				//Replace \" by a "
				return trimed.Replace("\\\"", "\"");
			}
			return s;
		}

		private void AddRow(DataTable table, string[] values)
		{
			table.Rows.Add(values);
		}

		public void Close()
		{
			if (_reader != null)
			{
				_reader.Close();
				_stream.Close();
				_reader.Dispose();
				_stream.Dispose();
				_reader = null;
				_stream = null;
			}
		}

		public int Depth
		{
			get { return Header ? _lineCount - 1 : _lineCount; }
		}

		public DataTable GetSchemaTable()
		{
			throw new NotImplementedException();
		}

		public bool IsClosed
		{
			get { return _stream == null; }
		}

		public bool NextResult()
		{
			throw new NotImplementedException();
		}

		private int _lineCount;

		private List<string> columns = new List<string>();

		private object[] _values;
		public bool Read()
		{
			if (_reader == null) return false;
			
			string line;
			do
				line =  _reader.ReadLine();
			while(line == string.Empty); //skip blank line

			if (line != null)
			{
				var values = Regex.Split(line);
				for (int i = 0; i < values.Length; i++)
					if (!string.IsNullOrEmpty(values[i]))
						values[i] = ReformatQuotedString(values[i]);

				if (_lineCount == 0 && Header)
					for (int i = 0; i < values.Length; i++)
						columns.Add(values[i]);
				else
				{
					for (int i = columns.Count; i < values.Length; i++)
						columns.Add("Column" + i);
					_values = values;
				}
				_lineCount++;
				return true;
			}
			
			Close();
			return false;
		}

		public void Analyze()
		{
			//if (_stream != null)
			//{
			//    long position = _stream.Position;

			//    while (Read())
			//    {
			//        if (_types == null)
			//            _types = new Type[FieldCount];


			//        for (int i = 0; i < FieldCount; i++)
			//        {
			//            var value = GetString(i);
			//            var code = Convert.GetTypeCode(value);
			//            bool boolTest;
			//            byte byteTest;
			//            short shortTest;
			//            int iTest;
			//            float fTest;
			//            double dblTest;
			//            decimal decTest;


			//            if (_types[i] == null)
			//            {
			//                if (byte.TryParse(value, out byteTest))
			//                    _types[i] = typeof(byte);
			//                else if (short.TryParse(value, out shortTest))
			//                    _types[i] = typeof(short);
			//                else if (int.TryParse(value, out iTest))
			//                    _types[i] = typeof(int);
			//                else if (float.TryParse(value, out fTest) && value == fTest.ToString())
			//                    _types[i] = fTest.GetType();
			//                else if (double.TryParse(value, out dblTest) && value == dblTest.ToString())
			//                    _types[i] = dblTest.GetType();
			//                else if (decimal.TryParse(value, out decTest) && value == decTest.ToString())
			//                    _types[i] = decTest.GetType();
			//                else if (bool.TryParse(value, out boolTest))
			//                    _types[i] = typeof(bool);
			//                else
			//                    _types[i] = typeof(string);
			//            }
			//            else if (_types[0] == typeof(byte))
			//            {
			//                if (byte.TryParse(value, out byteTest))
			//                    _types[i] = typeof(byte);
			//                else if (short.TryParse(value, out shortTest))
			//                    _types[i] = typeof(short);
			//                else if (int.TryParse(value, out iTest))
			//                    _types[i] = typeof(int);
			//                else if (float.TryParse(value, out fTest) && value == fTest.ToString())
			//                    _types[i] = fTest.GetType();
			//                else if (double.TryParse(value, out dblTest) && value == dblTest.ToString())
			//                    _types[i] = dblTest.GetType();
			//                else if (decimal.TryParse(value, out decTest) && value == decTest.ToString())
			//                    _types[i] = decTest.GetType();
			//                else
			//                    _types[i] = typeof(string);
			//            }
			//            else if (_types[0] == typeof(short))
			//            {
			//                if (short.TryParse(value, out shortTest))
			//                    _types[i] = typeof(short);
			//                else if (int.TryParse(value, out iTest))
			//                    _types[i] = typeof(int);
			//                else if (float.TryParse(value, out fTest) && value == fTest.ToString())
			//                    _types[i] = fTest.GetType();
			//                else if (double.TryParse(value, out dblTest) && value == dblTest.ToString())
			//                    _types[i] = dblTest.GetType();
			//                else if (decimal.TryParse(value, out decTest) && value == decTest.ToString())
			//                    _types[i] = decTest.GetType();
			//                else
			//                    _types[i] = typeof(string);
			//            }
			//        }
			//    }


			//    if (_stream.CanSeek)
			//        _stream.Seek(position, SeekOrigin.Begin);
			//}

		}

		public int RecordsAffected
		{
			get { throw new NotImplementedException(); }
		}

		public void Dispose()
		{
			Close();
		}

		public int FieldCount
		{
			get { return columns.Count; }
		}

		public bool GetBoolean(int i)
		{
			throw new NotImplementedException();
		}

		public byte GetByte(int i)
		{
			throw new NotImplementedException();
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public char GetChar(int i)
		{
			throw new NotImplementedException();
		}

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public IDataReader GetData(int i)
		{
			throw new NotImplementedException();
		}

		public string GetDataTypeName(int i)
		{
			throw new NotImplementedException();
		}

		public DateTime GetDateTime(int i)
		{
			throw new NotImplementedException();
		}

		public decimal GetDecimal(int i)
		{
			throw new NotImplementedException();
		}

		public double GetDouble(int i)
		{
			throw new NotImplementedException();
		}

		public Type GetFieldType(int i)
		{
			return typeof(string);
		}

		public float GetFloat(int i)
		{
			throw new NotImplementedException();
		}

		public Guid GetGuid(int i)
		{
			throw new NotImplementedException();
		}

		public short GetInt16(int i)
		{
			throw new NotImplementedException();
		}

		public int GetInt32(int i)
		{
			return Convert.ToInt32(_values[i]);
		}

		public long GetInt64(int i)
		{
			throw new NotImplementedException();
		}

		public string GetName(int i)
		{
			return columns[i];
		}

		public int GetOrdinal(string name)
		{
			for (int i = 0; i < columns.Count; i++)
				if (string.Compare(columns[i], name, true) == 0)
					return i;
			return -1;
		}

		public string GetString(int i)
		{
			return Convert.ToString(_values[i]);
		}

		public object GetValue(int i)
		{
			return _values[i];
		}

		public int GetValues(object[] values)
		{
			throw new NotImplementedException();
		}

		public bool IsDBNull(int i)
		{
			return _values[i] == null;
		}

		public object this[string name]
		{
			get { return GetValue(GetOrdinal(name)); }
		}

		public object this[int i]
		{
			get { return GetValue(i); }
		}
	}
}
