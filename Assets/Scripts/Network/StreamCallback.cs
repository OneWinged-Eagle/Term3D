﻿using UnityEngine;
using System.Collections;
using System.Text;
using UdpKit;
using Bolt;

[BoltGlobalBehaviour()]
public class StreamCallbacks : Bolt.GlobalEventListener
{
    public static UdpKit.UdpChannelName testChannel;
   // public AudioSource stingSource;

    public override void BoltStartBegin()
    {
        testChannel = BoltNetwork.CreateStreamChannel("test", UdpKit.UdpChannelMode.Reliable, 1);
    }

    public override void SceneLoadRemoteDone(BoltConnection c)
    {
        if (BoltNetwork.isServer)
        {
            byte[] data = Encoding.ASCII.GetBytes("Testing");
            BoltLog.Info("DATA TEST"+data.ToString());
            c.StreamBytes(testChannel, data);
        }
    }

    public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
    {
        BoltLog.Info(data);
    }
}