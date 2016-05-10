using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TextInput : MonoBehaviour
{
	private InputField input;
	private InputField.SubmitEvent se;
	public Text output;
	private CommandHandler commandHandler;

	void Start()
	{
		this.commandHandler = new CommandHandler();

		this.input = gameObject.GetComponent<InputField>();
		this.se = new InputField.SubmitEvent();
		this.se.AddListener(SubmitInput);
		this.input.onEndEdit = this.se;
	}

	private void SubmitInput(string arg)
	{
		string currentText = this.output.text;
		string newText;

		List<string> cmdline = new List<string>(arg.Split(' '));

		if (cmdline[0] == "clear")
			newText = "";
		else
			newText = String.IsNullOrEmpty(arg) ? currentText : currentText + "\n" + this.commandHandler.callFunction(cmdline);

		this.output.text = newText;
		this.input.text = "";
		this.input.ActivateInputField();
	}
};
