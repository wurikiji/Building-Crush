using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Security.Cryptography;
using NUnit.Framework.Internal;
using NUnit.Framework;

public class CreateBuilding : MonoBehaviour {

	public GameObject building;
	public float startHeight = 10.0f;

	private int level;
	private float timer;
	// Use this for initialization
	void Start () {
		timer = 0.0f;
		level = 1;
		GenerateBuilding ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateBuilding() {
		GameObject newBuilding = (GameObject)Instantiate (building);
		newBuilding.GetComponent<AssembleBuilding>().Assemble(level);
		newBuilding.transform.parent = this.transform;
		newBuilding.SetActive (true);
	}

	public void LevelUp() {
		level++;
		GenerateBuilding ();
	}

	public int GetLevel() {
		return level;
	}

	void FixedUpdate () {
		float delta = Time.fixedDeltaTime;
		timer += delta;
		if (timer >= 1.0) {
			timer = -10.0f;
			//GenerateBuilding ();
		}
	}
}
