using System;
using System.IO;

public class PathUtils
{
	public class Path
	{
		protected string _path;
		protected string _projectPath;

		protected Path(string path)
		{
			this._path = PathUtils.getPathFrom(path);

			this._projectPath = PathUtils.pathToProjectPath(this._path);
		}

		public string getPath()
		{
			return this._path;
		}

		public string getProjectPath()
		{
			return this._projectPath;
		}
	}

	static string _rootPath;
	static string _currPath;

	static public void setRootPath(string rootPath)
	{
		_rootPath = rootPath;
	}

	static public string getRootPath()
	{
		return _rootPath;
	}

	static public void setCurrPath(string currPath)
	{
		_currPath = currPath;
	}

	static public string getCurrPath()
	{
		return _currPath;
	}

	static public string getPathFrom(string path)
	{
		if (System.IO.Path.IsPathRooted(path))
			return PathUtils.getPathFromAbsolute(path);
		else
			return PathUtils.getPathFromRelative(path);
	}

	static public string getPathFromAbsolute(string absPath)
	{
		return System.IO.Path.Combine(_rootPath, absPath);
	}

	static public string getPathFromRelative(string relPath)
	{
		return System.IO.Path.Combine(_currPath, relPath);
	}

	static public string getPathFromRelative(string relPath, string currPath)
	{
		return System.IO.Path.Combine(currPath, relPath);
	}

	static public string pathToProjectPath(string path)
	{
		return path.Remove(path.IndexOf(_rootPath), _rootPath.Length);
	}
}
