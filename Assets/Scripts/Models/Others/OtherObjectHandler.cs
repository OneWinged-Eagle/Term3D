using UnityEngine;
using System.Collections;
using Bolt;

[System.Serializable]
public class OtherObjectHandler : Bolt.EntityBehaviour<IOtherObjectState> {
	static public BoltEntity objEntity;
	static public  BoltEntity playerEntity;
	public  Transform objPos;
	static public bool objControl;
	public Vector3 tmpPos;

	public override void Attached()
	{
	}

	public override void SimulateOwner()
	{

		if (BoltNetwork.isServer) {
			if (objControl) {
				//gameObject.GetComponent<Rigidbody>().useGravity = false;
				tmpPos = playerEntity.GetState<IPlayerState> ().Transform.Position;
				tmpPos.z = playerEntity.GetState<IPlayerState> ().Transform.Position.z + 2;
				objEntity.transform.position = tmpPos;
				objEntity.transform.rotation = playerEntity.GetState<IPlayerState> ().Transform.Rotation;
				//transform.position = tmpPos;
				//transform.rotation = playerEntity.GetState<IPlayerState> ().Transform.Rotation;
				Debug.Log ("ouocuocucououuouazoeuzaoeuoazeuzao   " + playerEntity.GetState<IPlayerState> ().Transform.Position);
			}
		}
		if (BoltNetwork.isClient) {
			Debug.Log ("je suis un client");
		}
	}

	public void controlObjEvent(NetworkId objNetworkId, NetworkId playerId, bool haveControl)
	{
		objEntity = BoltNetwork.FindEntity (objNetworkId);
		playerEntity = BoltNetwork.FindEntity (playerId);
		objControl = haveControl;

		Debug.Log("coucou" + playerEntity.GetState<IPlayerState>().Transform);
		Debug.Log("coucou" + playerEntity.GetState<IPlayerState>().Transform.Position);
	}
}
