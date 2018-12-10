using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	private float speed = -3000f;
	private float rotationSpeed = -3000f;
	private float movement = 0f;
	private float rotation = 0f;
	public Rigidbody2D rb;
	public WheelJoint2D backwheel;
	public WheelJoint2D frontwheel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		movement = speed*Input.GetAxis("Vertical");
		rotation = Input.GetAxisRaw("Horizontal");
	}

	void FixedUpdate() {
		if (movement == 0f) {
			backwheel.useMotor = false;
			frontwheel.useMotor = false;
		} else {
			backwheel.useMotor = true;
			frontwheel.useMotor = true;
			JointMotor2D motor = new JointMotor2D {motorSpeed = movement, maxMotorTorque = 10000};
			backwheel.motor = motor;
			frontwheel.motor = motor;
		}

		rb.AddTorque(rotation*rotationSpeed*Time.fixedDeltaTime);

	}
}
