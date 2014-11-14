using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour 
{
	public GameObject guiRoot;

	public bool useDebugData = true;

	public int debug_levelSizeX = 32;
	public int debug_levelSizeY = 48;
	public int debug_levelDensity = 5;
	public int debug_difficulty = 3;
	public int debug_characters = 2;




	void Awake () 
	{
		//Do necessary stuff to set up level: import save data, etc

		GameObject map = new GameObject();
		map.AddComponent<Map>();
		map.name = "MAP";

		map.GetComponent<Map>().Constructor(debug_levelSizeX, debug_levelSizeY, debug_levelDensity,debug_difficulty,debug_characters);
		Vector2 spawnPos = map.GetComponent<Map>().Get_Start_Pos();

		//set up the game camera and link the gui camera
		GameObject cameraHolder = new GameObject();
		cameraHolder.name = "CAMERA";
		cameraHolder.transform.position = new Vector3 (0,20,0);
		cameraHolder.transform.eulerAngles = new Vector3 (0,45,0);
		cameraHolder.AddComponent<GameCamera>();
		cameraHolder.GetComponent<GameCamera>().Initialize(cameraHolder, debug_levelSizeX, debug_levelSizeY, spawnPos);

	
		//add input script to this object
		this.gameObject.AddComponent<TacInput>();
		this.gameObject.GetComponent<TacInput>().Initialize(cameraHolder,guiRoot.GetComponent<Camera>());
	
	}
}
