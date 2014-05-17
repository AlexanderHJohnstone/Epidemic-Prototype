using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class LevelLoadBehavior : MonoBehaviour {

	public enum levelLoad
	{
		level_01,
		level_02,
		level_03,
	}

	public levelLoad levelSelection = levelLoad.level_01;

	public GameObject landscapeTile;
	
	void Start () 
	{
		Color[][] mapData = Read_Map_Image_File(levelSelection.ToString());

		if (mapData != null)
		{
			Debug.Log("Loaded "+levelSelection.ToString());
			Debug.Log("Map Size is "+mapData.Length);

			Draw_Map(mapData);
		}



	}
	
	void Update () 
	{
	
	}

	 

	public Color[][] Read_Map_Image_File (string levelName)
	{

		UnityEngine.Object mapFile = Resources.Load("MapFiles/"+ levelName);

		try 
		{
			Texture2D mapImage = mapFile as Texture2D;
			Color[] mapArray = mapImage.GetPixels();
			Color [][] mapData = null;


			for ( int x = 0; x < mapImage.width; x++)
			{
				for ( int y = 0; y < mapImage.height; y++)
				{
				
				}
			}



			return mapData;
		}

		catch (InvalidCastException e)
		{
			Debug.Log("Map file isn't an image yo!");
			return null;
		}

		catch (UnityException e)
		{
			Debug.Log("Map file isn't readable");
			return null;
		}
	}

	private void Draw_Map (Color[][] mapData)
	{

	}
}
