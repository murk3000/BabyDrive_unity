using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sr;
	private float xSpeed = 14f, ySpeed = 14f;
	private float respawnDelay = 2f;
	private int maxJump = 1;
	private int curJump = 0;
	private bool movable = true;
	public Transform respawnPoint;
	public Transform onGroundCheck;
	public float groundCheckRadius;
	public LayerMask groundLayer;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (movable) {
			bool isTouchingGround = Physics2D.OverlapCircle(onGroundCheck.position, groundCheckRadius, groundLayer);
			bool hasJumped = false;
			float x_input = Input.GetAxis("Horizontal");

			if (x_input > 0) {
				rb.velocity = new Vector2(x_input*xSpeed, rb.velocity.y);
				transform.localScale = new Vector3(1.5f, 1.5f, 1f);
			} else if (x_input < 0) {
				rb.velocity = new Vector2(x_input*xSpeed, rb.velocity.y);
				transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
			} 
			
			if (Input.GetButtonDown("Jump") && (isTouchingGround || curJump < maxJump)) {
				rb.velocity = new Vector2(rb.velocity.x, ySpeed/(++curJump));
				hasJumped = true;
			}
			if (isTouchingGround) {
				curJump = 0;
			}

			anim.SetBool("groundCheck", isTouchingGround);
			anim.SetBool("jumpCheck", hasJumped);
			anim.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		switch (other.tag) {
			case "Fall":	
				Debug.Log("Yo");
				StartCoroutine(respawnAt(respawnPoint, respawnDelay));
			break;
		}	
	}

	IEnumerator respawnAt(Transform coord, float seconds) {
		movable = false;
		sr.enabled = false;
		yield return new WaitForSeconds(seconds);
		sr.enabled = true;
		rb.velocity = new Vector2(0, 0);
		rb.transform.position = coord.position;
		movable = true;
	}
}
