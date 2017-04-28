using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {

	public int heart;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void KillPlayer() {
		heart--;
		if (heart < 0) {
			GameObject.Find ("BuildingRespawn").SetActive (false);
		}
	}
}
