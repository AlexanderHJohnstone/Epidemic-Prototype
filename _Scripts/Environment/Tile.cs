using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	
	private Vector3 location = Vector3.zero;
	private int x;
	private int y;
	private bool open = false;
	private bool highlighted = false;
	private bool selected = false;
	private landscapeType type = landscapeType.open;
	private Map map;													// A reference to the map that contains this tile.

	private Color neutral_Color = new Color (0.8f,0.8f, 0.8f, 0.6f);
	private Color highlighted_Color = new Color (0.8f,0, 0, 0.4f);
	private Color selected_Color = new Color (0.9f,0.3f, 0.3f, 0.8f);

	public void Constructor (Map cMap, Vector3 loc, bool access, landscapeType tileType)
	{
		location = loc;
		map = cMap;
		x = (int)loc.x;
		y = (int)loc.z;

		Set_Open(access);

		type = tileType;
		Set_Tag ();

		Set_Highlighted(false);

		Set_Visibility ();


	}

	public Vector3 Get_Pos () { return location; }

	public bool Get_Open () { return open; }

	public List<Tile> Get_OpenNeighbors() { 
		List<Tile> openNeighbors = new List<Tile>();

		Tile t;

		// TODO: ADD RANDOM TILE SEARCH ORDERING
		if(x > 0) { 
			t = map.Get_Tile(x - 1, y);
			if(t.Get_Open()) { openNeighbors.Add (t); }
		}
		if(x < map.GetMaxX() - 1) { 
			t = map.Get_Tile(x + 1, y);
			if(t.Get_Open()) { openNeighbors.Add (t); }
		}
		if(y > 0) { 
			t = map.Get_Tile(x, y - 1);
			if(t.Get_Open()) { openNeighbors.Add (t); }
		}
		if(y < map.GetMaxY() - 1) { 
			t = map.Get_Tile(x, y + 1);
			if(t.Get_Open()) { openNeighbors.Add (t); }
		}

		return openNeighbors; 
	}

	private void Set_Tag ()
	{
		if (type == landscapeType.open)
			this.gameObject.tag = "open";
		else if ( type == landscapeType.moveBlock )
			this.gameObject.tag = "moveBlock";
		else
			this.gameObject.tag = "visBlock";
	}

	public void Set_Open (bool value) 
	{ 
		open = value;

		if (open)
			this.gameObject.tag = "open";	
		else
			this.gameObject.tag = "moveBlock";
		
		Set_Visibility ();
	}

	public landscapeType Get_Type () { return type; }

	public void Set_Selected (bool value)
	{
		selected = value;
	
		Renderer[] tileRender = GetComponentsInChildren<Renderer>();

		if (open)
		{
			if (selected)
				foreach ( Renderer r in tileRender ) 
					r.material.color = selected_Color; 
			else
			{
				if (highlighted)
					foreach ( Renderer r in tileRender ) 
						r.material.color = highlighted_Color; 
				else
					foreach ( Renderer r in tileRender ) 
						r.material.color = neutral_Color;
			}
		}
	}

	public bool Get_Selected () { return selected; }

	public void Set_Highlighted (bool value) 
	{ 
		highlighted = value; 

		Renderer[] tileRender = GetComponentsInChildren<Renderer>();

		if (open)
		{
			if (value)
				foreach ( Renderer r in tileRender ) 
					r.material.color = highlighted_Color; 
			else 
				foreach ( Renderer r in tileRender ) 
					r.material.color = neutral_Color;
		}
	}

	public int Get_X () { return (int)location.x; }

	public int Get_Y () { return (int)location.z; }

	public float Get_Height () { return location.y; }

	private void Set_Visibility () 
	{ 
		Renderer[] tileRender = GetComponentsInChildren<Renderer>();

		foreach ( Renderer r in tileRender ) 
			r.gameObject.renderer.enabled = open;
	}
}