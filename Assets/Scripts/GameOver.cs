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
	}
}
