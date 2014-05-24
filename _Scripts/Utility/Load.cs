using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour 
{
	public GameObject guiRoot;

	public bool useDebugData = true;

	public int debug_leveSize = 32;
	public int debug_levelDensity = 5;
	public int debug_difficulty = 3;
	public int debug_characters = 2;


	void Start () 
	{
		//Do necessary stuff to set up level: import save data, etc

		GameObject map = new GameObject();
		map.AddComponent<Map>();
		map.name = "MAP";

		if (useDebugData) map.GetComponent<Map>().Constructor(debug_leveSize,debug_levelDensity,debug_difficulty,debug_characters);

		//set up the game camera and link the gui camera
		GameObject cameraHolder = new GameObject();
		cameraHolder.name = "CAMERA";
		cameraHolder.transform.position = new Vector3 (0,20,0);
		cameraHolder.transform.eulerAngles = new Vector3 (0,45,0);
		cameraHolder.AddComponent<GameCamera>();
		cameraHolder.GetComponent<GameCamera>().Initialize(cameraHolder);
	
		//add input script to this object
		this.gameObject.AddComponent<Inputs>();
		this.gameObject.GetComponent<Inputs>().Initialize(cameraHolder,guiRoot.GetComponent<Camera>());
	
	}
}
