using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CommandHandler
{
	private Dictionary<string, Delegate> commands;

	public CommandHandler()
	{
		commands = new Dictionary<string, Delegate>();
		commands.Add("cat", new Func<List<string>, string>(cat));
		commands.Add("cd", new Func<List<string>, string>(cd));
		commands.Add("cp", new Func<List<string>, string>(cp));
		commands.Add("ls", new Func<List<string>, string>(ls));
		commands.Add("mkdir", new Func<List<string>, string>(mkdir));
		commands.Add("mv", new Func<List<string>, string>(mv));
		// commands.Add("ps",  new Func<List<string>, string>(ps));
		commands.Add("pwd", new Func<List<string>, string>(pwd));
		commands.Add("rm", new Func<List<string>, string>(rm));
		commands.Add("rmdir", new Func<List<string>, string>(rmdir));
		commands.Add("touch", new Func<List<string>, string>(touch));
		//commands.Add("whoami", new Func<List<string>, string>(whoami));
	}

	public string CallFunction(List<string> cmdLine)
	{
		if (cmdLine == null)
			return String.Empty;

		string command = cmdLine[0].Substring(1);

		if (String.IsNullOrEmpty(command))
			return String.Empty;

		if (!commands.ContainsKey(command))
			return command + " : " + "No such command\n";

		Delegate del = commands[command];
		cmdLine.RemoveAt(0);

		return del.DynamicInvoke(cmdLine).ToString();
	}

	// TODO: Gérer les accès pour cette fonction
	private string cat(List<string> args)
	{
		if (args.Count == 0)
			return "cat command needs 1 argument.\n"; // TODO: plus verbose
		else
		{
			FileUtils.File file = new FileUtils.File(args[0]);

			if (!file.IsFile())
				return "File not found\n"; // TODO: plus verbose

			return file.GetContent();
		}
	}

	private string cd(List<string> args)
	{
		if (args.Count == 0)
			PathUtils.CurrPath = PathUtils.RootPath;
		else
		{
			string path = (args[0][0] == '/' ? PathUtils.GetPathFromAbsolute(PathUtils.RootPath + args[0]) : PathUtils.GetPathFromAbsolute(PathUtils.CurrPath + "/" + args[0]));
			if (path.Contains(PathUtils.RootPath))
			{
				DirectoryUtils.Directory dir = new DirectoryUtils.Directory(path);
				if (!dir.IsDirectory())
					return dir.ProjectPath + " : No such file or directory.\n"; // TODO: plus verbose
				PathUtils.CurrPath = dir.RealPath;
			}
			else
			 return ("Cannot access " + args[0] + " : Permission denied.\n");
		}
		return ("Now in " + PathUtils.PathToProjectPath(PathUtils.CurrPath) + "\n");
	}

	// TODO: Gérer les accès pour cette fonction
	private string cp(List<string> args) // TODO: à pousser ?
	{
		if (args.Count == 0)
			return "cp command needs 1 (or 2) argument(s).\n"; // TODO: plus verbose
		else
		{
			FileUtils.File file1 = new FileUtils.File(args[0]);

			if (!file1.IsFile())
				return file1.ProjectPath + " : No such file or directory.\n"; // TODO: plus verbose

			FileUtils.File file2 = new FileUtils.File(file1.GetName());

			if (args.Count >= 2)
				file2 = new FileUtils.File(args[1]);

			if (file2.IsFile())
				return file2.ProjectPath + " : already exists.\n"; // TODO: plus verbose

			File.Copy(file1.RealPath, file2.RealPath); // TODO: à mettre dans les utils
			return file1.ProjectPath + "copied to " + file2.ProjectPath + ".\n"; // TODO: plus verbose
		}
	}

	private string ls(List<string> args)  // TODO: à pousser ? Changer les utils ?
	{
		string result = "";
		DirectoryInfo dir;

		for (int i = 0; i < args.Count || i == 0; ++i)
		{
			if (args.Count == 0)
				dir = new DirectoryInfo(PathUtils.CurrPath);
			else
			{
				result += "Listing " + args[i] + " directory.\n";
				if (args[i].Equals("/"))
					dir = new DirectoryInfo(PathUtils.RootPath);
				else
				{
					string path = (args[i][0] == '/' ? PathUtils.GetPathFromAbsolute(PathUtils.RootPath + args[i]) : PathUtils.GetPathFromAbsolute(PathUtils.CurrPath + "/" + args[i]));
					if (path.Contains(PathUtils.RootPath))
					{
						if (PathUtils.IsValidPath(path))
							dir = new DirectoryInfo(path);
						else
						{
							dir = null;
							result += "Cannot access " + args[i] + " directory : No such file or directory.\n";
						}
					}
					else
					{
						dir = null;
						result += "Cannot access " + args[i] + " directory : Permission denied.\n";
					}
				}
			}
			if (dir != null)
			{
				foreach (DirectoryInfo d in dir.GetDirectories())
					result += "Directory : " +  d.Name + "\n";
				foreach (FileInfo f in dir.GetFiles())
					result += "File : " + f.Name + "\n";
			}
		}
		return result;
	}

	private string mkdir(List<string> args)
	{
		string result = "";

		if (args.Count == 0)
			return "mkdir command needs at least 1 argument.\n"; // TODO: plus verbose

		for (int i = 0; i < args.Count; ++i)
		{
			string path = (args[i][0] == '/' ? PathUtils.GetPathFromAbsolute(PathUtils.RootPath + args[i]) : PathUtils.GetPathFromAbsolute(PathUtils.CurrPath + "/" + args[i]));

			if (path.Contains(PathUtils.RootPath))
			{
				DirectoryUtils.Directory dir = new DirectoryUtils.Directory(path);
				if (dir.IsDirectory())
					result += "Such directory already exists : " + dir.ProjectPath + "\n"; // TODO: plus verbose
				else
				{
					Directory.CreateDirectory(dir.RealPath);
					result += "Directory " + dir.ProjectPath + " created\n";
				}
			}
			else
				result += "Cannot access " + args[i] + " directory : Permission denied\n";
		}
		return result;
	}

	// TODO: Gérer les accès pour cette fonction
	private string mv(List<string> args) // TODO: à faire
	{
		if (args.Count == 0)
			return "mv command needs 1 (or 2) argument(s).\n"; // TODO: plus verbose
		else
		{
			FileUtils.File file1 = new FileUtils.File(args[0]);

			if (!file1.IsFile())
				return file1.ProjectPath + " : No such file or directory.\n"; // TODO: plus verbose

			FileUtils.File file2 = new FileUtils.File(file1.GetName());

			if (args.Count >= 2)
				file2 = new FileUtils.File(args[1]);

			if (file2.IsFile())
				return file2.ProjectPath + " : already exists.\n"; // TODO: plus verbose

			File.Move(file1.RealPath, file2.RealPath); // TODO: à mettre dans les utils
			return file1.ProjectPath + " moved to " + file2.ProjectPath + "\n"; // TODO: plus verbose
		}
	}

	/*private void ps()
	{
		Process[] processes = Process.GetProcesses();

    Console.WriteLine("-----------------------------------------------------");
    Console.WriteLine("{0, -8} {1, -30} {2, -10}", "PID", "Process Name", "Status");
    Console.WriteLine("-----------------------------------------------------");

    foreach (Process process in processes)
      try
      {
        Console.WriteLine("{0, -8} {1, -30} {2, -10}", process.Id, process.ProcessName, process.Responding ? "Running" : "IDLE");
      }
      catch (Exception)
      {
        continue; // TODO: gérer les exception, stp
      }
	}*/

	private string pwd(List<string> args)
	{
		return PathUtils.PathToProjectPath(PathUtils.CurrPath) + "\n";
	}

	private string rm(List<string> args)
	{
		string result = "";

		if (args.Count == 0)
			return "rm command needs at least 1 argument.\n"; // TODO: plus verbose

			for (int i = 0; i < args.Count; ++i)
			{
				string path = (args[i][0] == '/' ? PathUtils.GetPathFromAbsolute(PathUtils.RootPath + args[i]) : PathUtils.GetPathFromAbsolute(PathUtils.CurrPath + "/" + args[i]));

				if (path.Contains(PathUtils.RootPath))
				{
					FileUtils.File file = new FileUtils.File(path);
					if (!file.IsFile())
						result += file.ProjectPath + " : File not found.\n"; // TODO: plus verbose
					else
					{
						File.Delete(file.RealPath);
						result += file.ProjectPath + " deleted.\n";
					}
				}
				else
					result += "Cannot access " + args[i] + " directory : Permission denied\n";
			}
		return result;
	}

	private string rmdir(List<string> args)
	{
		string result = "";

		if (args.Count == 0)
			return "rmdir command needs at least 1 argument.\n"; // TODO: plus verbose

		for (int i = 0; i < args.Count; ++i)
		{
			string path = (args[i][0] == '/' ? PathUtils.GetPathFromAbsolute(PathUtils.RootPath + args[i]) : PathUtils.GetPathFromAbsolute(PathUtils.CurrPath + "/" + args[i]));

			if (path.Contains(PathUtils.RootPath))
			{
				DirectoryUtils.Directory dir = new DirectoryUtils.Directory(path);
				if (!dir.IsDirectory())
					result += dir.ProjectPath + " : Directory not found.\n"; // TODO: plus verbose
				else
				{
					Directory.Delete(dir.RealPath);
					result += dir.ProjectPath + " deleted.\n";
				}
			}
			else
				result += "Cannot access " + args[i] + " directory : Permission denied\n";
		}
		return result;
	}


	private string touch(List<string> args)
	{
		string result = "";

		if (args.Count == 0)
			return "Touch command needs at least 1 arguments.\n"; // TODO: plus verbose

		for (int i = 0; i < args.Count; ++i)
		{
			string path = (args[i][0] == '/' ? PathUtils.GetPathFromAbsolute(PathUtils.RootPath + args[i]) : PathUtils.GetPathFromAbsolute(PathUtils.CurrPath + "/" + args[i]));

			if (path.Contains(PathUtils.RootPath))
			{
				FileUtils.File file = new FileUtils.File(path);
				if (file.IsFile())
				{
					result += "Such file already exists : " + file.ProjectPath + "\n"; // TODO: plus verbose
				}
				else
				{
					File.Create(file.RealPath);
					result += "File " + file.ProjectPath + " created\n";
				}
			}
			else
				result += "Cannot access " + args[i] + " directory : Permission denied\n";
		}
		return result;
	}

	/*private string whoami(List<string> args)
	{
		return Environment.UserName + "\n";
	}*/
}
