using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public Transform start;
	public Transform end;
	public Transform top;
	public Transform bottom;
	private float x_cameraSize = 14f;
	private float y_cameraSize = 7f;
	private float x_offset = 10f;
	private float y_offset = 2f;
	private float offsetSmoothing = 2f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float x, y;
		Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
		
		if (player.transform.localScale.x > 0) {
			x = returnMin(end.transform.position.x-x_cameraSize, returnMax(playerPosition.x + x_offset, start.transform.position.x+x_cameraSize));
		} else {
			x = returnMin(end.transform.position.x-x_cameraSize, returnMax(playerPosition.x - x_offset, start.transform.position.x+x_cameraSize));
		}
  		y = returnMin(top.transform.position.y-y_cameraSize, returnMax(playerPosition.y+y_offset, bottom.transform.position.y+y_cameraSize));
		
		transform.position = Vector3.Lerp(transform.position, new Vector3(x, transform.position.y, playerPosition.z), offsetSmoothing*Time.deltaTime);
		transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y, playerPosition.z), offsetSmoothing*Time.deltaTime*2f);

	}

	float returnMax(float a, float b) {
		if (a>b)
			return a;
		return b;
	}

	float returnMin(float a, float b) {
		if (a<b)
			return a;
		return b;
	}

}
