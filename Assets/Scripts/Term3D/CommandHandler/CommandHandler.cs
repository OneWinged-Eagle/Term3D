using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
		_commands.Add("exit", new Func<List<string>, string>(exit));
		_commands.Add("ls", new Func<List<string>, string>(ls));
		_commands.Add("mkdir", new Func<List<string>, string>(mkdir));
		_commands.Add("mv", new Func<List<string>, string>(mv));
		// _commands.Add("ps",  new Func<List<string>, string>(ps));
		_commands.Add("pwd", new Func<List<string>, string>(pwd));
		_commands.Add("rm", new Func<List<string>, string>(rm));
		_commands.Add("rmdir", new Func<List<string>, string>(rmdir));
		_commands.Add("touch", new Func<List<string>, string>(touch));
		_commands.Add("whoami", new Func<List<string>, string>(whoami));
	}

	public string CallFunction(List<string> cmdLine)
	{
		if (cmdLine == null)
			return String.Empty;

		string command = cmdLine[0];

		if (String.IsNullOrEmpty(command))
			return String.Empty;

		if (!_commands.ContainsKey(command))
			return command + " : " + "No such command\n";

		Delegate del = _commands[command];
		cmdLine.RemoveAt(0);

		return del.DynamicInvoke(cmdLine).ToString();
	}

	// TODO: retaper un peu toutes les fonctions avec les Utils, si besoin

	private string cat(List<string> args)
	{
		if (args.Count != 1)
			return "Command's syntax is incorrect.\n"; // TODO: plus verbose
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
				return dir.ProjectPath + " : No such file or directory."; // TODO: plus verbose

			PathUtils.CurrPath = dir.RealPath;
		}
		return (null); // TODO: ???
	}

	private string cp(List<string> args) // TODO: à pousser ?
	{
		if (args.Count != 2)
			return "Cp command needs 2 arguments.\n"; // TODO: plus verbose
		else
		{
			if (!File.Exists(args[0]))
				return args[0] + " : No such file or directory.\n"; // TODO: plus verbose
			else if (!Directory.Exists(args[1]))
				return args[1] + " : No such file or directory.\n"; // TODO: plus verbose
			else
				File.Copy(args[0], args[1] + "\\" + args[0]);
		}
		return String.Empty; // TODO: plus verbose
	}

	private string exit(List<string> args) // TODO: wut?
	{
		return "exit";
	}

	private string ls(List<string> args)  // TODO: à pousser ?
	{
		string result = "";
		DirectoryInfo dir;

		if (args.Count == 0)
			dir = new DirectoryInfo(Directory.GetCurrentDirectory());
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
		int i;

		if (args.Count != 1)
			return "Touch command needs 2 arguments.\n"; // TODO: plus verbose

		if (Directory.Exists(args[0]))
			return "Such directory already exists : " + args[0] + "\n"; // TODO: plus verbose

		if ((i = args[0].LastIndexOf('/')) != -1)													//
			if (!Directory.Exists(args[0].Substring(0, i)))									// TODO: wut?
				return (args[0].Substring(0, i) + " : No such directory.\n");	//

		Directory.CreateDirectory(args[0]);

		return String.Empty;
	}

	private string mv(List<string> args) // TODO: à faire
	{
		return ("mv\n");
	}

	private void ps()
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
	}

	private string pwd(List<string> args)
	{
		return Directory.GetCurrentDirectory() + "\n";
	}

	private string rm(List<string> args)
	{
		if (args.Count != 1)
			return "Rm command needs only 1 argument.\n"; // TODO: plus verbose
		else
		{
			if (!File.Exists(args[0]))
				return args[0] + " : File not found.\n"; // TODO: plus verbose
			else
				File.Delete(args[0]);
		}
		return String.Empty;
	}

	private string rmdir(List<string> args)
	{
		if (args.Count != 1)
			return "Rmdir command needs only 1 argument.\n"; // TODO: plus verbose
		else
		{
			if (!Directory.Exists(args[0]))
				return args[0] + " : Directory not found.\n"; // TODO: plus verbose
			else
				Directory.Delete(args[0]);
		}
		return String.Empty;
	}

	private string touch(List<string> args)
	{
		int i;

		if (args.Count != 1)
			return "Touch command needs 2 arguments.\n"; // TODO: plus verbose

		if (File.Exists(args[0]))
			return "Such file already exists : " + args[0] + "\n"; // TODO: plus verbose

		if ((i = args[0].LastIndexOf('/')) != -1)												//
			if (!Directory.Exists(args[0].Substring(0, i)))								// TODO: wut?
				return args[0].Substring(0, i) + " : No such directory.\n";	//

		File.Create(args[0]);

		return String.Empty;
	}

	private string whoami(List<string> args)
	{
		return Environment.UserName + "\n";
	}
}
