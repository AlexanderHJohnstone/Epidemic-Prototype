using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	private Vector3 location = Vector3.zero;
	private bool open = false;
	private bool highlighted = false;
	private landscapeType type = landscapeType.open;

	private Color neutral_Color = new Color (0.8f,0.8f, 0.8f, 0f);
	private Color highlighted_Color = new Color (0.8f,0, 0, 0.4f);
	private Color selected_Color = new Color (0.8f,0, 0, 0.8f);
	 

	public void Constructor (Vector3 loc, bool access, landscapeType tileType)
	{
		location = loc;
		Set_Open(access);

		type = tileType;
		Set_Tag ();

		Set_Highlighted(true);

		Set_Visibility () ;
	}

	public Vector3 Get_Pos () { return location; }

	public bool Get_Open () { return open; }

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

	public void Selected ()
	{
		Renderer[] tileRender = GetComponentsInChildren<Renderer>();
		
		if (open)
		{
			foreach ( Renderer r in tileRender ) 
				r.material.color = selected_Color; 
		}

		Debug.Log("HERE");
	}

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