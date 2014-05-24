using UnityEngine;
using System.Collections;

public class Globals 
{
	private Globals ()
	{
		singleton_init ();
	}
	
	private void singleton_init()
	{
		
	}
	
	private static Globals _instance = new Globals ();
	
	/// <summary>Singleton instance for the active Globals.</summary>
	/// <value>Singleton instance for the active Globals.</value>
	public static Globals Instance 
	{
		get {
			if (_instance == null) {
				_instance = new Globals ();  
			}
			return _instance;
		}
	}
	
	#region PLAYER_VARS

	public float CAMERA_MOVE_SPEED = 0.3f;
	
	#endregion
	
	public void TestFunction ()
	{
		Debug.Log("HERE");
	}
}



public enum ShapeType {
	
	Circle,
	Square,
	Triangle,
	Hexagon,
	Star
	
}