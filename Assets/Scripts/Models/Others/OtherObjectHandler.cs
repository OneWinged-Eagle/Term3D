using UnityEngine;
using System.Collections;
using Bolt;

[System.Serializable]
public class OtherObjectHandler : Bolt.EntityBehaviour<IOtherObjectState> {
	static public BoltEntity objEntity;
	static public  BoltEntity playerEntity;
	public  Transform objPos;
	static public bool check = false;

	public override void Attached()
	{
	}

	public override void SimulateOwner()
	{

		Debug.Log ("check  =====" + check);
		if (BoltNetwork.isServer) {
			Debug.Log ("je sui sun serveur");
			if (playerEntity != null) {
				transform.position = playerEntity.GetState<IPlayerState> ().Transform.Position;
				transform.rotation = playerEntity.GetState<IPlayerState> ().Transform.Rotation;
				Debug.Log ("ouocuocucououuouazoeuzaoeuoazeuzao   " + playerEntity.GetState<IPlayerState> ().Transform.Position);
			}
		}
		if (BoltNetwork.isClient) {
			Debug.Log ("je suis un client");
		}
	}

	public void test(NetworkId objNetworkId, NetworkId playerId)
	{
		//toto.position;
		//toto.rotation;
		objEntity = BoltNetwork.FindEntity (objNetworkId);
		playerEntity = BoltNetwork.FindEntity (playerId);
		Debug.Log("coucou" + playerEntity.GetState<IPlayerState>().Transform);
		Debug.Log("coucou" + playerEntity.GetState<IPlayerState>().Transform.Position);
		check = true;
		//toto.position = playerEntity.GetState<IPlayerState> ().Transform.Position;

		//transform.position = toto.position;
		//objEntity.GetState<IOtherObjectState>().SetTransforms(toto);
	}
}
