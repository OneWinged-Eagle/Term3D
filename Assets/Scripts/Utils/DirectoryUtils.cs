using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

///<summary>
///Utility functions and classes for Directories
///</summary>
public class DirectoryUtils
{
	[System.Serializable]
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
			string[] files = System.IO.Directory.GetFiles(RealPath).Where(s => extensionsList.Any(e => s.EndsWith(e, true, null))).ToArray();

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

			if (PathUtils.GetPathFrom(this.RealPath) != PathUtils.RootPath)
				directoriesList.Add(new Directory(this.RealPath + "/.."));

			return directoriesList.ToArray();
		}
	}
}
