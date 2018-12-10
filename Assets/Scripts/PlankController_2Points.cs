using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankController_2Points : MonoBehaviour {

	public Transform start;
	public Transform end;
	public float xSpeed, ySpeed;
	private bool xAxis, yAxis;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		if (rb.transform.position.x <= start.position.x) {
			xAxis = true;
		} else {
			xAxis = false;
		}
		if (rb.transform.position.y <= start.position.y) {
			yAxis = true;
		} else {
			yAxis = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (xAxis) {
			rb.velocity = new Vector2(xSpeed, rb.velocity.y);
			if (rb.transform.position.x >= end.position.x)
				xAxis = false;
		} else {
			rb.velocity = new Vector2(-1*xSpeed, rb.velocity.y);
			if (rb.transform.position.x <= start.position.x)
				xAxis = true;
		}
		if (yAxis) {
			rb.velocity = new Vector2(rb.velocity.x, ySpeed);
			if (rb.transform.position.y >= end.position.y)
				yAxis = false;
		} else {
			rb.velocity = new Vector2(rb.velocity.x, -1*ySpeed);
			if (rb.transform.position.y <= start.position.y)
				yAxis = true;
		}		
	}
}
