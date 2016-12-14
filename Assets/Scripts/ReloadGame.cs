using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class ReloadGame : MonoBehaviour {
	public void reloadGame(String scene){
		SceneManager.LoadScene (scene);
	}
}
