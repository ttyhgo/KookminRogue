using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private BoardManager boardManager;
	private int level = 3;
	public int playerFoodPoints = 100;
	[HideInInspector] public bool playerTurn = true;

	// Use this for initialization
	void Awake () {

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
		boardManager = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame(){
		boardManager.SetupScene (level);
	}

	public void GameOver(){
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
