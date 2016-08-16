using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour]
public class AskControlToHost : Bolt.GlobalEventListener {

	public void AskControl()
	{
		var test = askControl.Create ();

		//test.protocolToken = null;
		//test.networkId = null;



		test.Send ();

	}

	public override void OnEvent(askControl e)
	{

		foreach (var entity in BoltNetwork.entities) {
			foreach (var connection in BoltNetwork.connections) {
				entity.AssignControl (connection);
				Debug.Log (entity);
				Debug.Log (connection);
			}
		}


		Debug.Log (e.prefabId);
		Debug.Log (e.protocolToken);
		Debug.Log (e.networkId);
	}

}
