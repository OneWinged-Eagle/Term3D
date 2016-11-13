using UnityEngine;

using System;
using System.IO;

///<summary>
///Utility functions and classes for Paths
///</summary>
public class PathUtils
{
	public class Path
	{
		public string RealPath { get; private set; }
		public string ProjectPath { get; private set; }

		public bool IsFile()
		{
			return File.Exists(RealPath);
		}

		public bool IsDirectory()
		{
			return Directory.Exists(RealPath);
		}

		public string GetName()
		{
			return System.IO.Path.GetFileName(RealPath);
		}

		public Path(string path)
		{
			RealPath = PathUtils.GetPathFrom(path);

			ProjectPath = PathUtils.PathToProjectPath(RealPath);
		}
	}

	public static string RootPath { get; set; }
	public static string CurrPath { get; set; }

	public static string GetPathFrom(string path)
	{
		if (System.IO.Path.IsPathRooted(path))
			return GetPathFromAbsolute(path);
		else
			return GetPathFromRelative(path);
	}

	public static string GetPathFromAbsolute(string path)
	{
		return System.IO.Path.GetFullPath(path);
	}

	public static string GetPathFromRelative(string relPath)
	{
		return System.IO.Path.GetFullPath(System.IO.Path.Combine(CurrPath, relPath));
	}

	public static string GetPathFromRelative(string relPath, string currPath)
	{
		return System.IO.Path.GetFullPath(System.IO.Path.Combine(currPath, relPath));
	}

	public static string PathToProjectPath(string path)
	{
		string projectPath = path.Remove(path.IndexOf(RootPath), RootPath.Length);

		if (String.IsNullOrEmpty(projectPath))
			projectPath = System.IO.Path.DirectorySeparatorChar.ToString();

		return projectPath;
	}

	public static bool IsValidPath(string path)
	{
		return Directory.Exists(path);
	}
}
