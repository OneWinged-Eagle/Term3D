using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour]
public class changeColor : Bolt.EntityBehaviour<ICubeRougeState>
{
	public override void Attached() {
		state.CubeColor = new Color(Random.value, Random.value, Random.value);
		state.AddCallback("CubeColor", ColorChanged);
	}

	void ColorChanged() {
		GetComponent<Renderer>().material.color = state.CubeColor;
	}
}
