using UnityEngine;
using System.Collections;
using Bolt;

[System.Serializable]
public class OtherObjectHandler : Bolt.EntityBehaviour<IOtherObjectState> {

	public override void Attached()
	{
	}

	public override void SimulateOwner()
	{
	}

	public void test(NetworkId objNetworkId, BoltEntity entity)
	{
	}
}
