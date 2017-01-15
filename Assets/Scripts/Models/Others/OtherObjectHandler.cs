using UnityEngine;
using System.Collections;
using Bolt;

public class OtherObjectHandler : Bolt.EntityBehaviour<IOtherObjectState> {
	 public BoltEntity objEntity;
	 public  BoltEntity playerEntity;
	  Transform objPos;
	public bool objControl;
	public Vector3 tmpPos;
	 public bool checkGrav = false;

	public override void SimulateOwner()
	{

		if (BoltNetwork.isServer) {
			if (objControl) {
				//gameObject.GetComponent<Rigidbody> ().useGravity = false;

				if (checkGrav) {
					gameObject.GetComponent<Rigidbody> ().useGravity = false;
					checkGrav = false;
				}
				/*tmpPos = playerEntity.GetState<IPlayerState> ().HookTransform.Position;
				tmpPos.z = playerEntity.GetState<IPlayerState> ().Transform.Position.z + 2;
				objEntity.transform.position = tmpPos;
				objEntity.transform.rotation = playerEntity.GetState<IPlayerState> ().Transform.Rotation;*/
				objEntity.transform.position = playerEntity.GetState<IPlayerState> ().HookTransform.Position;
				objEntity.transform.rotation = playerEntity.GetState<IPlayerState> ().HookTransform.Rotation;
				//transform.position = tmpPos;
				//transform.rotation = playerEntity.GetState<IPlayerState> ().Transform.Rotation;
				//Debug.Log ("ouocuocucououuouazoeuzaoeuoazeuzao   " + playerEntity.GetState<IPlayerState> ().Transform.Position);
			} else {
				if (!checkGrav) {
					gameObject.GetComponent<Rigidbody> ().useGravity = true;
					checkGrav = true;
				}
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
		checkGrav = haveControl;

	}
}
