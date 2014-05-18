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

	public GameObject tree;

	public float terrainHeight;
	
	void Start () 
	{
		Color[][] mapData = Read_Map_Image_File(levelSelection.ToString());

		if (mapData != null)
		{
			Debug.Log("Loaded "+levelSelection.ToString());
			Debug.Log("Map Size is "+mapData.Length +" x "+mapData[0].Length);


			Extract_Level_Data (mapData);

			Create_Landscape(mapData);
		}
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

	private void Extract_Level_Data (Color[][] mapData)
	{

	}


	private void Create_Landscape (Color[][] mapData)
	{
		int terrainScalar = (int)512/mapData.Length;

		float[,] heights = new float[mapData.Length*terrainScalar,mapData[0].Length*terrainScalar];

		landscapeHolder = new GameObject();
		landscapeHolder.name = "Landscape Holder";

		for ( int x = 0; x < mapData.Length; x++)
		{
			for ( int y = 0; y < mapData[0].Length; y++)
			{
				Color pixelColor = mapData [x][y];

				bool tileOpen = true;

				if ( pixelColor.r < 0.05f && pixelColor.g > 0.95f && pixelColor.b < 0.05f )
				{
					GameObject treeObject = (GameObject)Instantiate(tree);
					
					treeObject.transform.position = new Vector3 (x + 0.5f,mapData[y][x].grayscale * terrainHeight,y - 0.5f);
					
					//smooth map
					mapData[x][y] = (mapData[x+1][y] + mapData[x-1][y] + mapData[x][y+1] + mapData[x][y-1])/4;

					tileOpen = false;
				}

				GameObject mapTile = (GameObject)Instantiate(landscapeTile);
				landscapeTile.transform.position = new Vector3 (x + 0.5f,(mapData[y][x].grayscale * terrainHeight)+0.01f,y + 0.5f);

				mapTile.transform.parent = landscapeHolder.transform;

				mapTile.AddComponent<Tile>();
				mapTile.GetComponent<Tile>().Constructor(new Vector3 (x,mapData[y][x].grayscale * terrainHeight,y),tileOpen,landscapeType.plane);
			}
		}



		for (int h1 = 0 ; h1 < mapData.Length*terrainScalar; h1++)
		{
			for (int h2 = 0 ; h2 < mapData[0].Length*terrainScalar; h2++)
			{
				int xPos = (int)h1/terrainScalar;
				int yPos = (int)h2/terrainScalar;

				float xOffset = (h1 - (xPos*terrainScalar));
				float yOffset = (h2 - (yPos*terrainScalar));

				xOffset = xOffset/terrainScalar;
				yOffset = yOffset/terrainScalar;

				float value = mapData[xPos][yPos].grayscale;

				if (xPos > 0 && xPos < mapData.Length-1 && yPos > 0 && yPos < mapData[0].Length-1)
				{
					float value1 = (((mapData[xPos+1][yPos].grayscale - mapData[xPos-1][yPos].grayscale)*xOffset)+mapData[xPos][yPos].grayscale);
					float value2 = (((mapData[xPos][yPos+1].grayscale - mapData[xPos][yPos-1].grayscale)*yOffset)+mapData[xPos][yPos].grayscale);


					value = (value1 + value2)/2;

					
					value = (((heights[h1+1,h2] + heights[h1,h2+1] + heights[h1-1,h2] + heights[h1,h2-1])/4)+value)/2;
				}
				
				
				heights[h1,h2] = value * (terrainHeight/10);
			}
			
		}

		landscapeTerrain.SetHeights(1,1,heights);
				
		GameObject mapTerrain = Terrain.CreateTerrainGameObject(landscapeTerrain);
	}
}
