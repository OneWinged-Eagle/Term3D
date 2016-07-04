using System;
using System.Collections.Generic;
using System.IO;

public class DirectoryUtils
{
	public class Directory : PathUtils.Path
	{
		public Directory(string path) : base(path) {}

		public string getDirName()
		{
			return this._path;
		}
	
		public FileUtils.File[] getFiles()
		{
			List<FileUtils.File> filesList = new List<FileUtils.File>();
			String[] files = System.IO.Directory.GetFiles(this._path);
			foreach (string file in files)
				filesList.Add(new FileUtils.File(file));
			return filesList.ToArray();
		}

		public Directory[] getDirectories()
		{
			List<Directory> directoriesList = new List<Directory>();
			String[] directories = System.IO.Directory.GetDirectories(this._path);
			foreach (string directory in directories)
				directoriesList.Add(new Directory(directory));
			return directoriesList.ToArray();
		}
	}
}
