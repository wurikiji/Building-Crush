using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework.Internal;
using BuildingCrush;

public class CrushFloor : MonoBehaviour {

	public int reflectForce;
	public GameObject controller;

	private PlayerLife playerLife;
	private int block;
	// Use this for initialization
	void Start () {
		reflectForce = 120;
		controller = GameObject.Find ("GameController");
		playerLife = controller.GetComponent<PlayerLife> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddBlock() {
		block++;
	}

	public void RemoveBlock() {
		block--;
	}

	public int GetLevel() {
		return GetComponentInParent<AssembleBuilding> ().GetLevel ();
	}

	public void CrushAll() {
		foreach (Transform t in transform) {
			if (t.tag == "Building") {
				if (1 == t.GetComponent<HitPoint> ().Crush ()) {
					Destroy (gameObject);
					GetComponentInParent<AssembleBuilding> ().DestroyFloor ();
					break;
				}
			}
		}
	}

	float GetForce(Rigidbody2D rb) {
		float ret;
		float mass = rb.mass;
		float speed = rb.velocity.y;
		float gravity = Physics.gravity.y;

		ret = gravity * mass + reflectForce;

		if (ret < 0)
			ret = -ret;

		if (speed > 0)
			ret = 0;
		
		return ret;
	}

	void OnCollisionEnter2D(Collision2D col) {
		Rigidbody2D rb = GetComponentInParent<Rigidbody2D> ();
		float multiplier = 1.0f;
		if (col.collider.name == "Player") {
			if (PlayerState.Attack == col.collider.GetComponent<MovePlayer> ().GetUserState ()) {
				CrushAll ();
				multiplier = 0.5f;
			}
		} else if (col.collider.tag == "Floor") {
			return;
		} else {
			playerLife.KillPlayer ();
		}
		rb.AddForce (new Vector2 (0f, GetForce(rb) * multiplier));
		print (col.collider.name + col.collider.GetComponent<Rigidbody2D>().simulated);
	}
}
