using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace Cyrez
{
	public class RecentFilesManager : Component
	{
		private const char SEPARATOR = ';';

		private ToolStripMenuItem _menu;
		private List<String> _recentFiles;
		private  int _maxRecentFiles;

		public event EventHandler<FileEventArgs> RecentFileClick;

		public RecentFilesManager()
		{
			_maxRecentFiles = 10;
			_recentFiles = new List<string>(_maxRecentFiles);
			RefreshRecentsFilesMenu();
		}

		public ToolStripMenuItem menu 
		{
			get { return _menu; }
			set
			{
				_menu = value;
				RefreshRecentsFilesMenu();
			}
		}

		public int MaxRecentFiles 
		{ 
			get{return _maxRecentFiles;}
			set
			{
				_maxRecentFiles = Math.Max(1, value);
				EnsureCapacity(_maxRecentFiles);
			}
		}
			
		private void EnsureCapacity(int capacity)
		{
			while(_recentFiles.Count > capacity)
				_recentFiles.RemoveAt(0);
		}

		public String RecentFiles 
		{ 
			get
			{
				return string.Join(SEPARATOR.ToString(), _recentFiles.ToArray());
			}
			set
			{
				_recentFiles.Clear();
				_recentFiles.AddRange(value.Split(new[]{SEPARATOR}, StringSplitOptions.RemoveEmptyEntries));
				RefreshRecentsFilesMenu();
			}
		}

		public void SetFileRecent(string filename)
		{
			int index = _recentFiles.FindIndex(s => string.Compare(s, filename, true) == 0);
			if (index >= 0 && index == _recentFiles.Count -1) return;
			if (index > 0) 
				_recentFiles.RemoveAt(index);
			else
				EnsureCapacity(_maxRecentFiles - 1);
			
			_recentFiles.Add(filename);
			
			if (index < 0 && _menu != null)
			{
				_menu.Enabled = true;
				AddMenuItem(0, filename);
			}
			else
				RefreshRecentsFilesMenu();
		}
					
		public void Clear()
		{
			_recentFiles.Clear();
			RefreshRecentsFilesMenu();
		}

		private void RefreshRecentsFilesMenu()
		{
			if (_menu == null) return;
			_menu.DropDownItems.Clear();
			
			for(int i = _recentFiles.Count -1; i >= 0; i--)
				AddMenuItem(_menu.DropDownItems.Count, _recentFiles[i]);

			_menu.Enabled = _menu.DropDownItems.Count > 0;
		}

		private void AddMenuItem(int index, string filename)
		{
			var item = new ToolStripMenuItem(Path.GetFileName(filename));
			item.Tag = filename;
			item.Click += recentFile_Click;
			_menu.DropDownItems.Insert(index, item);
		}
		
		void recentFile_Click(object sender, EventArgs e)
		{
			var menu = (ToolStripMenuItem)sender;
			if (RecentFileClick != null)
				RecentFileClick(this, new FileEventArgs((string)menu.Tag));
		}

	}

	public class FileEventArgs : EventArgs
	{
		internal FileEventArgs(string filename)
		{
			Filename = filename;
		}

		public string Filename { get; private set; }
	}

}
