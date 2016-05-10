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
		this.commands.Add("whoami", new Func<List<string>, string>(whoami));
		this.commands.Add("cat", new Func<List<string>, string>(cat));
		this.commands.Add("ls", new Func<List<string>, string>(ls));
//		this.commands.Add("ps", new Func<string>(ps));
	}

	public string callFunction(List<string> cmdline)
	{
		string command = cmdline[0];
		Delegate del = this.commands[command];
		cmdline.RemoveAt(0);
		return (del.DynamicInvoke(cmdline).ToString());
	}

	private string cat(List<string> args)
	{
		if (args.Count != 1)
		{
				return ("The syntax of the command is incorrect.");
		}
		else
		{
			if (File.Exists(args[0]) == false)
			{
					return ("File not found");
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
		return (Directory.GetCurrentDirectory());
	}

	private string whoami(List<string> args)
	{
		return (Environment.UserName);
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
