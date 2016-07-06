using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[BoltGlobalBehaviour]
public class Chat : Bolt.GlobalEventListener
{
	public InputField _ip; // TODO: variable utilisée uniquement dans sendMsg : à passer en variable locale ?
	public Text _content; // TODO: variable utilisée uniquement dans displayMsg : à passer en variable locale ?
	public Scrollbar _scrollBar;
	// TODO: toutes les variables ci-dessus sont publiques, normal ? Peut-être à passer en properties ?
	private CommandHandler _commandHandler;

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
		string display = "";
		string lastCommand = "";
		string result = "";

		foreach (string msg in _logChat)
		{
			display += msg.ToString() + "\n";
			lastCommand = msg;
		}

		List<string> cmdline = new List<string>(lastCommand.Split(' '));
		result = _commandHandler.CallFunction(cmdline);
		display += result;

		if (lastCommand == "clear")
			display = result;
		else
		{
			result = _commandHandler.CallFunction(cmdline);
			display += result;
		}
		_logChat.Add(result);

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

	public override void OnEvent(messageEvent evnt)
	{
		Debug.Log(evnt.message);
		_scrollBar.value = 0.0f;
		_logChat.Add(evnt.message);
		_addText = true;
		//base.OnEvent(evnt); // késako ?
	}
}
