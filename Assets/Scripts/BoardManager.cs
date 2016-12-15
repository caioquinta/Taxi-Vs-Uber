using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.IO;
using System.Globalization;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class BoardManager : MonoBehaviour {

	public static List<PassengerController> activePassengers = new List<PassengerController> ();

	public int totalActiveObjectives = 3;
	public const int TIME = 90;

	public PassengerController passengerController;
	public GameObject passenger;

	public GameObject dropzone;

	//UI
	public Text player1CountDownText;
	public Text player2CountDownText;

	void SetCountDown(){
		player1CountDownText = GameObject.Find ("Player1Countdown").GetComponent<Text>();
		player2CountDownText = GameObject.Find ("Player2Countdown").GetComponent<Text>();
		StartCoroutine ("Countdown", TIME);
	}

	void setCountdownText(int time){
		player1CountDownText.text = time.ToString();
		player2CountDownText.text = time.ToString();
	}

	private IEnumerator Countdown(int time){
		while(time > 0){
			setCountdownText (time--);
			yield return new WaitForSeconds(1);
		}
		if (time == 0) {
			GameManager.TaxiScore = GameObject.Find ("Player1Delivered").GetComponent<Text>().text.ToString();
			GameManager.UberScore = GameObject.Find ("Player2Delivered").GetComponent<Text> ().text.ToString ();
			SceneManager.LoadScene ("GameOver");
		}
	}

	void LayoutAllObjectives()
	{
		GameObject newPassenger;
		GameObject newDropzone;
		for (int i=0; i < GameManager.objectives.Count (); i++) {
			Vector3 passengerPos = GameManager.objectives [i].GetPassengerPosition();
			newPassenger =  (GameObject) Instantiate (passenger, passengerPos, Quaternion.identity);
			newPassenger.GetComponent<PassengerController> ().SetId ( GameManager.objectives [i].GetId());
			newPassenger.GetComponent<PassengerController> ().Toogle ();
			newPassenger.GetComponent<PassengerController> ().ChangeColor ();

			Vector3 dropzonePos = GameManager.objectives [i].GetDropzonePosition();
			newDropzone = (GameObject) Instantiate (dropzone, dropzonePos, Quaternion.identity);
			newDropzone.GetComponent<DropZoneController> ().SetId (GameManager.objectives [i].GetId());
		}
		for (int i=0; i < totalActiveObjectives; i++)
			ActivatePassengerAtRandom ();
	}
		
	public void ActivatePassengerAtRandom(){
		bool isActive = true;
		while (isActive) {
			int objectCount = Random.Range (0, GameManager.objectives.Count () - 1 ); 
			passenger = GameObject.FindGameObjectsWithTag("Passenger")[objectCount];
			passengerController = passenger.GetComponent<PassengerController>();
			isActive = passengerController.active;
		}
		passengerController.Toogle();
		activePassengers.Add (passengerController);
	}

	public void ActivateDropZone(int id){
		GameObject dropzone;
		DropZoneController dropzoneController;
	
		dropzone = GameObject.FindGameObjectsWithTag("Dropzone")[id];
		dropzoneController = dropzone.GetComponent<DropZoneController>();
		dropzoneController.Toogle();
	}
		
	public void LoadResources (){
		passenger = Resources.Load ("Passenger") as GameObject;
		dropzone = Resources.Load ("Dropzone") as GameObject;
	}

	public void SetupScene(int level)
	{
		LoadResources ();
		SetCountDown ();
		activePassengers.Clear ();
		LayoutAllObjectives ();
	}
}
