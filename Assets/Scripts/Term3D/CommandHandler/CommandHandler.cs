using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CommandHandler
{
	private Dictionary<string, Delegate> _commands;

	public CommandHandler()
	{
		_commands = new Dictionary<string, Delegate>();
		_commands.Add("cat", new Func<List<string>, string>(cat));
		_commands.Add("cd", new Func<List<string>, string>(cd));
		_commands.Add("cp", new Func<List<string>, string>(cp));
		//_commands.Add("exit", new Func<List<string>, string>(exit));
		_commands.Add("ls", new Func<List<string>, string>(ls));
		_commands.Add("mkdir", new Func<List<string>, string>(mkdir));
		_commands.Add("mv", new Func<List<string>, string>(mv));
		// _commands.Add("ps",  new Func<List<string>, string>(ps));
		_commands.Add("pwd", new Func<List<string>, string>(pwd));
		_commands.Add("rm", new Func<List<string>, string>(rm));
		_commands.Add("rmdir", new Func<List<string>, string>(rmdir));
		_commands.Add("touch", new Func<List<string>, string>(touch));
		//_commands.Add("whoami", new Func<List<string>, string>(whoami));
	}

	public string CallFunction(List<string> cmdLine)
	{
		if (cmdLine == null)
			return String.Empty;

		string command = cmdLine[0].Substring(1);

		if (String.IsNullOrEmpty(command))
			return String.Empty;

		if (!_commands.ContainsKey(command))
			return command + " : " + "No such command\n";

		Delegate del = _commands[command];
		cmdLine.RemoveAt(0);

		return del.DynamicInvoke(cmdLine).ToString();
	}

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
			DirectoryUtils.Directory dir = new DirectoryUtils.Directory(args[0]);

			if (!dir.IsDirectory())
				return dir.ProjectPath + " : No such file or directory.\n"; // TODO: plus verbose

			PathUtils.CurrPath = dir.RealPath;
		}
		return ("Now in " + PathUtils.PathToProjectPath(PathUtils.CurrPath) + "\n");
	}

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

	/*private string exit(List<string> args) // TODO: wut?
	{
		return "exit";
	}*/

	private string ls(List<string> args)  // TODO: à pousser ? Changer les utils ?
	{
		string result = "";
		DirectoryInfo dir;

		if (args.Count == 0)
			dir = new DirectoryInfo(PathUtils.CurrPath);
		else
			dir = new DirectoryInfo(args[0]);

		foreach (DirectoryInfo d in dir.GetDirectories())
			result += "Directory : " +  d.Name + "\n";

		foreach (FileInfo f in dir.GetFiles())
			result += "File : " + f.Name + "\n";

		return result;
	}

	private string mkdir(List<string> args)
	{
		if (args.Count == 0)
			return "mkdir command needs 1 argument.\n"; // TODO: plus verbose

		DirectoryUtils.Directory dir = new DirectoryUtils.Directory(args[0]);
		if (dir.IsDirectory())
			return "Such directory already exists : " + dir.ProjectPath + "\n"; // TODO: plus verbose

		/*
		int i;
		if ((i = args[0].LastIndexOf('/')) != -1)													//
			if (!Directory.Exists(args[0].Substring(0, i)))									// TODO: wut?
				return (args[0].Substring(0, i) + " : No such directory.\n");	//
		*/

		Directory.CreateDirectory(dir.RealPath);

		return "Directory " + dir.ProjectPath + " created\n";
	}

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
		if (args.Count == 0)
			return "rm command needs 1 argument.\n"; // TODO: plus verbose
		else
		{
			FileUtils.File file = new FileUtils.File(args[0]);
			if (!file.IsFile())
				return file.ProjectPath + " : File not found.\n"; // TODO: plus verbose
			File.Delete(file.RealPath);
			return file.ProjectPath + " deleted.";
		}
	}

	private string rmdir(List<string> args)
	{
		if (args.Count == 0)
			return "rmdir command needs 1 argument.\n"; // TODO: plus verbose
		else
		{
			DirectoryUtils.Directory dir = new DirectoryUtils.Directory(args[0]);
			if (!dir.IsDirectory())
				return dir.ProjectPath + " : Directory not found.\n"; // TODO: plus verbose
			Directory.Delete(dir.RealPath);
			return dir.ProjectPath + " deleted.";
		}
	}

	private string touch(List<string> args)
	{
		if (args.Count == 0)
			return "Touch command needs 1 arguments.\n"; // TODO: plus verbose

		FileUtils.File file = new FileUtils.File(args[0]);
		if (file.IsFile())
			return "Such file already exists : " + file.ProjectPath + "\n"; // TODO: plus verbose

		/*
		int i;
		if ((i = args[0].LastIndexOf('/')) != -1)												//
			if (!Directory.Exists(args[0].Substring(0, i)))								// TODO: wut?
				return args[0].Substring(0, i) + " : No such directory.\n";	//
		*/

		File.Create(file.RealPath);

		return "File " + file.ProjectPath + " created\n";
	}

	/*private string whoami(List<string> args)
	{
		return Environment.UserName + "\n";
	}*/
}
