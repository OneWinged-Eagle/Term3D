using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Host, "Term3D")] //change to .Server if there is a bug
public class ServerCallback : Bolt.GlobalEventListener
{
	void Awake()
	{
		PlayerObjectRegistry.CreateServerPlayer ();
	}

    public override void Connected(BoltConnection connection)
    {
		onConnection (connection);
		PlayerObjectRegistry.CreateClientPlayer (connection);
    }

    public override void Disconnected(BoltConnection connection)
    {
		onDisconnect (connection);
    }

	public override void SceneLoadLocalDone(string map)
	{
		PlayerObjectRegistry.serverPlayer.Spawn ();
	}

	public override void SceneLoadRemoteDone(BoltConnection connection)
	{
		PlayerObjectRegistry.GetPlayer (connection).Spawn ();
	}



	public void onConnection(BoltConnection connection)
	{
		var log = EventLog.Create();
		log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
		log.Send();
	}

	public void onDisconnect(BoltConnection connection)
	{
		var log = EventLog.Create();
		log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
		log.Send();
	}

}

