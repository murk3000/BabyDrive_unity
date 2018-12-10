using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour {

	private float speed = -5000f;
	private float rotationSpeed = 500000f;
	private float movement = 0f;
	private float rotation = 0f;

	private float totalFuel = 30f;
	private float fuel = 30f;
	private float totalAir = 0;
	private float tempAir = 0;
	private float initX = 0f;
	private float maxX = 0f;
	private float waitToKill = 0f;
	private int score = 0;
	
	private bool onGround = false;
	private bool inGround = false;
	private bool airTime = false;
	
	public Rigidbody2D rb;
	public WheelJoint2D backwheel;
	public WheelJoint2D frontwheel;
	public Text scoreText;
	public Text coinText;
	public Image fuelImage;

	// Use this for initialization
	void Start () {
		initX = rb.position.x;
		maxX = initX;
		PlayerPrefs.SetInt("Alive", 1);
		// scoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (fuel > 0 && PlayerPrefs.GetInt("Alive", 1)==1) {
			rotation = rotationSpeed*Input.GetAxisRaw("Horizontal");
			movement = speed*Input.GetAxis("Horizontal");
		} else {
			movement = 0;
			rotation = 0;
			if (score > PlayerPrefs.GetInt("HighScore", 0)) {
				PlayerPrefs.SetInt("HighScore", score);
			}
			waitToKill += Time.fixedDeltaTime;
			if (waitToKill > 3) {
        		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
			}
		}

		score = Mathf.RoundToInt((maxX-initX)*10) + Mathf.RoundToInt(totalAir*5); 
		scoreText.text = score.ToString();
		coinText.text = PlayerPrefs.GetInt("Coins", 0).ToString();

		if (!(inGround || onGround)) {
			if (!airTime) {
				airTime = true;				
			}
			tempAir += Time.fixedDeltaTime;

		} else {
			if (airTime) {
				airTime = false;
				if (tempAir > 1 && PlayerPrefs.GetInt("Alive", 1)==1) {
					// Debug.Log("YES "+tempAir);
					totalAir += tempAir;
					tempAir = 0;
				}
			}
		} 

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

		if (rb.position.x > maxX) {
			maxX = rb.position.x;
		}
		
		if (airTime){
			// Debug.Log(rotate);
			rb.AddTorque(rotation*Time.fixedDeltaTime);
		} else {
			// Debug.Log(rotate);
			rb.AddTorque(-1*rotation/5f*Time.fixedDeltaTime);
		}

		if (fuel > 0 && PlayerPrefs.GetInt("Alive", 1) == 1) {
			fuel = fuel-Time.fixedDeltaTime;
			fuelImage.fillAmount = fuel/totalFuel;
			if (fuel < totalFuel/3f) {
				var tempColor = fuelImage.color;
				tempColor.b = 0;
				tempColor.g = 0;
				fuelImage.color = tempColor;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		switch (other.tag) {
			case "Ground":
				onGround = true;
			break;
			case "Respawn":
				// Debug.Log("DEAD");
				inGround = true;
				PlayerPrefs.SetInt("Alive", 0);
			break;
			case "Fuel":
				fuel = totalFuel;
				waitToKill = 0f;
				var tempColor = fuelImage.color;
				tempColor.b = 255;
				tempColor.g = 255;
				fuelImage.color = tempColor;
				Destroy(other.gameObject);
			break;
			case "Coin":
				PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0)+5);
				Destroy(other.gameObject);
			break;
			default:
			break;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		switch (other.tag) {
			case "Ground":
				onGround = false;
			break;
			default:
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		
		switch (other.gameObject.tag) {
			case "Ground":
				inGround = true;
			break;
			default:
			break;
		}		
	}

	void OnCollisionExit2D(Collision2D other) {

		switch (other.gameObject.tag) {
			case "Ground":
				inGround = false;
			break;
			default:

			break;
		}	
	}


}
