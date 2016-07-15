using System;
using System.IO;

public class PathUtils
{
	public class Path
	{
		public string RealPath { get; private set; }
		public string ProjectPath { get; private set; }

		protected Path(string path)
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
			return PathUtils.GetPathFromAbsolute(path);
		else
			return PathUtils.GetPathFromRelative(path);
	}

	public static string GetPathFromAbsolute(string absPath)
	{
		return System.IO.Path.Combine(RootPath, absPath);
	}

	public static string GetPathFromRelative(string relPath)
	{
		return System.IO.Path.Combine(CurrPath, relPath);
	}

	public static string GetPathFromRelative(string relPath, string currPath)
	{
		return System.IO.Path.Combine(currPath, relPath);
	}

	public static string PathToProjectPath(string path)
	{
		return path.Remove(path.IndexOf(RootPath), RootPath.Length);
	}
}
