using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour 
{
	//class map storage
	private GameObject[,] tiles;
	private List<Unit> enemies;
	private List<Unit> units;

	public void Constructor ( int levelSize, int landscapeDensity, int difficulty, int characters )
	{
		Draw_Map (levelSize);
		Draw_Landscape (landscapeDensity, levelSize);
		Spawn_Characters (characters, levelSize);
		Spawn_Enemies (difficulty, levelSize);
	}

	/// <summary>
	/// Instantiates and places map tiles, as well as temporary 'ground' texture
	/// </summary>
	private void Draw_Map ( int size )
	{
		Debug.Log ("Draw Map with size: " + size);

		GameObject tileHolder = new GameObject();
		tileHolder.name = "TILES";

		GameObject tile = (GameObject)Resources.Load ("Landscape/Navigation/tempTile");
		
		tiles = new GameObject[size,size];

		for (int i = 0; i < size; i++ )
		{
			for (int j = 0; j < size; j++ )
			{
				tiles[i,j] = (GameObject)Instantiate(tile);
				tiles[i,j].transform.position = new Vector3 (i,0,j);
				tiles[i,j].AddComponent<Tile>();
				tiles[i,j].GetComponent<Tile>().Constructor(new Vector3(i,0,j),true,landscapeType.open);
				tiles[i,j].name = i+","+j;
				tiles[i,j].transform.parent = tileHolder.transform;
			}
		}
	}

	/// <summary>
	/// Instantiates random set of terrain objects from 'terrain/features' folder, adds them to map
	/// </summary>
	/// <param name="density">Density.</param>
	private void Draw_Landscape ( int density, int size )
	{
		Debug.Log ("Draw Landscape with density: " + density);

		Object[] landscapeFeatures = Resources.LoadAll("Landscape/Features/");

		GameObject featuresHolder = new GameObject();
		featuresHolder.name = "FEATURES";

		for (int i = 0; i < size; i++ )
		{
			for (int j = 0; j < size; j++ )
			{
				if ((Random.Range(0,100)) < density)
				{
					GameObject feature = (GameObject)Instantiate(landscapeFeatures[(int)Random.Range(0,landscapeFeatures.Length-0.1f)]);
					feature.transform.parent = featuresHolder.transform;
					feature.transform.position = new Vector3 (i,0,j);
					tiles[i,j].GetComponent<Tile>().Set_Open(false);
				}
			}
		}
	}

	/// <summary>
	/// Spawn Characters on the map
	/// </summary>
	private void Spawn_Characters ( int characters, int levelSize )
	{
		Debug.Log ("Spawned " + characters + " characters");

		units = new List<Unit>();

		//find a spawn location that's free from landscape features
		bool legalSpawn = false;

		while (legalSpawn == false)
		{
			int centerX = (int)Random.Range(3, levelSize-3);
			int centerY = (int)Random.Range(3, levelSize-3);

			if (tiles[centerX,centerY].GetComponent<Tile>().Get_Open())
			{
				legalSpawn = true;

				int charactersPlaced = 0;
				
				while ( charactersPlaced < characters )
				{
					int spawnX = centerX + ((int)Random.Range(-2.9f,2.9f));
					int spawnY = centerY + ((int)Random.Range(-2.9f,2.9f));

					if ( tiles[spawnX,spawnY].GetComponent<Tile>().Get_Open() )
					{
						GameObject character = (GameObject)Instantiate((GameObject)Resources.Load ("Models/tempUnit"));
						character.name = "CHAR_"+charactersPlaced;

						character.transform.position = new Vector3 (spawnX,0,spawnY);

						character.AddComponent<Unit>();
						character.GetComponent<Unit>().InitializeOnMap(this,tiles[spawnX,spawnY].GetComponent<Tile>());

						units.Add(character.GetComponent<Unit>());

						charactersPlaced++;
					}
				}
			}
		}
	}
	
	/// <summary>
	/// Spawns enemies on the map
	/// </summary>
	private void Spawn_Enemies ( int difficulty, int levelSize )
	{
		Debug.Log ("Spawned " + difficulty + " monsters");

		enemies = new List<Unit>();

		for ( int i = 0; i < difficulty ; i++ )
		{
			bool legalSpawn = false;

			while ( legalSpawn == false )
			{
				int spawnX = (int)Random.Range(3, levelSize-3);
				int spawnY = (int)Random.Range(3, levelSize-3);

				if ( tiles[spawnX,spawnY].GetComponent<Tile>().Get_Open() )
				{
					GameObject enemy = (GameObject)Instantiate((GameObject)Resources.Load ("Models/tempEnemy"));
					enemy.name = "ENEMY_"+i;

					enemy.transform.position = new Vector3 (spawnX,0,spawnY);

					enemy.AddComponent<Unit>();
					enemy.GetComponent<Unit>().InitializeOnMap(this,tiles[spawnX,spawnY].GetComponent<Tile>());

					enemies.Add(enemy.GetComponent<Unit>());
					
					legalSpawn = true;
				}
			}
		}
	}

	/// <summary>
	/// Returns the tile at given location
	/// </summary>
	public Tile Get_Tile (int X, int Y)
	{
		try { return tiles [X,Y].GetComponent<Tile>(); }

		catch (UnityException e)
		{
			Debug.Log ("Get Tile could not return a tile as index is out of range. Returning null.");
			return null;
		}		
	}

	public List<Unit> Get_Units () {return units;}
	public Unit Get_Unit (int id) { if (id < units.Count) return units[id]; else return null; }

	public List<Unit> Get_Enimies () { return enemies; }
	public Unit Get_Enemy (int id) { if (id < enemies.Count) return enemies[id]; else return null; }
	
}


	/*
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

	public int landscapeSmoothing;

	private GameObject[] tiles;
	private int mapWidth;


	void Start () 
	{
		Color[][] mapData = Read_Map_Image_File(levelSelection.ToString());

		if (mapData != null)
		{
			Debug.Log("Loaded "+levelSelection.ToString());
			Debug.Log("Map Size is "+mapData.Length +" x "+mapData[0].Length);

			Create_Landscape(mapData);
		}
	}	

	public Tile Get_Tile (int X, int Y)
	{
		return tiles [X * mapWidth + Y ].GetComponent<Tile>();
	}


	public Color[][] Read_Map_Image_File (string levelName)
	{

		UnityEngine.Object mapFile = Resources.Load("MapFiles/"+ levelName);

		try 
		{
			Texture2D mapImage = mapFile as Texture2D;
			Color[] mapArray = mapImage.GetPixels();
			Color [][] mapData = new Color[mapImage.width][];

			mapWidth = mapData.Length;

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

	private void Create_Landscape (Color[][] mapData)
	{
		int terrainScalar = (int)512/mapData.Length;

		float[,] heights = new float[mapData.Length*terrainScalar,mapData[0].Length*terrainScalar];

		landscapeHolder = new GameObject();
		landscapeHolder.name = "Landscape Holder";

		tiles = new GameObject[mapData.Length * mapData[0].Length];

		for ( int x = 0; x < mapData.Length; x++)
		{
			for ( int y = 0; y < mapData[0].Length; y++)
			{
				Color pixelColor = mapData [x][y];

				bool tileOpen = true;

				if ( pixelColor.r < 0.05f && pixelColor.g > 0.95f && pixelColor.b < 0.05f )
				{
					GameObject treeObject = (GameObject)Instantiate(tree);
					
					treeObject.transform.position = new Vector3 (x + 0.5f,mapData[y][x].grayscale * (terrainHeight/landscapeSmoothing),y - 0.5f);
					
					//smooth map
					mapData[x][y] = (mapData[x+1][y] + mapData[x-1][y] + mapData[x][y+1] + mapData[x][y-1])/4;

					tileOpen = false;
				}

				GameObject mapTile = (GameObject)Instantiate(landscapeTile);
				landscapeTile.transform.position = new Vector3 (x + 0.5f,(mapData[y][x].grayscale * (terrainHeight/landscapeSmoothing))+0.01f,y + 0.5f);

				mapTile.transform.parent = landscapeHolder.transform;

				mapTile.AddComponent<Tile>();
				mapTile.GetComponent<Tile>().Constructor(new Vector3 (x,mapData[y][x].grayscale * (terrainHeight/landscapeSmoothing),y),tileOpen,landscapeType.plane);

				tiles [(int)(mapData.Length*x) + y]=mapTile;
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

					for (int s = 0; s < landscapeSmoothing; s++)
					{
						value = (((heights[h1+s,h2] + heights[h1,h2+s] + heights[h1-s,h2] + heights[h1,h2-s])/4)+value)/2;
					}

				}
				else
				{
					value = 0;
				}	
				
				heights[h1,h2] = value * (terrainHeight*0.15f);
			}
		}

		landscapeTerrain.size = new Vector3 (mapData.Length,10,mapData[0].Length);
		landscapeTerrain.SetHeights(1,1,heights);
				
		GameObject mapTerrain = Terrain.CreateTerrainGameObject(landscapeTerrain);
	}
	*/

