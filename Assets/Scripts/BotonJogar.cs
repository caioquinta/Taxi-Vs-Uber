using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BotonJogar : MonoBehaviour {
	
	public void StartGame(){
		SceneManager.LoadScene ("scene");
	}
}
