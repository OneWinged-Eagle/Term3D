using System.Collections;

﻿using UnityEngine;

///<summary>
///Handle client connections
///</summary>
[BoltGlobalBehaviour(BoltNetworkModes.Host, "Term3D")]
public class ServerCallback : Bolt.GlobalEventListener // TODO: gérer les comms ou les virer
{
  public override void Connected(BoltConnection connection)
  {
		OnConnection(connection);
  	//PlayerObjectRegistry.CreateClientPlayer(connection);
  }

  public override void Disconnected(BoltConnection connection)
  {
		OnDisconnect(connection);
  }

	public override void SceneLoadLocalDone(string map)
	{
	  //PlayerObjectRegistry.serverPlayer.Spawn();
	}

	public override void SceneLoadRemoteDone(BoltConnection connection)
	{
	  //PlayerObjectRegistry.GetPlayer(connection).Spawn();
	}

	private void OnConnection(BoltConnection connection)
	{
		var log = EventLog.Create();
		log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
		log.Send();
		connection.SetStreamBandwidth(4092 * 20); // 80kb/s
	}

	private void Awake()
	{
	  //PlayerObjectRegistry.CreateServerPlayer();
	}

	private void OnDisconnect(BoltConnection connection)
	{
		var log = EventLog.Create();
		log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
		log.Send();
	}
}
