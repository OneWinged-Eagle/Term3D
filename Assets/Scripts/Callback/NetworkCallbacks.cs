using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
  private List<string> _logMsgs = new List<string>();

  public override void OnEvent(EventLog e)
  {
    _logMsgs.Insert(0, e.Message);
    base.OnEvent(e); // TODO: utile ?
  }

  private void OnGUI()
  {
    int maxMsgs = Mathf.Min(5, _logMsgs.Count);

    GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);
    for (int i = 0; i < maxMsgs; i++)
      GUILayout.Label(_logMsgs[i]);
    GUILayout.EndArea();
  }
}
