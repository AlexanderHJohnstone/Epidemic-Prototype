using UnityEngine;
using System.Collections;

public class Inputs : MonoBehaviour {
	
	private GameObject cameraHolder;
	private Camera guiCamera;

	private float doubleClick;
	private GameObject selectedGO;

	public void Initialize (GameObject camHolder, Camera guiCam)
	{

		cameraHolder = camHolder;
		guiCamera = guiCam;

		selectedGO = null;
	}

	void FixedUpdate () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			Camera gameCam = cameraHolder.GetComponent<GameCamera>().Get_Camera();

			Ray gameRay = gameCam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.1f));
			RaycastHit gameHit;
			if (Physics.Raycast(gameRay, out gameHit))
			{
				if (doubleClick < 0.35f && gameHit.collider.gameObject == selectedGO) cameraHolder.GetComponent<GameCamera>().Move_To_Position(selectedGO.transform.position);
				doubleClick = 0;

				selectedGO = gameHit.collider.gameObject;
				selectedGO.SendMessage("Selected", SendMessageOptions.DontRequireReceiver);
			}
		}

		if ( doubleClick < 0.35f) doubleClick += Time.deltaTime;
	}

	public GameObject Get_Selected ()
	{
		return selectedGO;
	}
}
