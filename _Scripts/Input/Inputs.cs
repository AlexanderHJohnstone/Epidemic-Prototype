using UnityEngine;
using System.Collections;

public class Inputs : MonoBehaviour {
	
	private GameObject cameraHolder;
	private Camera gameCamera;
	private Camera guiCamera;

	private Vector3 cameraVector;

	private GameObject selectedGO;

	public void Initialize (GameObject camHolder, Camera gameCam, Camera guiCam)
	{

		cameraHolder = camHolder;
		gameCamera = gameCam;
		guiCamera = guiCam;

		selectedGO = null;
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{

			Ray gameRay = gameCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.1f));
			RaycastHit gameHit;
			if (Physics.Raycast(gameRay, out gameHit))
			{
				Debug.Log(gameHit.collider.gameObject.name);

				selectedGO = gameHit.collider.gameObject;

				selectedGO.SendMessage("Selected", SendMessageOptions.DontRequireReceiver);
			}
		}


		if ( Mathf.Abs ( cameraVector.x ) < Globals.Instance.CAMERA_MOVE_SPEED )
		{
			if ( Input.mousePosition.x < (Screen.height * 0.1f)) 
				cameraVector.x -= 0.075f;
			else if ( Input.mousePosition.x > Screen.width-(Screen.height * 0.1f)) 
				cameraVector.x += 0.075f;
		}


		if ( Mathf.Abs ( cameraVector.z ) < Globals.Instance.CAMERA_MOVE_SPEED )
		{
			if ( Input.mousePosition.y < (Screen.height * 0.1f)) 
				cameraVector.z -= 0.055f;
			else if ( Input.mousePosition.y > Screen.height-(Screen.height * 0.1f)) 
				cameraVector.z += 0.055f;
		}


		cameraHolder.gameObject.transform.Translate(cameraVector);

		if ( Mathf.Abs(cameraVector.x) > 0.01f || Mathf.Abs(cameraVector.z) > 0.01f )
		{
			cameraVector *= 0.925f;
		}
		else cameraVector = Vector3.zero;


	}

	public GameObject Get_Selected ()
	{
		return selectedGO;
	}
}
