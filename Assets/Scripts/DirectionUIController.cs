using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DirectionUIController : MonoBehaviour {
	SpriteRenderer passengerSprite;

	private float minDist;
	public Text minDistText;
	public GameObject inIcon;
	public GameObject arrow;
	private int points;
	public Text pointsText;

	public DirectionUIController(){
		points = 0;
		minDist = 99999;
	}

	public void updateDistanceArrow(int passengerIn){
		if (passengerIn == GameManager.NO_PASSENGER) {
			minDist = getMinPassengerDistance ();
		} else {
			minDist = getDropzoneDistance(passengerIn);
		}
		SetDistText ();
	}
		
	//	Distance Methods 
	public float getMinPassengerDistance(){
		int id= -1;
		minDist = 99999;
		GameObject[] passengers;
		passengers = GameObject.FindGameObjectsWithTag ("Passenger");
		for (int i = 0; i < BoardManager.activePassengers.Count ; i++) {
			float dist = Vector3.Distance (passengers[BoardManager.activePassengers [i].GetId()].transform.position, transform.position);
			if ( minDist > dist) {
				minDist = dist;
				id = BoardManager.activePassengers[i].GetId();
				changePassengerAvatar(BoardManager.objectives[id].GetMoney());
			}
		}
		rotateArrow (passengers [id].transform.position);
		return minDist;
	}

	public float getDropzoneDistance(int passengerIn){
		GameObject dropzone = GameObject.FindGameObjectsWithTag("Dropzone")[passengerIn];
		float dist = Vector3.Distance (dropzone.transform.position, transform.position);
		rotateArrow (dropzone.transform.position);
		return dist;
	}
		
	public void SetMinDist(float dist){
		minDist = dist;
	}

	// Arrow Methods
	public void rotateArrow(Vector3 pos){
		Vector3 dir = pos - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		arrow.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
	}

	public void SetArrowColor(Color color){
		arrow.GetComponent<Image>().color = color;
	}

	// Distance Text
	public void SetDistText(){
		minDistText.text = Math.Ceiling(minDist).ToString ()+ "m";
	}
		
	// Avatar UI Images methods
	public void tooglePassengerInImage(){
		Image inImage = inIcon.GetComponent<Image> ();
		inImage.enabled = !inImage.enabled;
	}

	public void changePassengerAvatar (int money_tax)
	{
		Image inImage = inIcon.GetComponent<Image> ();
		if (GameManager.gameMode == GameManager.MONEYMAKER) {
			switch (money_tax) {
			case GameManager.LOW_PRICE:
				inImage.sprite = Resources.Load<Sprite> ("green");
				break;
			case GameManager.MEDIUM_PRICE:
				inImage.sprite = Resources.Load<Sprite> ("yellow");
				break;
			case GameManager.HIGH_PRICE:
				inImage.sprite = Resources.Load<Sprite> ("red");
				break;
			}
		}
	}

	//Set Points
	public void updatePoints(int id){
		if (GameManager.gameMode == GameManager.DELIVERY) {
			points++;
		} else 
			points += BoardManager.objectives [id].GetMoney ();
	}

	public void SetCountText(){
		pointsText.text = points.ToString ();
	}

	public void SetPoints(int points){
		this.points = points;
	}

}
