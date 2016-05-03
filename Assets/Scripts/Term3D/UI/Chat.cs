using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class Chat : Bolt.GlobalEventListener {

	public InputField ip;
	public Text content;
	public Scrollbar scrollBar;


	List<string> logChat;

	private string display = "";

	bool addText = false;



	// Use this for initialization
	void Start () {
		logChat = new List<string> ();
	}
	
	// Update is called once per frame
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
		var chatLogEvent = messageEvent.Create ();
		chatLogEvent.message = ip.text;
		chatLogEvent.Send ();
		/*logChat.Add(ip.text);
		ip.text = "";
		addText = true;*/
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
