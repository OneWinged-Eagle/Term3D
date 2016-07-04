using System;
using System.IO;

public class FileUtils
{
	public class File : PathUtils.Path
	{
		public File(string path) : base(path) {}

		public string getExtension()
		{
			return Path.GetExtension(this._path);
		}

		public string getFileName()
		{
			return Path.GetFileName(this._path);
		}

		public string getFileNameWithoutExtension()
		{
			return Path.GetFileNameWithoutExtension(this._path);
		}

		public string getContent()
		{
			return System.IO.File.ReadAllText(this._path);
		}
	}
}
