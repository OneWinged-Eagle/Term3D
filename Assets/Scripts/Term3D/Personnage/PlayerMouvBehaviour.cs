using UnityEngine;
using System.Collections;

public class PlayerMouvBehaviour : Bolt.EntityBehaviour<IRobotState> {

	private CharacterController m_characterController;
	private Camera m_camera;
	private Vector3 m_originalCameraPosition;


	public override void Attached ()
	{
		state.Transform.SetTransforms (transform);

		m_characterController = GetComponent<CharacterController> ();
		m_camera = Camera.main;
		m_originalCameraPosition = m_camera.transform.localPosition;

		base.Attached ();
	}


	public override void SimulateOwner ()
	{


		base.SimulateOwner ();
	}

}