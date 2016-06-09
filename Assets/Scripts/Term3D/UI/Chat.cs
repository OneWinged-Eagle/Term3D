using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class Chat : Bolt.GlobalEventListener {

	public InputField ip;
	public Text content;
	public Scrollbar scrollBar;



	//chat entre clients ou non
	public bool reseau = false;


	List<string> logChat;

	private string display = "";

	bool addText = false;



	void Start () {
		logChat = new List<string> ();
	}
	
	void Update () {
		if (addText) {
			displayMsg ();
		}

	}

	void displayMsg()
	{
		display = "";
		foreach (string msg in logChat) {
			display = display.ToString () + msg.ToString () + "\n";
		}
		content.text = display;
		display = "";
		addText = false;
	}

	public void sendMsg()
	{

		if (reseau == true) {
			var chatLogEvent = messageEvent.Create ();
			chatLogEvent.message = ip.text;
			chatLogEvent.Send ();
		} else {
		scrollBar.value = 0.0f;
		logChat.Add(ip.text);
		addText = true;
		}
			ip.text = "";
	}

	 
	public override void OnEvent(messageEvent evnt)
	{
		Debug.Log (evnt.message);
		scrollBar.value = 0.0f;
		logChat.Add (evnt.message);
		addText = true;
		//base.OnEvent (evnt); // késako ?
	}
}
