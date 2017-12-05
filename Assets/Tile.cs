using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	public int xcord;
	public int ycord;
	public GameController gameController;
	bool active = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		active = true;
		gameController.MoveCursor (xcord, ycord);
		Player player = gameController.player;
		if (Mathf.Abs (player.xcord - xcord) < 2 && Mathf.Abs (player.ycord - ycord) < 2) {
			if (gameController.GetUnit (xcord, ycord) != null) {
				gameController.cursor.AttackMode ();
			} else {
				gameController.cursor.MoveMode ();
			}
		} else {
			gameController.cursor.LookMode ();
		}
	}

	void OnMouseExit(){
		active = false;
	}

	void OnMouseDown(){
		if (active) {
			Player player = gameController.player;
			if (gameController.GetUnit (xcord, ycord) != null) {
				//gameController.cursor.AttackMode ();
			} else {
				gameController.player.Move(xcord, ycord);
			}
		}
	}


}
