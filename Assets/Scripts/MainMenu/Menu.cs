using UnityEngine;
using System.Collections;

public class Menu : Bolt.GlobalEventListener
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));

        if (GUILayout.Button("Start Server", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
        {
            // START SERVER
            BoltLauncher.StartServer(UdpKit.UdpEndPoint.Parse("127.0.0.1:27000"));
        }

        if (GUILayout.Button("Start Client", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
        {
            // START CLIENT
            BoltLauncher.StartClient();
        }

        GUILayout.EndArea();
    }

  /*  public override void BoltStartDone()
    {
        if (BoltNetwork.isServer)
        //    BoltNetwork.LoadScene("Tutorial1");
			BoltNetwork.LoadScene("Term3D");
        else BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse("127.0.0.1:27000"));

    }
*/
}
