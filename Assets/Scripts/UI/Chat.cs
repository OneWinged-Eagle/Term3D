using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

///<summary>
///Chat and commands
///</summary>
[BoltGlobalBehaviour()]
public class Chat : Bolt.GlobalEventListener // TODO: à retaper
{
	public InputField _ip; // TODO: variable utilisée uniquement dans sendMsg : à passer en variable locale ?
	public Text _content; // TODO: variable utilisée uniquement dans displayMsg : à passer en variable locale ?
	public Scrollbar _scrollBar;
	// TODO: toutes les variables ci-dessus sont publiques, normal ? Peut-être à passer en properties ?
	public CommandHandler _commandHandler;

	//chat entre clients ou non
	public bool _reseau = false; // TODO: variable utilisée uniquement dans sendMsg : à passer en variable locale ?
	// TODO: pourquoi en public ? Property ?

	private List<string> _logChat;

	//private string display = "";

	private bool _addText = false;

	private void Start()
	{
		_commandHandler = new CommandHandler();
		_logChat = new List<string>();
	}

	private void Update()
	{
		if (_addText)
			displayMsg();
	}

	void displayMsg()
	{
		string display = String.Empty;

		foreach (string msg in _logChat)
			display += msg + "\n";

		string cmd = _logChat[_logChat.Count - 1];
		if (!String.IsNullOrEmpty(cmd) && cmd[0] == '/')
		{
			string result = _commandHandler.CallFunction(new List<string>(cmd.Split(' ')));

			display += result;
			_logChat.Add(result);
		}

		/*List<string> cmdline = new List<string>(lastCommand.Split(' '));
		result = _commandHandler.CallFunction(cmdline);
		display += result;

		if (lastCommand == "clear")
			display = result;
		else
		{
			result = _commandHandler.CallFunction(cmdline);
			display += result;
		}
		_logChat.Add(result);*/

		_content.text = display;
		_addText = false;
	}

	public void sendMsg()
	{
		if (_reseau == true)
		{
			var chatLogEvent = messageEvent.Create();
			chatLogEvent.message = _ip.text;
			chatLogEvent.Send();
		}
		else
		{
			_scrollBar.value = 0.0f;
			_logChat.Add(_ip.text);
			_addText = true;
		}
		_ip.text = "";
	}

	public override void OnEvent(messageEvent e)
	{
		Debug.Log(e.message);
		_scrollBar.value = 0.0f;
		_logChat.Add(e.message);
		_addText = true;
		base.OnEvent(e); // TODO: utile ?
	}
}
