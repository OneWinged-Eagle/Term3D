using System.Collections;

﻿using UnityEngine;
using Bolt;

///<summary>
///Ask item control to host
///</summary>
[BoltGlobalBehaviour(BoltNetworkModes.Client, "Term3D")]
public class AskControlToHost : Bolt.GlobalEventListener // TODO: à retravailler 100%
{
	


	public void AskControl(NetworkId id)
	{
		var test = askControl.Create ();
		test.networkIdPlayer = id;
		test.networkIdObj = GetComponent<BoltEntity> ().networkId;
		test.Send ();
	}

	public override void OnEvent(askControl e)
	{
		Debug.Log ("e.Entity " + e.entity);
		Debug.Log ("e.prefabid " + e.prefabId);
		Debug.Log ("e.protocoltoken " + e.protocolToken);
		Debug.Log ("e.networkid" + e.networkIdPlayer);
		Debug.Log ("e.networkid" + e.networkIdObj);

		BoltEntity entity = BoltNetwork.FindEntity (e.networkIdPlayer);
		Debug.Log (	entity.GetState<IPlayerState>());

		//Transform test = entity.GetState<IPlayerState> ().Transform;
		// Debug.Log (test);

	}
}
