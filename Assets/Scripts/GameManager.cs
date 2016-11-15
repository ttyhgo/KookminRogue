using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private BoardManager boardManager;
	private int level = 1;
	public int playerFoodPoints = 100;
	[HideInInspector] public bool playerTurn = true;
	public float turnDelay = 0.1f;
	public float levelStartDelay = 2f;

	private List<Enemy> enemies;
	private bool enemiesMoving;

	private GameObject levelImage;
	private bool doingSetup;
	private Text levelText;


	// Use this for initialization
	void Awake () {

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy> ();
		boardManager = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame(){

		doingSetup = true;

		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		levelText.text = "Day " + level;
		levelImage.SetActive (true);
		Invoke ("HideLevelImage", levelStartDelay);

		enemies.Clear ();
		boardManager.SetupScene (level);
	}

	private void HideLevelImage(){
		levelImage.SetActive (false);
		doingSetup = false;
	}

	void OnLevelWasLoaded(int index){
		level++;
		InitGame ();
	}

	public void GameOver(){
		levelText.text = "After " + level + " days, you servived";
		levelImage.SetActive (true);
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTurn || enemiesMoving || doingSetup)
			return;
		StartCoroutine (MoveEnemies ());
	}

	public void AddEnemyToList(Enemy script){
		enemies.Add (script);
	}

	IEnumerator MoveEnemies(){
		enemiesMoving = true;

		yield return new WaitForSeconds (turnDelay);

		if (enemies.Count == 0) {
			yield return new WaitForSeconds (turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++) {
			enemies [i].MoveEnemy ();

			yield return new WaitForSeconds (enemies [i].moveTime);
		}
		playerTurn = true;
		enemiesMoving = false;
	}
}
