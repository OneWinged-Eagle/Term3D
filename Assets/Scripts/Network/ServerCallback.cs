using System.Collections;
﻿using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Host)] // change to .Server if there is a bug // TODO: comm' à enlever ?
public class ServerCallback : Bolt.GlobalEventListener
{
  public override void Connected(BoltConnection connection)
  {
    var log = EventLog.Create(); // TODO: "var" à changer svp

    log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
    log.Send();
  }

  public override void Disconnected(BoltConnection connection)
  {
    var log = EventLog.Create(); // TODO: "var" à changer svp

    log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
    log.Send();
  }
}
