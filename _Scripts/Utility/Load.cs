using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour 
{

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

		if (useDebugData)
			map.GetComponent<Map>().Constructor(debug_leveSize,debug_levelDensity,debug_difficulty,debug_characters);
	
	}
}
