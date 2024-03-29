using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HitPoint : MonoBehaviour {

	public int hp;
	private int level;
	// Use this for initialization
	void Start () {
		Text showHP = gameObject.AddComponent<Text> ();

		GetComponentInParent<CrushFloor> ().AddBlock ();
		level = GetComponentInParent<CrushFloor> ().GetLevel ();
		if (level > 10)
			level = 10;
		
		hp = (int)(UnityEngine.Random.value * 1000) % level + 1;
		showHP.text = hp.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetHp(int hit) {
		hp = hit;
	}

	public int GetHp() {
		return hp;
	}

	public int Crush() {
		hp--;
		if (hp <= 0) {
			Destroy (gameObject);
			GetComponentInParent<CrushFloor> ().RemoveBlock ();
			return 1;
		}
		return 0;
	}
}
