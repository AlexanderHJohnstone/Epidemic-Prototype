using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	private Vector3 location = Vector3.zero;
	private bool open = false;
	private bool highlighted = false;
	private landscapeType type = landscapeType.plane;

	private Color neutral_Color = new Color (0.8f,0.8f, 0.8f, 0.2f);
	private Color highlighted_Color = new Color (0.8f,0, 0, 0.6f);
	 

	public void Instantiate (Vector3 loc, bool access, landscapeType tileType)
	{
		location = loc;
		open = access;
		type = tileType;

	}

	public Vector3 Get_Pos () { return location; }

	public bool Get_Open () { return open; }

	public landscapeType Get_Type () { return type; }

	public void Set_Highlighted (bool value) 
	{ 
		highlighted = value; 
		if (value){	this.renderer.material.color = neutral_Color; }
		else { this.renderer.material.color = highlighted_Color; }
	}
}
