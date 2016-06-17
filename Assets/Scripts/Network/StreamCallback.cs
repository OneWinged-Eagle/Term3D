using UnityEngine;
using System.Collections;
using System.Text;
using UdpKit;
using Bolt;

[BoltGlobalBehaviour()]
public class StreamCallbacks : Bolt.GlobalEventListener
{
    public static UdpKit.UdpChannelName testChannel;
    public override void BoltStartBegin()
    {
        testChannel = BoltNetwork.CreateStreamChannel("test", UdpKit.UdpChannelMode.Reliable, 1);
    }

    public override void SceneLoadRemoteDone(BoltConnection c)
    {
        if (BoltNetwork.isServer)
        {
            byte[] data = Encoding.ASCII.GetBytes("http://williamnayrole.fr/test.ogg");
            BoltLog.Info("DATA TEST" + data.ToString() + " " + c);
            c.StreamBytes(testChannel, data);
        }
    }

    public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
    {
        BoltLog.Info("TEST CLIENT");
        BoltLog.Info("DATA SENT: "+ System.Text.Encoding.UTF8.GetString(data.Data));
        string url = System.Text.Encoding.UTF8.GetString(data.Data);
        var sound = new GameObject();
        AudioCube aud = sound.GetComponent<AudioCube>();
        //aud.setClipByUrl(url);
    } 
}
