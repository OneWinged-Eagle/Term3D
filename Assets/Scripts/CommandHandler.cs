using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Diagnostics;
using System.Collections.Generic;

public class CommandHandler
{
	private Dictionary<string, Delegate> commands;

	public CommandHandler()
	{
		this.commands = new Dictionary<string, Delegate>();
		this.commands.Add("pwd", new Func<List<string>, string>(pwd));
		this.commands.Add("cp", new Func<List<string>, string>(cp));
		this.commands.Add("mv", new Func<List<string>, string>(mv));
		this.commands.Add("rm", new Func<List<string>, string>(rm));
		this.commands.Add("whoami", new Func<List<string>, string>(whoami));
		this.commands.Add("cat", new Func<List<string>, string>(cat));
		this.commands.Add("ls", new Func<List<string>, string>(ls));
		this.commands.Add("exit", new Func<List<string>, string>(exit));
		this.commands.Add("cd", new Func<List<string>, string>(cd));
//		this.commands.Add("ps", new Func<string>(ps));
	}

	public string callFunction(List<string> cmdline)
	{
		string command = cmdline[0];

		if (command == "")
			return ("");
		if (!this.commands.ContainsKey(command))
			return (command + " : " + "No such command\n");
		Delegate del = this.commands[command];
		cmdline.RemoveAt(0);
		return (del.DynamicInvoke(cmdline).ToString());
	}

	private string exit(List<string> args)
	{
		return ("exit");
	}

	private string cp(List<string> args)
	{
		if (args.Count != 2)
			return ("Cp command needs 2 arguments.\n");
		else
		{
			if (!File.Exists(args[0]))
				return (args[0] + " : No such file or directory.\n");
			else if (!Directory.Exists(args[1]))
				return (args[1] + " : No such file or directory.\n");
			else
				File.Copy(args[0], args[1] + "\\" + args[0]);
		}
		return ("");
	}

	private string mv(List<string> args)
	{
		return ("mv\n");
	}

	private string rm(List<string> args)
	{
		if (args.Count != 1)
			return ("Rm command needs only 1 argument.\n");
		else
		{
			if (!File.Exists(args[0]))
				return (args[0] + " : File not found.\n");
			else
				File.Delete(args[0]);
		}
		return("");
	}

	private string cd(List<string> args)
	{
		return ("cd\n");
	}

	private string cat(List<string> args)
	{
		if (args.Count != 1)
			return ("The syntax of the command is incorrect.\n");
		else
		{
			if (File.Exists(args[0]) == false)
			{
					return ("File not found\n");
			}
			else
			{
					string contents = File.ReadAllText("test.txt");
					return (contents);
			}
		}
	}

	private string ls(List<string> args)
	{
		string result = "";
		DirectoryInfo dir;

		if (args.Count == 0)
			dir = new DirectoryInfo(Directory.GetCurrentDirectory());
		else
			dir = new DirectoryInfo(args[0]);
		foreach (DirectoryInfo d in dir.GetDirectories())
		{
			result += "Directory : " +  d.Name + "\n";
		}
		foreach (FileInfo f in dir.GetFiles())
		{
			result += "File : " + f.Name + "\n";
		}
		return (result);
	}

	private string pwd(List<string> args)
	{
		return (Directory.GetCurrentDirectory() + "\n");
	}

	private string whoami(List<string> args)
	{
		return (Environment.UserName + "\n");
	}

	private void ps()
	{
		Process[] mYProcs = Process.GetProcesses();

    Console.WriteLine("-----------------------------------------------------");
    Console.WriteLine("{0, -8} {1, -30} {2, -10}", "PID", "Process Name", "Status");
    Console.WriteLine("-----------------------------------------------------");

    foreach (Process p in mYProcs)
    {
      try
      {
        Console.WriteLine("{0, -8} {1, -30} {2, -10}", p.Id, p.ProcessName, p.Responding ? "Running" : "IDLE");
      }
      catch (Exception)
      {
        continue;
      }
    }
	}
}
