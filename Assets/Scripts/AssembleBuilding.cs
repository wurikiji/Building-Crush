using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AssembleBuilding : MonoBehaviour {

	public GameObject basicFloor;
	private int numFloor;
	private String[] blockName = {"Left", "Center",  "Right"};
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
	}

	void RandomizeArray(int[] arr) {
		for (int i = arr.Length - 1; i > 0; i--) {	
			int r = UnityEngine.Random.Range (0, i + 1);
			int tmp = arr [i];
			arr [i] = arr [r];
			arr [r] = tmp;
		}
	}

	void RandomizeBlock(GameObject floor) {
		int[] rnd = {-1, 0, 1};
		int cnt = 0;

		RandomizeArray (rnd);

		Vector3 blockScale = basicFloor.transform.Find("center").transform.lossyScale;

		foreach(Transform t in floor.transform) {
			if (t.tag == "Building") {
				t.transform.position = new Vector3 (rnd [cnt] * blockScale.x, 0f, 0f);
				t.name = blockName [rnd [cnt] + 1];
				cnt++;
			}
		}
	}

	public void Assemble(int num) {
		numFloor = num;
		print ("Assemble " + num + " floors");
		for (int i = 0; i < num; i++) {
			/* Create floor and set parent to this building */
			GameObject obj = (GameObject) Instantiate (basicFloor);
			RandomizeBlock(obj);
			obj.transform.position = new Vector2 (0.0f, 8f + 0.21f * i);
			obj.transform.parent = gameObject.transform;
		}
	}

	public void DestroyFloor() {
		numFloor--;
		if (numFloor <= 0) {
			Destroy (gameObject);
			GetComponentInParent<CreateBuilding> ().LevelUp ();
		}
	}

	public int GetNumFloor() {
		return numFloor;
	}

	public int GetLevel() {
		return GetComponentInParent<CreateBuilding> ().GetLevel ();
	}
}
