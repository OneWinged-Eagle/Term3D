using System.Collections;
﻿using UnityEngine;

[BoltGlobalBehaviour]
public class ChangeColor : Bolt.EntityBehaviour<ICubeRougeState>
{
	public override void Attached()
	{
		state.CubeColor = new Color(Random.value, Random.value, Random.value);
		state.AddCallback("CubeColor", colorChanged);
	}

	private void colorChanged()
	{
		GetComponent<Renderer>().material.color = state.CubeColor;
	}
}
