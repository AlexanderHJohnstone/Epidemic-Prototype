using UnityEngine;
using System.Collections;

public class TacInput : MonoBehaviour {
	
	private GameObject cameraHolder;
	private Camera guiCamera;

	private float doubleClick;
	private GameObject selectedGO = null;

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
//				if (doubleClick < 0.5f && gameHit.collider.gameObject == selectedGO) cameraHolder.GetComponent<GameCamera>().Move_To_Position(selectedGO.transform.position);
//				doubleClick = 0;

				GameObject clickedGO = gameHit.collider.gameObject;
				Tile clickedTile = clickedGO.GetComponent<Tile>();
				Unit clickedUnit = clickedGO.GetComponent<Unit>();

				if(clickedTile != null && selectedGO != null && selectedGO.GetComponent<Unit>() != null) { // Move code
					Unit selectedUnit = selectedGO.GetComponent<Unit>();
					selectedUnit.MoveTo(clickedTile);
				}

				else if(clickedTile != null && selectedGO != null && selectedGO.GetComponent<Tile>() != null) { // Tile Select Swap code
					selectedGO.SendMessage("Set_Selected", false, SendMessageOptions.DontRequireReceiver);
					clickedGO.SendMessage ("Set_Selected", true, SendMessageOptions.DontRequireReceiver);
					selectedGO = clickedTile.gameObject;
				}

				else if(clickedUnit != null && selectedGO != null && selectedGO.GetComponent<Unit>() != null) { // Unit attack code
					selectedGO.SendMessage("Set_Selected", false, SendMessageOptions.DontRequireReceiver);
					clickedGO.SendMessage ("Set_Selected", true, SendMessageOptions.DontRequireReceiver);
					selectedGO = clickedUnit.gameObject;
				}

				else if(clickedUnit != null && selectedGO != null && selectedGO.GetComponent<Tile>() != null) { // Deselect Tile and select Unit
					selectedGO.SendMessage("Set_Selected", false, SendMessageOptions.DontRequireReceiver);
					clickedGO.SendMessage ("Set_Selected", true, SendMessageOptions.DontRequireReceiver);
					selectedGO = clickedUnit.gameObject;
				}

				if (selectedGO == clickedGO) selectedGO.SendMessage("Set_Selected", false, SendMessageOptions.DontRequireReceiver);

				selectedGO = gameHit.collider.gameObject;
				selectedGO.SendMessage("Set_Selected", true, SendMessageOptions.DontRequireReceiver);
			}
		}

		if (Input.GetAxis("Mouse ScrollWheel") < -0.3f) cameraHolder.GetComponent<GameCamera>().Zoom(true);
		else if (Input.GetAxis("Mouse ScrollWheel") > 0.3f) cameraHolder.GetComponent<GameCamera>().Zoom(false);

		if ( doubleClick < 0.35f) doubleClick += Time.deltaTime;
	}

	public GameObject Get_Selected ()
	{
		return selectedGO;
	}
}
