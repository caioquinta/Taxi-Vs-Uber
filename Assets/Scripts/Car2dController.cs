﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.IO;

public class Car2dController : MonoBehaviour {
	float speedForce = 20f;
	float torqueForce = -190f;
	float driftFactorSticky = 0.85f; // Fator que permite o carro "deslizar" um pouco e não apenas ficar rigido
	float driftFactorSlippy = 1f;
	float maxStickyVelocity = 3.5f;

	public int passengerIn = GameManager.NO_PASSENGER;
	public bool playerOne = false;

	private Rigidbody2D rb2d;

	//UI
	public GameObject playerUI;
	private DirectionUIController playerUIcontroller;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		playerUIcontroller = playerUI.GetComponent<DirectionUIController> ();
		passengerIn = GameManager.NO_PASSENGER;
		playerUIcontroller.SetArrowColor (Color.red);
		playerUIcontroller.SetCountText();
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		velocityHandler ();
		updateDistanceArrow ();			
	}

	void updateDistanceArrow(){
		playerUIcontroller.updateDistanceArrow (passengerIn);
	}
		
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Passenger") && passengerIn == GameManager.NO_PASSENGER) {
			PassengerController passengerController = other.gameObject.GetComponent<PassengerController> ();
			passengerController.Toogle ();
			BoardManager.activePassengers.Remove (passengerController);
			GameManager.instance.boardScript.ActivateDropZone (passengerController.GetId());
			passengerIn = passengerController.GetId();
			playerUIcontroller.tooglePassengerInImage ();
			playerUIcontroller.SetArrowColor(Color.blue);
		} else if (other.gameObject.CompareTag ("Dropzone")) {
			DropZoneController dropzoneController  = other.gameObject.GetComponent<DropZoneController> (); 
			if (dropzoneController.GetId () == passengerIn) {
				dropzoneController.Toogle ();
				GameManager.instance.boardScript.ActivatePassengerAtRandom ();
				playerUIcontroller.tooglePassengerInImage ();
				passengerIn = GameManager.NO_PASSENGER;
				playerUIcontroller.updatePoints (dropzoneController.GetId ());
				playerUIcontroller.SetCountText ();
				playerUIcontroller.SetArrowColor(Color.red);
			}
		}
	}
		
	void velocityHandler() {
		float driftFactor = driftFactorSticky;
		if(RightVelocity().magnitude > maxStickyVelocity) {
			driftFactor = driftFactorSlippy;
		} 
			
		rb2d.velocity = ForwardVelocity () + RightVelocity() * driftFactor; //independe da posição de chamada
		if (playerOne) {
			if (Input.GetButton ("Accelerate")) {
				rb2d.AddForce (transform.up * speedForce); //transform é um componente 3D, up significa para cima enquanato transform.forward seria apra dentro da tela
			}

			if (Input.GetButton ("Brakes")) {
				rb2d.AddForce (transform.up * -speedForce / 5f);
			}
		} else {
			if (Input.GetButton ("AccelerateP2")) {
				rb2d.AddForce (transform.up * speedForce); //transform é um componente 3D, up significa para cima enquanato transform.forward seria apra dentro da tela
			}

			if (Input.GetButton ("BrakesP2")) {
				rb2d.AddForce (transform.up * -speedForce / 5f);
			}
		}
		float tf = Mathf.Lerp(0, torqueForce, rb2d.velocity.magnitude / 2 );
		if (playerOne)
			rb2d.angularVelocity = Input.GetAxis ("Horizontal") * tf;
		else
			rb2d.angularVelocity = Input.GetAxis ("HorizontalP2") * tf;
	}	

	// Para manter o comportamento do carro andando segundo a direção das rodas e não lateralmente esse método pega a componente na direção da velocidade
	// Realiza o produto escalar entre a componente lateral e para frente para pegar a resultante
	Vector2 ForwardVelocity(){
		return transform.up * Vector2.Dot (GetComponent<Rigidbody2D>().velocity, transform.up);
	}

	Vector2 RightVelocity(){
		return transform.right * Vector2.Dot (GetComponent<Rigidbody2D>().velocity, transform.right);
	}


}

