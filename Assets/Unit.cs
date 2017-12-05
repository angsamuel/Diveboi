using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	// Use this for initialization
	public GameController gameController;
	public int xcord, ycord;
	public int maxBois, healthyBois, injuredBois;
	public void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Move(int x, int y){
		Debug.Log (x + "," + y);		
		Debug.Log (xcord * gameController.levelHeight + ycord);
		gameController.units[xcord * gameController.levelHeight + ycord] = null;
		gameController.units [x * gameController.levelHeight + y] = this;
		xcord = x;
		ycord = y;
		transform.position = gameController.GetTile (x, y).transform.position;
	}
}
