using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyKiller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other) {
		PlayerPrefs.SetInt("Alive", 0);	
	}

}
