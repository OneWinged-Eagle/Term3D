using System.Collections;

﻿using UnityEngine;
using Bolt;

///<summary>
///Ask item control to host
///</summary>
[BoltGlobalBehaviour(BoltNetworkModes.Client, "Term3D")]
public class AskControlToHost : Bolt.GlobalEventListener // TODO: à retravailler 100%
{

	public GameObject otherObj;

	public void AskControl(NetworkId id)
	{
		var controlObjEvent = askControl.Create ();
		controlObjEvent.networkIdPlayer = id;
		controlObjEvent.networkIdObj = GetComponent<BoltEntity> ().networkId;
		controlObjEvent.haveControl = true;
		gameObject.GetComponent<Rigidbody>().useGravity = false;
		controlObjEvent.Send ();
	}

	public void giveUpControl(NetworkId id)
	{
		var controlObjEvent = askControl.Create ();
		controlObjEvent.networkIdPlayer = id;
		controlObjEvent.networkIdObj = GetComponent<BoltEntity> ().networkId;
		controlObjEvent.haveControl = false;
		controlObjEvent.Send ();
	}

	public override void OnEvent(askControl e)
	{
		gameObject.GetComponent<OtherObjectHandler> ().controlObjEvent (e.networkIdObj, e.networkIdPlayer, e.haveControl);
	}
}
