using System;
using System.Collections.Generic;
using System.IO;

public class DirectoryUtils
{
	public class Directory : PathUtils.Path
	{
		public Directory(string path) : base(path) {}

		public string GetDirName()
		{
			return RealPath;
		}

		public FileUtils.File[] GetFiles()
		{
			List<FileUtils.File> filesList = new List<FileUtils.File>();
			String[] files = System.IO.Directory.GetFiles(RealPath);

			foreach (string file in files)
				filesList.Add(new FileUtils.File(file));

			return filesList.ToArray();
		}

		public Directory[] GetDirectories()
		{
			List<Directory> directoriesList = new List<Directory>();
			String[] directories = System.IO.Directory.GetDirectories(RealPath);

			foreach (string directory in directories)
				directoriesList.Add(new Directory(directory));

			return directoriesList.ToArray();
		}
	}
}
