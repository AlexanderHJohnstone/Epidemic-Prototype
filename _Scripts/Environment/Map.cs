using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class Map : MonoBehaviour {

	public enum levelLoad
	{
		level_01,
		level_02,
		level_03,
	}

	public levelLoad levelSelection = levelLoad.level_01;

	public GameObject landscapeTile;

	public TerrainData landscapeTerrain;

	private GameObject landscapeHolder;
	
	void Start () 
	{
		Color[][] mapData = Read_Map_Image_File(levelSelection.ToString());

		if (mapData != null)
		{
			Debug.Log("Loaded "+levelSelection.ToString());
			Debug.Log("Map Size is "+mapData.Length +" x "+mapData[0].Length);

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
			Color [][] mapData = new Color[mapImage.width][];

			for ( int x = 0; x < mapImage.width; x++)
			{
				mapData[x] = new Color[mapImage.height];

				for ( int y = 0; y < mapImage.height; y++)
				{
					mapData[x][y] = mapArray[x + (y * mapImage.width)];
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
		//create new terrain
		//GameObject mapTerrain = new Terrain();

		//mapTerrain.terrainData = landscapeTerrain;
		//mapTerrain.name = "Terrain";



		landscapeHolder = new GameObject();
		landscapeHolder.name = "Landscape Holder";




		for ( int x = 0; x < mapData.Length; x++)
		{
			for ( int y = 0; y < mapData[0].Length; y++)
			{
				GameObject mapTile = (GameObject)Instantiate(landscapeTile);
				landscapeTile.transform.position = new Vector3 (x,mapData[x][y].grayscale * 3,y);

				mapTile.transform.parent = landscapeHolder.transform;
			}
		}
	}
}
