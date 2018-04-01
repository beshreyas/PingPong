using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

	public string win;
	void Start () {
		win = PlayerPrefs.GetString ("winner");
	}

	void OnGUI(){
		GUI.color = Color.green;
		GUI.Label (new Rect (Screen.width / 2 - 20, 50, 80, 30), "GAME OVER");
		GUI.Label (new Rect (Screen.width / 2 - 40, 70, 150, 30), "Result :"+ win);
		if (GUI.Button (new Rect (Screen.width / 2 - 30, 100, 150, 30), "Play Again")) {
			SceneManager.LoadScene (0);
		}
	}
}
