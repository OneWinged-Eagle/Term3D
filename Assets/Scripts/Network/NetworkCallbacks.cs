using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    List<string> logMessages = new List<string>();

 
    public override void OnEvent(EventLog evnt)
    {
        logMessages.Insert(0, evnt.Message);
        base.OnEvent(evnt); // késako ?
    }



	//a decommenter pour afficher le debug de bolt
    void OnGUI()
    {
        int maxMsg = Mathf.Min(5, logMessages.Count);

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);
        for (int i = 0; i < maxMsg; i++)
            GUILayout.Label(logMessages[i]);
        GUILayout.EndArea();
    }
		
}
