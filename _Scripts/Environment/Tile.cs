using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	private Vector3 location = Vector3.zero;
	private bool open = false;
	private bool highlighted = false;
	private landscapeType type = landscapeType.plane;

	private Color neutral_Color = new Color (0.8f,0.8f, 0.8f, 0f);
	private Color highlighted_Color = new Color (0.8f,0, 0, 0.6f);
	 

	public void Constructor (Vector3 loc, bool access, landscapeType tileType)
	{
		location = loc;
		open = access;
		type = tileType;
		Set_Highlighted(false);

		Set_Visibility () ;
	}

	public Vector3 Get_Pos () { return location; }

	public bool Get_Open () { return open; }

	public void Set_Open (bool value) 
	{ 
		open = value; 
		Set_Visibility ();
	}

	public landscapeType Get_Type () { return type; }

	public void Set_Highlighted (bool value) 
	{ 
		highlighted = value; 
		if (open)
		{
			if (value){	this.renderer.material.color = highlighted_Color; }
			else { this.renderer.material.color = neutral_Color; }
		}
	}

	public int Get_X () { return (int)location.x; }

	public int Get_Y () { return (int)location.z; }

	public float Get_Height () { return location.y; }

	private void Set_Visibility () { this.gameObject.renderer.enabled = open; }
}
