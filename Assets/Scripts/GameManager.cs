using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	// Use this for initialization	
	public static GameManager instance = null;

	public static string TaxiScore;
	public static string UberScore;

	//Constants
	public const int DELIVERY = 1;
	public const int MONEYMAKER = 2;
	public const int LOW_PRICE = 25;
	public const int MEDIUM_PRICE = 50;
	public const int HIGH_PRICE = 100;
	public const int NO_PASSENGER = -1;

	public BoardManager boardScript;
	private int level = 1;
	public static int gameMode = MONEYMAKER;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<BoardManager> ();
		InitGame ();

	}

	public void InitGame(){
		boardScript.SetupScene (level);
	}

	public void ReloadGame(){
		//Destroy (GetComponent<BoardManager> ());
		Debug.Log ("reloadgame");
		boardScript = GetComponent<BoardManager> ();
		boardScript.SetupScene (level);
	}
		
}
