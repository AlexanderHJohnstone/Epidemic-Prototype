using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Camera CameraComponent;
	private Vector3 cameraVector;
	private bool cameraLocked = true;
	private bool targeting = false;
	private Vector3 targetLocation = Vector3.zero;
	private Vector3 startLocation = Vector3.zero;
	private float targetingTimer = 0;

	public void Initialize (GameObject parent) 
	{
		GameObject gameCamera = new GameObject();
		gameCamera.name = "CAMERA_OBJECT";
		gameCamera.transform.position = this.gameObject.transform.position;
		gameCamera.transform.parent = this.gameObject.transform;
		gameCamera.transform.eulerAngles = new Vector3 (50,45,0);
		gameCamera.AddComponent<Camera>();
		CameraComponent = gameCamera.GetComponent<Camera>();

		CameraComponent.backgroundColor = Color.black;
		CameraComponent.fieldOfView = 50;

		cameraLocked = false;
	}

	void FixedUpdate () 
	{
		if (!cameraLocked) 
		{
			if (targeting) Target_Camera(); 
			else Translate_Camera();
		}
	}

	private void Translate_Camera ()
	{
		if ( Mathf.Abs ( cameraVector.x ) < Globals.Instance.CAMERA_MOVE_SPEED )
		{
			if ( Input.mousePosition.x < (Screen.height * Globals.Instance.CAMERA_MOVE_BORDER)) cameraVector.x += (Input.mousePosition.x - (Screen.height * Globals.Instance.CAMERA_MOVE_BORDER))  / (Screen.height);
			else if ( Input.mousePosition.x > Screen.width-(Screen.height * Globals.Instance.CAMERA_MOVE_BORDER)) cameraVector.x += (Input.mousePosition.x - (Screen.width - (Screen.height * Globals.Instance.CAMERA_MOVE_BORDER))) / Screen.height;
		}

		if ( Mathf.Abs ( cameraVector.z ) < Globals.Instance.CAMERA_MOVE_SPEED )
		{
			if ( Input.mousePosition.y < (Screen.height * Globals.Instance.CAMERA_MOVE_BORDER)) cameraVector.z += (Input.mousePosition.y - (Screen.height * Globals.Instance.CAMERA_MOVE_BORDER)) / (Screen.height * 2);
			else if ( Input.mousePosition.y > Screen.height-(Screen.height * Globals.Instance.CAMERA_MOVE_BORDER)) cameraVector.z += (Input.mousePosition.y - (Screen.height * (1 - Globals.Instance.CAMERA_MOVE_BORDER))) / (Screen.height * 2);
		}

		this.gameObject.transform.Translate(cameraVector * Globals.Instance.CAMERA_MOVE_SENSITIVITY);
		
		if ( Mathf.Abs(cameraVector.x) > 0.01f || Mathf.Abs(cameraVector.z) > 0.01f ) cameraVector *= 0.925f;
		else cameraVector = Vector3.zero;
	}

	private void Target_Camera ()
	{
		targetingTimer += Time.deltaTime;

		this.transform.position = new Vector3 ( Mathf.Lerp(startLocation.x,targetLocation.x -10,targetingTimer),this.transform.position.y,Mathf.Lerp(startLocation.z,targetLocation.z -10,targetingTimer));

		if ( targetingTimer > 1 ) 
		{ 
			targeting = false; 
			targetingTimer = 0;
		}
	}

	public void Move_To_Position ( Vector3 targetLoc )
	{
		targeting = true;
		targetLocation = targetLoc;
		startLocation = this.transform.position;
	}

	public Camera Get_Camera () { return CameraComponent; }

	public void Set_Camera_Lock ( bool value ) { cameraLocked = value; }

	public bool Get_Camera_Lock () { return cameraLocked; }


}
