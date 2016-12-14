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

	public static List <Objective> objectives = new List<Objective> ();
	public static List<PassengerController> activePassengers = new List<PassengerController> ();

	public GameObject passenger;
	public PassengerController passengerController;

	public GameObject dropzone;
	public DropZoneController dropzoneController;

	public int totalActiveObjectives = 3;
	public int time = 5;

	//UI
	public Text player1CountDownText;
	public Text player2CountDownText;

	void SetCountDown(){
		time = 90;
		player1CountDownText = GameObject.Find ("Player1Countdown").GetComponent<Text>();
		player2CountDownText = GameObject.Find ("Player2Countdown").GetComponent<Text>();
		Debug.Log ("start");
		StartCoroutine ("Countdown", time);
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
		
	void InitialiseList()
	{
		objectives.Clear ();
		activePassengers.Clear ();
		Debug.Log ("initialise list");
		objectives = Objective.LoadAll ();
	}

	void LayoutAllObjectives()
	{
		GameObject newPassenger;
		GameObject newDropzone;
		Debug.Log ("layout objectives");
		for (int i=0; i < objectives.Count (); i++) {
			Vector3 passengerPos = objectives [i].GetPassengerPosition();
			newPassenger =  (GameObject) Instantiate (passenger, passengerPos, Quaternion.identity);

			newPassenger.GetComponent<PassengerController> ().SetId (objectives [i].GetId());
			newPassenger.GetComponent<PassengerController> ().Toogle ();
			newPassenger.GetComponent<PassengerController> ().ChangeColor ();

			Vector3 dropzonePos = objectives [i].GetDropzonePosition();
			newDropzone = (GameObject) Instantiate (dropzone, dropzonePos, Quaternion.identity);
			newDropzone.GetComponent<DropZoneController> ().SetId (objectives [i].GetId());
		}
		for (int i=0; i < totalActiveObjectives; i++)
			ActivatePassengerAtRandom ();
	}
		
	public void ActivatePassengerAtRandom(){
		bool isActive = true;
		while (isActive) {
			int objectCount = Random.Range (0, objectives.Count () - 1 ); 
			passenger = GameObject.FindGameObjectsWithTag("Passenger")[objectCount];
			passengerController = passenger.GetComponent<PassengerController>();
			isActive = passengerController.active;
		}

		passengerController.Toogle();
		activePassengers.Add (passengerController);
	}


	public void ActivateDropZone(int id){
		dropzone = GameObject.FindGameObjectsWithTag("Dropzone")[id];
		dropzoneController = dropzone.GetComponent<DropZoneController>();
		dropzoneController.Toogle();
	}


	public void SetupScene(int level)
	{
		SetCountDown ();
		InitialiseList ();
		LayoutAllObjectives ();
	}
}
