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
	public float CAMERA_MOVE_SENSITIVITY = 0.6f;
	public float CAMERA_MOVE_BORDER = 0.15f;
	public float CAMERA_ZOOM_IN = 30;
	public float CAMERA_ZOOM_OUT = 55;

	public Color SELECTED_COLOR =  new Color (0.8f, 0, 0, 0.8f);
	public Color NEUTRAL_COLOR = new Color(1f, 1f, 1f, 0f);
	
	#endregion
	
	public void TestFunction ()
	{
		Debug.Log("HERE");
	}
}

