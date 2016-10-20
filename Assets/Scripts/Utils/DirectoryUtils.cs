using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class DirectoryUtils
{
	public class Directory : PathUtils.Path
	{
		public Directory(string path) : base(path) {}

		public string GetDirectoryName()
		{
			return GetName();
		}

		public FileUtils.File[] GetFiles()
		{
			List<FileUtils.File> filesList = new List<FileUtils.File>();
			string[] files = System.IO.Directory.GetFiles(RealPath);

			foreach (string file in files)
				filesList.Add(new FileUtils.File(file));

			return filesList.ToArray();
		}

		public FileUtils.File[] GetFiles(string[] extensions)
		{
			List<string> extensionsList = new List<string>(extensions);
			List<FileUtils.File> filesList = new List<FileUtils.File>();
			string[] files = System.IO.Directory.GetFiles(RealPath).Where(s => extensionsList.Any(e => s.EndsWith(e))).ToArray();

			foreach (string file in files)
				filesList.Add(new FileUtils.File(file));

			return filesList.ToArray();
		}

		public Directory[] GetDirectories()
		{
			List<Directory> directoriesList = new List<Directory>();
			string[] directories = System.IO.Directory.GetDirectories(RealPath);

			foreach (string directory in directories)
				directoriesList.Add(new Directory(directory));

			return directoriesList.ToArray();
		}
	}
}
