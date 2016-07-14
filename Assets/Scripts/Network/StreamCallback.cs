using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
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
            /*
            string text;
            var fileStream = new FileStream(@"c:\testin.mp3", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            */
            byte[] data = File.ReadAllBytes(@"c:\testin.mp3");
            BoltLog.Info("DATA TEST" + data.ToString() + " " + c);
            c.StreamBytes(testChannel, data);
        }
    }

    public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
    {
        BoltLog.Info("TEST CLIENT");
        BoltLog.Info("DATA SENT: "/*+ System.Text.Encoding.UTF8.GetString(data.Data)*/);
        System.IO.File.WriteAllBytes(@"c:\testout.mp3", data.Data);
        //var sound = new GameObject();
        //AudioCube aud = sound.GetComponent<AudioCube>();
        //aud.setClipByUrl(url);
    } 
}
