using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Host)] //change to .Server if there is a bug
public class ServerCallback : Bolt.GlobalEventListener
{
    public override void Connected(BoltConnection connection)
    {
        var log = EventLog.Create();
        log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
        log.Send();
    }

    public override void Disconnected(BoltConnection connection)
    {
        var log = EventLog.Create();
        log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
        log.Send();
    }
}

