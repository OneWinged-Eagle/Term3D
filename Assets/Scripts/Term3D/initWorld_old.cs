using UnityEngine;
using System.Collections;

public class initWorld_old : MonoBehaviour {
	public GameObject[] objets;



	public Vector3[] posObjets;

	// Use this for initialization
	void Start () {
		worldCreation ();


	}


	void worldCreation()
	{
		initTerrain ();
		initObjects ();
	}

	void initTerrain()
	{
		GameObject terrainObj = new GameObject ("terrainObj");
		TerrainData _TerrainData = new TerrainData ();

		_TerrainData.size = new Vector3 (10, 600, 10);
		_TerrainData.heightmapResolution = 512;
		_TerrainData.baseMapResolution = 1024;
		_TerrainData.SetDetailResolution (1024, 14);

		int _heightmapwidth = _TerrainData.heightmapWidth;
		int _heightmapheight = _TerrainData.heightmapHeight;

		TerrainCollider _TerrainCollider = terrainObj.AddComponent<TerrainCollider> ();
		Terrain _Terrain2 = terrainObj.AddComponent<Terrain> ();

		_TerrainCollider.terrainData = _TerrainData;
		_Terrain2.terrainData = _TerrainData;
	}

	void initObjects()
	{
		for (int i = 0; objets[i]; i++) {
			Instantiate (objets [i], posObjets[i], Quaternion.identity);
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
