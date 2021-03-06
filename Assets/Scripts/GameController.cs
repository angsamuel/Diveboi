﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour {
	public Cursor cursor;
	public Tile tile;
	public Player player;
	public GameObject stairs;
	public GameObject place;
	public int levelWidth = 7;
	public int levelHeight = 7;
	Tile[] tiles;
	Place[] places;
	public Unit[] units;
	int level = 0;
	public GameObject diveTeam;

	//UI elements
	public Text levelText;

	// Use this for initialization
	void Start () {
		GenerateLevel ();
	}

	void GenerateLevel(){
		levelText.text = level.ToString();
		tiles = new Tile[levelWidth * levelHeight];
		places = new Place[levelWidth * levelHeight];
		units = new Unit[levelWidth * levelHeight];

		List<Vector2> cords = new List<Vector2>();
		List<Vector2> unitCords = new List<Vector2>();
		//generate board
		for (int y = 0; y < levelHeight; y++) {
			for (int x = 0; x < levelWidth; x++) {
				Tile t = Instantiate (tile, new Vector3 (x, y, 0), Quaternion.identity);
				t.xcord = x;
				t.ycord = y;
				t.gameController = this;
				tiles [x * levelHeight + y] = t;
				cords.Add(new Vector2 (x, y));
				unitCords.Add(new Vector2 (x, y));
			}
		}
		//shuffle cords
		cords = ShuffleCords(cords);
		unitCords = ShuffleCords (unitCords);
		//spawn player
		Vector2 playerSpawn = unitCords [0];
	    unitCords.RemoveAt (0);
		SpawnPlayer ((int)playerSpawn.x, (int)playerSpawn.y);
		//SpawnPlace (place, (int)playerSpawn.x, (int)playerSpawn.x);  //spawn temp place

		//spawn units
		for (int i = 0; i < 3; i++) {
			SpawnUnit (diveTeam, (int)unitCords [i].x, (int)unitCords [i].y); 
			unitCords.RemoveAt (0);
		}

		//spawn stairs
		Vector2 stairsSpawn = cords[1];
		cords.RemoveAt (1);
		SpawnPlace (stairs, (int)stairsSpawn.x, (int)stairsSpawn.y);

		//spawn garbage places
		for (int i = 0; i < cords.Count; i++) {
			SpawnPlace (place, (int)cords [i].x, (int)cords [i].y); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public Tile GetTile(int x, int y){
		return tiles [x * levelHeight + y];
	}
	Place GetPlace(int x, int y){
		return  places [x * levelHeight + y];
	}

	public Unit GetUnit(int x, int y){
		return units [x * levelHeight + y];
	}

	void SpawnPlace(GameObject p, int x, int y){
		places [x * levelHeight + y] = Instantiate (p, new Vector3 (x, y, 0), Quaternion.identity).GetComponent<Place>();
		p.transform.position = GetTile (x, y).transform.position;
	}


	 void SpawnPlayer(int x, int y){
		player.transform.position = GetTile (x, y).transform.position;
		player.xcord = x;
		player.ycord = y;
		units[x * levelHeight + y] = player.GetComponent<Unit> ();
	 }

	void SpawnUnit(GameObject unit, int x, int y){
		GameObject u = Instantiate (unit, new Vector2 (x, y), Quaternion.identity);
		unit.GetComponent<Unit> ().xcord = x; 
		unit.GetComponent<Unit> ().ycord = y;
		units[x * levelHeight + y] = u.GetComponent<Unit> ();
	}

	public void MovePlayer(int x, int y){
		player.transform.position = GetTile (x, y).transform.position;
		player.xcord = x;
		player.ycord = y;
		if (GetPlace (x, y).GetComponent<Place> ().exit) {
			NextLevel ();
		}
	}


	void NextLevel(){
		level += 1;

		if (level > 9) {
			SceneManager.LoadScene("WinScene");
		}


		//destroy everything from previous level
		for (int i = 0; i < levelWidth * levelHeight; i++) {
			Destroy (tiles [i].gameObject);
			Destroy (places [i].gameObject);
			if (units [i] != null) {
				Destroy (units [i].gameObject);
			}
		}

		GenerateLevel ();
	}

	public void MoveCursor(int x, int y){
		cursor.transform.position = GetTile (x, y).transform.position;
	}


	List<Vector2> ShuffleCords(List<Vector2> cords){
		for (int i = 0; i < cords.Count; i++) {
			Vector2 temp = cords[i];
			int randomIndex = Random.Range(i, cords.Count);
			cords[i] = cords[randomIndex];
			cords[randomIndex] = temp;
		}
		return cords;
	}
}
