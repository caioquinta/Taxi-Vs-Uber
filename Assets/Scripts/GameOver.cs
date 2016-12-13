using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public Text taxiPoints;
	public Text uberPoints;

	// Update is called once per frame
	void Start () {
		taxiPoints.text = GameManager.TaxiScore + " reais";
		uberPoints.text = GameManager.UberScore + " reais";

		if ((int.Parse (GameManager.TaxiScore) > int.Parse (GameManager.UberScore)))
			GameObject.Find ("TaxiWins").GetComponent<Text> ().enabled = true;
		else {
			if ((int.Parse (GameManager.TaxiScore) < int.Parse (GameManager.UberScore)))
				GameObject.Find ("UberWins").GetComponent<Text> ().enabled = true;
			else 
				GameObject.Find ("Draw").GetComponent<Text> ().enabled = true;
			}
	}
}
