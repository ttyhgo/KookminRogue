﻿using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public Sprite dmgSprite;
	public int hp = 3;

	private SpriteRenderer spriteRenderer;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void DamangeWall(int loss){
		spriteRenderer.sprite = dmgSprite;

		hp -= loss;

		if (hp <= 0) {
			gameObject.SetActive (false);
		}
	}
}
