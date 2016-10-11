using System;
using System.IO;

public class FileUtils
{
	public class File : PathUtils.Path
	{
		public File(string path) : base(path) {}

		public string GetFileName()
		{
			return GetName();
		}

		public string GetExtension()
		{
			return Path.GetExtension(RealPath);
		}

		public string GetFileNameWithoutExtension()
		{
			return Path.GetFileNameWithoutExtension(RealPath);
		}

		public string GetContent()
		{
			return System.IO.File.ReadAllText(RealPath);
		}

		public byte[] GetData()
		{
			return System.IO.File.ReadAllBytes(RealPath);
		}
	}
}