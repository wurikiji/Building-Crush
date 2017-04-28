using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingCrush;

public class MovePlayer : MonoBehaviour {

	public Camera mainCamera;
	private Vector3 winSize;
	private PlayerState state;
	private SpriteRenderer sr;
	private Color originColor;
	private Rigidbody2D rg;
	private float timer;
	public bool onGround;

	// Use this for initialization
	void Start () {
		mainCamera = (Camera)GameObject.Find ("Camera").GetComponent<Camera> ();
		state = PlayerState.Attack;
		sr = GetComponent<SpriteRenderer> ();
		originColor = sr.color;
		rg = GetComponent<Rigidbody2D> ();
		timer = 0f;
		onGround = true;
		rg.simulated = false;
	}

	public PlayerState GetUserState() {
		return state;
	}

	// Update is called once per frame
	void Update () {
		int direction = 0;
		timer += Time.deltaTime;
		print (timer);
		if (Input.GetMouseButtonDown (0)) {
			Vector3 pos = mainCamera.ScreenToWorldPoint (Input.mousePosition);

			if (pos.x < gameObject.transform.position.x - 1e-2)
				direction = -1;
			if (pos.x > gameObject.transform.position.x + 1e-2)
				direction = 1;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			state = PlayerState.Guard;
			sr.color = Color.gray;
			rg.simulated = true;
			timer = 0f;
		} else if (Input.GetKeyDown (KeyCode.Z)) {
			timer = 0f;
			state = PlayerState.Attack;
			sr.color = Color.red;
			rg.simulated = true;
		} else if (onGround) {
			if (state == PlayerState.NONE) {
				if (Input.GetKeyDown (KeyCode.LeftArrow) || direction < 0) {
					Vector2 pos = new Vector2 (0f, gameObject.transform.position.y);
					pos.x = gameObject.transform.position.x - 0.5f;
					if (pos.x < -0.5f)
						pos.x = -0.5f;
					gameObject.transform.position = pos;
				} else if (Input.GetKeyDown (KeyCode.RightArrow) || direction > 0) {
					Vector2 pos = new Vector2 (0f, gameObject.transform.position.y);
					pos.x = gameObject.transform.position.x + 0.5f;
					if (pos.x > 0.5f)
						pos.x = 0.5f;
					gameObject.transform.position = pos;
				}
			} 
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				/* Jump when the player is on the ground */
				onGround = false;
				rg.simulated = true;
				rg.constraints ^= RigidbodyConstraints2D.FreezePositionY;
				rg.AddForce (new Vector2 (0f, 300f));
			} else if (timer > 0.3f) {
				timer = 0f;
				state = PlayerState.NONE;
				sr.color = originColor;
				rg.simulated = false;
			} 
		} else if (timer > 0.3f) {
			timer = 0f;
			sr.color = originColor;
			state = PlayerState.NONE;
			rg.simulated = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		Collider2D cd = col.collider;
		if (cd.name == "Crasher") {
			/* Player is on the ground */
			onGround = true;
			rg.simulated = false;
			rg.constraints = RigidbodyConstraints2D.FreezeAll;
			rg.constraints ^= RigidbodyConstraints2D.FreezePositionX;
		}
	}
}
