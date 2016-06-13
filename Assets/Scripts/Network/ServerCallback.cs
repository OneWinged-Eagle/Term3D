using UnityEngine;
using System.Collections;
using System.Text;
using UdpKit;
using Bolt;

[BoltGlobalBehaviour(BoltNetworkModes.Host)] //change to .Server if there is a bug
public class ServerCallback : Bolt.GlobalEventListener
{
    public override void Connected(BoltConnection connection)
    {
        var log = EventLog.Create();
        log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
        log.Send();
        connection.SetStreamBandwidth(1024 * 20);
    }

    public override void Disconnected(BoltConnection connection)
    {
        var log = EventLog.Create();
        log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
        log.Send();
    }
}

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
            byte[] data = Encoding.ASCII.GetBytes("Testing");
            c.StreamBytes(testChannel, data);
        }
    }

    public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
    {
        BoltLog.Info(data);
    }
}