using UnityEngine;
using System.Collections;

public class PassengerController : MonoBehaviour {

	Renderer passengerRenderer;
	Collider2D passengerCollider;
	SpriteRenderer passengerSprite;

	private int id;
	public bool active = true;

	public void Toogle(){
		passengerRenderer = GetComponents<Renderer>()[0];
		passengerRenderer.enabled = !passengerRenderer.enabled;

		passengerCollider = GetComponents<Collider2D>()[0];
		passengerCollider.enabled = !passengerCollider.enabled;

		active = !active;
	}
		
	public void SetId(int pos){
		id = pos;
	}
		
	public int GetId(){
		return id;
	}

	public void ChangeColor(){
		passengerSprite = GetComponent<SpriteRenderer> ();
		if (GameManager.gameMode == GameManager.MONEYMAKER) {
			switch (BoardManager.objectives [id].GetMoney()) {
			case GameManager.LOW_PRICE:
				passengerSprite.color = Color.green;
				break;
			case GameManager.MEDIUM_PRICE:
				passengerSprite.color = Color.yellow;
				break;
			case GameManager.HIGH_PRICE:
				passengerSprite.color = Color.red;
				break;
			}
		
		}
	}
}
