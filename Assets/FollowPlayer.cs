using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;
	public Vector3 origPlayer;
	public Vector3 origCamera;
	// Use this for initialization
	void Start () {
		origPlayer = player.transform.position;
		origCamera = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float dy = player.transform.position.y - origPlayer.y;

		gameObject.transform.position = 
			new Vector3 (origCamera.x, origCamera.y + dy,
				origCamera.z);
	}
}
