using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {
	public Color lookColor;
	public Color attackColor;
	public Color moveColor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LookMode(){
		GetComponent<SpriteRenderer> ().color = lookColor;
	}

	public void AttackMode(){
		GetComponent<SpriteRenderer> ().color = attackColor;
	}

	public void MoveMode(){
		GetComponent<SpriteRenderer> ().color = moveColor;
	}
}
