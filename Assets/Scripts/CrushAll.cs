using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.CodeDom;

public class CrushAll : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionStay2D(Collision2D col) {
		if (col.collider.tag == "Floor") {
			//col.collider.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, 15f));
		}
	}
}