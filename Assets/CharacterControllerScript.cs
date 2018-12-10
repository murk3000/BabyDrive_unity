using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {

	private Rigidbody2D rb2d;
	private int money = 0;
	private float x_velocity = 8.0f;
	private float y_impulse = 8.0f;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Vector2 curVel = rb2d.velocity;
		float x_input = Input.GetAxis("Horizontal");
		float y_input = Input.GetAxisRaw("Vertical");

		if (curVel.y == 0)
			rb2d.velocity = new Vector2(x_velocity*x_input, curVel.y);

		if (y_input > 0 && curVel.y == 0) {
			rb2d.velocity = new Vector2(curVel.x, y_impulse);
		}		
	}

	void OnCollisionEnter2D(Collision2D collided) {
		switch (collided.gameObject.tag) {
			case "Coin":
			case "Floor":
			break;
			case "Death":
				StartCoroutine(delay(3f));				
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D collided) {
		switch(collided.gameObject.tag) {
			case "Coin":
				money += 1;
				Destroy(collided.gameObject);
			break;
		}
	}

	IEnumerator delay(float seconds) {
		Debug.Log("Begin");
		rb2d.gameObject.SetActive(false);
		rb2d.gameObject.transform.localPosition = new Vector3(-18.5f, 0f, -1f);
		yield return new WaitForSeconds(seconds);
		rb2d.gameObject.SetActive(true);
		Debug.Log("End");
		
	}
}
