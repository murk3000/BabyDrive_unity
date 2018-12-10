using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public Transform left;
	public Transform right;
	public Transform top;
	public Transform bottom;
	private float x_cameraSize = 51f;
	private float y_cameraSize = 30.2f;
	private float offsetSmoothing = 2f;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		float x, y;
		Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0);	//Current Position

		x = returnMax(left.position.x+x_cameraSize/2f, returnMin(playerPosition.x, right.position.x-x_cameraSize/2f));	
		y = returnMax(bottom.position.y+y_cameraSize/2f, returnMin(playerPosition.y, top.position.y-y_cameraSize/2f));
		transform.position = Vector3.Lerp(transform.position, new Vector3(x, transform.position.y, -1), offsetSmoothing*Time.deltaTime);
		transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y, -1), offsetSmoothing*Time.deltaTime*2f);

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
