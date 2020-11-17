using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public Text bestResult;

    void Start()
    {
        if (!PlayerPrefs.HasKey("record")) {
			PlayerPrefs.SetInt("record", 0);
		}
		//GameController.record = PlayerPrefs.GetInt("record");
		if (bestResult != null) {
			bestResult.text = "Лучший результат: " + PlayerPrefs.GetInt("record").ToString();
		}
    }

	public void StartGame() {
		SceneManager.LoadScene("GameScene");
	}

	public void BackToMenu() {
		SceneManager.LoadScene("MenuScene");
	}
}
