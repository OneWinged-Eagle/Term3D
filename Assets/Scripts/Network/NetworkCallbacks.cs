using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
  private List<string> _logMsgs = new List<string>();

  public override void OnEvent(EventLog evnt)
  {
    _logMsgs.Insert(0, evnt.Message);
    base.OnEvent(evnt);
  }

  private void onGUI()
  {
    int maxMsg = Mathf.Min(5, _logMsgs.Count);

    GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);
    for (int i = 0; i < maxMsg; i++)
      GUILayout.Label(_logMsgs[i]);
    GUILayout.EndArea();
  }
}
